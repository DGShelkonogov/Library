using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library2
{
    class Vidacha : INotifyPropertyChanged
    {
        string nameBook, fioWorker, dateOfVidachi;


        public int Id { get; set; }
        public Vidacha(int Id, string nameBook, string fioWorker, string dateOfVidachi)
        {
            this.Id = Id;
            this.nameBook = nameBook;
            this.fioWorker = fioWorker;
            this.dateOfVidachi = dateOfVidachi;

        }

        public string NameBook
        {
            get { return this.nameBook; }
            set
            {
                if (this.nameBook != value)
                {
                    this.nameBook = value;
                    this.NotifyPropertyChanged("NameBook");
                }
            }
        }

        public string FioWorker
        {
            get { return this.fioWorker; }
            set
            {
                if (this.fioWorker != value)
                {
                    this.fioWorker = value;
                    this.NotifyPropertyChanged("FioWorker");
                }
            }
        }

        public string DateOfVidachi
        {
            get { return this.dateOfVidachi; }
            set
            {
                if (this.dateOfVidachi != value)
                {
                    this.dateOfVidachi = value;
                    this.NotifyPropertyChanged("DateOfVidachi");
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
