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
      this.SuspendLayout();
      // 
      // _cmb_filters
      // 
      this._cmb_filters.Dock = System.Windows.Forms.DockStyle.Top;
      this._cmb_filters.FormattingEnabled = true;
      this._cmb_filters.Location = new System.Drawing.Point(0, 0);
      this._cmb_filters.Name = "_cmb_filters";
      this._cmb_filters.Size = new System.Drawing.Size(286, 22);
      this._cmb_filters.TabIndex = 0;
      this._cmb_filters.SelectedIndexChanged += new System.EventHandler(this._cmb_filters_SelectedIndexChanged);
      // 
      // _pg
      // 
      this._pg.Dock = System.Windows.Forms.DockStyle.Fill;
      this._pg.Location = new System.Drawing.Point(0, 22);
      this._pg.Name = "_pg";
      this._pg.Size = new System.Drawing.Size(286, 371);
      this._pg.TabIndex = 1;
      this._pg.ToolbarVisible = false;
      // 
      // PropertyForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(286, 393);
      this.Controls.Add(this._pg);
      this.Controls.Add(this._cmb_filters);
      this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.Name = "PropertyForm";
      this.Text = "PropertyForm";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ComboBox _cmb_filters;
    private System.Windows.Forms.PropertyGrid _pg;
  }
}