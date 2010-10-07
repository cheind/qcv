namespace QCV {
  partial class ShowQueryForm {
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
      this._lb_query_text = new System.Windows.Forms.Label();
      this._pg = new System.Windows.Forms.PropertyGrid();
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this._btn_ok = new System.Windows.Forms.Button();
      this._btn_cancel = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.flowLayoutPanel1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // _lb_query_text
      // 
      this._lb_query_text.Dock = System.Windows.Forms.DockStyle.Top;
      this._lb_query_text.ForeColor = System.Drawing.SystemColors.Highlight;
      this._lb_query_text.Location = new System.Drawing.Point(3, 18);
      this._lb_query_text.Name = "_lb_query_text";
      this._lb_query_text.Size = new System.Drawing.Size(284, 31);
      this._lb_query_text.TabIndex = 1;
      this._lb_query_text.Text = "QueryTextCaption";
      this._lb_query_text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // _pg
      // 
      this._pg.Dock = System.Windows.Forms.DockStyle.Fill;
      this._pg.Location = new System.Drawing.Point(3, 49);
      this._pg.Name = "_pg";
      this._pg.Size = new System.Drawing.Size(284, 190);
      this._pg.TabIndex = 2;
      this._pg.ToolbarVisible = false;
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.Controls.Add(this._btn_ok);
      this.flowLayoutPanel1.Controls.Add(this._btn_cancel);
      this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 245);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Size = new System.Drawing.Size(290, 33);
      this.flowLayoutPanel1.TabIndex = 3;
      // 
      // _btn_ok
      // 
      this._btn_ok.Location = new System.Drawing.Point(3, 3);
      this._btn_ok.Name = "_btn_ok";
      this._btn_ok.Size = new System.Drawing.Size(75, 25);
      this._btn_ok.TabIndex = 0;
      this._btn_ok.Text = "Ok";
      this._btn_ok.UseVisualStyleBackColor = true;
      this._btn_ok.Click += new System.EventHandler(this._btn_ok_Click);
      // 
      // _btn_cancel
      // 
      this._btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this._btn_cancel.Location = new System.Drawing.Point(84, 3);
      this._btn_cancel.Name = "_btn_cancel";
      this._btn_cancel.Size = new System.Drawing.Size(75, 25);
      this._btn_cancel.TabIndex = 1;
      this._btn_cancel.Text = "Cancel";
      this._btn_cancel.UseVisualStyleBackColor = true;
      this._btn_cancel.Click += new System.EventHandler(this._btn_cancel_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this._pg);
      this.groupBox1.Controls.Add(this._lb_query_text);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(3, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(290, 242);
      this.groupBox1.TabIndex = 4;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Please respond";
      // 
      // ShowQueryForm
      // 
      this.AcceptButton = this._btn_ok;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this._btn_cancel;
      this.ClientSize = new System.Drawing.Size(296, 281);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.flowLayoutPanel1);
      this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "ShowQueryForm";
      this.Padding = new System.Windows.Forms.Padding(3);
      this.Text = "ShowQueryForm";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShowQueryForm_FormClosing);
      this.flowLayoutPanel1.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label _lb_query_text;
    private System.Windows.Forms.PropertyGrid _pg;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.Button _btn_ok;
    private System.Windows.Forms.Button _btn_cancel;
    private System.Windows.Forms.GroupBox groupBox1;
  }
}