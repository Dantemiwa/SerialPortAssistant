using System;
using System.Windows.Forms;

namespace SerialPortAssistant
{
    class SendCommand
    {
        /// <summary>
        /// 执行指定的命令
        /// </summary>
        /// <param name="command">指定的命令</param>
        /// <param name="f1">发送命令的窗体</param>
        public static void action(string command, FrmMain fm)
        {
            try
            {
                string open = command;
                char[] send=new char[open.Length];

                for (int i = 0; i < send.Length; i++)
                {
                     send[i] =open[i];
                }

                fm.serialPort1.Write(send, 0, send.Length);
            }
            catch (Exception err)
            {
                fm.timer1.Enabled = false;
                fm.groupBox2.Enabled = true;
                fm.button1.Enabled = false;
                fm.button5.Text = "开始发送";
                MessageBox.Show(err.Message);
            }
        }
    }
}
