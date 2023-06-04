using ClientAndStaff.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ClientAndStaff.Helpers
{
    public class OrderDetailViewModel : INotifyPropertyChanged
    {
        private OrderLk _currentOrder;

        public event PropertyChangedEventHandler PropertyChanged;

        public OrderLk CurrentOrder
        {
            get { return _currentOrder; }
            set
            {
                if (_currentOrder != value)
                {
                    _currentOrder = value;
                    OnPropertyChanged(nameof(CurrentOrder));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
