using ATMApplication.MVVM.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ATMApplication.MVVM.View
{
	/// <summary>
	/// Логика взаимодействия для BalanceView.xaml
	/// </summary>
	public partial class BalanceView : UserControl
	{
		public BalanceView()
		{
			InitializeComponent();
			// Создаем экземпляр AuthViewModel
			
		}
		private void OpenWithDraw(object sender, RoutedEventArgs e)
		{
			
			WithdrawBorder.Visibility = Visibility.Visible;
		}
		private void OpenTopUp(object sender, RoutedEventArgs e)
		{
			
			TopUpBorder.Visibility = Visibility.Visible;
		}

		private void CloseWithDraw(object sender, RoutedEventArgs e)
		{
			WithdrawBorder.Visibility = Visibility.Collapsed;
		}
		private void CloseTopUp(object sender, RoutedEventArgs e)
		{
			TopUpBorder.Visibility = Visibility.Collapsed;
		}

		private void OpenTransaction(object sender, RoutedEventArgs e)
		{

			TransactionBorder.Visibility = Visibility.Visible;
		}
		private void CloseTransaction(object sender, RoutedEventArgs e)
		{
			TransactionBorder.Visibility = Visibility.Collapsed;
		}

		private void OpenTransferBorder(object sender, RoutedEventArgs e)
		{

			TransferBorder.Visibility = Visibility.Visible;
		}
		private void CloseTransferBorder(object sender, RoutedEventArgs e)
		{
			TransferBorder.Visibility = Visibility.Collapsed;
		}

		private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Clipboard.SetText(CardCopy.Text);
		}
	}

}
