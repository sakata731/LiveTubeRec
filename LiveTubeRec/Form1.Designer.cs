﻿namespace LiveTubeRec
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
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
			this.dataColumn10 = new System.Data.DataColumn();
			this.dataColumn11 = new System.Data.DataColumn();
			this.dataColumn12 = new System.Data.DataColumn();
			this.dataColumn13 = new System.Data.DataColumn();
			this.textBoxChannelID = new System.Windows.Forms.TextBox();
			this.buttonInsert = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.dgvThumbnail = new System.Windows.Forms.DataGridViewImageColumn();
			this.dgvThumbnailUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvChannelID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvLiveID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvLiveTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvLiveURL = new System.Windows.Forms.DataGridViewLinkColumn();
			this.dgvLiveStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvLiveEndDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvLastRequestDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvNextRequestTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvAddDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
			this.textBox_Log.Location = new System.Drawing.Point(12, 18);
			this.textBox_Log.Multiline = true;
			this.textBox_Log.Name = "textBox_Log";
			this.textBox_Log.ReadOnly = true;
			this.textBox_Log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox_Log.Size = new System.Drawing.Size(898, 88);
			this.textBox_Log.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBox_Log);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(0, 369);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(916, 112);
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
            this.dgvThumbnail,
            this.dgvThumbnailUrl,
            this.dgvChannelID,
            this.dgvUserName,
            this.dgvLiveID,
            this.dgvLiveTitle,
            this.dgvStatus,
            this.dgvLiveURL,
            this.dgvLiveStartTime,
            this.dgvLiveEndDate,
            this.dgvLastRequestDate,
            this.dgvNextRequestTime,
            this.dgvAddDate});
			this.dataGridView.ContextMenuStrip = this.contextMenuStrip;
			this.dataGridView.DataMember = "ChannelTable";
			this.dataGridView.DataSource = this.liveTubeDataSet;
			this.dataGridView.Location = new System.Drawing.Point(12, 66);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.ReadOnly = true;
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.RowTemplate.Height = 21;
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.Size = new System.Drawing.Size(898, 297);
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
            this.dataColumn9,
            this.dataColumn10,
            this.dataColumn11,
            this.dataColumn12,
            this.dataColumn13});
			this.channelTable.TableName = "ChannelTable";
			// 
			// dataColumn1
			// 
			this.dataColumn1.ColumnName = "ChannelName";
			// 
			// dataColumn2
			// 
			this.dataColumn2.AllowDBNull = false;
			this.dataColumn2.Caption = "channelID";
			this.dataColumn2.ColumnName = "ChannelID";
			// 
			// dataColumn3
			// 
			this.dataColumn3.ColumnName = "LiveID";
			// 
			// dataColumn4
			// 
			this.dataColumn4.ColumnName = "LiveTitle";
			// 
			// dataColumn5
			// 
			this.dataColumn5.ColumnName = "LiveURL";
			// 
			// dataColumn6
			// 
			this.dataColumn6.AllowDBNull = false;
			this.dataColumn6.ColumnName = "LiveStartTime";
			this.dataColumn6.DataType = typeof(System.DateTime);
			// 
			// dataColumn7
			// 
			this.dataColumn7.AllowDBNull = false;
			this.dataColumn7.ColumnName = "LiveEndTime";
			this.dataColumn7.DataType = typeof(System.DateTime);
			// 
			// dataColumn8
			// 
			this.dataColumn8.ColumnName = "Status";
			this.dataColumn8.DataType = typeof(bool);
			// 
			// dataColumn9
			// 
			this.dataColumn9.AllowDBNull = false;
			this.dataColumn9.ColumnName = "LastRequestTime";
			this.dataColumn9.DataType = typeof(System.DateTime);
			// 
			// dataColumn10
			// 
			this.dataColumn10.ColumnName = "Thumbnail";
			// 
			// dataColumn11
			// 
			this.dataColumn11.AllowDBNull = false;
			this.dataColumn11.ColumnName = "NextRequestTime";
			this.dataColumn11.DataType = typeof(System.DateTime);
			// 
			// dataColumn12
			// 
			this.dataColumn12.ColumnName = "AddDate";
			// 
			// dataColumn13
			// 
			this.dataColumn13.ColumnName = "ThumbnailImage";
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
			this.groupBox2.Size = new System.Drawing.Size(898, 48);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "記録するチャンネルを追加";
			// 
			// dgvThumbnail
			// 
			this.dgvThumbnail.HeaderText = "";
			this.dgvThumbnail.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
			this.dgvThumbnail.Name = "dgvThumbnail";
			this.dgvThumbnail.ReadOnly = true;
			this.dgvThumbnail.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvThumbnail.Width = 25;
			// 
			// dgvThumbnailUrl
			// 
			this.dgvThumbnailUrl.DataPropertyName = "Thumbnail";
			this.dgvThumbnailUrl.HeaderText = "サムネイルURL";
			this.dgvThumbnailUrl.Name = "dgvThumbnailUrl";
			this.dgvThumbnailUrl.ReadOnly = true;
			this.dgvThumbnailUrl.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvThumbnailUrl.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.dgvThumbnailUrl.Visible = false;
			this.dgvThumbnailUrl.Width = 40;
			// 
			// dgvChannelID
			// 
			this.dgvChannelID.DataPropertyName = "ChannelID";
			this.dgvChannelID.HeaderText = "チャンネルID";
			this.dgvChannelID.Name = "dgvChannelID";
			this.dgvChannelID.ReadOnly = true;
			// 
			// dgvUserName
			// 
			this.dgvUserName.DataPropertyName = "ChannelName";
			this.dgvUserName.HeaderText = "チャンネル名";
			this.dgvUserName.Name = "dgvUserName";
			this.dgvUserName.ReadOnly = true;
			// 
			// dgvLiveID
			// 
			this.dgvLiveID.DataPropertyName = "LiveID";
			this.dgvLiveID.HeaderText = "ライブID";
			this.dgvLiveID.Name = "dgvLiveID";
			this.dgvLiveID.ReadOnly = true;
			// 
			// dgvLiveTitle
			// 
			this.dgvLiveTitle.DataPropertyName = "LiveTitle";
			this.dgvLiveTitle.HeaderText = "番組名";
			this.dgvLiveTitle.Name = "dgvLiveTitle";
			this.dgvLiveTitle.ReadOnly = true;
			// 
			// dgvStatus
			// 
			dataGridViewCellStyle2.NullValue = null;
			this.dgvStatus.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgvStatus.HeaderText = "状況";
			this.dgvStatus.Name = "dgvStatus";
			this.dgvStatus.ReadOnly = true;
			// 
			// dgvLiveURL
			// 
			this.dgvLiveURL.DataPropertyName = "LiveURL";
			this.dgvLiveURL.HeaderText = "配信URL";
			this.dgvLiveURL.MinimumWidth = 100;
			this.dgvLiveURL.Name = "dgvLiveURL";
			this.dgvLiveURL.ReadOnly = true;
			this.dgvLiveURL.Width = 200;
			// 
			// dgvLiveStartTime
			// 
			this.dgvLiveStartTime.DataPropertyName = "LiveStartTime";
			dataGridViewCellStyle3.Format = "G";
			dataGridViewCellStyle3.NullValue = null;
			this.dgvLiveStartTime.DefaultCellStyle = dataGridViewCellStyle3;
			this.dgvLiveStartTime.HeaderText = "放送開始";
			this.dgvLiveStartTime.Name = "dgvLiveStartTime";
			this.dgvLiveStartTime.ReadOnly = true;
			// 
			// dgvLiveEndDate
			// 
			this.dgvLiveEndDate.DataPropertyName = "LiveEndTime";
			dataGridViewCellStyle4.Format = "G";
			dataGridViewCellStyle4.NullValue = null;
			this.dgvLiveEndDate.DefaultCellStyle = dataGridViewCellStyle4;
			this.dgvLiveEndDate.HeaderText = "放送終了";
			this.dgvLiveEndDate.Name = "dgvLiveEndDate";
			this.dgvLiveEndDate.ReadOnly = true;
			// 
			// dgvLastRequestDate
			// 
			this.dgvLastRequestDate.DataPropertyName = "LastRequestTime";
			dataGridViewCellStyle5.Format = "G";
			dataGridViewCellStyle5.NullValue = null;
			this.dgvLastRequestDate.DefaultCellStyle = dataGridViewCellStyle5;
			this.dgvLastRequestDate.HeaderText = "最終リクエスト";
			this.dgvLastRequestDate.MinimumWidth = 100;
			this.dgvLastRequestDate.Name = "dgvLastRequestDate";
			this.dgvLastRequestDate.ReadOnly = true;
			this.dgvLastRequestDate.Width = 120;
			// 
			// dgvNextRequestTime
			// 
			this.dgvNextRequestTime.DataPropertyName = "NextRequestTime";
			dataGridViewCellStyle6.Format = "G";
			this.dgvNextRequestTime.DefaultCellStyle = dataGridViewCellStyle6;
			this.dgvNextRequestTime.HeaderText = "次回リクエスト";
			this.dgvNextRequestTime.Name = "dgvNextRequestTime";
			this.dgvNextRequestTime.ReadOnly = true;
			// 
			// dgvAddDate
			// 
			this.dgvAddDate.DataPropertyName = "AddDate";
			this.dgvAddDate.HeaderText = "追加日";
			this.dgvAddDate.Name = "dgvAddDate";
			this.dgvAddDate.ReadOnly = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(916, 481);
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
		private System.Data.DataColumn dataColumn10;
		private System.Data.DataColumn dataColumn11;
		private System.Data.DataColumn dataColumn12;
		private System.Data.DataColumn dataColumn13;
		private System.Windows.Forms.DataGridViewImageColumn dgvThumbnail;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvThumbnailUrl;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvChannelID;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvUserName;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvLiveID;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvLiveTitle;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvStatus;
		private System.Windows.Forms.DataGridViewLinkColumn dgvLiveURL;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvLiveStartTime;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvLiveEndDate;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvLastRequestDate;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvNextRequestTime;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvAddDate;
	}
}

