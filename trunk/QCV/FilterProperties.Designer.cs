namespace QCV {
  partial class FilterProperties {
    /// <summary> 
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Vom Komponenten-Designer generierter Code

    /// <summary> 
    /// Erforderliche Methode für die Designerunterstützung. 
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent() {
      this._pg = new System.Windows.Forms.PropertyGrid();
      this.SuspendLayout();
      // 
      // _pg
      // 
      this._pg.BackColor = System.Drawing.SystemColors.Control;
      this._pg.Dock = System.Windows.Forms.DockStyle.Fill;
      this._pg.Location = new System.Drawing.Point(0, 0);
      this._pg.Margin = new System.Windows.Forms.Padding(0);
      this._pg.Name = "_pg";
      this._pg.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
      this._pg.Size = new System.Drawing.Size(440, 391);
      this._pg.TabIndex = 0;
      this._pg.ToolbarVisible = false;
      // 
      // FilterProperties
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this._pg);
      this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Margin = new System.Windows.Forms.Padding(0);
      this.Name = "FilterProperties";
      this.Size = new System.Drawing.Size(440, 391);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PropertyGrid _pg;

  }
}
