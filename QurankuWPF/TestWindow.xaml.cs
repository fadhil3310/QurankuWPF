using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QurankuWPF
{
		/// <summary>
		/// Interaction logic for TestWindow.xaml
		/// </summary>
		public partial class TestWindow : Window
		{
				public TestWindow()
				{
						InitializeComponent();

						//this.WindowState = WindowState.Maximized;
						double screenWidth = SystemParameters.PrimaryScreenWidth;
						double screenHeight = SystemParameters.PrimaryScreenHeight;
						this.Width = screenWidth;
						this.Height = screenHeight;
						this.Top = 0;
						this.Left = 0;
						//double screenWidth = grid.ActualWidth;
						//double screenHeight = grid.ActualHeight;

						//this.Width = 1366;
						//this.Height = 768;

						//this.WindowState = WindowState.Normal;
						//labelText.Content = screenWidth + " " + screenHeight;
						//this.Width = screenWidth;
						//this.Height = screenHeight;
						//this.Top = 1;
						//this.Left = 1;


						//mediaElement.Play();
				}

				private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
				{
						//this.Width = screenWidth;
						//this.Height = screenHeight;
						//labelText.Content = screenWidth + " " + screenHeight;
						labelText.Content = this.ActualWidth +  " " + this.ActualHeight;
				}
		}
}
