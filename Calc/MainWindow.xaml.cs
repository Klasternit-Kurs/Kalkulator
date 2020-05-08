using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Calc
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		private long _a; //Treba nam nesto sto nije ceo broj radi deljenja
		public long A  //proveriti koliko cifara ima, staviti limit na npr 10
		{
			get => _a; 
			set
			{
				_a = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("A"));
			}
		}
		public long _b;
		public long _m;

		char UnesenaOperacija;
		bool ProsloDugmeOperacija;

		public MainWindow()
		{
			InitializeComponent();
			DataContext = this;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void Unos(object sender, RoutedEventArgs e)
		{
			if (int.TryParse((sender as Button).Content.ToString(), out int broj))
			{
				if (ProsloDugmeOperacija)
				{
					A = broj;
					ProsloDugmeOperacija = false;
				}
				else
				{
					A *= 10;
					A += broj;
				}
			} else
				switch((sender as Button).Content.ToString())
				{
					case "CE":
						A = 0;
						break;
					case "C": 
						A = 0;
						_b = 0;
						ProsloDugmeOperacija = false;
						UnesenaOperacija = ' ';
						break;
					case "+":
						if (_b == 0)
							_b = A;
						else
						{
							A += _b;
							_b = A;
						}
						UnesenaOperacija = '+';
						ProsloDugmeOperacija = true;
						break;
					case "=":
						switch(UnesenaOperacija)
						{
							case '+':
								A += _b;
								_b = 0;
								UnesenaOperacija = ' ';
								ProsloDugmeOperacija = true;
								break;
						}
						break;
					case "MR":
						A = _m;
						ProsloDugmeOperacija = true;
						break;
					case "MC":
						_m = 0;
						break;
					case "M+":
						_m += A;
						break;
					case "M-":
						_m -= A;
						break;
				}
		}
	}
}
