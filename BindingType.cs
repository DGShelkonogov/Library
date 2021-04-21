

using System.ComponentModel;

namespace Library2
{
    class BindingType : INotifyPropertyChanged
    {
        string name;

        public event PropertyChangedEventHandler PropertyChanged;

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


        public BindingType(int Id, string name)
        {
            this.name = name;
            this.Id = Id;
        }

     
        public void NotifyPropertyChanged(string fieldName)
        {
            if (this.PropertyChanged != null)
            {
                  this.PropertyChanged(this, new PropertyChangedEventArgs(fieldName));
            }
        }
    }
}
