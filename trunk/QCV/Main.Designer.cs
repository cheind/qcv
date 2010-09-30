namespace QCV {
  partial class Main {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this._btn_run = new System.Windows.Forms.Button();
      this._btn_props = new System.Windows.Forms.Button();
      this._lb_status = new System.Windows.Forms.Label();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this._mnu_help_arguments = new System.Windows.Forms.ToolStripMenuItem();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this._mnu_save_filter_list = new System.Windows.Forms.ToolStripMenuItem();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // _btn_run
      // 
      this._btn_run.Location = new System.Drawing.Point(22, 36);
      this._btn_run.Name = "_btn_run";
      this._btn_run.Size = new System.Drawing.Size(75, 50);
      this._btn_run.TabIndex = 0;
      this._btn_run.Text = "Run";
      this._btn_run.UseVisualStyleBackColor = true;
      this._btn_run.Click += new System.EventHandler(this._btn_play_Click);
      // 
      // _btn_props
      // 
      this._btn_props.Location = new System.Drawing.Point(103, 36);
      this._btn_props.Name = "_btn_props";
      this._btn_props.Size = new System.Drawing.Size(75, 50);
      this._btn_props.TabIndex = 1;
      this._btn_props.Text = "Properties";
      this._btn_props.UseVisualStyleBackColor = true;
      this._btn_props.Click += new System.EventHandler(this._btn_props_Click);
      // 
      // _lb_status
      // 
      this._lb_status.BackColor = System.Drawing.SystemColors.Control;
      this._lb_status.Location = new System.Drawing.Point(22, 89);
      this._lb_status.Name = "_lb_status";
      this._lb_status.Size = new System.Drawing.Size(156, 19);
      this._lb_status.TabIndex = 2;
      this._lb_status.Text = "Press Run to Start";
      this._lb_status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(200, 24);
      this.menuStrip1.TabIndex = 3;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._mnu_help_arguments});
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.helpToolStripMenuItem.Text = "Help";
      // 
      // _mnu_help_arguments
      // 
      this._mnu_help_arguments.Name = "_mnu_help_arguments";
      this._mnu_help_arguments.Size = new System.Drawing.Size(152, 22);
      this._mnu_help_arguments.Text = "Arguments";
      this._mnu_help_arguments.Click += new System.EventHandler(this._mnu_help_arguments_Click);
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._mnu_save_filter_list});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // _mnu_save_filter_list
      // 
      this._mnu_save_filter_list.Name = "_mnu_save_filter_list";
      this._mnu_save_filter_list.Size = new System.Drawing.Size(152, 22);
      this._mnu_save_filter_list.Text = "Save Filter List";
      this._mnu_save_filter_list.Click += new System.EventHandler(this._mnu_save_filter_list_Click);
      // 
      // Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(200, 118);
      this.Controls.Add(this._lb_status);
      this.Controls.Add(this._btn_props);
      this.Controls.Add(this._btn_run);
      this.Controls.Add(this.menuStrip1);
      this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Main";
      this.Text = "QCV";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button _btn_run;
    private System.Windows.Forms.Button _btn_props;
    private System.Windows.Forms.Label _lb_status;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem _mnu_help_arguments;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem _mnu_save_filter_list;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;

  }
}

