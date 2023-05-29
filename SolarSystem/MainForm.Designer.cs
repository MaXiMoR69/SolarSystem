
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
            _hScrollBar.Scroll += HScrollBar_Scroll;
            startButton.Click += ClickOnButtonStart;
            Resize += Form1_Resize;
            Load += Form1_Load;
            FormClosed += MainFormClosed;
           

            BackColor = Color.Black;

            ResumeLayout(false);


            SendEtcd();




        }

       





        #endregion


    }
}