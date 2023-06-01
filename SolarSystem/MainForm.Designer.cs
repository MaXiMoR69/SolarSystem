

namespace SolarSystem
{
    partial class MainForm
    {
       
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            
            SuspendLayout();
           
            
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1900, 1000);
            DoubleBuffered = true;
            Name = "MainForm";
            Text = "SolarSystem";
            StartPosition = FormStartPosition.CenterScreen;
            _hScrollBar.MouseEnter += HScrollBarChange;
            _hScrollBar.Scroll += HScrollBarScroll;
            _vScrollBar.MouseEnter += VScrollBarScroll;
            startButton.Click += ClickOnButtonStart;
            stopButton.Click += ClickOnButtonStop;
            Resize += Form1_Resize;
            Load += Form1_Load;
           
         
            BackColor = Color.Black;

            ResumeLayout(false);


            RegistrationInEtcd();




        }

       
       




        #endregion


    }
}