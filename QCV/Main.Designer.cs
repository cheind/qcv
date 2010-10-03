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
      this.components = new System.ComponentModel.Container();
      this._btn_run = new System.Windows.Forms.Button();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this._mnu_save_filter_list = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this._mnu_help_arguments = new System.Windows.Forms.ToolStripMenuItem();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this._tabctrl = new System.Windows.Forms.TabControl();
      this._tb_console = new System.Windows.Forms.TabPage();
      this._rtb_console = new System.Windows.Forms.RichTextBox();
      this._tp_filters = new System.Windows.Forms.TabPage();
      this._filter_properties = new QCV.FilterProperties();
      this._tb_values = new System.Windows.Forms.TabPage();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.valuesDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.valuesDataSet = new QCV.ValuesDataSet();
      this.keyValuesBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.menuStrip1.SuspendLayout();
      this._tabctrl.SuspendLayout();
      this._tb_console.SuspendLayout();
      this._tp_filters.SuspendLayout();
      this._tb_values.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.valuesDataSetBindingSource)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.valuesDataSet)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.keyValuesBindingSource)).BeginInit();
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
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(510, 24);
      this.menuStrip1.TabIndex = 3;
      this.menuStrip1.Text = "menuStrip1";
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
      this._mnu_save_filter_list.Size = new System.Drawing.Size(148, 22);
      this._mnu_save_filter_list.Text = "Save Filter List";
      this._mnu_save_filter_list.Click += new System.EventHandler(this._mnu_save_filter_list_Click);
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
      this._mnu_help_arguments.Size = new System.Drawing.Size(133, 22);
      this._mnu_help_arguments.Text = "Arguments";
      this._mnu_help_arguments.Click += new System.EventHandler(this._mnu_help_arguments_Click);
      // 
      // _tabctrl
      // 
      this._tabctrl.Controls.Add(this._tb_console);
      this._tabctrl.Controls.Add(this._tp_filters);
      this._tabctrl.Controls.Add(this._tb_values);
      this._tabctrl.Dock = System.Windows.Forms.DockStyle.Bottom;
      this._tabctrl.Location = new System.Drawing.Point(0, 156);
      this._tabctrl.Multiline = true;
      this._tabctrl.Name = "_tabctrl";
      this._tabctrl.SelectedIndex = 0;
      this._tabctrl.Size = new System.Drawing.Size(510, 235);
      this._tabctrl.TabIndex = 4;
      // 
      // _tb_console
      // 
      this._tb_console.Controls.Add(this._rtb_console);
      this._tb_console.Location = new System.Drawing.Point(4, 23);
      this._tb_console.Name = "_tb_console";
      this._tb_console.Padding = new System.Windows.Forms.Padding(3);
      this._tb_console.Size = new System.Drawing.Size(502, 208);
      this._tb_console.TabIndex = 0;
      this._tb_console.Text = "Console";
      this._tb_console.UseVisualStyleBackColor = true;
      // 
      // _rtb_console
      // 
      this._rtb_console.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this._rtb_console.Dock = System.Windows.Forms.DockStyle.Fill;
      this._rtb_console.Location = new System.Drawing.Point(3, 3);
      this._rtb_console.Name = "_rtb_console";
      this._rtb_console.Size = new System.Drawing.Size(496, 202);
      this._rtb_console.TabIndex = 0;
      this._rtb_console.Text = "";
      // 
      // _tp_filters
      // 
      this._tp_filters.Controls.Add(this._filter_properties);
      this._tp_filters.Location = new System.Drawing.Point(4, 23);
      this._tp_filters.Name = "_tp_filters";
      this._tp_filters.Padding = new System.Windows.Forms.Padding(3);
      this._tp_filters.Size = new System.Drawing.Size(502, 208);
      this._tp_filters.TabIndex = 1;
      this._tp_filters.Text = "Filters";
      this._tp_filters.UseVisualStyleBackColor = true;
      // 
      // _filter_properties
      // 
      this._filter_properties.Dock = System.Windows.Forms.DockStyle.Fill;
      this._filter_properties.Location = new System.Drawing.Point(3, 3);
      this._filter_properties.Name = "_filter_properties";
      this._filter_properties.Size = new System.Drawing.Size(496, 202);
      this._filter_properties.TabIndex = 0;
      // 
      // _tb_values
      // 
      this._tb_values.Controls.Add(this.dataGridView1);
      this._tb_values.Location = new System.Drawing.Point(4, 23);
      this._tb_values.Name = "_tb_values";
      this._tb_values.Size = new System.Drawing.Size(502, 208);
      this._tb_values.TabIndex = 2;
      this._tb_values.Text = "Values";
      this._tb_values.UseVisualStyleBackColor = true;
      // 
      // dataGridView1
      // 
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.AutoGenerateColumns = false;
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.valueDataGridViewTextBoxColumn});
      this.dataGridView1.DataSource = this.keyValuesBindingSource;
      this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataGridView1.Location = new System.Drawing.Point(0, 0);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.ReadOnly = true;
      this.dataGridView1.Size = new System.Drawing.Size(502, 208);
      this.dataGridView1.TabIndex = 0;
      // 
      // valuesDataSetBindingSource
      // 
      this.valuesDataSetBindingSource.DataSource = this.valuesDataSet;
      this.valuesDataSetBindingSource.Position = 0;
      // 
      // valuesDataSet
      // 
      this.valuesDataSet.DataSetName = "ValuesDataSet";
      this.valuesDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
      // 
      // keyValuesBindingSource
      // 
      this.keyValuesBindingSource.DataMember = "KeyValues";
      this.keyValuesBindingSource.DataSource = this.valuesDataSet;
      // 
      // idDataGridViewTextBoxColumn
      // 
      this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
      this.idDataGridViewTextBoxColumn.HeaderText = "ID";
      this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
      this.idDataGridViewTextBoxColumn.ReadOnly = true;
      this.idDataGridViewTextBoxColumn.ToolTipText = "Identifier given by IFilter";
      // 
      // valueDataGridViewTextBoxColumn
      // 
      this.valueDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.valueDataGridViewTextBoxColumn.DataPropertyName = "value";
      this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
      this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
      this.valueDataGridViewTextBoxColumn.ReadOnly = true;
      this.valueDataGridViewTextBoxColumn.ToolTipText = "Value given by IFilter";
      // 
      // Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(510, 391);
      this.Controls.Add(this._tabctrl);
      this.Controls.Add(this._btn_run);
      this.Controls.Add(this.menuStrip1);
      this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Main";
      this.Text = "QCV";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this._tabctrl.ResumeLayout(false);
      this._tb_console.ResumeLayout(false);
      this._tp_filters.ResumeLayout(false);
      this._tb_values.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.valuesDataSetBindingSource)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.valuesDataSet)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.keyValuesBindingSource)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button _btn_run;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem _mnu_help_arguments;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem _mnu_save_filter_list;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.TabControl _tabctrl;
    private System.Windows.Forms.TabPage _tb_console;
    private System.Windows.Forms.TabPage _tp_filters;
    private System.Windows.Forms.TabPage _tb_values;
    private System.Windows.Forms.RichTextBox _rtb_console;
    private FilterProperties _filter_properties;
    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.BindingSource valuesDataSetBindingSource;
    private ValuesDataSet valuesDataSet;
    private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
    private System.Windows.Forms.BindingSource keyValuesBindingSource;

  }
}

