namespace QCV {
  partial class PropertyForm {
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
      this._cmb_filters = new System.Windows.Forms.ComboBox();
      this._pg = new System.Windows.Forms.PropertyGrid();
      this._tabctrl = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this._tabctrl.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.SuspendLayout();
      // 
      // _cmb_filters
      // 
      this._cmb_filters.Dock = System.Windows.Forms.DockStyle.Top;
      this._cmb_filters.FormattingEnabled = true;
      this._cmb_filters.Location = new System.Drawing.Point(3, 3);
      this._cmb_filters.Name = "_cmb_filters";
      this._cmb_filters.Size = new System.Drawing.Size(519, 22);
      this._cmb_filters.TabIndex = 0;
      this._cmb_filters.SelectedIndexChanged += new System.EventHandler(this._cmb_filters_SelectedIndexChanged);
      // 
      // _pg
      // 
      this._pg.Dock = System.Windows.Forms.DockStyle.Fill;
      this._pg.Location = new System.Drawing.Point(3, 25);
      this._pg.Name = "_pg";
      this._pg.Size = new System.Drawing.Size(519, 395);
      this._pg.TabIndex = 1;
      this._pg.ToolbarVisible = false;
      // 
      // _tabctrl
      // 
      this._tabctrl.Controls.Add(this.tabPage1);
      this._tabctrl.Controls.Add(this.tabPage2);
      this._tabctrl.Dock = System.Windows.Forms.DockStyle.Fill;
      this._tabctrl.Location = new System.Drawing.Point(0, 0);
      this._tabctrl.Name = "_tabctrl";
      this._tabctrl.SelectedIndex = 0;
      this._tabctrl.Size = new System.Drawing.Size(533, 450);
      this._tabctrl.TabIndex = 2;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this._pg);
      this.tabPage1.Controls.Add(this._cmb_filters);
      this.tabPage1.Location = new System.Drawing.Point(4, 23);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(525, 423);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Properties";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // tabPage2
      // 
      this.tabPage2.Location = new System.Drawing.Point(4, 23);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(525, 423);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Values";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // PropertyForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(533, 450);
      this.Controls.Add(this._tabctrl);
      this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.Name = "PropertyForm";
      this.Text = "PropertyForm";
      this._tabctrl.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ComboBox _cmb_filters;
    private System.Windows.Forms.PropertyGrid _pg;
    private System.Windows.Forms.TabControl _tabctrl;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
  }
}