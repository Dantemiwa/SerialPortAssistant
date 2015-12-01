using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Drawing;
using System.IO;
using System.Text;
using System.Collections;

namespace SerialPortAssistant
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //如果有配置 则读取配置文件
            if (File.Exists(Application.StartupPath + @"\SerConfig.wjs"))
            {
                using (StreamReader sr = new StreamReader(Application.StartupPath + @"\SerConfig.wjs", Encoding.UTF8))
                {
                    comboBox1.SelectedItem = sr.ReadLine();
                    comboBox2.SelectedItem = sr.ReadLine();
                    comboBox3.SelectedItem = sr.ReadLine();
                    comboBox4.SelectedItem = sr.ReadLine();
                    comboBox5.SelectedItem = sr.ReadLine();
                }
            }
            else
            {
                comboBox1.SelectedIndex = 2;
                comboBox2.SelectedIndex = 6;
                comboBox3.SelectedIndex = 0;
                comboBox4.SelectedIndex = 0;
                comboBox5.SelectedIndex = 0;
            }
            FrmMain.CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "打开串口")
            {
                //打开串口
                try
                {
                    serialPort1.PortName = comboBox1.SelectedItem.ToString();
                    serialPort1.BaudRate = Convert.ToInt32(comboBox2.SelectedItem.ToString());
                    switch (comboBox3.SelectedIndex)
                    {
                        case 0:
                            serialPort1.Parity = Parity.None;
                            break;
                        case 1:
                            serialPort1.Parity = Parity.Odd;
                            break;
                        case 2:
                            serialPort1.Parity = Parity.Even;
                            break;
                        case 3:
                            serialPort1.Parity = Parity.Mark;
                            break;
                        case 4:
                            serialPort1.Parity = Parity.Space;
                            break;
                    }

                    serialPort1.DataBits = Convert.ToInt32(comboBox4.SelectedItem.ToString());

                    switch (comboBox5.SelectedIndex)
                    {
                        case 0:
                            serialPort1.StopBits = StopBits.One;
                            break;
                        case 1:
                            serialPort1.StopBits = StopBits.Two;
                            break;
                        case 2:
                            serialPort1.StopBits = StopBits.OnePointFive;
                            break;
                    }
                    serialPort1.Open();
                    panel1.BackColor = Color.LawnGreen;
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                    comboBox3.Enabled = false;
                    comboBox4.Enabled = false;
                    comboBox5.Enabled = false;
                    tssl01.Text = "串口" + comboBox1.SelectedItem.ToString() + "已经打开";
                    button1.Text = "关闭串口";
                }
                catch (Exception)
                {
                    MessageBox.Show("串口打开失败" + "\n" + "1.请检查配置的参数是否正确" + "\n" + "2.外围设备是否没有连接" + "\n" + "3.串口是否已经打开或被占用" + "\n" + "4.串口驱动是否没有安装");
                }
            }
            else
            {
                serialPort1.Close();
                panel1.BackColor = Color.Red;
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
                comboBox5.Enabled = true;
                tssl01.Text = "串口" + comboBox1.SelectedItem.ToString() + "已经关闭";
                button1.Text = "打开串口";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.DiscardInBuffer();
                textBox1.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.DiscardOutBuffer();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                using (StreamWriter sw = new StreamWriter(Application.StartupPath + @"\SerConfig.wjs", false, Encoding.UTF8))
                {
                    sw.WriteLine(comboBox1.SelectedItem.ToString());
                    sw.WriteLine(comboBox2.SelectedItem.ToString());
                    sw.WriteLine(comboBox3.SelectedItem.ToString());
                    sw.WriteLine(comboBox4.SelectedItem.ToString());
                    sw.WriteLine(comboBox5.SelectedItem.ToString());
                    sw.Flush();
                }
                MessageBox.Show("配置已成功保存 下次程序启动后会自动读取配置");
            }
            else
            {
                MessageBox.Show("只有成功打开了串口 才能保存配置");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            serialPort1.Close();
            this.Close();
        }

        public ArrayList commmand = new ArrayList();
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "存储有命令的文本文件|*.txt";
            openFile.ShowDialog();

            if (openFile.FileName.Length > 0)
            {
                using (StreamReader sr = new StreamReader(openFile.FileName, Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        commmand.Add(sr.ReadLine());
                    }
                }
            }
        }

        public int type = 0;
        private void button5_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                if (button5.Text == "开始发送")
                {
                    if (checkBox1.Checked == true)
                    {
                        type = 1;
                        timer1.Interval = Convert.ToInt32(numericUpDown1.Value) * 1000;
                        timer1.Enabled = true;
                        button1.Enabled = false;
                    }
                    else
                    {
                        TimerAction.timerAction(this,type);
                    }            
                }
                else if (button5.Text == "停止发送")
                {
                    timer1.Enabled = false;
                    button1.Enabled = true;
                    textBox1.Text = "";
                    groupBox2.Enabled = true;
                    button5.Text = "开始发送";
                }
            }
            else
            {
                timer1.Enabled = false;
                groupBox2.Enabled = true;
                textBox1.Text = "";
                button5.Text = "开始发送";
                MessageBox.Show("请先打开串口 再发送数据");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimerAction.timerAction(this,type);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                numericUpDown1.Enabled = true;
            }
            else
            {
                numericUpDown1.Enabled = false;
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                string text = string.Empty;
                byte[] result = new byte[serialPort1.BytesToRead];

                serialPort1.Read(result, 0, serialPort1.BytesToRead);
                text = Convert.ToString(System.Text.Encoding.ASCII.GetString(result));
                //foreach (byte b in result)
                //{
                  //  text = text + Convert.ToString(b, 16);
                //}
                textBox1.Text += text + " ";
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
