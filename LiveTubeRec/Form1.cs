using System;
using System.Windows.Forms;
using NLog;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Data;
using System.Drawing;
using System.Timers;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

/*
 * 更新日 2018/04/23
 *
 */

namespace LiveTubeReport {
	public partial class Form1 : Form {
		private static NLog.Logger logger = LogManager.GetCurrentClassLogger();

		private ChannelManager _ChannelManager;
		private System.Timers.Timer _Timer;
		private Config conf;

		public Form1() {
			InitializeComponent();
		}

		/******************             ここから下はイベントハンドラ              *******************************/

		private void Form1_Load(object sender, EventArgs e) {
			//設定ファイルからインスタンス設定
			conf = new Config(@".\config\config.ini");

			//データセットの初期化
			liveTubeDataSet.Clear();
			DirectoryUtils.SafeCreateDirectory(@".\data");
			DirectoryUtils.SafeCreateDirectory(@".\data\image");

			if (System.IO.File.Exists(@".\data\data.xml")) {
				liveTubeDataSet.ReadXml(@".\data\data.xml");
				logger.Info("データの読み込みに成功しました。");
			}
			else {
				liveTubeDataSet.WriteXml(@".\data\data.xml");
			}

			for (int i = 0; i < channelTable.Rows.Count; i++) {
				channelTable.Rows[i]["liveStatus"] = false;
				channelTable.Rows[i]["appStat"] = false;
			}

			//データグリッドビューの初期処理
			replaceDataGridView();
			statusSetToDataGridView();

			//チャンネルマネージャの初期処理
			//IniFile ini = new IniFile(@".\config\config.ini");

			logger.Debug("初期設定が完了");

			ProcessExe process = new ProcessExe();
			process.SynchronizingObject = this;
			process.EnableRaisingEvents = true;

			_ChannelManager = new ChannelManager(
				channelTable, new Schedule(@".\config\config.ini"), new YouTubeDataProvider(conf.ApiKey), process);

			_Timer = new System.Timers.Timer();
			_Timer.Elapsed += new System.Timers.ElapsedEventHandler(doMonitaring);
			_Timer.Interval = 60000; //msec

			_Timer.Start();
			label1.Text = "[ 状態：記録中 ]";

			logger.Info("チャンネルの記録を開始しました。");
		}
		private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
			DirectoryUtils.SafeCreateDirectory(@".\data");
			liveTubeDataSet.WriteXml(@".\data\data.xml");
		}
		public void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) {
			DataGridView dgv = (DataGridView)sender;
			//"Link"列ならば、ボタンがクリックされた
			if (dgv.Columns[e.ColumnIndex].Name == "dgvLiveURL") {
				//訪問済みにする
				DataGridViewLinkCell cell = (DataGridViewLinkCell)dgv[e.ColumnIndex, e.RowIndex];
				cell.LinkVisited = true;

				Process.Start(cell.Value.ToString());
			}
		}
		private void buttonInsert_Click(object sender, EventArgs e) {
			if ("".Equals(textBoxChannelID.Text)) {
				return;
			}
			
			string channelID = this.getChannelIDByUrl(textBoxChannelID.Text);
			if ("".Equals(channelID) || this.hasChannelID(channelID)) {
				logger.Error("入力したURLが正しくないか、一覧に存在しているため追加できません。");
				textBoxChannelID.Text = "";
				return;
			}

			if(dataGridView.Rows.Count >= conf.ChannelNumMax || channelTable.Rows.Count >= conf.ChannelNumMax) {
				logger.Error("記録可能なチャンネル数が上限に達しているため追加できません。");
				textBoxChannelID.Text = "";
				return;
			}

			DataRow row = _ChannelManager.GetPreparedDataRow(channelID, channelTable);

			logger.Info("チャンネル " + row["channelName"].ToString() + " を追加しました。");

			_ChannelManager.SetLiveStatusAndExecApp(row);

			channelTable.Rows.Add(row);

			this.replaceDataGridView();
			//this.doMonitaring();

			textBoxChannelID.Text = "";
		}
		private void toolStripMenuItemDelete_Click(object sender, EventArgs e) {
			if (dataGridView.SelectedRows.Count == 1) {
				for (int i = 0; i < channelTable.Rows.Count; i++) {
					var a = channelTable.Rows[i]["channelID"];
					var b = dataGridView.SelectedRows[0].Cells["dgvChannelID"].Value;
					if (channelTable.Rows[i]["channelID"].Equals(dataGridView.SelectedRows[0].Cells["dgvChannelID"].Value)) {
						string name = channelTable.Rows[i]["channelName"].ToString();

						channelTable.Rows[i].Delete();
						logger.Info("チャンネル： " + name + " を削除しました");
					}
				}
			}
		}
		private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
			if (dataGridView.SelectedRows.Count < 1) {
				toolStripMenuItemDelete.Enabled = false;
			}
			else {
				toolStripMenuItemDelete.Enabled = true;
			}
		}
		private void textBoxChannelID_KeyPress(object sender, KeyPressEventArgs e) {
			if (e.KeyChar == (char)Keys.Enter) {
				e.Handled = true;
				buttonInsert_Click(sender, e);
			}
		}
		private void button1_Click(object sender, EventArgs e) {
			_Timer.Start();
			label1.Text = "[ 状態：記録中 ]";
			logger.Info("チャンネルの記録を開始しました。");
		}
		private void button3_Click(object sender, EventArgs e) {
			_Timer.Stop();
			label1.Text = "[ 状態：停止中 ]";
			logger.Info("チャンネルの記録を停止しました。");
		}

		//更新ボタン
		private async void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			await Task.Run(() => _ChannelManager.ForceDoLogic());

			this.statusSetToDataGridView();

			Application.DoEvents();
			button2.Enabled = true;

		}
		private void textBox_Log_TextChanged(object sender, EventArgs e) {
			if(textBox_Log.Lines.Length > 1000) {
				textBox_Log.Text = "";
			}
		}

		private void dataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
			// ヘッダ以外のセルか？
			if (e.ColumnIndex >= 0 && e.RowIndex >= 0) {
				dataGridView.CurrentCell = dataGridView[e.ColumnIndex, e.RowIndex];
			}
			else {
				dataGridView.CurrentCell = null;
			}
		}

		/******************          ここから先はプライベートメソッド            ***************************/

		//リストにチャンネルIDが登録されているかをチェック
		// 登録済み:true 未登録:false
		private bool hasChannelID(string inputCannelID) {
			logger.Debug("start");

			bool rtn = false;
			foreach (DataGridViewRow row in dataGridView.Rows) {
				// 編集中の行どうしでないとき
				if (row.Cells["dgvChannelID"].Value != null && row.Cells["dgvChannelID"].Value.Equals(inputCannelID)) {
					rtn = true;
				}
			}

			return rtn;
		}

		//URLからチェンネルIDを取得 取得できなければ空文字を返す
		private string getChannelIDByUrl(string inputURL) {
			logger.Debug("Start");

			string expression = "(?<type>channel)/(?<id>.*?)(&|$|/)";

			Regex reg = new Regex(expression);
			Match match = reg.Match(inputURL);
			bool rtn = match.Success;
			if (rtn == true) {
				return match.Groups["id"].Value;
			}
			else {
				return "";
			}
		}

		//datagridviewの列ごとの処理を記述
		private void replaceDataGridView() {
			logger.Trace("");

			for (int i = 0; i < dataGridView.Rows.Count; i++) {
				//チャンネルIDからレコードを取得
				DataRow[] rows = liveTubeDataSet.Tables[0].Select("ChannelID = " + "'" + dataGridView.Rows[i].Cells["dgvChannelID"].Value.ToString() + "'");

				//チャンネルIDは一意
				if (rows.Length == 1) {
					//サムネイルをセット
					object o = rows[0]["thumbnailPath"];
					if (o != null && !"".Equals(o.ToString())) {
						dataGridView.Rows[i].Cells["dgvThumbnail"].Value = new Bitmap(o.ToString());
					}
				}
				else {
					logger.Error("チャンネルデータに誤りがあります。");
					break;
				}
			}
		}

		private void doMonitaring(object sender, ElapsedEventArgs e) {
			doMonitaring();
		}

		private async Task doMonitaring() {
			await Task.Run(() => _ChannelManager.DoBaseLogic());
			
			statusSetToDataGridView();

			loggingLiveData();
		}
		//datatableのデータをdatagridviewに反映させる
		private void statusSetToDataGridView() {
			//datatableをdatagridviewに反映させる
			for (int i = 0; i < dataGridView.Rows.Count; i++) {
				for (int j = 0; j < channelTable.Rows.Count; j++) {
					if (dataGridView.Rows[i].Cells["dgvChannelID"].Value.ToString().Equals(channelTable.Rows[j]["channelID"].ToString())) {
						if (true.Equals(channelTable.Rows[j]["liveStatus"])) {
							dataGridView.Rows[i].Cells["dgvStatus"].Value = "配信中";
						}
						else {
							dataGridView.Rows[i].Cells["dgvStatus"].Value = "　-　";
						}
					}
				}
			}
		}

		private void loggingLiveData() {
			foreach (DataRow row in channelTable.Rows) {
				logger.Debug("Name          : " + row["channelName"].ToString());
				logger.Debug("LiveStatus    : " + row["liveStatus"].ToString());
			}
		}
	}
}
