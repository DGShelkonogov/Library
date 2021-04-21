using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library2
{
    class Position : INotifyPropertyChanged
    {

        string name, salary;
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

        public Position(int Id, string name, string salary)
        {
            this.Id = Id;
            this.name = name;
            this.salary = salary;
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
