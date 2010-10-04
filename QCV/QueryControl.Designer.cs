namespace QCV {
  partial class QueryControl {
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
      this._lb_query_text = new System.Windows.Forms.Label();
      this._pg = new System.Windows.Forms.PropertyGrid();
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this._btn_ok = new System.Windows.Forms.Button();
      this._btn_cancel = new System.Windows.Forms.Button();
      this.flowLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // _lb_query_text
      // 
      this._lb_query_text.Dock = System.Windows.Forms.DockStyle.Top;
      this._lb_query_text.Location = new System.Drawing.Point(0, 0);
      this._lb_query_text.Name = "_lb_query_text";
      this._lb_query_text.Size = new System.Drawing.Size(533, 29);
      this._lb_query_text.TabIndex = 0;
      this._lb_query_text.Text = "QueryTextCaption";
      this._lb_query_text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // _pg
      // 
      this._pg.Dock = System.Windows.Forms.DockStyle.Fill;
      this._pg.Location = new System.Drawing.Point(0, 29);
      this._pg.Name = "_pg";
      this._pg.Size = new System.Drawing.Size(533, 285);
      this._pg.TabIndex = 1;
      this._pg.ToolbarVisible = false;
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.Controls.Add(this._btn_ok);
      this.flowLayoutPanel1.Controls.Add(this._btn_cancel);
      this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 314);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Size = new System.Drawing.Size(533, 31);
      this.flowLayoutPanel1.TabIndex = 2;
      // 
      // _btn_ok
      // 
      this._btn_ok.Location = new System.Drawing.Point(3, 3);
      this._btn_ok.Name = "_btn_ok";
      this._btn_ok.Size = new System.Drawing.Size(75, 23);
      this._btn_ok.TabIndex = 0;
      this._btn_ok.Text = "Ok";
      this._btn_ok.UseVisualStyleBackColor = true;
      this._btn_ok.Click += new System.EventHandler(this._btn_ok_Click);
      // 
      // _btn_cancel
      // 
      this._btn_cancel.Location = new System.Drawing.Point(84, 3);
      this._btn_cancel.Name = "_btn_cancel";
      this._btn_cancel.Size = new System.Drawing.Size(75, 23);
      this._btn_cancel.TabIndex = 1;
      this._btn_cancel.Text = "Cancel";
      this._btn_cancel.UseVisualStyleBackColor = true;
      this._btn_cancel.Click += new System.EventHandler(this._btn_cancel_Click);
      // 
      // QueryControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this._pg);
      this.Controls.Add(this.flowLayoutPanel1);
      this.Controls.Add(this._lb_query_text);
      this.Name = "QueryControl";
      this.Size = new System.Drawing.Size(533, 345);
      this.flowLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label _lb_query_text;
    private System.Windows.Forms.PropertyGrid _pg;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.Button _btn_ok;
    private System.Windows.Forms.Button _btn_cancel;
  }
}
