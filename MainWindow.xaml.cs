using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Contadores
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		int CuentaManual = 0;
		int CuentaAutomatica = 0;
		bool hiloExiste = false;
		Thread hiloDeContador;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void empezarAutoContador_Click(object sender, RoutedEventArgs e)
		{
			if ((bool)tieneHilos.IsChecked)
			{
				if (!hiloExiste)
				{
					hiloDeContador = new Thread(new ThreadStart(HiloDeContador))
					{
						IsBackground = true
					};
					hiloDeContador.Start();
					hiloExiste = true;
				} 
				else
				{
					hiloDeContador.Abort();
					hiloExiste = false;
				}
			}
			else if (!hiloExiste)
			{
				while (true)
				{
					CuentaAutomatica++;
					autoContador.Content = CuentaAutomatica;
				}
			}
			else 
			{
				hiloDeContador.Abort();
				hiloExiste = false;
			}
		}

		private void contarManualmente_Click(object sender, RoutedEventArgs e)
		{
			CuentaManual++;
			contadorManual.Content = CuentaManual;
		}

		public delegate void ActualizarContador(string message);

		private void HiloDeContador()
		{
			for (int i = 0; i <= 1000000000; i++)
			{
				Thread.Sleep(50);
				autoContador.Dispatcher.Invoke(
					new ActualizarContador(this.ActualizarInterfaz),
					new object[] { i.ToString() }
				);
			}
		}
		private void ActualizarInterfaz(string message)
		{
			CuentaAutomatica++;
			autoContador.Content = CuentaAutomatica;
		}
	}
}
