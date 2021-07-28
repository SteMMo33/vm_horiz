using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using static vm_horiz.ItemButton;

namespace vm_horiz
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		List<VmItem> items;


		public MainWindow()
		{
			InitializeComponent();

			// Riempie la griglia di prodotti
			items = LoadProducts();
			PopulateGrid(items);
		}


		private void PopulateGrid(List<VmItem> items)
		{
			// Sempre 2 righe
			RowDefinition gridRow1 = new RowDefinition();
			RowDefinition gridRow2 = new RowDefinition();

			gridItems.RowDefinitions.Add(gridRow1);
			gridItems.RowDefinitions.Add(gridRow2);

			// Colonne
			int colonne = (items.Count + 1) / 2;
			for (int i = 0; i < colonne; i++)
			{
				ColumnDefinition coldef = new ColumnDefinition();
				coldef.Width = new GridLength(420);
				gridItems.ColumnDefinitions.Add(coldef);
			}

			// Popolo le celle
			int row = 0;
			int col = 0;
			foreach( VmItem item in items)
			{
				// Oggetto interno alla cella
				ItemButton btn = new ItemButton(item);
				btn.ItemClick += ItemClick;

				Grid.SetRow( btn, row);
				Grid.SetColumn( btn, col);
				gridItems.Children.Add(btn);

				// Prossima cella
				if (++col >= colonne)
				{
					++row;
					col = 0;
				}
			}
		}


		private void ItemClick(object sender, EventArgs e)
		{
			ItemClickEventArgs args = (ItemClickEventArgs)e;
			VmItem item = args.Item;

			Debug.WriteLine($">> Pressed: {item.descrizione1}");
		}

		private List<VmItem> LoadProducts()
		{
			List<VmItem> list = new List<VmItem>();
			
			list.Add(new VmItem() { descrizione1 = "Prodotto 1", descrizione2 = "Prodotto molto bello ed utile", importo = 5.00, pathImmagine = "..\\..\\..\\images\\pingu200.png" });
			list.Add(new VmItem() { descrizione1 = "Prodotto 2", descrizione2 = "Descrizione molto lunga per provare il campo della descrizione", importo = 15.00, pathImmagine = "" });
			list.Add(new VmItem() { descrizione1 = "Prodotto 3", descrizione2 = "prod3", importo = 35.50, pathImmagine = "..\\..\\images\\pingu200.png" });
			list.Add(new VmItem() { descrizione1 = "Prodotto 4", descrizione2 = "prod4", importo = 45.00, pathImmagine = "..\\images\\pingu200.png" });
			list.Add(new VmItem() { descrizione1 = "Prodotto 5", descrizione2 = "prod5", importo = 55.00, pathImmagine = "images\\pingu200.png" });
			list.Add(new VmItem() { descrizione1 = "Prodotto 6", descrizione2 = "Prodotto molto bello ed utile", importo = 65.00, pathImmagine = "\\images\\pingu200.png" });
			list.Add(new VmItem() { descrizione1 = "Prodotto 7", descrizione2 = "prod7", importo = 75.00, pathImmagine = "/images/pingu200.png" });
			return list;
		}


		// -- Gestione dello scroll orizzontale del contenuto finestra
		Point ScrollMousePoint;
		double HorizontalOff;

		private void ScrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			ScrollMousePoint = e.GetPosition(ScrollViewer);
			HorizontalOff = ScrollViewer.HorizontalOffset;
			// VerticalOff1 = ScrollViewer1.VerticalOffset;
			ScrollViewer.CaptureMouse();
		}

		private void ScrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			ScrollViewer.ReleaseMouseCapture();
		}

		private void ScrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
		{
			if (ScrollViewer.IsMouseCaptured)
			{
				ScrollViewer.ScrollToHorizontalOffset(HorizontalOff + (ScrollMousePoint.X - e.GetPosition(ScrollViewer).X));
				// ScrollViewer.ScrollToVerticalOffset(VerticalOff1 + (ScrollMousePoint1.Y - e.GetPosition(ScrollViewer1).Y));
			}
		}
	}
}
