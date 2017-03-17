using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Circuit.GUI
{
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application
	{
		private bool isDebug = false;

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			if (isDebug)
			{
				DebugWindow window = new DebugWindow();
				window.Show();
			}
			else
			{
				MainWindow window = new MainWindow();
				window.Show();
			}
		}
	}
}
