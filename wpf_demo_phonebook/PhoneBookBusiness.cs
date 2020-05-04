using System;
using System.Collections.ObjectModel;
using System.Data;

namespace wpf_demo_phonebook
{
    static class PhoneBookBusiness
    {
        private static PhonebookDAO dao = new PhonebookDAO();

        public static ObservableCollection<ContactModel> GetContactByName(string _name)
        {
            ContactModel cm;

            DataTable dt = new DataTable();

            ObservableCollection<ContactModel> SearchResults = new ObservableCollection<ContactModel>();

            dt = dao.SearchByName(_name);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cm = RowToContactModel(row);
                    SearchResults.Add(cm);
                }
            }

            return SearchResults;
        }

        public static ContactModel GetContactByID(int _id)
        {
            ContactModel cm = null;

            DataTable dt = new DataTable();

            dt = dao.SearchByID(_id);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cm = RowToContactModel(row);
                }
            }

            return cm;
        }

        private static ContactModel RowToContactModel(DataRow row)
        {
            ContactModel cm = new ContactModel();

            cm.ContactID = Convert.ToInt32(row["ContactID"]);
            cm.FirstName = row["FirstName"].ToString();
            cm.LastName = row["LastName"].ToString();
            cm.Email = row["Email"].ToString();
            cm.Phone = row["Phone"].ToString();
            cm.Mobile = row["Mobile"].ToString();

            return cm;
        }

        public static ObservableCollection<ContactModel> GetAllContacts()
        {
            ContactModel cm;
            ObservableCollection<ContactModel> Contacts = new ObservableCollection<ContactModel>();
            DataTable dt = new DataTable();

            dt = dao.GetAll();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cm = RowToContactModel(row);
                    Contacts.Add(cm);
                }
            }
            return Contacts;
        }

        public static int UpdateContact(ContactModel cm)
        {
            int update;
            int id = cm.ContactID;
            update = dao.Update(cm, id);

            return update;
        }

        public static int DeleteContact(ContactModel cm)
        {
            int delete;
            int id = cm.ContactID;
            delete = dao.Delete(id);

            return delete;
        }

        public static int NewContact(ContactModel cm)
        {
            int id;

            dao.Insert(cm);
            id = dao.NewID();
            //id ++; //pour no unique, pas nécessaire finalement
            return id;
        }


    }
}
