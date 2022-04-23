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
		/// Interaction logic for NavigationView.xaml
		/// </summary>
		public partial class NavigationView : UserControl
		{
				public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent(
						"SelectionChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NavigationView));

				List<NavigationViewItem> items = new List<NavigationViewItem>();

				Storyboard rotateAnimationStoryboard;
				TimeSpan shortDuration = TimeSpan.FromMilliseconds(250);
				TimeSpan longDuration = TimeSpan.FromMilliseconds(1000);

				public int selectedItem = 1;
				public Boolean isAnimating;
				private Boolean isSettingClicked;

				private DoubleAnimation scaleAnimation1;
				private ObjectAnimationUsingKeyFrames objectAnimation1;
				private DiscreteObjectKeyFrame visibilityAnimation1;
				private DoubleAnimation scaleAnimation2;
				private ObjectAnimationUsingKeyFrames objectAnimation2;
				private DiscreteObjectKeyFrame visibilityAnimation2;
				private Storyboard scaleAnimationStoryboard;

				public NavigationView()
				{
						InitializeComponent();

						NameScope.SetNameScope(this, new NameScope());
						RegisterName("settingsItemRotate", settingsItemRotate);
						RegisterName("settingsItemSelection", settingsItemSelection);
						RegisterName("settingsItemSelectionScale", settingsItemSelectionScale);

						DoubleAnimation rotateAnimation = new DoubleAnimation();
						rotateAnimation.EasingFunction = new CubicEase();
						Storyboard.SetTargetName(rotateAnimation, "settingsItemRotate");
						Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath(RotateTransform.AngleProperty));
						rotateAnimationStoryboard = new Storyboard();
						rotateAnimationStoryboard.Children.Add(rotateAnimation);

						settingsItem.AddHandler(Button.ClickEvent, new RoutedEventHandler(delegate
						{
								isSettingClicked = true;
								changeItemSelection(0);
						}), true);

						settingsItem.AddHandler(Button.MouseDownEvent, new RoutedEventHandler(delegate {
								rotateAnimation.From = 0;
								rotateAnimation.To = -20;
								rotateAnimation.Duration = new Duration(shortDuration);
								rotateAnimationStoryboard.Begin(this);
						}), true);

						settingsItem.AddHandler(Button.MouseUpEvent, new RoutedEventHandler(delegate {
								rotateAnimation.To = 360;
								rotateAnimation.Duration = new Duration(longDuration);
								rotateAnimationStoryboard.Begin(this);
						}), true);

						setupItemAnimation();
				}

				public void AddItem(string pathData)
				{
						NavigationViewItem navigationViewItem = new NavigationViewItem { Icon = pathData };
						items.Add(navigationViewItem);
						itemStackPanel.Children.Add(navigationViewItem);
						int itemIndex = items.Count;
						RegisterName($"itemSelection{itemIndex - 1}", navigationViewItem.itemSelection);
						RegisterName($"itemSelectionScale{itemIndex - 1}", navigationViewItem.itemSelectionScale);

						if (itemIndex == 1) navigationViewItem.itemSelection.Visibility = Visibility.Visible;

						navigationViewItem.AddHandler(Button.ClickEvent, new RoutedEventHandler(delegate {
								if(!isAnimating) changeItemSelection(itemIndex);
						}), true);
				}

				public void changeItemSelection(int index)
				{
						if (selectedItem != index)
						{
								if (index != 0)
								{
										if (index < selectedItem || selectedItem == 0)
												animateItemUp(selectedItem, index);
										else
												animateItemDown(selectedItem, index);
								}
								else
								{
										animateItemDown(selectedItem, 0);
								}

								selectedItem = index;
								RoutedEventArgs eventArgs = new RoutedEventArgs(NavigationView.SelectionChangedEvent);
								RaiseEvent(eventArgs);
						}
				}

				private void setupItemAnimation()
				{
						// Animating previous item
						//items[selectedItem].itemSelectionScale.CenterY = 0;
						scaleAnimation1 = new DoubleAnimation();
						scaleAnimation1.EasingFunction = new CubicEase();
						//scaleAnimation1.From = 1;
						//scaleAnimation1.To = 2;
						scaleAnimation1.Duration = new Duration(shortDuration);

						objectAnimation1 = new ObjectAnimationUsingKeyFrames();
						visibilityAnimation1 = new DiscreteObjectKeyFrame();
						visibilityAnimation1.KeyTime = TimeSpan.FromMilliseconds(200);
						//visibilityAnimation1.Value = Visibility.Hidden;
						objectAnimation1.KeyFrames.Add(visibilityAnimation1);

						//Storyboard.SetTargetName(scaleAnimation1, $"itemSelectionScale{selectedItem}");
						Storyboard.SetTargetProperty(scaleAnimation1, new PropertyPath(ScaleTransform.ScaleYProperty));
						//Storyboard.SetTargetName(objectAnimation1, $"itemSelection{selectedItem}");
						Storyboard.SetTargetProperty(objectAnimation1, new PropertyPath(VisibilityProperty));

						// Animating selected item
						//items[index].itemSelectionScale.CenterY = 20;
						scaleAnimation2 = new DoubleAnimation();
						scaleAnimation2.EasingFunction = new CubicEase();
						scaleAnimation2.BeginTime = TimeSpan.FromMilliseconds(200);
						//scaleAnimation2.From = 2;
						//scaleAnimation2.To = 1;
						scaleAnimation2.Duration = new Duration(shortDuration);

						objectAnimation2 = new ObjectAnimationUsingKeyFrames();
						visibilityAnimation2 = new DiscreteObjectKeyFrame();
						visibilityAnimation2.KeyTime = TimeSpan.FromMilliseconds(200);
						//visibilityAnimation2.Value = Visibility.Visible;
						objectAnimation2.KeyFrames.Add(visibilityAnimation2);

						//Storyboard.SetTargetName(scaleAnimation2, $"itemSelectionScale{index}");
						Storyboard.SetTargetProperty(scaleAnimation2, new PropertyPath(ScaleTransform.ScaleYProperty));
						//Storyboard.SetTargetName(objectAnimation2, $"itemSelection{index}");
						Storyboard.SetTargetProperty(objectAnimation2, new PropertyPath(VisibilityProperty));

						// Animate
						scaleAnimationStoryboard = new Storyboard();
						scaleAnimationStoryboard.Children.Add(scaleAnimation1);
						scaleAnimationStoryboard.Children.Add(scaleAnimation2);
						scaleAnimationStoryboard.Children.Add(objectAnimation1);
						scaleAnimationStoryboard.Children.Add(objectAnimation2);
				}

				private void animateItemUp(int previousIndex, int selectedIndex)
				{
						scaleAnimation1.From = 1;
						scaleAnimation1.To = 2;//(previousIndex - selectedIndex) * 2;
						visibilityAnimation1.Value = Visibility.Hidden;
						if (previousIndex != 0)
						{
								// Item is selected
								items[previousIndex - 1].itemSelectionScale.CenterY = 16;
								Storyboard.SetTargetName(scaleAnimation1, $"itemSelectionScale{previousIndex - 1}");
								Storyboard.SetTargetName(objectAnimation1, $"itemSelection{previousIndex - 1}");
						}
						else
						{
								// Settings is selected
								settingsItemSelectionScale.CenterY = 16;
								Storyboard.SetTargetName(scaleAnimation1, "settingsItemSelectionScale");
								Storyboard.SetTargetName(objectAnimation1, "settingsItemSelection");
						}

						scaleAnimation2.From = 2;//(previousIndex - selectedIndex) * 2;
						scaleAnimation2.To = 1;
						visibilityAnimation2.Value = Visibility.Visible;
						if (selectedIndex != 0)
						{
								// Item is selected
								items[selectedIndex - 1].itemSelectionScale.CenterY = 0;
								Storyboard.SetTargetName(scaleAnimation2, $"itemSelectionScale{selectedIndex - 1}");
								Storyboard.SetTargetName(objectAnimation2, $"itemSelection{selectedIndex - 1}");
						}
						else
						{
								// Settings is selected
								settingsItemSelectionScale.CenterY = 0;
								Storyboard.SetTargetName(scaleAnimation2, "settingsItemSelectionScale");
								Storyboard.SetTargetName(objectAnimation2, "settingsItemSelection");
						}

						scaleAnimationStoryboard.Begin(this);
				}

				private void animateItemDown(int previousIndex, int selectedIndex)
				{
						
						scaleAnimation1.From = 1;
						scaleAnimation1.To = 2;//(selectedIndex - previousIndex) * 2;
						visibilityAnimation1.Value = Visibility.Hidden;
						if (previousIndex != 0)
						{
								// Item is selected
								items[previousIndex - 1].itemSelectionScale.CenterY = 0;
								Storyboard.SetTargetName(scaleAnimation1, $"itemSelectionScale{previousIndex - 1}");
								Storyboard.SetTargetName(objectAnimation1, $"itemSelection{previousIndex - 1}");
						}
						else
						{
								// Settings is selected
								settingsItemSelectionScale.CenterY = 0;
								Storyboard.SetTargetName(scaleAnimation1, "settingsItemSelectionScale");
								Storyboard.SetTargetName(objectAnimation1, "settingsItemSelection");
						}

						scaleAnimation2.From = 2;//(selectedIndex - previousIndex) * 2;
						scaleAnimation2.To = 1;
						visibilityAnimation2.Value = Visibility.Visible;
						if (selectedIndex != 0)
						{
								// Item is selected
								items[selectedIndex - 1].itemSelectionScale.CenterY = 16;
								Storyboard.SetTargetName(scaleAnimation2, $"itemSelectionScale{selectedIndex - 1}");
								Storyboard.SetTargetName(objectAnimation2, $"itemSelection{selectedIndex - 1}");
						}
						else
						{
								// Settings is selected
								settingsItemSelectionScale.CenterY = 16;
								Storyboard.SetTargetName(scaleAnimation2, "settingsItemSelectionScale");
								Storyboard.SetTargetName(objectAnimation2, "settingsItemSelection");
						}

						scaleAnimationStoryboard.Begin(this);
				}

				public event RoutedEventHandler SelectionChanged
				{
						add { AddHandler(SelectionChangedEvent, value); }
						remove { RemoveHandler(SelectionChangedEvent, value); }
				}
		}
		public class NavigationViewSelection
		{
				public int selectedItem;

				public NavigationViewSelection(int _selectedItem)
				{
						selectedItem = _selectedItem;
				}
		}
}
