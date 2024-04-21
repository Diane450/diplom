using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.ModelsDTo
{
    public class DishDTO : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string _name = null!;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private DishTypeDTO _type = null!;

        public DishTypeDTO Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged(nameof(_type));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
