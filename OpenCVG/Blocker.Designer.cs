namespace OpenCVG
{
    partial class Blocker
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
            this.funcsList = new System.Windows.Forms.ListBox();
            this.filterTxt = new System.Windows.Forms.TextBox();
            this._filterLbl = new System.Windows.Forms.Label();
            this.resultPb = new System.Windows.Forms.PictureBox();
            this.blocksPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.resultPb)).BeginInit();
            this.SuspendLayout();
            // 
            // funcsList
            // 
            this.funcsList.FormattingEnabled = true;
            this.funcsList.Location = new System.Drawing.Point(12, 48);
            this.funcsList.Name = "funcsList";
            this.funcsList.Size = new System.Drawing.Size(186, 589);
            this.funcsList.TabIndex = 0;
            this.funcsList.DoubleClick += new System.EventHandler(this.funcsList_DoubleClick);
            // 
            // filterTxt
            // 
            this.filterTxt.Location = new System.Drawing.Point(52, 12);
            this.filterTxt.Name = "filterTxt";
            this.filterTxt.Size = new System.Drawing.Size(146, 20);
            this.filterTxt.TabIndex = 1;
            this.filterTxt.TextChanged += new System.EventHandler(this.filterTxt_TextChanged);
            // 
            // _filterLbl
            // 
            this._filterLbl.AutoSize = true;
            this._filterLbl.Location = new System.Drawing.Point(9, 15);
            this._filterLbl.Name = "_filterLbl";
            this._filterLbl.Size = new System.Drawing.Size(32, 13);
            this._filterLbl.TabIndex = 2;
            this._filterLbl.Text = "Filter:";
            // 
            // resultPb
            // 
            this.resultPb.Location = new System.Drawing.Point(204, 12);
            this.resultPb.Name = "resultPb";
            this.resultPb.Size = new System.Drawing.Size(666, 466);
            this.resultPb.TabIndex = 3;
            this.resultPb.TabStop = false;
            // 
            // blocksPanel
            // 
            this.blocksPanel.Location = new System.Drawing.Point(204, 484);
            this.blocksPanel.Name = "blocksPanel";
            this.blocksPanel.Size = new System.Drawing.Size(666, 153);
            this.blocksPanel.TabIndex = 4;
            this.blocksPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.blocksPanel_Paint);
            // 
            // Blocker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 649);
            this.Controls.Add(this.blocksPanel);
            this.Controls.Add(this.resultPb);
            this.Controls.Add(this._filterLbl);
            this.Controls.Add(this.filterTxt);
            this.Controls.Add(this.funcsList);
            this.Name = "Blocker";
            this.Text = "Blocker";
            this.Load += new System.EventHandler(this.Blocker_Load);
            ((System.ComponentModel.ISupportInitialize)(this.resultPb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox funcsList;
        private System.Windows.Forms.TextBox filterTxt;
        private System.Windows.Forms.Label _filterLbl;
        private System.Windows.Forms.PictureBox resultPb;
        private System.Windows.Forms.Panel blocksPanel;
    }
}