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
using Batoulapps.Adhan;
using Batoulapps.Adhan.Internal;

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

						if (QurankuWPF.Properties.Settings.Default.location != "")
						{
								locationNotSpecifiedPanel.Visibility = Visibility.Collapsed;
								prayerTimePanel.Visibility = Visibility.Visible;

								string[] location = QurankuWPF.Properties.Settings.Default.location.Split(" ");

								calculatePrayerTime(new Coordinates(double.Parse(location[0]), double.Parse(location[1])));
						}
				}

				private void findLocationFirstTime_Click(object sender, RoutedEventArgs e)
				{
						findLocation();
				}

				private void findLocation()
				{
						FindLocation.GetLocation((position) =>
						{
								if (position.Latitude == 0 && position.Longitude == 0)
										return;

								locationNotSpecifiedPanel.Visibility = Visibility.Collapsed;
								prayerTimePanel.Visibility = Visibility.Visible;

								locationUpdated(position);
						});
				}

				private void calculatePrayerTime(Coordinates location)
				{
						DateComponents date = DateComponents.From(DateTime.Now);
						CalculationParameters parameters = CalculationMethod.SINGAPORE.GetParameters();
						parameters.Madhab = Madhab.SHAFI;

						PrayerTimes prayerTimes = new PrayerTimes(location, date, parameters);

						TimeZoneInfo timeZone = TimeZoneInfo.Local;
						shubuhTime.Text = prayerTimes.Fajr.ToLocalTime().ToShortTimeString();
						dhuhrTime.Text = prayerTimes.Dhuhr.ToLocalTime().ToShortTimeString();
						asrTime.Text = prayerTimes.Asr.ToLocalTime().ToShortTimeString();
						maghribTime.Text = prayerTimes.Maghrib.ToLocalTime().ToShortTimeString();
						ishaTime.Text = prayerTimes.Isha.ToLocalTime().ToShortTimeString();
				}

				private void findLocationButton_Click(object sender, RoutedEventArgs e)
				{
						//prayerTimePanel.Visibility = Visibility.Collapsed;
				}
				private void locationUpdated(Coordinates location)
				{
						QurankuWPF.Properties.Settings.Default.location = location.Latitude + " " + location.Longitude;
						QurankuWPF.Properties.Settings.Default.Save();

						calculatePrayerTime(location);
				}

				private void changeLocationButton_Click(object sender, RoutedEventArgs e)
				{
						findLocation();
				}
		}
}
