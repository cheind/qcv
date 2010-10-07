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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
      this._btn_run = new System.Windows.Forms.Button();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this._mnu_save_filter_list = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this._mnu_help_arguments = new System.Windows.Forms.ToolStripMenuItem();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.keyValuesBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.valuesDataSet = new QCV.ValuesDataSet();
      this.valuesDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this._tp_values = new System.Windows.Forms.TabPage();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this._tp_console = new System.Windows.Forms.TabPage();
      this._rtb_console = new System.Windows.Forms.RichTextBox();
      this._tabctrl = new System.Windows.Forms.TabControl();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this._fl_settings = new QCV.FilterSettings();
      this.menuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.keyValuesBindingSource)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.valuesDataSet)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.valuesDataSetBindingSource)).BeginInit();
      this._tp_values.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      this._tp_console.SuspendLayout();
      this._tabctrl.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // _btn_run
      // 
      this._btn_run.Location = new System.Drawing.Point(8, 3);
      this._btn_run.Margin = new System.Windows.Forms.Padding(8, 3, 3, 3);
      this._btn_run.Name = "_btn_run";
      this._btn_run.Size = new System.Drawing.Size(75, 27);
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
      this.menuStrip1.Size = new System.Drawing.Size(512, 24);
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
      // keyValuesBindingSource
      // 
      this.keyValuesBindingSource.DataMember = "KeyValues";
      this.keyValuesBindingSource.DataSource = this.valuesDataSet;
      // 
      // valuesDataSet
      // 
      this.valuesDataSet.DataSetName = "ValuesDataSet";
      this.valuesDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
      // 
      // valuesDataSetBindingSource
      // 
      this.valuesDataSetBindingSource.DataSource = this.valuesDataSet;
      this.valuesDataSetBindingSource.Position = 0;
      // 
      // _tp_values
      // 
      this._tp_values.BackColor = System.Drawing.Color.Transparent;
      this._tp_values.Controls.Add(this.dataGridView1);
      this._tp_values.Location = new System.Drawing.Point(4, 23);
      this._tp_values.Name = "_tp_values";
      this._tp_values.Size = new System.Drawing.Size(504, 128);
      this._tp_values.TabIndex = 2;
      this._tp_values.Text = "Values";
      this._tp_values.UseVisualStyleBackColor = true;
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
      this.dataGridView1.Size = new System.Drawing.Size(504, 129);
      this.dataGridView1.TabIndex = 0;
      // 
      // valueDataGridViewTextBoxColumn
      // 
      this.valueDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
      this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
      this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
      this.valueDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // idDataGridViewTextBoxColumn
      // 
      this.idDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
      this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
      this.idDataGridViewTextBoxColumn.HeaderText = "Id";
      this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
      this.idDataGridViewTextBoxColumn.ReadOnly = true;
      this.idDataGridViewTextBoxColumn.Width = 43;
      // 
      // _tp_console
      // 
      this._tp_console.BackColor = System.Drawing.Color.Transparent;
      this._tp_console.Controls.Add(this._rtb_console);
      this._tp_console.Location = new System.Drawing.Point(4, 23);
      this._tp_console.Name = "_tp_console";
      this._tp_console.Padding = new System.Windows.Forms.Padding(3);
      this._tp_console.Size = new System.Drawing.Size(504, 128);
      this._tp_console.TabIndex = 0;
      this._tp_console.Text = "Console";
      this._tp_console.UseVisualStyleBackColor = true;
      // 
      // _rtb_console
      // 
      this._rtb_console.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this._rtb_console.Dock = System.Windows.Forms.DockStyle.Fill;
      this._rtb_console.Location = new System.Drawing.Point(3, 3);
      this._rtb_console.Name = "_rtb_console";
      this._rtb_console.Size = new System.Drawing.Size(498, 122);
      this._rtb_console.TabIndex = 0;
      this._rtb_console.Text = "";
      // 
      // _tabctrl
      // 
      this._tabctrl.Controls.Add(this._tp_console);
      this._tabctrl.Controls.Add(this._tp_values);
      this._tabctrl.Dock = System.Windows.Forms.DockStyle.Bottom;
      this._tabctrl.Location = new System.Drawing.Point(0, 372);
      this._tabctrl.Multiline = true;
      this._tabctrl.Name = "_tabctrl";
      this._tabctrl.SelectedIndex = 0;
      this._tabctrl.Size = new System.Drawing.Size(512, 155);
      this._tabctrl.TabIndex = 4;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this._fl_settings, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this._btn_run, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(512, 348);
      this.tableLayoutPanel1.TabIndex = 6;
      // 
      // _fl_settings
      // 
      this._fl_settings.Dock = System.Windows.Forms.DockStyle.Fill;
      this._fl_settings.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._fl_settings.Location = new System.Drawing.Point(3, 36);
      this._fl_settings.Name = "_fl_settings";
      this._fl_settings.Padding = new System.Windows.Forms.Padding(3);
      this._fl_settings.Size = new System.Drawing.Size(506, 309);
      this._fl_settings.TabIndex = 5;
      // 
      // Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.ClientSize = new System.Drawing.Size(512, 527);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Controls.Add(this._tabctrl);
      this.Controls.Add(this.menuStrip1);
      this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Main";
      this.Text = "QCV";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.keyValuesBindingSource)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.valuesDataSet)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.valuesDataSetBindingSource)).EndInit();
      this._tp_values.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      this._tp_console.ResumeLayout(false);
      this._tabctrl.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
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
    private System.Windows.Forms.BindingSource valuesDataSetBindingSource;
    private ValuesDataSet valuesDataSet;
    private System.Windows.Forms.BindingSource keyValuesBindingSource;
    private System.Windows.Forms.TabPage _tp_values;
    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
    private System.Windows.Forms.TabPage _tp_console;
    private System.Windows.Forms.RichTextBox _rtb_console;
    private System.Windows.Forms.TabControl _tabctrl;
    private FilterSettings _fl_settings;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

  }
}

