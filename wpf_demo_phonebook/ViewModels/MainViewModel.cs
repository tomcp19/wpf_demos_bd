using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using wpf_demo_phonebook.ViewModels.Commands;

namespace wpf_demo_phonebook.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private ContactModel selectedContact;

        private ObservableCollection<ContactModel> contacts = new ObservableCollection<ContactModel>();

        public ContactModel SelectedContact
        {
            get => selectedContact;
            set { 
                selectedContact = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ContactModel> Contacts
        {
            get => contacts;
            set
            {
                contacts = value;
                OnPropertyChanged();
            }
        }

        private string criteria;

        public string Criteria
        {
            get { return criteria; }
            set { 
                criteria = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SearchContactCommand { get; set; }
        //public RelayCommand GetAllCommand { get; set; }

        public MainViewModel()
        {
            //SearchContactCommand = new RelayCommand(SearchContact);
            //SelectedContact = PhoneBookBusiness.GetContactByID(1);

            Contacts = PhoneBookBusiness.GetAllContacts();
            SelectedContact = Contacts.First<ContactModel>();
        }

        private void SearchContact(object parameter)
        {
            string input = parameter as string;
            int output;
            string searchMethod;
            if (!Int32.TryParse(input, out output))
            {
                searchMethod = "name";
            } else
            {
                searchMethod = "id";
            }

            switch (searchMethod)
            {
                case "id":
                    SelectedContact = PhoneBookBusiness.GetContactByID(output);
                    break;
                case "name":
                    SelectedContact = PhoneBookBusiness.GetContactByName(input);
                    break;
                default:
                    MessageBox.Show("Unkonwn search method");
                    break;
            }
        }

        private void ShowAllContact(Object parameter)
        {
            Contacts = PhoneBookBusiness.GetAllContacts();
        }

        /*public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }*/

    }
}
