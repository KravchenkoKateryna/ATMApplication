using ATMApplication.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ATMApplication.MVVM.ViewModel
{
	class MainViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		private void OnPropertyChanged(string propName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}
		private object _selectedViewModel;
		public object SelectedViewModel
		{
			get => _selectedViewModel;
			set { _selectedViewModel = value; OnPropertyChanged(nameof(SelectedViewModel)); }
		}
		public MainViewModel()
		{
			SelectedViewModel = new AuthViewModel();
		}

		public void SwitchViews(object parameter)
		{
			Console.WriteLine($"SwitchViews called with parameter: {parameter}");

			switch (parameter)
			{

				case "Home":
					SelectedViewModel = new AuthViewModel();
					break;
				case "Auth":

					SelectedViewModel = new AuthViewModel();
					break;
				case "Validate_InputAuth_Data":
					int flag = ((AuthViewModel)SelectedViewModel).SaveUserData();
					if (flag == 1)
					{
						SelectedViewModel = new BalanceViewModel((AuthViewModel)SelectedViewModel);
					}
					break;


				default:
					SelectedViewModel = new AuthViewModel();
					break;
			}
			OnPropertyChanged("SelectedViewModel");
		}

		// Menu Button Command
		private ICommand _menucommand;
		public ICommand MenuCommand
		{
			get
			{
				_menucommand ??= new RelayCommand(SwitchViews);
				return _menucommand;
			}
		}
		private ICommand _Authcommand;

		public ICommand AuthCommand
		{
			get
			{
				_Authcommand ??= new RelayCommand(SwitchViews);
				return _Authcommand;
			}
		}

	}
}
