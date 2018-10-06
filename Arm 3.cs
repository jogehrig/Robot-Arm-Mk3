using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports; //

using System.Diagnostics;//
using System.Threading;//

using System.Xml.Linq;//

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        /// /////////////
        List<String> PosList = new List<String>();
        List<String> PosListXML = new List<String>();
        List<double> TimeList = new List<double>();
        List<double> TimeListXML = new List<double>();
        bool isRecording = false;
        Stopwatch stopWatch = new Stopwatch();


        /// /////////////
        bool isConnected = false;
        String[] ports;
        SerialPort port;

        public Form1() //init
        {
            InitializeComponent();
            disableControls();
            getAvailableComPorts();

            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
                Console.WriteLine(port);
                if (ports[0] != null)
                {
                    comboBox1.SelectedItem = ports[0];
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) //CONNECT
        {
            if (!isConnected)
            {
                connectToArduino();
            }
            else
            {
                disconnectFromArduino();
            }
        }

 ////////////////////////////////////////////////////////////////////////////
    
        private void trackBar1_Scroll(object sender, EventArgs e)   //ACHSE 1 - a
        {
            if (isConnected)                                                
            {                                                               
                port.WriteLine(trackBar1.Value.ToString() + "a");           
                label2.Text = "A Pos: " + trackBar1.Value.ToString();  
            }
        }
        private void trackBar2_Scroll(object sender, EventArgs e)   //ACHSE 2 - b
        {
            if (isConnected)
            {
                port.WriteLine(trackBar1.Value.ToString() + "b");
                label1.Text = "B Pos: " + trackBar2.Value.ToString();
            }
        }
        private void trackBar3_Scroll(object sender, EventArgs e)   //ACHSE 3 - c
        {
            if (isConnected)
            {
                port.WriteLine(trackBar1.Value.ToString() + "c");
                label3.Text = "C Pos: " + trackBar3.Value.ToString();
            }
        }
        private void trackBar4_Scroll(object sender, EventArgs e)   //ACHSE 4 - d
        {
            if (isConnected)
            {
                port.WriteLine(trackBar1.Value.ToString() + "d");
                label4.Text = "D Pos: " + trackBar4.Value.ToString();
            }
        }
        private void trackBar5_Scroll(object sender, EventArgs e)   //ACHSE 5 - e
        {
            if (isConnected)
            {
                port.WriteLine(trackBar1.Value.ToString() + "e");
                label5.Text = "E Pos: " + trackBar5.Value.ToString();
            }
        }

/// //////////////////////////////////////////////////////////////////////////////

        private void disableControls()
        {
            trackBar1.Enabled = false;
            trackBar2.Enabled = false;
            trackBar3.Enabled = false;
            trackBar4.Enabled = false;
            trackBar5.Enabled = false;

            button2.Enabled = false;
            button3.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;

            button8.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
            button11.Enabled = false;

        }
        private void enableControls()
        {
            trackBar1.Enabled = true;
            trackBar2.Enabled = true;
            trackBar3.Enabled = true;
            trackBar4.Enabled = true;
            trackBar5.Enabled = true;

            button2.Enabled = true;
            button3.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;

            button8.Enabled = true;
            button10.Enabled = true;
            button11.Enabled = true;
        }

        void getAvailableComPorts()
        {
            ports = SerialPort.GetPortNames();
        }

        private void connectToArduino()
        {
            isConnected = true;
            string selectedPort = comboBox1.GetItemText(comboBox1.SelectedItem);
            port = new SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One);
            port.Open();
            //port.Write("test");
            button1.Text = "Disconnect";
            enableControls();

        }
        private void disconnectFromArduino()
        {
            isConnected = false;
            port.Close();
            button1.Text = "Connect";
            disableControls();

        }

        private void button7_Click(object sender, EventArgs e) //NOTAUS
        {
            port.Write("#stop");
        }

        private void button6_Click(object sender, EventArgs e) //COM
        {
            port.Write("#com");
        }

