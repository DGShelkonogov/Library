using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library2
{
    class Worker : INotifyPropertyChanged
    {
        string name, surname, lastname, position, salary;
        string passportSeries, passportNumber;

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

        public string PassportSeries
        {
            get { return this.passportSeries; }
            set
            {
                if (this.passportSeries != value)
                {
                    this.passportSeries = value;
                    this.NotifyPropertyChanged("PassportSeries");

                }
            }
        }

        public string PassportNumber
        {
            get { return this.passportNumber; }
            set
            {
                if (this.passportNumber != value)
                {
                    this.passportNumber = value;
                    this.NotifyPropertyChanged("PassportNumber");

                }
            }
        }

        public string Position
        {
            get { return this.position; }
            set
            {
                if (this.position != value)
                {
                    this.position = value;
                    this.NotifyPropertyChanged("Position");

                }
            }
        }

        public string Salary
        {
            get { return this.salary; }
            set
            {
                if (this.salary != value)
                {
                    this.salary = value;
                    this.NotifyPropertyChanged("Salary");

                }
            }
        }

        public Worker(int Id, string name, string surname, string lastname,
            string position, string passportSeries, string passportNumber
            )
        {
            this.Id = Id;
            this.name = name;
            this.surname = surname;
            this.lastname = lastname;
            this.position = position;
            this.passportSeries = passportSeries;
            this.passportNumber = passportNumber;

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
