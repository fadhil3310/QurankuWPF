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

				public void AddItem(string text)
				{
						ListBoxItem listBoxItem = new ListBoxItem();
						listBoxItem.Content = text;
						listBox.Items.Add(listBoxItem);
						listBox.ScrollIntoView(listBoxItem);
				}
		}
}
