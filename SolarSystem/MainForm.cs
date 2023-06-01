using NLog;
using Microsoft.Extensions.Configuration;
using EtcdNet;

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
            CreateSolarSystem();
            FormClosed += UnregistrationInEtcd;

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


        void Form1_Resize(object sender, EventArgs e)
        {
            _vScrollBar.Location = new Point(Width + 20 - Width, Height + 200 - Height);
            _hScrollBar.Location = new Point(Width / 4, Height - 100);
        }

        void CreateBody(SpaceBody Body, Label body, string name, double x, double y, double a_rad, double speed, double rad, double width, double height, Color color)
        {
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


            body.TextAlign = ContentAlignment.MiddleCenter;
            body.BackColor = color;
            body.Text = name;
            Controls.Add(body);

            body.Size = new Size(Width, Height);
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse((int)Body.BodyLocation_x, (int)Body.BodyLocation_y, (int)Body.BodySize_Width, (int)Body.BodySize_Height);
            Region rgn = new Region(path);
            body.Region = rgn;



            body.Click += BodyClick;
            void BodyClick(object sender, EventArgs e)
            {
                info.Text = name;
                logger.Debug("Click on " + name);
            }

        }


        void HScrollBarScroll(object sender, EventArgs e)
        {
            CreateSolarSystem();
            logger.Debug("Changing value of horisontal scroll bar (solar rotation)");
        }

        void VScrollBarScroll(object sender, EventArgs e)
        {
            logger.Debug("Changing value of verticall scroll bar (solar speed)");
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

            CreateBody(Moon, moon, "Moon", (Width - 150) / 2 + x_loc, 350 + y_loc, 0.025, 1 / 3.2, 2.5, 7, 7, Color.Gray);

            Refresh();
        }

        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();

        void TimerEvent(object sender, EventArgs e)
        {
            CreateSolarSystem();
            if (_hScrollBar.Value > 90000) _hScrollBar.Value = 0;
            if (_hScrollBar.Value < 90000) _hScrollBar.Value = _hScrollBar.Value + 1 + _vScrollBar.Value / 100;
            if (_hScrollBar.Value > 99900)
            {
                _hScrollBar.Value = 0;
                startButton.Visible = true;
                stopButton.Visible = false;
            }
        }

        void ClickOnButtonStart(object sender, EventArgs e)
        {
            logger.Debug("Click Start");
            myTimer.Tick += new EventHandler(TimerEvent);
            myTimer.Interval = 5;
            myTimer.Start();
            stopButton.Visible = true;
            startButton.Visible = false;
        }

        void ClickOnButtonStop(object sender, EventArgs e)
        {
            logger.Debug("Click Stop");
            stopButton.Visible = false;
            startButton.Visible = true;
            myTimer.Stop();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            logger.Debug("Program was started");
        }


        async void RegistrationInEtcd()
        {
            var config = new ConfigurationBuilder().AddJsonFile("EtcdConfig.json").Build();
            var section = config.GetSection("EtcdConfig");
            var etcdConfig = section.Get<EtcdConfig>();
            var date = DateTime.Now.ToString();

            EtcdClientOpitions options = new EtcdClientOpitions()
            {
                Urls = new string[] { etcdConfig.EtcdUrl },
            };
            EtcdClient etcdClient = new EtcdClient(options);

            try
            {
                var response = await etcdClient.SetNodeAsync("/services/" + etcdConfig.EtcdKey, "{\r\n    \"name\": \"Solar System on C#\",\r\n    \"ip\": \"195.245.244.64\",\r\n    \"online\": true,\r\n    \"reason\": \"open\",\r\n    \"lastUpdate\": \"" + date + "\",\r\n    \"configsPath\": \"C:/Users/maksg/source/repos/SolarSystem/SolarSystem/EtcdConfig.json\"\r\n}");
                var responseText = await etcdClient.GetNodeValueAsync("/services/" + etcdConfig.EtcdKey);
                logger.Info("Communication with Etcd established");
                logger.Debug("/services/" + etcdConfig.EtcdKey + ":" + responseText);
            }
            catch
            {
                logger.Error("Failed to communicate with Etcd");
            }
        }



        void UnregistrationInEtcd(object sender, FormClosedEventArgs e)
        {

            Task.Run(() =>
            {
                var config = new ConfigurationBuilder().AddJsonFile("EtcdConfig.json").Build();
                var section = config.GetSection("EtcdConfig");
                var etcdConfig = section.Get<EtcdConfig>();
                var date = DateTime.Now.ToString();

                EtcdClientOpitions options = new EtcdClientOpitions()
                {
                    Urls = new string[] { etcdConfig.EtcdUrl },
                };
                EtcdClient etcdClient = new EtcdClient(options);

                try
                {
                    var response = etcdClient.SetNodeAsync("/services/" + etcdConfig.EtcdKey, "{\r\n    \"name\": \"Solar System on C#\",\r\n    \"ip\": \"195.245.244.64\",\r\n    \"online\": false,\r\n    \"reason\": \"closed\",\r\n    \"lastUpdate\": \"" + date + "\",\r\n    \"configsPath\": \"C:/Users/maksg/source/repos/SolarSystem/SolarSystem/EtcdConfig.json\"\r\n}");
                    logger.Info("Communication with Etcd established");
                }
                catch
                {
                    logger.Error("Failed to communicate with Etcd");
                }
                logger.Debug("Program was terminated" + "\r\n ----------------------------------------------------------------------------");
            });

        }



    }
}


