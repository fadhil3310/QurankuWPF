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
using static System.Net.Mime.MediaTypeNames;

namespace QurankuWPF
{
		/// <summary>
		/// Interaction logic for DebugWindow.xaml
		/// </summary>
		public partial class DebugWindow : Window
		{
				public DebugWindow()
				{
						InitializeComponent();
						Width = 600;
						Height = 400;
						Top = SystemParameters.WorkArea.Bottom - Height;
						Left = SystemParameters.WorkArea.Right - Width;
				}

				public void Log(string message)
				{
						ListBoxItem listBoxItem = new ListBoxItem();
						listBoxItem.Content = message;
						listBox.Items.Add(listBoxItem);
						listBox.ScrollIntoView(listBoxItem);
				}

				public void Log(int message)
				{
						ListBoxItem listBoxItem = new ListBoxItem();
						listBoxItem.Content = message.ToString();
						listBox.Items.Add(listBoxItem);
						listBox.ScrollIntoView(listBoxItem);
				}

				public void Log(Boolean message)
				{
						ListBoxItem listBoxItem = new ListBoxItem();
						listBoxItem.Content = message.ToString();
						listBox.Items.Add(listBoxItem);
						listBox.ScrollIntoView(listBoxItem);
				}
		}
}