/// ///////////////////////////////////////////////////////////////

        private void button2_Click(object sender, EventArgs e) //AUTO 1
        {
        // port.Write(PosList[0]);

            int j = 0;
            int len = PosList.Count - 1;
            while(j<=len)
            {
                
                Console.WriteLine(PosList[j]);
                port.Write(PosList[j]);
                Console.WriteLine(TimeList[j]);
                int time = Convert.ToInt32(TimeList[j]);
                Thread.Sleep(time);
                j++;
             }

        }
        private void button3_Click(object sender, EventArgs e) //AUTO 2
        {
            int j = 0;
            int len = PosListXML.Count - 1;
            while (j <= len)
            {

                Console.WriteLine(PosListXML[j]);
                port.Write(PosListXML[j]);
                Console.WriteLine(TimeListXML[j]);
                int time = Convert.ToInt32(TimeListXML[j]);
                Thread.Sleep(time);
                j++;
            }
        }

        /// ///////////////////////////////////////////////////////// SPEICHERN
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e) //START
        {   if (!isRecording)
            { button8.Text = "Next";
                button9.Enabled = true;
                isRecording = true;
                PosList.Clear();

                String pos = "";
                String posa = trackBar1.Value.ToString() + "a";
                pos += posa;
                String posb = trackBar2.Value.ToString() + "b";
                pos += posb;
                String posc = trackBar3.Value.ToString() + "c";
                pos += posc;
                String posd = trackBar4.Value.ToString() + "d";
                pos += posd;
                String pose = trackBar5.Value.ToString() + "e";
                pos += pose;
                PosList.Add(pos);
                //TimeList.Add(0);
                label6.Text = "Step No:" + PosList.Count;
                label8.Text = "Koordinate:" + pos;
                stopWatch.Start();
            }
            else
            {
                String pos = "";
                String posa = trackBar1.Value.ToString() + "a";
                pos += posa;
                String posb = trackBar2.Value.ToString() + "b";
                pos += posb;
                String posc = trackBar3.Value.ToString() + "c";
                pos += posc;
                String posd = trackBar4.Value.ToString() + "d";
                pos += posd;
                String pose = trackBar5.Value.ToString() + "e";
                pos += pose;
                PosList.Add(pos);

                label6.Text = "Current Step:" + PosList.Count;


                stopWatch.Stop();
                double ts = stopWatch.Elapsed.TotalMilliseconds;

                TimeList.Add(ts);
                label7.Text = "Time since last step:" + ts;
                stopWatch.Reset();
                stopWatch.Start();
                label8.Text = "Koordinate:" + pos;

            }

        }

        private void button9_Click(object sender, EventArgs e) //STOP
        {
            button8.Text = "Record";
            isRecording = false;
            button9.Enabled = false;
            String pos = "";
            String posa = trackBar1.Value.ToString() + "a";
            pos += posa;
            String posb = trackBar2.Value.ToString() + "b";
            pos += posb;
            String posc = trackBar3.Value.ToString() + "c";
            pos += posc;
            String posd = trackBar4.Value.ToString() + "d";
            pos += posd;
            String pose = trackBar5.Value.ToString() + "e";
            pos += pose;
            PosList.Add(pos);

            label6.Text = "Step No:" + PosList.Count;


            stopWatch.Stop();
            double ts = stopWatch.Elapsed.TotalMilliseconds;

            TimeList.Add(ts);
            label7.Text = "El Time:" + ts;
            stopWatch.Reset();
            TimeList.Add(1000);
            label8.Text = "Koordinate:" + pos;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            XDocument doc = XDocument.Load("abcde.xml");
            var poss = doc.Descendants("Pos");
            foreach (var pos in poss)
            {
                Console.WriteLine(pos.Value);
                PosListXML.Add(pos.Value);
            }
            var times = doc.Descendants("Time");
            foreach (var time in times)
            {
                Console.WriteLine(time.Value);
                String temptime = time.Value;
                TimeListXML.Add(Convert.ToDouble(temptime));
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {


            XDocument d = new XDocument();
            
            XElement ueber = new XElement("Steps");

            int j = 0;
            int len = PosList.Count - 1;
            while (j <= len)
            {
                XElement schritt = new XElement("Step",

                new XElement("Pos", PosList[j]),
                new XElement("Time", TimeList[j])
                            );
                ueber.Add(schritt);

                j++;
            }
            d.Add(ueber);
            d.Declaration = new XDeclaration("1.0", "utf-8", "true");
            Console.WriteLine(d);

            d.Save("abcde.xml");
        }
    }
}
