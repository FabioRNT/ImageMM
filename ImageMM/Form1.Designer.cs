namespace ImageMM
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this._btnOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._pcbImage = new System.Windows.Forms.PictureBox();
            this._cmbSelectChannel = new System.Windows.Forms.ComboBox();
            this._btnChangeChannel = new System.Windows.Forms.Button();
            this._lblAvgR = new System.Windows.Forms.Label();
            this._lblAvgG = new System.Windows.Forms.Label();
            this._lblAvgB = new System.Windows.Forms.Label();
            this._lblAvgY = new System.Windows.Forms.Label();
            this._lblAvgCr = new System.Windows.Forms.Label();
            this._lblAvgCb = new System.Windows.Forms.Label();
            this._ofdOpenImage = new System.Windows.Forms.OpenFileDialog();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pcbImage)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnOpenFile,
            this.zoomOutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1207, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // _btnOpenFile
            // 
            this._btnOpenFile.Name = "_btnOpenFile";
            this._btnOpenFile.Size = new System.Drawing.Size(69, 20);
            this._btnOpenFile.Text = "Open File";
            this._btnOpenFile.Click += new System.EventHandler(this._btnOpenFile_Click);
            // 
            // zoomOutToolStripMenuItem
            // 
            this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.zoomOutToolStripMenuItem.Text = "Zoom Out";
            this.zoomOutToolStripMenuItem.Click += new System.EventHandler(this.zoomOutToolStripMenuItem_Click);
            // 
            // _pcbImage
            // 
            this._pcbImage.Location = new System.Drawing.Point(9, 32);
            this._pcbImage.Name = "_pcbImage";
            this._pcbImage.Size = new System.Drawing.Size(100, 50);
            this._pcbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this._pcbImage.TabIndex = 1;
            this._pcbImage.TabStop = false;
            this._pcbImage.Paint += new System.Windows.Forms.PaintEventHandler(this._pcbImage_Paint);
            this._pcbImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this._pcbImage_MouseDown);
            this._pcbImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this._pcbImage_MouseMove);
            this._pcbImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this._pcbImage_MouseUp);
            // 
            // _cmbSelectChannel
            // 
            this._cmbSelectChannel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cmbSelectChannel.FormattingEnabled = true;
            this._cmbSelectChannel.Location = new System.Drawing.Point(3, 3);
            this._cmbSelectChannel.Name = "_cmbSelectChannel";
            this._cmbSelectChannel.Size = new System.Drawing.Size(111, 21);
            this._cmbSelectChannel.TabIndex = 2;
            // 
            // _btnChangeChannel
            // 
            this._btnChangeChannel.Enabled = false;
            this._btnChangeChannel.Location = new System.Drawing.Point(3, 30);
            this._btnChangeChannel.Name = "_btnChangeChannel";
            this._btnChangeChannel.Size = new System.Drawing.Size(111, 23);
            this._btnChangeChannel.TabIndex = 3;
            this._btnChangeChannel.Text = "Select";
            this._btnChangeChannel.UseVisualStyleBackColor = true;
            this._btnChangeChannel.Click += new System.EventHandler(this._btnChangeChannel_Click);
            // 
            // _lblAvgR
            // 
            this._lblAvgR.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this._lblAvgR, true);
            this._lblAvgR.Location = new System.Drawing.Point(3, 56);
            this._lblAvgR.Name = "_lblAvgR";
            this._lblAvgR.Size = new System.Drawing.Size(40, 13);
            this._lblAvgR.TabIndex = 4;
            this._lblAvgR.Text = "Avg R:";
            // 
            // _lblAvgG
            // 
            this._lblAvgG.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this._lblAvgG, true);
            this._lblAvgG.Location = new System.Drawing.Point(3, 69);
            this._lblAvgG.Name = "_lblAvgG";
            this._lblAvgG.Size = new System.Drawing.Size(40, 13);
            this._lblAvgG.TabIndex = 5;
            this._lblAvgG.Text = "Avg G:";
            // 
            // _lblAvgB
            // 
            this._lblAvgB.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this._lblAvgB, true);
            this._lblAvgB.Location = new System.Drawing.Point(3, 82);
            this._lblAvgB.Name = "_lblAvgB";
            this._lblAvgB.Size = new System.Drawing.Size(39, 13);
            this._lblAvgB.TabIndex = 6;
            this._lblAvgB.Text = "Avg B:";
            // 
            // _lblAvgY
            // 
            this._lblAvgY.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this._lblAvgY, true);
            this._lblAvgY.Location = new System.Drawing.Point(3, 95);
            this._lblAvgY.Name = "_lblAvgY";
            this._lblAvgY.Size = new System.Drawing.Size(39, 13);
            this._lblAvgY.TabIndex = 7;
            this._lblAvgY.Text = "Avg Y:";
            // 
            // _lblAvgCr
            // 
            this._lblAvgCr.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this._lblAvgCr, true);
            this._lblAvgCr.Location = new System.Drawing.Point(3, 108);
            this._lblAvgCr.Name = "_lblAvgCr";
            this._lblAvgCr.Size = new System.Drawing.Size(42, 13);
            this._lblAvgCr.TabIndex = 8;
            this._lblAvgCr.Text = "Avg Cr:";
            // 
            // _lblAvgCb
            // 
            this._lblAvgCb.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this._lblAvgCb, true);
            this._lblAvgCb.Location = new System.Drawing.Point(3, 121);
            this._lblAvgCb.Name = "_lblAvgCb";
            this._lblAvgCb.Size = new System.Drawing.Size(45, 13);
            this._lblAvgCb.TabIndex = 9;
            this._lblAvgCb.Text = "Avg Cb:";
            // 
            // _ofdOpenImage
            // 
            this._ofdOpenImage.FileName = "openFileDialog1";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this._cmbSelectChannel);
            this.flowLayoutPanel1.Controls.Add(this._btnChangeChannel);
            this.flowLayoutPanel1.Controls.Add(this._lblAvgR);
            this.flowLayoutPanel1.Controls.Add(this._lblAvgG);
            this.flowLayoutPanel1.Controls.Add(this._lblAvgB);
            this.flowLayoutPanel1.Controls.Add(this._lblAvgY);
            this.flowLayoutPanel1.Controls.Add(this._lblAvgCr);
            this.flowLayoutPanel1.Controls.Add(this._lblAvgCb);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1093, 24);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(114, 539);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1207, 563);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this._pcbImage);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pcbImage)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem _btnOpenFile;
        private System.Windows.Forms.PictureBox _pcbImage;
        private System.Windows.Forms.ComboBox _cmbSelectChannel;
        private System.Windows.Forms.Button _btnChangeChannel;
        private System.Windows.Forms.Label _lblAvgR;
        private System.Windows.Forms.Label _lblAvgG;
        private System.Windows.Forms.Label _lblAvgB;
        private System.Windows.Forms.Label _lblAvgY;
        private System.Windows.Forms.Label _lblAvgCr;
        private System.Windows.Forms.Label _lblAvgCb;
        private System.Windows.Forms.OpenFileDialog _ofdOpenImage;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
    }
}

