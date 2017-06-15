using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UITestSampleApp
{
	public class BaseViewModel : INotifyPropertyChanged
	{
        #region Fields
        bool _isAccessingInternet;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
        public bool IsAccessingInternet
        {
            get => _isAccessingInternet;
            set => SetProperty(ref _isAccessingInternet, value);
        }
        #endregion

        protected void SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyname = "", Action onChanged = null)
		{
			if (EqualityComparer<T>.Default.Equals(backingStore, value))
				return;

			backingStore = value;

			onChanged?.Invoke();

			OnPropertyChanged(propertyname);
		}

		void OnPropertyChanged([CallerMemberName]string name = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	} 
}
