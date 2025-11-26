
namespace DllFinance
{
    partial class ChildForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PanelControl = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.PanelControl)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelControl
            // 
            this.PanelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelControl.Location = new System.Drawing.Point(0, 0);
            this.PanelControl.Name = "PanelControl";
            this.PanelControl.Size = new System.Drawing.Size(898, 487);
            this.PanelControl.TabIndex = 0;
            // 
            // ChildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 487);
            this.Controls.Add(this.PanelControl);
            this.Name = "ChildForm";
            this.Text = "ChildForm";
            ((System.ComponentModel.ISupportInitialize)(this.PanelControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl PanelControl;
    }
}