using Diplom.Models;
using Diplom.ModelsDTo;
using Diplom.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.ViewModels
{
    public class TeacherWindowViewModel : ViewModelBase
    {
        private Dictionary<Class, List<StudentsDTO>> _classesStudents = null!;

        public Dictionary<Class, List<StudentsDTO>> ClassesStudents
        {
            get { return _classesStudents; }
            set { _classesStudents = this.RaiseAndSetIfChanged(ref _classesStudents, value); }
        }

        private ObservableCollection<Class> _classes = null!;

        public ObservableCollection<Class> Classes
        {
            get { return _classes; }
            set { _classes = this.RaiseAndSetIfChanged(ref _classes, value); }
        }


        private Class _selectedClass = null!;

        public Class SelectedClass
        {
            get { return _selectedClass; }
            set
            {
                _selectedClass = this.RaiseAndSetIfChanged(ref _selectedClass, value);
                Students = new ObservableCollection<StudentsDTO>(ClassesStudents[SelectedClass]);
            }
        }

        private ObservableCollection<StudentsDTO> _students;

        public ObservableCollection<StudentsDTO> Students
        {
            get { return _students; }
            set { _students = this.RaiseAndSetIfChanged(ref _students, value); }
        }

        public TeacherWindowViewModel(Teacher teacher)
        {
            GetContent(teacher);
        }
        private async Task GetContent(Teacher teacher)
        {
            ClassesStudents = await DBCall.GetClassesData(teacher);
            Classes = await DBCall.GetClasses(teacher);
            Students = new ObservableCollection<StudentsDTO>(ClassesStudents[Classes[0]]);
            SelectedClass = Classes[0];
        }
    }
}
