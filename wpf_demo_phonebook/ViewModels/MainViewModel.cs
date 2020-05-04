using System;
using System.Collections.ObjectModel;
using System.Linq;
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

        public MainViewModel()
        {
            SearchContactCommand = new RelayCommand(SearchContact);
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
                    Contacts = PhoneBookBusiness.GetContactByName(input);
                    if (Contacts.Count > 0)
                    {
                        SelectedContact = Contacts[0];
                    }
                    else 
                    {
                        Contacts = PhoneBookBusiness.GetAllContacts();
                        SelectedContact = Contacts.First<ContactModel>();
                        MessageBox.Show("Aucun contact trouvé");
                    }
                    break;
                default:
                    MessageBox.Show("Unknown search method");
                    break;
            }
        }

    }
}
