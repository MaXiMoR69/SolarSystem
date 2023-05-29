using NLog;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Drawing;

namespace SolarSystem
{

    public partial class MainForm : Form
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public delegate void MyDelegate();

        HScrollBar _hScrollBar = new HScrollBar();
        Button startButton = new Button();
        Button stopButton = new Button();
        VScrollBar _vScrollBar = new VScrollBar();

        Label sun = new Label();
        Label mercury = new Label();
        Label venus = new Label();
        Label earth = new Label();
        Label mars = new Label();
        Label jupiter = new Label();
        Label saturn = new Label();
        Label uran = new Label();
        Label neptun = new Label();

        Label info = new Label();


        Label moon = new Label();

        SpaceBody Sun = new SpaceBody();
        SpaceBody Mercury = new SpaceBody();
        SpaceBody Venus = new SpaceBody();
        SpaceBody Earth = new SpaceBody();
        SpaceBody Mars = new SpaceBody();
        SpaceBody Jupiter = new SpaceBody();
        SpaceBody Saturn = new SpaceBody();
        SpaceBody Uran = new SpaceBody();
        SpaceBody Neptun = new SpaceBody();

        SpaceBody Moon = new SpaceBody();






        public MainForm()
        {
            InitializeComponent();


            _hScrollBar.Size = new Size(1000, 50);
            _hScrollBar.Location = new Point(Width / 4, Height - 100);
            _hScrollBar.Maximum = 100000;
            _hScrollBar.Minimum = 0;
            Controls.Add(_hScrollBar);

            _vScrollBar.Size = new Size(50, 500);
            _vScrollBar.Location = new Point(Width + 20 - Width, 200);
            _vScrollBar.Maximum = 10000;
            _vScrollBar.Minimum = 0;
            Controls.Add(_vScrollBar);

            startButton.Size = new Size(100, 50);
            startButton.Location = new Point(20, 20);
            startButton.Text = "Start";
            startButton.BackColor = Color.Green;
            startButton.Font = new Font("Arial", 10, FontStyle.Bold);
            Controls.Add(startButton);


            stopButton.Size = new Size(100, 50);
            stopButton.Location = new Point(20, 20);
            stopButton.Text = "Stop";
            stopButton.BackColor = Color.Red;
            stopButton.Font = new Font("Arial", 10, FontStyle.Bold);
            Controls.Add(stopButton);

            info.Size = new Size(200, 50);
            info.Location = new Point(200, 20);
            info.BackColor = Color.White;
            info.Text = "Body name";
            info.TextAlign = ContentAlignment.MiddleCenter;
            info.Font = new Font("Arial", 20, FontStyle.Bold);
            Controls.Add(info);




        }

        public class SpaceBody
        {
            public string? BodyName;
            public double BodyLocation_x;
            public double BodyLocation_y;
            public double BodySize_Width;
            public double BodySize_Height;
        }
        public class EtcdConfig
        {
            public string EtcdUrl { get; set; }
            public string EtcdKey { get; set; }

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            logger.Debug("Program Start");
        }

        void Form1_Resize(object sender, EventArgs e)
        {
            _vScrollBar.Location = new Point(Width + 20 - Width, Height + 200 - Height);
            _hScrollBar.Location = new Point(Width / 4, Height - 100);
        }

        void CreateBody(SpaceBody Body, Label body, string name, double x, double y, double a_rad, double speed, double rad, double width, double height, Color color)
        {

            body.Click += BodyClick;

            void BodyClick(object sender, EventArgs e)
            {
                info.Text = name;
                logger.Info(name);
            }

            double x_loc, y_loc;
            double a = 0;
            x_loc = x;
            y_loc = y;

            for (int i = 0; i < _hScrollBar.Value; i++)
            {
                x_loc += Math.Cos(a) * speed * rad;
                y_loc += Math.Sin(a) * speed * rad;
                a = a + a_rad * speed;
            }


            Body.BodyName = name;
            Body.BodyLocation_x = x_loc;
            Body.BodyLocation_y = y_loc;
            Body.BodySize_Width = width;
            Body.BodySize_Height = height;


            Controls.Add(body);
            body.BackColor = color;
            body.Text = name;

            body.Size = new Size(Width, Height);
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse((int)Body.BodyLocation_x, (int)Body.BodyLocation_y, (int)Body.BodySize_Width, (int)Body.BodySize_Height);
            Region rgn = new Region(path);
            body.Region = rgn;




        }


