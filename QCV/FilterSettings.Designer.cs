namespace QCV {
  partial class FilterSettings {
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this._cmb_filters = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this._fl_events = new QCV.FilterEvents();
      this._fl_properties = new QCV.FilterProperties();
      this.tableLayoutPanel1.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
      this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 2);
      this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this._cmb_filters, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(673, 353);
      this.tableLayoutPanel1.TabIndex = 5;
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this._fl_events);
      this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox3.Location = new System.Drawing.Point(449, 48);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(218, 299);
      this.groupBox3.TabIndex = 5;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Events";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this._fl_properties);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox2.Location = new System.Drawing.Point(3, 48);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(440, 299);
      this.groupBox2.TabIndex = 4;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Properties";
      // 
      // _cmb_filters
      // 
      this.tableLayoutPanel1.SetColumnSpan(this._cmb_filters, 2);
      this._cmb_filters.FormattingEnabled = true;
      this._cmb_filters.Location = new System.Drawing.Point(3, 20);
      this._cmb_filters.Name = "_cmb_filters";
      this._cmb_filters.Size = new System.Drawing.Size(266, 22);
      this._cmb_filters.TabIndex = 2;
      this._cmb_filters.SelectedIndexChanged += new System.EventHandler(this._cmb_filters_SelectedIndexChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label1.Location = new System.Drawing.Point(3, 3);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(440, 14);
      this.label1.TabIndex = 4;
      this.label1.Text = "Select Filter";
      // 
      // _fl_events
      // 
      this._fl_events.Dock = System.Windows.Forms.DockStyle.Fill;
      this._fl_events.Location = new System.Drawing.Point(3, 18);
      this._fl_events.Name = "_fl_events";
      this._fl_events.Size = new System.Drawing.Size(212, 278);
      this._fl_events.TabIndex = 0;
      // 
      // _fl_properties
      // 
      this._fl_properties.Dock = System.Windows.Forms.DockStyle.Fill;
      this._fl_properties.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._fl_properties.Location = new System.Drawing.Point(3, 18);
      this._fl_properties.Margin = new System.Windows.Forms.Padding(0);
      this._fl_properties.Name = "_fl_properties";
      this._fl_properties.Size = new System.Drawing.Size(434, 278);
      this._fl_properties.TabIndex = 0;
      // 
      // FilterSettings
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Name = "FilterSettings";
      this.Size = new System.Drawing.Size(673, 353);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.GroupBox groupBox3;
    private FilterEvents _fl_events;
    private System.Windows.Forms.GroupBox groupBox2;
    private FilterProperties _fl_properties;
    private System.Windows.Forms.ComboBox _cmb_filters;
    private System.Windows.Forms.Label label1;

  }
}
