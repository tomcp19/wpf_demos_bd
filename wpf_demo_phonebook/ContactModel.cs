using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace wpf_demo_phonebook
{
    public class ContactModel : INotifyPropertyChanged
    {
        public int ContactID { get; set; }
        private string firstName;
        private string lastName;
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Info)); //sinon le listview ne change pas
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Info)); //sinon le listview ne change pas
            }
        }

        public string Info => $"{LastName} , {FirstName}";

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
