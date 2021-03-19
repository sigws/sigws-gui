using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

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

		private void button1_Click(object sender, EventArgs e)
		{
			button1.Text = "已开启";
			button1.Enabled = false;
			string text = textBox1.Text;
			int.TryParse(text, out pORT);
			[DllImport("sig-kernel.dll", EntryPoint = "StartListenAndServe")] static extern void StartListenAndServer(int port_number_in);
			static void RunKernel(object port_in) {
				int port_number = (int)port_in;
				StartListenAndServer(port_number);
			}
			Thread thread = new(RunKernel);
			//if (button1.Text=="开始") {
			//	button1.Text = "暂停";
			//	thread.Start(pORT);
			//}
			//if (button1.Text == "暂停") {
			//	button1.Text = "开始";
			//	CallPause();
			//}
			thread.Start(pORT);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			System.Environment.Exit(0);
		}
	}
}