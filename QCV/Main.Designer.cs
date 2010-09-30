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
      this.SuspendLayout();
      // 
      // _btn_run
      // 
      this._btn_run.Location = new System.Drawing.Point(22, 13);
      this._btn_run.Name = "_btn_run";
      this._btn_run.Size = new System.Drawing.Size(75, 50);
      this._btn_run.TabIndex = 0;
      this._btn_run.Text = "Run";
      this._btn_run.UseVisualStyleBackColor = true;
      this._btn_run.Click += new System.EventHandler(this._btn_play_Click);
      // 
      // _btn_props
      // 
      this._btn_props.Location = new System.Drawing.Point(103, 13);
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
      this._lb_status.Location = new System.Drawing.Point(22, 66);
      this._lb_status.Name = "_lb_status";
      this._lb_status.Size = new System.Drawing.Size(156, 19);
      this._lb_status.TabIndex = 2;
      this._lb_status.Text = "Press Run to Start";
      this._lb_status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(200, 97);
      this.Controls.Add(this._lb_status);
      this.Controls.Add(this._btn_props);
      this.Controls.Add(this._btn_run);
      this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "Main";
      this.Text = "QCV";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button _btn_run;
    private System.Windows.Forms.Button _btn_props;
    private System.Windows.Forms.Label _lb_status;

  }
}

