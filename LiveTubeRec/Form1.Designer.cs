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
            this.textBox_Log = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.textBoxChannelID = new System.Windows.Forms.TextBox();
            this.buttonInsert = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.userName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.channelID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.liveID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.liveURL = new System.Windows.Forms.DataGridViewLinkColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_Log
            // 
            this.textBox_Log.Location = new System.Drawing.Point(6, 18);
            this.textBox_Log.Multiline = true;
            this.textBox_Log.Name = "textBox_Log";
            this.textBox_Log.Size = new System.Drawing.Size(382, 199);
            this.textBox_Log.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_Log);
            this.groupBox1.Location = new System.Drawing.Point(470, 348);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 223);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ログ";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.userName,
            this.channelID,
            this.status,
            this.liveID,
            this.liveURL});
            this.dataGridView.Location = new System.Drawing.Point(29, 137);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowTemplate.Height = 21;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(829, 194);
            this.dataGridView.TabIndex = 2;
            // 
            // textBoxChannelID
            // 
            this.textBoxChannelID.Location = new System.Drawing.Point(29, 53);
            this.textBoxChannelID.Name = "textBoxChannelID";
            this.textBoxChannelID.Size = new System.Drawing.Size(333, 19);
            this.textBoxChannelID.TabIndex = 3;
            // 
            // buttonInsert
            // 
            this.buttonInsert.Location = new System.Drawing.Point(393, 51);
            this.buttonInsert.Name = "buttonInsert";
            this.buttonInsert.Size = new System.Drawing.Size(75, 23);
            this.buttonInsert.TabIndex = 4;
            this.buttonInsert.Text = "追加";
            this.buttonInsert.UseVisualStyleBackColor = true;
            this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
            // 
            // timer
            // 
            this.timer.Interval = 180000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // userName
            // 
            this.userName.HeaderText = "ユーザー名";
            this.userName.Name = "userName";
            this.userName.ReadOnly = true;
            // 
            // channelID
            // 
            this.channelID.HeaderText = "チャンネルID";
            this.channelID.Name = "channelID";
            this.channelID.ReadOnly = true;
            // 
            // status
            // 
            this.status.HeaderText = "状況";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // liveID
            // 
            this.liveID.HeaderText = "ライブID";
            this.liveID.Name = "liveID";
            this.liveID.ReadOnly = true;
            // 
            // liveURL
            // 
            this.liveURL.HeaderText = "配信URL";
            this.liveURL.Name = "liveURL";
            this.liveURL.ReadOnly = true;
            this.liveURL.Width = 350;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 583);
            this.Controls.Add(this.buttonInsert);
            this.Controls.Add(this.textBoxChannelID);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Log;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TextBox textBoxChannelID;
        private System.Windows.Forms.Button buttonInsert;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.DataGridViewTextBoxColumn userName;
        private System.Windows.Forms.DataGridViewTextBoxColumn channelID;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn liveID;
        private System.Windows.Forms.DataGridViewLinkColumn liveURL;
    }
}

