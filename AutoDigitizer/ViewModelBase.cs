using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoDigitizer {
    /// <summary>
    /// The base class for View Model classes.
    /// All classes for View Model should be inherited this class.
    /// </summary>
    internal class ViewModelBase : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise an event that the propterty specified by <paramref name = "propertyName"/> has been changed.
        /// </summary>
        /// <param name="propertyName"> A Property name to notify it has been changed. </param>
        protected void RaisePropertyChanged(string propertyName) {
            if (this.PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
