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
using Windows.Devices.Geolocation;
using Windows.System;

namespace QurankuWPF
{
		/// <summary>
		/// Interaction logic for FindLocationDialog.xaml
		/// </summary>
		public partial class FindLocationDialog : Window
		{
				private Func<QGeoposition, int> callback; 

				public FindLocationDialog(Func<QGeoposition, int> _callback)
				{
						InitializeComponent();

						callback = _callback;

						if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 1024, 0))
						{
								findLocationAuto();
						}
				}

				public async void findLocationAuto()
				{
						var accessStatus = await Geolocator.RequestAccessAsync();
						switch (accessStatus)
						{
								case GeolocationAccessStatus.Allowed:
										findLocationAutoGrid.Visibility = Visibility.Visible;
										Geolocator geolocator = new Geolocator();
										Geoposition geoposition = await geolocator.GetGeopositionAsync();
										callback(new QGeoposition(geoposition.Coordinate.Point.Position.Latitude, geoposition.Coordinate.Point.Position.Longitude));
										Close();
										break;
								case GeolocationAccessStatus.Denied:
										Width = 600; Height = 460;
										findLocationAutoDeniedGrid.Visibility = Visibility.Visible;
										break;
								case GeolocationAccessStatus.Unspecified:
										
										break;
						}

				}

				private void openSettingsButton_Click(object sender, RoutedEventArgs e)
				{
						Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-location"));
				}
		}

		public class QGeoposition { 
				public double latitude { get; set; }
				public double longitude { get; set; }

				public QGeoposition(double _latitude, double _longitude)
				{
						latitude = _latitude;
						longitude = _longitude;
				}
		}
}
