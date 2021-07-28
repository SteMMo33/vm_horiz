using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace vm_horiz
{
	/// <summary>
	/// Logica di interazione per ItemButton.xaml
	/// </summary>
	public partial class ItemButton : UserControl
	{
		// Membri interni
		private VmItem _item { set; get; }


		// Evento click passato ai listeners
		public event EventHandler ItemClick;
		//public delegate void ItemClickEventHandler(object sender, ItemClickEventArgs e);

		public class ItemClickEventArgs : EventArgs
		{
			public VmItem Item { get; set; }
		};



		public ItemButton(VmItem item)
		{
			InitializeComponent();

			_item = item;
			lblDesc1.Content = item.descrizione1;
			lblDesc2.Content = item.descrizione2;
			lblPrezzo.Content = String.Format($"€ {item.importo, 0:F2}");
		}

		private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
		{
			Debug.WriteLine("> ItemButton.MouseUp");

			ItemClickEventArgs i = new ItemClickEventArgs();
			i.Item = _item;
			ItemClick.Invoke( this, i);
		}
	}
}
