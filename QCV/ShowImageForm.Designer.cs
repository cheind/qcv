namespace QCV {
  partial class ShowImageForm {
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
      this._imgbox = new Emgu.CV.UI.ImageBox();
      ((System.ComponentModel.ISupportInitialize)(this._imgbox)).BeginInit();
      this.SuspendLayout();
      // 
      // _imgbox
      // 
      this._imgbox.Dock = System.Windows.Forms.DockStyle.Fill;
      this._imgbox.Location = new System.Drawing.Point(0, 0);
      this._imgbox.Name = "_imgbox";
      this._imgbox.Size = new System.Drawing.Size(284, 262);
      this._imgbox.TabIndex = 2;
      this._imgbox.TabStop = false;
      // 
      // ShowImageForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(284, 262);
      this.Controls.Add(this._imgbox);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.Name = "ShowImageForm";
      this.Text = "ShowImageForm";
      ((System.ComponentModel.ISupportInitialize)(this._imgbox)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private Emgu.CV.UI.ImageBox _imgbox;
  }
}