﻿namespace ClothWin
{
    sealed partial class ClothForm
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
            this.SuspendLayout();
            // 
            // ClothForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 428);
            this.Name = "ClothForm";
            this.Text = "Cloth";
            this.Load += new System.EventHandler(this.ClothForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ClothForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ClothForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ClothForm_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

