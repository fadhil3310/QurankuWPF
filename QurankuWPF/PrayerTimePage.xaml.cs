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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QurankuWPF
{
		/// <summary>
		/// Interaction logic for PrayerTimePage.xaml
		/// </summary>
		public partial class PrayerTimePage : Page
		{
				MainWindow parent;
				public PrayerTimePage(MainWindow _parent)
				{
						InitializeComponent();
						parent = _parent;
				}

				private void findLocationFirstTime_Click(object sender, RoutedEventArgs e)
				{
						FindLocationDialog findLocationDialog = new FindLocationDialog(delegate
						{
								return 0;
						});
						findLocationDialog.Top = parent.Top + (parent.Height / 2) - (findLocationDialog.Height / 2);
						findLocationDialog.Left = parent.Left + (parent.Width / 2) - (findLocationDialog.Width / 2);
						findLocationDialog.ShowDialog();
				}

				private void findLocationButton_Click(object sender, RoutedEventArgs e)
				{
						//prayerTimePanel.Visibility = Visibility.Collapsed;
				}
		}
}
