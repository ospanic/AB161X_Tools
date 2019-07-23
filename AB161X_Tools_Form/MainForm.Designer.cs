namespace AB161X_Tools_Form
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.baud_cBox = new System.Windows.Forms.ComboBox();
            this.sp_cBox = new System.Windows.Forms.ComboBox();
            this.download_pBar = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.log_tBox = new System.Windows.Forms.TextBox();
            this.start_btn = new System.Windows.Forms.Button();
            this.file_select_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.binFile_tBox = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(1, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(797, 445);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.baud_cBox);
            this.tabPage1.Controls.Add(this.sp_cBox);
            this.tabPage1.Controls.Add(this.download_pBar);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.log_tBox);
            this.tabPage1.Controls.Add(this.start_btn);
            this.tabPage1.Controls.Add(this.file_select_btn);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.binFile_tBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(789, 411);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Download Tool";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // baud_cBox
            // 
            this.baud_cBox.FormattingEnabled = true;
            this.baud_cBox.Location = new System.Drawing.Point(317, 314);
            this.baud_cBox.Name = "baud_cBox";
            this.baud_cBox.Size = new System.Drawing.Size(213, 29);
            this.baud_cBox.TabIndex = 9;
            // 
            // sp_cBox
            // 
            this.sp_cBox.FormattingEnabled = true;
            this.sp_cBox.Location = new System.Drawing.Point(317, 268);
            this.sp_cBox.Name = "sp_cBox";
            this.sp_cBox.Size = new System.Drawing.Size(213, 29);
            this.sp_cBox.TabIndex = 8;
            // 
            // download_pBar
            // 
            this.download_pBar.Location = new System.Drawing.Point(24, 361);
            this.download_pBar.Name = "download_pBar";
            this.download_pBar.Size = new System.Drawing.Size(741, 34);
            this.download_pBar.Step = 1;
            this.download_pBar.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(235, 314);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 27);
            this.label3.TabIndex = 6;
            this.label3.Text = "BAUD:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(240, 269);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 27);
            this.label2.TabIndex = 5;
            this.label2.Text = "COM:";
            // 
            // log_tBox
            // 
            this.log_tBox.Location = new System.Drawing.Point(24, 69);
            this.log_tBox.Multiline = true;
            this.log_tBox.Name = "log_tBox";
            this.log_tBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.log_tBox.Size = new System.Drawing.Size(736, 173);
            this.log_tBox.TabIndex = 4;
            // 
            // start_btn
            // 
            this.start_btn.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.start_btn.Location = new System.Drawing.Point(24, 260);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(191, 91);
            this.start_btn.TabIndex = 3;
            this.start_btn.Text = "START";
            this.start_btn.UseVisualStyleBackColor = true;
            this.start_btn.Click += new System.EventHandler(this.start_btn_Click);
            // 
            // file_select_btn
            // 
            this.file_select_btn.Location = new System.Drawing.Point(685, 18);
            this.file_select_btn.Name = "file_select_btn";
            this.file_select_btn.Size = new System.Drawing.Size(75, 34);
            this.file_select_btn.TabIndex = 2;
            this.file_select_btn.Text = "OPen";
            this.file_select_btn.UseVisualStyleBackColor = true;
            this.file_select_btn.Click += new System.EventHandler(this.file_select_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(21, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "Firmware：";
            // 
            // binFile_tBox
            // 
            this.binFile_tBox.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.binFile_tBox.Location = new System.Drawing.Point(146, 19);
            this.binFile_tBox.Name = "binFile_tBox";
            this.binFile_tBox.Size = new System.Drawing.Size(521, 33);
            this.binFile_tBox.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(789, 411);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "LabTest Tool";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "AB161X Tools";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button start_btn;
        private System.Windows.Forms.Button file_select_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox binFile_tBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox log_tBox;
        private System.Windows.Forms.ComboBox baud_cBox;
        private System.Windows.Forms.ComboBox sp_cBox;
        private System.Windows.Forms.ProgressBar download_pBar;
    }
}

