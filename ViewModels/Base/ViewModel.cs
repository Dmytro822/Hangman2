using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hangman2.ViewModels.Base
{
    public abstract class ViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string proptyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proptyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string proptyName = null)
        {
            if (Equals(field, value))
            {
                return false;
            }
            field = value;
            OnPropertyChanged(proptyName);
            return true;
        }

    }
}
