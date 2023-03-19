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
using Batoulapps.Adhan;
using Windows.System;

namespace QurankuWPF
{
		/// <summary>
		/// Interaction logic for FindLocationDialog.xaml
		/// </summary>
		public partial class FindLocationDialog : Window
		{
				private Coordinates coordinates = new Coordinates(0, 0);
				private Action<Coordinates> callback; 

				public enum DialogAction
				{
						NO_PERMISSION,
						MANUAL
				};

				public FindLocationDialog(DialogAction action, Action<Coordinates> _callback)
				{
						InitializeComponent();

						callback = _callback;

						switch (action)
						{
								case DialogAction.NO_PERMISSION:
										Width = 600; Height = 460;
										findLocationAutoDeniedGrid.Visibility = Visibility.Visible;
										break;
						}
				}

				private void openSettingsButton_Click(object sender, RoutedEventArgs e)
				{
						Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-location"));
				}

				private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
				{
						callback(coordinates);
				}
		}

		/* public class QurankuGeoposition { 
				public double latitude { get; set; }
				public double longitude { get; set; }

				public QurankuGeoposition(double _latitude, double _longitude)
				{
						latitude = _latitude;
						longitude = _longitude;
				}
		} */
}
