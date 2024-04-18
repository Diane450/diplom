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
        public StudentsDTO CurrentStudent { get; set; }

        private StudentsDTO _student;

        public StudentsDTO Student
        {
            get { return _student; }
            set { _student = value; }
        }

        private static ObservableCollection<Status> _statuses;

        public static ObservableCollection<Status> Statuses
        {
            get { return _statuses; }
            set { _statuses = value; }
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

        public StudentWindowViewModel(StudentsDTO student)
        {
            try
            {
                CurrentStudent = student;
                Student = new StudentsDTO()
                {
                    Id = student.Id,
                    FullName = student.FullName,
                    Statuss = new StatusDTO
                    {
                        Id = student.Statuss.Id,
                        Name = student.Statuss.Name,
                    },
                    ClassId = student.ClassId,
                };
                SelectedStatus = Statuses.Where(s => s.Id == Student.Statuss.Id).First();
            }
            catch
            {
                Message = "Ошибка соединения";
            }
        }
        public static async Task GetContent()
        {
            if (Statuses == null)
            {
                Statuses = await DBCall.GetStatuses();
            }
        }

        public async Task SaveChanges()
        {
            try
            {
                CurrentStudent.Statuss = new StatusDTO()
                {
                    Id = SelectedStatus.Id,
                    Name = SelectedStatus.Name
                };
                
                Student.Statuss = new StatusDTO
                {
                    Id = SelectedStatus.Id,
                    Name = SelectedStatus.Name,
                };
                await DBCall.SaveStudentChanges(Student);
                Message = "Успешно сохранено";
            }
            catch
            {
                Message = "Не удалось сохранить изменения";
            }
        }
    }
}
