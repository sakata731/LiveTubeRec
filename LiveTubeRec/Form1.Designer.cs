namespace LiveTubeRec
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			this.textBox_Log = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.liveTubeDataSet = new System.Data.DataSet();
			this.channelTable = new System.Data.DataTable();
			this.dataColumn1 = new System.Data.DataColumn();
			this.dataColumn2 = new System.Data.DataColumn();
			this.dataColumn3 = new System.Data.DataColumn();
			this.dataColumn4 = new System.Data.DataColumn();
			this.dataColumn5 = new System.Data.DataColumn();
			this.dataColumn6 = new System.Data.DataColumn();
			this.dataColumn7 = new System.Data.DataColumn();
			this.dataColumn8 = new System.Data.DataColumn();
			this.dataColumn9 = new System.Data.DataColumn();
			this.textBoxChannelID = new System.Windows.Forms.TextBox();
			this.buttonInsert = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.userName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.channelID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.liveID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.liveTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.liveURL = new System.Windows.Forms.DataGridViewLinkColumn();
			this.LiveStartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.LiveEndDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.requestLastDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.contextMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.liveTubeDataSet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.channelTable)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBox_Log
			// 
			this.textBox_Log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_Log.BackColor = System.Drawing.SystemColors.Window;
			this.textBox_Log.Location = new System.Drawing.Point(6, 18);
			this.textBox_Log.Multiline = true;
			this.textBox_Log.Name = "textBox_Log";
			this.textBox_Log.ReadOnly = true;
			this.textBox_Log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox_Log.Size = new System.Drawing.Size(735, 58);
			this.textBox_Log.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBox_Log);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(0, 341);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(747, 82);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "ログ";
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToDeleteRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView.AutoGenerateColumns = false;
			this.dataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.userName,
            this.channelID,
            this.liveID,
            this.liveTitle,
            this.liveURL,
            this.LiveStartDate,
            this.LiveEndDate,
            this.status,
            this.requestLastDate});
			this.dataGridView.ContextMenuStrip = this.contextMenuStrip;
			this.dataGridView.DataMember = "ChannelTable";
			this.dataGridView.DataSource = this.liveTubeDataSet;
			this.dataGridView.Location = new System.Drawing.Point(12, 66);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.ReadOnly = true;
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.RowTemplate.Height = 21;
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.Size = new System.Drawing.Size(729, 265);
			this.dataGridView.TabIndex = 2;
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDelete});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(99, 26);
			this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
			// 
			// toolStripMenuItemDelete
			// 
			this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
			this.toolStripMenuItemDelete.Size = new System.Drawing.Size(98, 22);
			this.toolStripMenuItemDelete.Text = "削除";
			this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
			// 
			// liveTubeDataSet
			// 
			this.liveTubeDataSet.DataSetName = "LiveTubeDataSet";
			this.liveTubeDataSet.Tables.AddRange(new System.Data.DataTable[] {
            this.channelTable});
			// 
			// channelTable
			// 
			this.channelTable.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn8,
            this.dataColumn9});
			this.channelTable.TableName = "ChannelTable";
			// 
			// dataColumn1
			// 
			this.dataColumn1.ColumnName = "channelName";
			// 
			// dataColumn2
			// 
			this.dataColumn2.Caption = "channelID";
			this.dataColumn2.ColumnName = "channelID";
			// 
			// dataColumn3
			// 
			this.dataColumn3.ColumnName = "liveID";
			// 
			// dataColumn4
			// 
			this.dataColumn4.ColumnName = "liveTitle";
			// 
			// dataColumn5
			// 
			this.dataColumn5.ColumnName = "liveURL";
			// 
			// dataColumn6
			// 
			this.dataColumn6.ColumnName = "liveStartDate";
			this.dataColumn6.DataType = typeof(System.DateTime);
			// 
			// dataColumn7
			// 
			this.dataColumn7.ColumnName = "liveEndDate";
			this.dataColumn7.DataType = typeof(System.DateTime);
			// 
			// dataColumn8
			// 
			this.dataColumn8.ColumnName = "status";
			this.dataColumn8.DataType = typeof(bool);
			// 
			// dataColumn9
			// 
			this.dataColumn9.ColumnName = "requestLastDate";
			this.dataColumn9.DataType = typeof(System.DateTime);
			// 
			// textBoxChannelID
			// 
			this.textBoxChannelID.AllowDrop = true;
			this.textBoxChannelID.Location = new System.Drawing.Point(6, 19);
			this.textBoxChannelID.Name = "textBoxChannelID";
			this.textBoxChannelID.Size = new System.Drawing.Size(526, 19);
			this.textBoxChannelID.TabIndex = 3;
			this.textBoxChannelID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxChannelID_KeyPress);
			// 
			// buttonInsert
			// 
			this.buttonInsert.Location = new System.Drawing.Point(538, 17);
			this.buttonInsert.Name = "buttonInsert";
			this.buttonInsert.Size = new System.Drawing.Size(75, 23);
			this.buttonInsert.TabIndex = 4;
			this.buttonInsert.Text = "追加";
			this.buttonInsert.UseVisualStyleBackColor = true;
			this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.textBoxChannelID);
			this.groupBox2.Controls.Add(this.buttonInsert);
			this.groupBox2.Location = new System.Drawing.Point(12, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(729, 48);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "記録するチャンネルを追加";
			// 
			// userName
			// 
			this.userName.DataPropertyName = "channelName";
			this.userName.HeaderText = "ユーザー名";
			this.userName.Name = "userName";
			this.userName.ReadOnly = true;
			// 
			// channelID
			// 
			this.channelID.DataPropertyName = "channelID";
			this.channelID.HeaderText = "チャンネルID";
			this.channelID.Name = "channelID";
			this.channelID.ReadOnly = true;
			// 
			// liveID
			// 
			this.liveID.DataPropertyName = "liveID";
			this.liveID.HeaderText = "ライブID";
			this.liveID.Name = "liveID";
			this.liveID.ReadOnly = true;
			// 
			// liveTitle
			// 
			this.liveTitle.DataPropertyName = "liveTitle";
			this.liveTitle.HeaderText = "番組名";
			this.liveTitle.Name = "liveTitle";
			this.liveTitle.ReadOnly = true;
			// 
			// liveURL
			// 
			this.liveURL.DataPropertyName = "liveURL";
			this.liveURL.HeaderText = "配信URL";
			this.liveURL.MinimumWidth = 100;
			this.liveURL.Name = "liveURL";
			this.liveURL.ReadOnly = true;
			this.liveURL.Width = 200;
			// 
			// LiveStartDate
			// 
			this.LiveStartDate.DataPropertyName = "liveStartDate";
			dataGridViewCellStyle2.Format = "G";
			dataGridViewCellStyle2.NullValue = null;
			this.LiveStartDate.DefaultCellStyle = dataGridViewCellStyle2;
			this.LiveStartDate.HeaderText = "放送開始";
			this.LiveStartDate.Name = "LiveStartDate";
			this.LiveStartDate.ReadOnly = true;
			// 
			// LiveEndDate
			// 
			this.LiveEndDate.DataPropertyName = "liveEndDate";
			dataGridViewCellStyle3.Format = "G";
			dataGridViewCellStyle3.NullValue = null;
			this.LiveEndDate.DefaultCellStyle = dataGridViewCellStyle3;
			this.LiveEndDate.HeaderText = "放送終了";
			this.LiveEndDate.Name = "LiveEndDate";
			this.LiveEndDate.ReadOnly = true;
			// 
			// status
			// 
			this.status.DataPropertyName = "status";
			dataGridViewCellStyle4.NullValue = null;
			this.status.DefaultCellStyle = dataGridViewCellStyle4;
			this.status.HeaderText = "状況";
			this.status.Name = "status";
			this.status.ReadOnly = true;
			// 
			// requestLastDate
			// 
			this.requestLastDate.DataPropertyName = "requestLastDate";
			dataGridViewCellStyle5.Format = "G";
			dataGridViewCellStyle5.NullValue = null;
			this.requestLastDate.DefaultCellStyle = dataGridViewCellStyle5;
			this.requestLastDate.HeaderText = "最終リクエスト日時";
			this.requestLastDate.MinimumWidth = 100;
			this.requestLastDate.Name = "requestLastDate";
			this.requestLastDate.ReadOnly = true;
			this.requestLastDate.Width = 120;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(747, 423);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.dataGridView);
			this.Controls.Add(this.groupBox1);
			this.Name = "Form1";
			this.Text = "LiveTubeReport";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.contextMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.liveTubeDataSet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.channelTable)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Log;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TextBox textBoxChannelID;
        private System.Windows.Forms.Button buttonInsert;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
		private System.Data.DataSet liveTubeDataSet;
		private System.Data.DataTable channelTable;
		private System.Data.DataColumn dataColumn1;
		private System.Data.DataColumn dataColumn2;
		private System.Data.DataColumn dataColumn3;
		private System.Data.DataColumn dataColumn4;
		private System.Data.DataColumn dataColumn5;
		private System.Data.DataColumn dataColumn6;
		private System.Data.DataColumn dataColumn7;
		private System.Data.DataColumn dataColumn8;
		private System.Data.DataColumn dataColumn9;
		private System.Windows.Forms.DataGridViewTextBoxColumn userName;
		private System.Windows.Forms.DataGridViewTextBoxColumn channelID;
		private System.Windows.Forms.DataGridViewTextBoxColumn liveID;
		private System.Windows.Forms.DataGridViewTextBoxColumn liveTitle;
		private System.Windows.Forms.DataGridViewLinkColumn liveURL;
		private System.Windows.Forms.DataGridViewTextBoxColumn LiveStartDate;
		private System.Windows.Forms.DataGridViewTextBoxColumn LiveEndDate;
		private System.Windows.Forms.DataGridViewTextBoxColumn status;
		private System.Windows.Forms.DataGridViewTextBoxColumn requestLastDate;
	}
}

