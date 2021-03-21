using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net.NetworkInformation;
using System.Net;

namespace simple_good_web_server
{
	public partial class mainform : Form
	{
		private int pORT;
		public mainform()
		{
			InitializeComponent();
		}

		//public string PORT { get => pORT; set => pORT = value; }

		delegate void SetTextCallback(string text);

		private void SetText(string text)
		{
			//如果调用控件的线程和创建创建控件的线程不是同一个则为True
			if (this.textBox1.InvokeRequired)
			{
				SetTextCallback d = new SetTextCallback(SetText);
				this.textBox1.Invoke(d, new object[] { text });
			}
			else
			{
				this.textBox1.Text = text;
			}
		}

		public static bool PortInUse(int port)
        {
            bool inUse = false;
 
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();//IP端口
 
            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    return inUse;
                }
            }
            ipEndPoints = ipProperties.GetActiveUdpListeners();//UDP端口
            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    return inUse;
                }
            }
            return inUse;
        }

		private void button1_Click(object sender, EventArgs e)
		{
			string text = textBox1.Text;
			int.TryParse(text, out pORT);
			[DllImport("sig-kernel.dll", EntryPoint = "StartListenAndServe")] static extern void StartListenAndServer(int port_number_in);
			static void RunKernel(object port_in) {
				int port_number = (int)port_in;
				StartListenAndServer(port_number);
			}
			Thread thread = new(RunKernel);
			thread.Start(pORT);
			button1.Enabled = false;
			button1.Text = "已开启";
			//if (button1.Text=="开始") {
			//	button1.Text = "暂停";
			//	thread.Start(pORT);
			//}
			//if (button1.Text == "暂停") {
			//	button1.Text = "开始";
			//	CallPause();
			//}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			System.Environment.Exit(0);
		}
	}
}