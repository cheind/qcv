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
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this._pg = new System.Windows.Forms.PropertyGrid();
      this.label1 = new System.Windows.Forms.Label();
      this._cmb_filters = new System.Windows.Forms.ComboBox();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.90909F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 84.09091F));
      this.tableLayoutPanel1.Controls.Add(this._pg, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this._cmb_filters, 1, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(440, 363);
      this.tableLayoutPanel1.TabIndex = 1;
      // 
      // _pg
      // 
      this.tableLayoutPanel1.SetColumnSpan(this._pg, 2);
      this._pg.Dock = System.Windows.Forms.DockStyle.Fill;
      this._pg.Location = new System.Drawing.Point(3, 30);
      this._pg.Name = "_pg";
      this._pg.Size = new System.Drawing.Size(434, 330);
      this._pg.TabIndex = 0;
      this._pg.ToolbarVisible = false;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Location = new System.Drawing.Point(3, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(64, 27);
      this.label1.TabIndex = 1;
      this.label1.Text = "Select Filter";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // _cmb_filters
      // 
      this._cmb_filters.Dock = System.Windows.Forms.DockStyle.Fill;
      this._cmb_filters.FormattingEnabled = true;
      this._cmb_filters.Location = new System.Drawing.Point(73, 3);
      this._cmb_filters.Name = "_cmb_filters";
      this._cmb_filters.Size = new System.Drawing.Size(364, 21);
      this._cmb_filters.TabIndex = 2;
      this._cmb_filters.SelectedIndexChanged += new System.EventHandler(this._cmb_filters_SelectedIndexChanged_1);
      // 
      // FilterProperties
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "FilterProperties";
      this.Size = new System.Drawing.Size(440, 363);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.PropertyGrid _pg;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox _cmb_filters;
  }
}
