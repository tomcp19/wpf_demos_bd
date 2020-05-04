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
        public RelayCommand SaveContactCommand { get; set; }
        public RelayCommand DeleteContactCommand { get; set; }
        public RelayCommand NewContactCommand { get; set; }

        public MainViewModel()
        {
            SearchContactCommand = new RelayCommand(SearchContact);
            SaveContactCommand = new RelayCommand(SaveContact);
            DeleteContactCommand = new RelayCommand(DeleteContact);
            NewContactCommand = new RelayCommand(NewContact);

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


        private void SaveContact(object parameter)
        {
            if (selectedContact.ContactID != 0) //si le id existe deja, aurait pu prendre les flags comme nico le suggere en cas de id pouvant donner 0
            {
                int id = PhoneBookBusiness.UpdateContact(SelectedContact);
                MessageBox.Show("Informations sauvegardées!");
            }
            else
            {

                int newID = PhoneBookBusiness.NewContact(SelectedContact);
                if (newID > 0)
                {
                    SelectedContact.ContactID = newID;
                    Contacts.Add(SelectedContact);
                    SelectedContact = Contacts.Last<ContactModel>();
                }
            }
        }

        private void DeleteContact(object parameter)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Supprimer ce contact?", "Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                int modif = PhoneBookBusiness.DeleteContact(SelectedContact);
                Contacts = PhoneBookBusiness.GetAllContacts();
                SelectedContact = Contacts.First<ContactModel>();
            }

        }

        private void NewContact(object parameter)
        {
            ContactModel NewContact = new ContactModel();
            SelectedContact = NewContact;
            MessageBox.Show("Veuillez remplir les champs");
        }

    }
}
