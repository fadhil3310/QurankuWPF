using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
		/// Control to implement RecyclerView-like in WPF, still imperfect but usable
		/// </summary>
		
		public partial class ModernListScrollView : Panel
		{
				private List<Rect> ChildrenRect = new List<Rect>();

				// Measure item size
				protected override Size MeasureOverride(Size availableSize)
				{
						Size desiredSize = new Size();

						for (int i = 0; i < InternalChildren.Count; i++)
						{
								UIElement child = InternalChildren[i];
								child.Measure(availableSize);

								desiredSize.Width = child.DesiredSize.Width;
								desiredSize.Height += child.DesiredSize.Height;
								ChildrenRect.Insert(i, new Rect(0, 0, child.DesiredSize.Width, child.DesiredSize.Height));
						}
						return desiredSize;
				}

				// Arrange item in list
				protected override Size ArrangeOverride(Size finalSize)
				{
						double childHeightBefore = 0;

						for (int i = 0; i < InternalChildren.Count; i++)
						{
								var child = InternalChildren[i];
								double childHeight = ChildrenRect[i].Height;

								child.Arrange(new Rect(new Point(0, childHeight + childHeightBefore), child.DesiredSize));

								childHeightBefore += childHeight;
						}
						return finalSize;
				}

				public void InsertItem(int index, FrameworkElement control)
				{
						Children.Add(control);

						/*if (isInitializing) waitForItem = true;
						else
						{
								//if (stackPanelTranslate.Y - templateRoot.ActualHeight < lastChildBottomY) CreateItem(index);
						}*/

						//debugWindow1.AddItem(index);

						//writeLine("Done adding item, IsInitializing: " + isInitializing + " , itemNeedMeasurement: " + itemNeedMeasurement);
				}
		}

		public partial class ModernListView : Panel
		{
				DebugWindow debug = new DebugWindow();

				// List view adapter
				public IRecyclerListAdapter Adapter {
						get => _adapter;
						set {
								_adapter = value;
								for(int i = 0; i < _adapter.GetItemCount(); i++)
								{
										scrollView.InsertItem(i, _adapter.GetItem(i));
								}
						}
				}

				private IRecyclerListAdapter _adapter;

				private ModernListScrollView scrollView;

				/*// States
				Boolean isInitializing = true;
				Boolean scrollable;
				Boolean isScrollableInitialized = false;
				double maximumTranslateY = 0;

				//List<RecyclerViewItem> visibleItems = new List<RecyclerViewItem>();

				// For calculation purpose
				Boolean waitForItem = false;
				int waitForItemIndex = 0;
				Boolean waitForItemMaxed = false;
				Boolean waitForItemEnd = false;
				Boolean itemNeedMeasurement = false;
				int firstChildIndex = 0;
				double firstChildTopY = 0;
				double firstChildBottomY = 0;
				int lastChildIndex = 0;
				double lastChildTopY = 0;
				double lastChildBottomY = 0;

				// For debugging purpose
				int calculated = 0;
				ListBox debugListBox;
				Label debugText;
				Label debugText1;
				Label debugText2;
				DebugWindow1 debugWindow1;

				public ModernListView() : base() { }

				/*private void writeLine(string text)
				{
						ListBoxItem listBoxItem = new ListBoxItem();
						listBoxItem.Content = text;
						debugListBox.Items.Add(listBoxItem);
						debugListBox.ScrollIntoView(listBoxItem);
				}
				private void writeLine(int text)
				{
						ListBoxItem listBoxItem = new ListBoxItem();
						listBoxItem.Content = text;
						debugListBox.Items.Add(listBoxItem);
						debugListBox.ScrollIntoView(listBoxItem);
				}
				private void writeLine(double text)
				{
						ListBoxItem listBoxItem = new ListBoxItem();
						listBoxItem.Content = text;
						debugListBox.Items.Add(listBoxItem);
						debugListBox.ScrollIntoView(listBoxItem);
				}*/

				public ModernListView()
				{
						debug.Show();

						scrollView = new ModernListScrollView();
						InternalChildren.Add(scrollView);
						InternalChildren.Add(new ScrollBar());
				}

				protected override Size MeasureOverride(Size availableSize)
				{
						Size desiredSize = new Size();

						InternalChildren[0].Measure(availableSize);
						InternalChildren[1].Measure(availableSize);

						desiredSize.Width = availableSize.Width;
						desiredSize.Height = availableSize.Height;

						

						return desiredSize;
				}

				protected override Size ArrangeOverride(Size finalSize)
				{
						var _scrollView = InternalChildren[0];
						var _scrollBar = InternalChildren[1];

						_scrollView.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
						_scrollBar.Arrange(new Rect(
								finalSize.Width - _scrollBar.DesiredSize.Width,
								0,
								_scrollBar.DesiredSize.Width,
								_scrollBar.DesiredSize.Height
								));
						
						return finalSize;
				}
				private void OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
				{
						debug.Log("Key focus");
				}



				private void OnButtonKeyDown(object sender, KeyEventArgs e) {
						if (e.Key == Key.Enter)
						{
								debug.Log("Key down");
								InternalChildren[0].RenderTransform = new TranslateTransform(0, 20);
						}
				}

				private void calculateMaximumTranslateY()
				{
						//writeLine("Start calculating maximumTranslateY");
						//maximumTranslateY = Math.Floor(-stackPanel.ActualHeight - -templateRoot.ActualHeight) - 1;
						//debug.Content = "Calculated " + maximumTranslateY;
						//verticalScrollBar.Maximum = -maximumTranslateY;
						//writeLine("Done calculating maximumTranslateY: " + maximumTranslateY + ", scrollBarEnabled: " + verticalScrollBar.IsEnabled);
						//calculated++;
				}

				/*private void checkIsScrollable()
				{
						//writeLine("Start checking is scrollable");
						if (stackPanel.ActualHeight > templateRoot.ActualHeight)
						{
								//writeLine("First checking, scrollable " + scrollable);
								if (!scrollable || !isScrollableInitialized)
								{
										scrollable = true;
										if (isScrollableInitialized) isScrollableInitialized = true;
										verticalScrollBar.IsEnabled = true;
								}
								verticalScrollBar.ViewportSize = templateRoot.ActualHeight;
								//writeLine("Done checking, scrollable " + scrollable);
						}
						else
						{
								//writeLine("First checking, not scrollable " + scrollable);
								if (scrollable || !isScrollableInitialized)
								{
										scrollable = false;
										if (isScrollableInitialized) isScrollableInitialized = true;
										if (stackPanelTranslate.Y != 0) stackPanelTranslate.Y = 0;
										verticalScrollBar.IsEnabled = false;
								}
								//writeLine("Done checking, not scrollable " + scrollable);
						}
				}

				private void stackPanel_MouseWheel(object sender, MouseWheelEventArgs e)
				{
						if (scrollable)
						{
								double translateY = stackPanelTranslate.Y;
								// Scroll up
								if (e.Delta > 0 && translateY != -1)
								{
										//debugText2.Content = "scrolling up " + e.Delta + ", maximumTranslateY: " + maximumTranslateY;
										double acceleration = e.Delta / 2;

										if (translateY + acceleration > 0)
										{
												//acceleration = acceleration - (acceleration + translateY);
										}

										//scrollBy(acceleration);
										//stackPanelTranslate.Y += acceleration;
										verticalScrollBar.Value -= acceleration;
										//if (stackPanelTranslate.Y)
								}
								// Scroll down
								else if (e.Delta < 0 && translateY != maximumTranslateY)
								{
										//debugText2.Content = "scrolling down " + e.Delta;
										double acceleration = e.Delta / 2;

										if (translateY + acceleration < maximumTranslateY)
										{
												//acceleration = acceleration - (acceleration - (maximumTranslateY - translateY));
										}

										//scrollBy(acceleration);
										//stackPanelTranslate.Y += acceleration;
										verticalScrollBar.Value -= acceleration;
								}

								//debug.Content = acceleration;
								//debug.Content = "accl: " + acceleration + " transY: " + translateY + " stackPanelHeight: " + stackPanel.ActualHeight + " templateRootHeight:" + templateRoot.ActualHeight + " maxTransY: " + maximumTranslateY + " calculated: " + calculated;
						}
				}

				int stackCal = 0;
				private void stackPanel_SizeChanged(object sender, SizeChangedEventArgs e)
				{
						//writeLine("stackPanel changed " + stackCal++);
						//writeLine("itemNeedMeasurement: " + itemNeedMeasurement);
						//AddItem(e.WidthChanged + " " + e.HeightChanged);
						if (waitForItem)
						{
								//writeLine("> waitForItem");
								//Boolean canBeAdded = false;
								//Boolean isMax = false;

								if (!waitForItemMaxed && waitForItemIndex != items.Count)
								{
										RecyclerListItem item = items[waitForItemIndex];
										//writeLine("index: " + item.Index + " stackPanelHeight: " + stackPanel.ActualHeight + " templateRootHeight: " + templateRoot.ActualHeight);


										if (stackPanel.ActualHeight >= templateRoot.ActualHeight)
										{
												waitForItemMaxed = true;
										}
										//writeLine("Start creating and measuring, waitForItemMaxed: " + waitForItemMaxed);
										CreateItem(item.Index, false);
										item.HasBeenAdded = true;
										if (waitForItemIndex == 1)
										{
												//writeLine("Index is 1, start measuring for index 0");
												firstChildTopY = 0;
												firstChildBottomY = -items[0].Control.ActualHeight;
												items[0].TopY = 0;
												items[0].BottomY = -items[0].Control.ActualHeight;
												//writeLine("TopY: " + items[waitForItemIndex - 1].TopY + " BottomY: " + items[waitForItemIndex - 1].BottomY);
										}
										else if (waitForItemIndex > 1)
										{
												//writeLine("Index is " + waitForItemIndex + ", start measuring for index " + (waitForItemIndex - 1));
												items[waitForItemIndex - 1].TopY = items[waitForItemIndex - 2].BottomY;
												items[waitForItemIndex - 1].BottomY = items[waitForItemIndex - 2].BottomY + -items[waitForItemIndex - 1].Control.ActualHeight;
												//writeLine("TopY: " + items[waitForItemIndex - 1].TopY + " BottomY: " + items[waitForItemIndex - 1].BottomY);
										}
										else //writeLine("Index is 0, skipping measurement");

										if (waitForItemIndex == items.Count - 1)
										{
												waitForItemMaxed = true;
										}

										waitForItemIndex++;
								}
								else
								{
										//writeLine(">> waitForItemMaxed");
										//writeLine("Index is " + waitForItemIndex + ", start measuring for index " + (waitForItemIndex - 1));
										lastChildIndex = waitForItemIndex - 1;
										lastChildTopY = items[waitForItemIndex - 2].BottomY;
										lastChildBottomY = items[waitForItemIndex - 2].BottomY + -items[waitForItemIndex - 1].Control.ActualHeight;
										items[waitForItemIndex - 1].TopY = lastChildTopY;
										items[waitForItemIndex - 1].BottomY = lastChildBottomY;
										//writeLine("TopY: " + items[waitForItemIndex - 1].TopY + " BottomY: " + items[waitForItemIndex - 1].BottomY);

										calculateMaximumTranslateY();
										checkIsScrollable();

										waitForItem = false;
								}

								//writeLine("> Done waitForItem");
						}

						if (itemNeedMeasurement)
						{
								//writeLine("Start measuring " + lastChildIndex);
								lastChildIndex++;
								lastChildTopY = items[lastChildIndex - 1].BottomY;
								lastChildBottomY = lastChildTopY + -items[lastChildIndex - 1].Control.ActualHeight;
								items[lastChildIndex].TopY = lastChildTopY;
								items[lastChildIndex].BottomY = lastChildBottomY;

								if (stackPanelTranslate.Y - templateRoot.ActualHeight < lastChildTopY && lastChildIndex != items.Count - 1)
								{
										//writeLine("Add lowermost item in itemNeedMeasurement, index: " + lastChildIndex + " TranslateY: " + stackPanelTranslate.Y + " TopY: " + items[lastChildIndex].TopY + " BottomY: " + items[lastChildIndex].BottomY);
										/*if (stackPanelTranslate.Y < lastChildBottomY)
										{
												writeLine("yes");
												items[lastChildIndex].Control.Visibility = Visibility.Hidden;
												//debugWindow1.ChangeStatus(lastChildIndex, "hide");
												firstChildIndex = lastChildIndex;
												firstChildTopY = items[firstChildIndex].TopY;
												firstChildBottomY = items[firstChildIndex].BottomY;
										}*/
										/*CreateItem(lastChildIndex + 1);
								}
								else
								{
										//writeLine("itemNeedMeasurement no");
										itemNeedMeasurement = false;

										calculateMaximumTranslateY();
										checkIsScrollable();
								}


								//writeLine("Done measuring, lastChildTopY: " + lastChildTopY + " lastChildBottomY: " + lastChildBottomY);
						}
				}

				int gridCal = 0;

				private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
				{
						//writeLine("Grid changed " + gridCal++);
						//debug.Content = "Grid changed: " + calculated++ + " Need measurement: " + itemNeedMeasurement + " Isinitializing: " + isInitializing;
						// If the RecyclerView is still initializing
						// And there is an item queue to be added to the stackPanel
						// If an item height hasn't measured, measure it now
						/*else if (itemNeedMeasurement)
						{
								StackPanel item = (StackPanel)stackPanel.Children[stackPanel.Children.Count - 1];
								itemsHeight.Add(item.ActualHeight);
								itemNeedMeasurement = false;
						}*/
				/*}

				int rootCal = 0;
				private void templateRoot_SizeChanged(object sender, SizeChangedEventArgs e)
				{
						//writeLine("templateRoot changed " + rootCal++);
						if (isInitializing) isInitializing = false;
						else
						{
								calculateMaximumTranslateY();
								checkIsScrollable();
						}
						//debug.Content = "Changed " + scrollable;

						//debug.Content = "Index: " + (stackPanel.Children.Count - 1) + " Item width: " + item.ActualWidth + " Item height: " + item.ActualHeight;
				}

				private void verticalScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
				{
						if (isInitializing) return;

						stackPanelTranslate.Y = -e.NewValue;
						//debugText.Content = stackPanelTranslate.Y - templateRoot.ActualHeight + " lastChildTopY: " + lastChildTopY + " maximumTranslateY: " + maximumTranslateY;
						//debugText1.Content = "maximumTranslateY: " + (maximumTranslateY - templateRoot.ActualHeight) + ", maximumVerticalScrollBar: " + verticalScrollBar.Maximum;

						// If scrolled down, for showing lowermost item
						Boolean endCheckingDownLowermost = false;
						while (!endCheckingDownLowermost)
						{
								//writeLine("scroll");
								//writeLine("scrollDownLowermost, lastChildIndex: " + lastChildIndex + " hasBeenAdded: " + items[lastChildIndex + 1].HasBeenAdded);
								if (stackPanelTranslate.Y - templateRoot.ActualHeight < lastChildTopY && !itemNeedMeasurement && lastChildIndex != items.Count - 1)
								{
										// If the item hasn't added to the stackPanel, add it
										if (!items[lastChildIndex + 1].HasBeenAdded)
										{
												//writeLine("Add lowermost item, index: " + lastChildIndex + " TranslateY: " + stackPanelTranslate.Y + " TopY: " + items[lastChildIndex].TopY + " BottomY: " + items[lastChildIndex].BottomY);
												CreateItem(lastChildIndex + 1);
												endCheckingDownLowermost = true;
										}
										else
										{
												//writeLine("Show lowermost item, index: " + lastChildIndex);

												lastChildIndex++;
												lastChildTopY = items[lastChildIndex].TopY;
												lastChildBottomY = items[lastChildIndex].BottomY;
												items[lastChildIndex].Control.Visibility = Visibility.Visible;
												//debugWindow1.ChangeStatus(lastChildIndex, "visible");
										}
								}
								else endCheckingDownLowermost = true;
						}

						//writeLine(firstChildIndex.ToString());
						// If scrolled down, for hiding the topmost item
						/*Boolean endCheckingDownTopmost = false;
						while (!endCheckingDownTopmost)
						{
								if (stackPanelTranslate.Y < firstChildBottomY && items[firstChildIndex].HasBeenAdded)
								{
										writeLine("Hide " + firstChildIndex + " TranslateY: " + stackPanelTranslate.Y + " TopY: " + items[firstChildIndex].TopY + " BottomY: " + items[firstChildIndex].BottomY);

										items[firstChildIndex].Control.Visibility = Visibility.Hidden;
										//debugWindow1.ChangeStatus(firstChildIndex, "hide");
										firstChildIndex++;
										firstChildTopY = items[firstChildIndex].TopY;
										firstChildBottomY = items[firstChildIndex].BottomY;
								}
								else endCheckingDownTopmost = true;
						}		

						// If scrolled up, for showing the topmost item
						Boolean endCheckingUpTopmost = false;
						while (!endCheckingUpTopmost)
						{
								if (stackPanelTranslate.Y > firstChildTopY)
								{
										writeLine("Show " + firstChildIndex + " TranslateY: " + stackPanelTranslate.Y + " TopY: " + items[firstChildIndex].TopY + " BottomY: " + items[firstChildIndex].BottomY);

										firstChildIndex--;
										firstChildTopY = items[firstChildIndex].TopY;
										firstChildBottomY = items[firstChildIndex].BottomY;
										items[firstChildIndex].Control.Visibility = Visibility.Visible;
										//debugWindow1.ChangeStatus(firstChildIndex, "visible");
								}
								else endCheckingUpTopmost = true;
						}

						// If scrolled up, for hiding the lowermost item
						Boolean endCheckingUpLowermost = false;
						while (!endCheckingUpLowermost)
						{
								if (stackPanelTranslate.Y - (templateRoot.ActualHeight + items[lastChildIndex].Control.ActualHeight) > lastChildTopY)
								{
										writeLine("Hide lowermost item " + (stackPanelTranslate.Y - templateRoot.ActualHeight) + " TopY: " + lastChildTopY);

										items[lastChildIndex].Control.Visibility = Visibility.Hidden;
										//debugWindow1.ChangeStatus(lastChildIndex, "hide");
										lastChildIndex--;
										lastChildTopY = items[lastChildIndex].TopY;
										lastChildBottomY = items[lastChildIndex].BottomY;
								}
								else endCheckingUpLowermost = true;
						}*/

						//scrollBy(-e.NewValue);
				//}

				private class RecyclerListItem
				{
						public int Index;
						public Boolean HasBeenAdded;
						public StackPanel Control;
						public double TopY;
						public double BottomY;

						public RecyclerListItem(int _index)
						{
								Index = _index;
						}
				}
		}

		public interface IRecyclerListAdapter
		{
				public int GetItemCount();

				public FrameworkElement GetItem(int index);
		}
}
