using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library2
{
    class Author : INotifyPropertyChanged
    {
        string name, surname, lastname;


        public int Id { get; set; }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.NotifyPropertyChanged("Name");
                }
            }
        }

        public string Surname
        {
            get { return this.surname; }
            set
            {
                if (this.surname != value)
                {
                    this.surname = value;
                    this.NotifyPropertyChanged("Surname");

                }
            }
        }

        public string Lastname
        {
            get { return this.lastname; }
            set
            {
                if (this.lastname != value)
                {
                    this.lastname = value;
                    this.NotifyPropertyChanged("Lastname");

                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string fieldName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(fieldName));
            }
        }
        public Author(int Id, string name, string surname, string lastname)
        {
            this.Id = Id;
            this.name = name;
            this.surname = surname;
            this.lastname = lastname;
        }


    }
}