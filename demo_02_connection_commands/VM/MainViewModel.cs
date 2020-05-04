using App.Models;
using System.Collections.ObjectModel;

namespace demo_02_connection_commands.VM
{
    class MainViewModel : BaseViewModel
    {
        private Employee selectedEmployee;
        private ObservableCollection<Employee> employees;

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<Employee> Employees
        {
            get => employees;
            set
            {
                employees = value;
                OnPropertyChanged();
            }
        }

    }
}
