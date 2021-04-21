using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library2
{
    class Book : INotifyPropertyChanged
    {
        string name, authorfio, typeBinding, genre;
        string kolnumber_pages;

        public int Id { get; set; }

        public Book(int Id, string name, string authorfio, string typeBinding, string genre, string kolnumber_pages)
        {
            this.Id = Id;
            this.name = name;
            this.authorfio = authorfio;
            this.typeBinding = typeBinding;
            this.genre = genre;
            this.kolnumber_pages = kolnumber_pages;
        }

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

        public string Authorfio
        {
            get { return this.authorfio; }
            set
            {
                if (this.authorfio != value)
                {
                    this.authorfio = value;
                    this.NotifyPropertyChanged("Authorfio");
                }
            }
        }

        public string TypeBinding
        {
            get { return this.typeBinding; }
            set
            {
                if (this.typeBinding != value)
                {
                    this.typeBinding = value;
                    this.NotifyPropertyChanged("TypeBinding");
                }
            }
        }

        public string Genre
        {
            get { return this.genre; }
            set
            {
                if (this.genre != value)
                {
                    this.genre = value;
                    this.NotifyPropertyChanged("Genre");
                }
            }
        }

        public string Kolnumber_pages
        {
            get { return this.kolnumber_pages; }
            set
            {
                if (this.kolnumber_pages != value)
                {
                    this.kolnumber_pages = value;
                    this.NotifyPropertyChanged("Kolnumber_pages");
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
    }
}
