using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ClientAndStaff.Helpers
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string callerMemberName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(callerMemberName));
    }
}
