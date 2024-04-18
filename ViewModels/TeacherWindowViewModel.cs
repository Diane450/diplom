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
        private Dictionary<Class, List<Student>> _classesStudents = null!;

        public Dictionary<Class, List<Student>> ClassesStudents
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
                Students = new ObservableCollection<Student>(ClassesStudents[SelectedClass]);
            }
        }

        private ObservableCollection<Student> _students;

        public ObservableCollection<Student> Students
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
            Students = new ObservableCollection<Student>(ClassesStudents[Classes[0]]);
            SelectedClass = Classes[0];
        }
        public void UpdateStudent(Student student)
        {
            Student oldStudent = ClassesStudents[SelectedClass].First(s => s.Id == student.Id);
            int index = ClassesStudents[SelectedClass].FindIndex(s => s == oldStudent);
            ClassesStudents[SelectedClass][index] = student;
            Students[index] = student;
        }
    }
}
