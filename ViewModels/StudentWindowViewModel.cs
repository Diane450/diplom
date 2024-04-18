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
    public class StudentWindowViewModel : ViewModelBase
    {
        public Student CurrentStudent { get; set; }

        private Student _student;

        public Student Student
        {
            get { return _student; }
            set { _student = value; }
        }

        private ObservableCollection<Status> _statuses;

        public ObservableCollection<Status> Statuses
        {
            get { return _statuses; }
            set { _statuses = this.RaiseAndSetIfChanged(ref _statuses, value); }
        }

        private Status _selectedStatus;

        public Status SelectedStatus
        {
            get { return _selectedStatus; }
            set { _selectedStatus = this.RaiseAndSetIfChanged(ref _selectedStatus, value); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = this.RaiseAndSetIfChanged(ref _message, value); }
        }

        private TeacherWindowViewModel _teacherWindowViewModel;

        public TeacherWindowViewModel TeacherWindowViewModel
        {
            get { return _teacherWindowViewModel; }
            set { _teacherWindowViewModel = this.RaiseAndSetIfChanged(ref _teacherWindowViewModel, value); }
        }

        public StudentWindowViewModel(Student student, TeacherWindowViewModel teacherWindowViewModel)
        {
            try
            {
                TeacherWindowViewModel = teacherWindowViewModel;
                CurrentStudent = student;
                Student = new Student()
                {
                    Id = student.Id,
                    FullName = student.FullName,
                    Class = student.Class,
                    Status = student.Status,
                    StatusId = student.StatusId,
                    ClassId = student.ClassId,
                };
                GetContent();
            }
            catch
            {
                Message = "Ошибка соединения";
            }
        }
        
        private async Task GetContent()
        {
            Statuses = await DBCall.GetStatuses();
            SelectedStatus = Statuses.Where(s => s.Id == Student.Status.Id).First();
        }
        
        public async Task SaveChanges()
        {
            try
            {
                Student.StatusId = SelectedStatus.Id;
                Student.Status = SelectedStatus;
                await DBCall.SaveStudentChanges(Student);
                Student oldStudent = TeacherWindowViewModel.ClassesStudents[TeacherWindowViewModel.SelectedClass].Where(s => s.Id == CurrentStudent.Id).First();
                
                CurrentStudent.Status.Name = SelectedStatus.Name;
                CurrentStudent.Status.Id = SelectedStatus.Id;
                CurrentStudent.StatusId = SelectedStatus.Id;
                int index = TeacherWindowViewModel.ClassesStudents[TeacherWindowViewModel.SelectedClass].FindIndex(s => s == oldStudent);
                TeacherWindowViewModel.ClassesStudents[TeacherWindowViewModel.SelectedClass][index] = new Student() 
                {
                    Id = CurrentStudent.Id,
                    FullName = CurrentStudent.FullName,
                    Class = CurrentStudent.Class,
                    Status = CurrentStudent.Status,
                };
                TeacherWindowViewModel.Students[index] = new Student()
                {
                    Id = CurrentStudent.Id,
                    FullName = CurrentStudent.FullName,
                    Class = CurrentStudent.Class,
                    Status = CurrentStudent.Status,
                };
                Message = "Успешно сохранено";
            }
            catch
            {
                Message = "Не удалось сохранить изменения";
            }
        }
    }
}
