using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.System;
using Batoulapps.Adhan;
using System.Windows;

namespace QurankuWPF
{
		internal static class FindLocation
		{
				public static void GetLocation(Action<Coordinates> callback)
				{

						if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 1024, 0))
						{
								findLocationAuto();
						}
						else
						{
								new FindLocationDialog(FindLocationDialog.DialogAction.MANUAL, callback).ShowDialog();
						}

						async void findLocationAuto()
						{
								var accessStatus = await Geolocator.RequestAccessAsync();
								switch (accessStatus)
								{
										case GeolocationAccessStatus.Allowed:
												//findLocationAutoGrid.Visibility = Visibility.Visible;
												Geolocator geolocator = new Geolocator();
												Geoposition geoposition = await geolocator.GetGeopositionAsync();
												callback(new Coordinates(geoposition.Coordinate.Point.Position.Latitude, geoposition.Coordinate.Point.Position.Longitude));
												break;
										case GeolocationAccessStatus.Denied:
												new FindLocationDialog(FindLocationDialog.DialogAction.NO_PERMISSION, callback).ShowDialog();
												break;
										case GeolocationAccessStatus.Unspecified:
												MessageBox.Show("Error occured");
												callback(new Coordinates(0, 0));
												break;
								}
						}
				}
		}
}
