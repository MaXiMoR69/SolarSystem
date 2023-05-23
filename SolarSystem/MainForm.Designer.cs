using Microsoft.VisualBasic.ApplicationServices;

namespace SolarSystem
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            
            SuspendLayout();
           
            
            // 
            // Form1
            // 
            
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1900, 1000);
            DoubleBuffered = true;
            Name = "Form1";
            Text = "SolarSystem";
            StartPosition = FormStartPosition.CenterScreen;
            _hScrollBar.Scroll += HScrollBar_Scroll;
            startButton.Click += ClickOnButtonStart;
            Resize += Form1_Resize;
            Load += Form1_Load;
            BackColor = Color.Black;

            ResumeLayout(false);


            //SendEtcd();




        }




        #endregion


    }
}