        void HScrollBar_Scroll(object sender, ScrollEventArgs e)
        {


            CreateSolarSystem();

        }

        void CreateSolarSystem()
        {
            CreateBody(Sun, sun, "Sun", (Width - 200) / 2, 900 / 2, 1, 0, 1, 40, 40, Color.Yellow);
            CreateBody(Mercury, mercury, "Mercury", (Width - 160) / 2, 430, 0.025, 1, 0.9, 8, 8, Color.Gray);
            CreateBody(Venus, venus, "Venus", (Width - 160) / 2, 400, 0.025, 1 / 2.6, 1.5, 15, 15, Color.Pink);
            CreateBody(Earth, earth, "Earth", (Width - 160) / 2, 360, 0.025, 1 / 3.2, 2.5, 20, 20, Color.Blue);
            CreateBody(Mars, mars, "Mars", (Width - 160) / 2, 320, 0.025, 0.25, 4, 10, 10, Color.Red);
            CreateBody(Jupiter, jupiter, "Jupiter", (Width - 160) / 2, 240, 0.025, 1 / 7.2, 5.5, 60, 60, Color.Orange);
            CreateBody(Saturn, saturn, "Saturn", (Width - 160) / 2, 180, 0.025, 1 / 9.6, 7, 50, 50, Color.Bisque);
            CreateBody(Uran, uran, "Uran", (Width - 160) / 2, 130, 0.025, 1 / 13.4, 8.5, 25, 25, Color.Aqua);
            CreateBody(Neptun, neptun, "Neptun", (Width - 160) / 2, 100, 0.025, 1 / 17.4, 9.5, 20, 20, Color.DeepSkyBlue);

            double speed = 1;
            double rad = 1.9;
            double a_rad = 0.1;

            double x_loc, y_loc;
            double a = 0;
            x_loc = 0;
            y_loc = 0;

            for (int i = 0; i < _hScrollBar.Value; i++)
            {
                x_loc += Math.Cos(a) * speed * rad;
                y_loc += Math.Sin(a) * speed * rad;
                a = a + a_rad * speed;
            }

            CreateBody(Moon, moon, "Earth", (Width - 150) / 2 + x_loc, 350 + y_loc, 0.025, 1 / 3.2, 2.5, 7, 7, Color.Gray);

            Refresh();
        }



        void ClickOnButtonStart(object sender, EventArgs e)
        {
            startButton.Visible = false;
            stopButton.Visible = true;
            logger.Debug("Click Start");



            Task.Run(() =>
            {
                int i = 0;
                stopButton.Click += ClickOnButtonStop;
                void ClickOnButtonStop(object sender, EventArgs e)
                {
                    logger.Debug("Click Stop");
                    stopButton.Visible = false;
                    startButton.Visible = true;
                    i = 10000;
                }
                while (i < 10000)
                {
                    i++;
                    Thread.Sleep(1);
                    Invoke((MethodInvoker)delegate
                    {
                        _hScrollBar.Value = _hScrollBar.Value + 1 + _vScrollBar.Value / 100;
                        CreateSolarSystem();
                        if (_hScrollBar.Value > 99900)
                        {
                            i = 10000;

                            _hScrollBar.Value = 0;
                            startButton.Visible = true;
                            stopButton.Visible = false;
                        }

                    });
                }
            });
        }




        async void SendEtcd()
        {
            var config = new ConfigurationBuilder().AddJsonFile("EtcdConfig.json").Build();
            var section = config.GetSection("EtcdConfig");
            var etcdConfig = section.Get<EtcdConfig>();

            var date = DateTime.Now.ToString();

            var values = new Dictionary<string, string>
            {
                    {"value","{\r\n    \"name\": \"Solar System on C#\",\r\n    \"ip\": \"195.245.244.64\",\r\n    \"online\": false,\r\n    \"reason\": 'closed',\r\n    \"lastUpdate\": \"2023-05-28T19:19:23.139Z\",\r\n    \"configsPath\": \"C:/home/kostar/documents/myconfig.json\"\r\n}" },
                    
            };
            var content = new FormUrlEncodedContent(values);

            using var client = new HttpClient();
            try
            {
                using var response = await client.PostAsync(etcdConfig.EtcdUrl + "/v2/keys/services/", content);
                string responseText = await response.Content.ReadAsStringAsync();
                logger.Info("����� � Etcd ����������");
                logger.Debug(responseText);
            }
            catch
            {

                logger.Error("����� � Etcd �� �����������");

            }





        }

        async void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {



        }






    }


}


