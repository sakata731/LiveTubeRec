using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestNewLogic {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			NonDispBrowser ndb = new NonDispBrowser();
			ndb.NavigateAndWait(textBox1.Text);

			string html = ndb.DocumentText;

			ndb.Dispose();

			textBox2.Text = "" + html.Contains("ライブ配信中");
		}
	}

	public class NonDispBrowser : WebBrowser {

		bool done;

		// タイムアウト時間（10秒）
		TimeSpan timeout = new TimeSpan(0, 0, 10);

		protected override void OnDocumentCompleted(
					  WebBrowserDocumentCompletedEventArgs e) {
			// ページにフレームが含まれる場合にはフレームごとに
			// このメソッドが実行されるため実際のURLを確認する
			if (e.Url == this.Url) {
				done = true;
			}
		}

		protected override void OnNewWindow(CancelEventArgs e) {
			// ポップアップ・ウィンドウをキャンセル
			e.Cancel = true;
		}

		public NonDispBrowser() {
			// スクリプト・エラーを表示しない
			this.ScriptErrorsSuppressed = true;
		}

		public bool NavigateAndWait(string url) {

			base.Navigate(url); // ページの移動

			done = false;
			DateTime start = DateTime.Now;

			while (done == false) {
				if (DateTime.Now - start > timeout) {
					// タイムアウト
					return false;
				}
				Application.DoEvents();
			}
			return true;
		}
	}
}
