using System;
using System.Windows.Forms;

namespace simple_good_web_server
{
	static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		static public bool flag;
		[STAThread]
		static void Main()
		{
			flag = false;
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new mainform());
		}
	}
}
