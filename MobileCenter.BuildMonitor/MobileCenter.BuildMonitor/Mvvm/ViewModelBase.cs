using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenter.BuildMonitor.Mvvm
{
    /// <summary>
    /// Base class for all ViewModels with INotifyPropertyChanged implementation
    /// </summary>
    public class ViewModelBase
    {
        // Data State properties

        protected bool _isDataLoaded = false;
        protected bool _isDataLoading = false;
        protected bool _hasDataError = false;

        /// <summary>
        /// Indicates that the viewmodel is currently loading data.
        /// </summary>
        public virtual bool IsDataLoading
        {
            get { return _isDataLoading; }
            set
            {
                if (SetProperty<bool>(ref _isDataLoading, value) && value)
                {
                    HasDataError = false;
                }
            }
        }

        /// <summary>
        /// Indicates that the viewmodel has already loaded its data.
        /// </summary>
        public virtual bool IsDataLoaded
        {
            get { return _isDataLoaded; }
            set { SetProperty<bool>(ref _isDataLoaded, value); }
        }

        /// <summary>
        /// Indicates that not data could be loaded.
        /// </summary>
        public virtual bool HasDataError
        {
            get { return IsDataLoaded && _hasDataError; }
            set { SetProperty(ref _hasDataError, value); }
        }

        #region INotifyPropertyChanged implementation

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Checks if a property already matches a desired value. Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support <see cref="CallerMemberNameAttribute"/>.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
