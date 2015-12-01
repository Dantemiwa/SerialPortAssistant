using System.Windows.Forms;

namespace SerialPortAssistant
{
    class TimerAction
    {
        public static void timerAction(FrmMain fm,int type)
        {

            if (fm.radioButton1.Checked == true)
            {
                if (fm.textBox2.Text.Trim() == "")
                {
                    if (type == 1)
                    {
                        fm.timer1.Enabled = false;
                        fm.button1.Enabled = false;
                        fm.groupBox2.Enabled = true;
                        fm.button5.Text = "开始发送";
                    }
                    MessageBox.Show("如果要发送单字符串 请填写字符");
                    fm.textBox2.Focus();
                }
                else
                {
                    if (type == 1)
                    {
                        fm.button5.Text = "停止发送";
                        fm.groupBox2.Enabled = false;
                    }
                    if (fm.radioButton4.Checked == true)
                    {
                        SendCommand.action(fm.textBox2.Text.Trim(), fm);
                    }
                    else if (fm.radioButton5.Checked == true)
                    {
                        fm.serialPort1.Write(fm.textBox2.Text.Trim());
                    }
                }
            }

            else if (fm.radioButton2.Checked == true)
            {
                int num = 0;
                if (fm.textBox3.Text.Length != 0)
                {
                    if (fm.radioButton4.Checked == true)
                    {
                        SendCommand.action(fm.textBox3.Text.Trim(), fm);
                        System.Threading.Thread.Sleep(50);
                    }
                    else if (fm.radioButton5.Checked == true)
                    {
                        fm.serialPort1.Write(fm.textBox3.Text.Trim());
                        System.Threading.Thread.Sleep(50);
                    }
                }
                else
                {
                    num++;
                }
                if (fm.textBox4.Text.Length != 0)
                {
                    if (fm.radioButton4.Checked == true)
                    {
                        SendCommand.action(fm.textBox4.Text.Trim(), fm);
                        System.Threading.Thread.Sleep(50);
                    }
                    else if (fm.radioButton5.Checked == true)
                    {
                        fm.serialPort1.Write(fm.textBox4.Text.Trim());
                        System.Threading.Thread.Sleep(50);
                    }
                }
                else
                {
                    num++;
                }
                if (fm.textBox5.Text.Length != 0)
                {
                    if (fm.radioButton4.Checked == true)
                    {
                        SendCommand.action(fm.textBox5.Text.Trim(), fm);
                        System.Threading.Thread.Sleep(50);
                    }
                    else if (fm.radioButton5.Checked == true)
                    {
                        fm.serialPort1.Write(fm.textBox5.Text.Trim());
                        System.Threading.Thread.Sleep(50);
                    }
                }
                else
                {
                    num++;
                }
                if (fm.textBox6.Text.Length != 0)
                {
                    if (fm.radioButton4.Checked == true)
                    {
                        SendCommand.action(fm.textBox6.Text.Trim(), fm);
                        System.Threading.Thread.Sleep(50);
                    }
                    else if (fm.radioButton5.Checked == true)
                    {
                        fm.serialPort1.Write(fm.textBox6.Text.Trim());
                        System.Threading.Thread.Sleep(50);
                    }
                }
                else
                {
                    num++;
                }
                if (num == 4)
                {
                    if (type == 1)
                    {
                        fm.timer1.Enabled = false;
                        fm.groupBox2.Enabled = true;
                        fm.button1.Enabled = false;
                        fm.button5.Text = "开始发送";
                    }
                    MessageBox.Show("如果要多字符串发送 请把命令填写上");
                }
                else
                {
                    if (type == 1)
                    {
                        fm.button5.Text = "停止发送";
                        fm.groupBox2.Enabled = false;
                    }
                }
            }

            else if (fm.radioButton3.Checked == true)
            {
                if (fm.commmand.Count == 0)
                {
                    fm.timer1.Enabled = false;
                    MessageBox.Show("请先导入命令文件");
                    return;
                }

                if (type == 1)
                {
                    fm.button5.Text = "停止发送";
                    fm.groupBox2.Enabled = false;
                }

                if (fm.radioButton4.Checked == true)
                {
                    for (int counter = 0; counter < fm.commmand.Count; counter++)
                    {
                        SendCommand.action(fm.commmand[counter].ToString(), fm);
                        System.Threading.Thread.Sleep(50);
                    }
                }
                else if (fm.radioButton5.Checked == true)
                {
                    for (int counter = 0; counter < fm.commmand.Count; counter++)
                    {
                        fm.serialPort1.Write(fm.textBox6.Text.Trim());
                        System.Threading.Thread.Sleep(50);
                    }
                }
            }
        }
    }
}
