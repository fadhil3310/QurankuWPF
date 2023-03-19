using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Xml.Serialization;
using System.Resources;
using System.Xml;
using System.Xml.XPath;

namespace QurankuWPF
{
		/// <summary>
		/// Interaction logic for QuranPage.xaml
		/// </summary>
		public partial class QuranPage : Page
		{
				public QuranPage()
				{
						InitializeComponent();
						suraListView.Adapter = new SuraListAdapter();
				}

				private class SuraListAdapter : IRecyclerListAdapter
				{
						List<Sura> suraList;

						public SuraListAdapter()
						{
								suraList = GetSuraList();
						}

						public List<Sura> GetSuraList()
						{
								var deserializer = new XmlSerializer(typeof(List<Sura>));
								var debugWindow = new DebugWindow();
								debugWindow.Show();

								List<Sura> suraList = new List<Sura>();

								using (var stream = typeof(QuranPage).Assembly.GetManifestResourceStream("QurankuWPF.Quran.quran_data.xml"))
								{
										var document = new XPathDocument(stream);
										var navigator = document.CreateNavigator();
										var suraNodesList = navigator.Select("quran/suras/*");

										for (int i = 0; i < suraNodesList.Count; i++)
										{
												suraNodesList.MoveNext();
												var suraNode = suraNodesList.Current;
												Sura suraClass = new Sura
												{
														index = int.Parse(suraNode.GetAttribute("index", "")),
														ayas = int.Parse(suraNode.GetAttribute("ayas", "")),
														start = int.Parse(suraNode.GetAttribute("start", "")),
														name = suraNode.GetAttribute("name", ""),
														tname = suraNode.GetAttribute("tname", ""),
														ename = suraNode.GetAttribute("ename", ""),
														type = suraNode.GetAttribute("type", ""),
														order = int.Parse(suraNode.GetAttribute("order", "")),
														rukus = int.Parse(suraNode.GetAttribute("rukus", ""))
												};
												suraList.Add(suraClass);
										}
								}

								return suraList;
						}

						public int GetItemCount() => suraList.Count;

						public FrameworkElement GetItem(int index)
						{
								var stackPanel = new StackPanel();
								var suraName = new Label { Content = suraList[index].name };
								stackPanel.Children.Add(suraName);
								return stackPanel;
						}
				}
		}
}
