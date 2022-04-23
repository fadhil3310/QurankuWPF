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
		/// Interaction logic for DebugWindow1.xaml
		/// </summary>
		public partial class DebugWindow1 : Window
		{
				public DebugWindow1()
				{
						InitializeComponent();
				}

				public void AddItem(int text)
				{
						Label label = new Label { Content = text, Margin = new Thickness(0) };
						Label statusLabel = new Label { Name = "statusLabel", Margin = new Thickness(0) };
						StackPanel stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
						stackPanel.Children.Add(label);
						stackPanel.Children.Add(statusLabel);
						listBox.Items.Add(stackPanel);
				}

				public void ChangeStatus(int index, string status)
				{
						StackPanel stackPanel = (StackPanel)listBox.Items[index];
						Label statusLabel = (Label)stackPanel.Children[1];
						statusLabel.Content = status;
				}
		}
}
