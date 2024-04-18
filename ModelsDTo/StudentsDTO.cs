using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Diplom.ModelsDTo
{
    public class StudentsDTO : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public int ClassId { get; set; }

        private StatusDTO _status;

        public StatusDTO Statuss
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Statuss));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
