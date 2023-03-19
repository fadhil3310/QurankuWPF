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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.ApplicationModel.Core;
using Windows.Devices.Geolocation;


namespace QurankuWPF
{
		/// <summary>
		/// Interaction logic for MainWindow.xaml
		/// </summary>
		public partial class MainWindow : Window
		{
				DebugWindow debugWindow;

				QuranPage quranPage;
				PrayerTimePage? prayerTimePage = null;
				SettingsPage? settingsPage = null;

				const string bookPath = "M6 5C6 4.44772 6.44772 4 7 4H13C13.5523 4 14 4.44772 14 5V6C14 6.55228 13.5523 7 13 7H7C6.44772 7 6 6.55228 6 6V5ZM7 5V6H13V5H7ZM4 4V16C4 17.1046 4.89543 18 6 18H15.5C15.7761 18 16 17.7761 16 17.5C16 17.2239 15.7761 17 15.5 17H6C5.44772 17 5 16.5523 5 16H15C15.5523 16 16 15.5523 16 15V4C16 2.89543 15.1046 2 14 2H6C4.89543 2 4 2.89543 4 4ZM14 3C14.5523 3 15 3.44772 15 4V15H5V4C5 3.44772 5.44772 3 6 3H14Z";
				const string clockPath = "M10 2C14.4183 2 18 5.58172 18 10C18 14.4183 14.4183 18 10 18C5.58172 18 2 14.4183 2 10C2 5.58172 5.58172 2 10 2ZM10 3C6.13401 3 3 6.13401 3 10C3 13.866 6.13401 17 10 17C13.866 17 17 13.866 17 10C17 6.13401 13.866 3 10 3ZM9.5 5C9.74546 5 9.94961 5.17688 9.99194 5.41012L10 5.5V10H12.5C12.7761 10 13 10.2239 13 10.5C13 10.7455 12.8231 10.9496 12.5899 10.9919L12.5 11H9.5C9.25454 11 9.05039 10.8231 9.00806 10.5899L9 10.5V5.5C9 5.22386 9.22386 5 9.5 5Z";

				public MainWindow()
				{
						InitializeComponent();

						// new Baru().Show();

						navigationView.AddItem(bookPath);
						navigationView.AddItem(clockPath);

						quranPage = new QuranPage();
						frame.Navigate(quranPage);


				}

				private void Button_Click(object sender, RoutedEventArgs e)
				{
						//TestWindow testWindow = new TestWindow();
						//testWindow.Show();
				}

				private void Button_Click_1(object sender, RoutedEventArgs e)
				{
						//labelContent = "abc " + items++;
						//recyclerView.AddItem();
				}

				/*private class TestItem : IRecyclerListAdapter
				{
						public StackPanel GetItem(int index)
						{
								StackPanel itemRoot = new StackPanel();
								return itemRoot;
						}

						public int GetItemCount() => 10;
				}*/

				private void navigationView_SelectionChanged(object sender, RoutedEventArgs e)
				{
						NavigationView selection = (NavigationView)e.Source;

						switch (selection.selectedItem)
						{
								case 0:
										if (settingsPage == null) settingsPage = new SettingsPage();
										frame.Navigate(settingsPage);
										break;
								case 1:
										frame.Navigate(quranPage);
										break;
								case 2:
										if (prayerTimePage == null) prayerTimePage = new PrayerTimePage(this);
										frame.Navigate(prayerTimePage);
										break;
						}
				}

				private void Window_Loaded(object sender, RoutedEventArgs e)
				{
						
				}
		}
}
