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

namespace QurankuWPF
{
		/// <summary>
		/// Interaction logic for NavigationViewItem.xaml
		/// </summary>
		public partial class NavigationViewItem : UserControl
		{
				private String _icon;
				public String Icon {
						get => _icon;
						set
						{
								_icon = value;
								itemIcon.Data = Geometry.Parse(value);
						}
				}

				TimeSpan animationDuration = TimeSpan.FromMilliseconds(250);

				public NavigationViewItem()
				{
						InitializeComponent();
				}

				public void animateIn()
				{
				}
		}
}
