using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library2
{
    public partial class MainWindow : Window
    {


        ObservableCollection<Worker> workers = new ObservableCollection<Worker>();
        ObservableCollection<Position> positions = new ObservableCollection<Position>();
        ObservableCollection<Author> authors = new ObservableCollection<Author>();
        ObservableCollection<BindingType> bindingTypes = new ObservableCollection<BindingType>();
        ObservableCollection<Book> books = new ObservableCollection<Book>();
        ObservableCollection<Genre> genres = new ObservableCollection<Genre>();
        ObservableCollection<Vidacha> vidachas = new ObservableCollection<Vidacha>();

       

        public void updateBD()
        {
            DataBase.LoadWorkersFromDB(workers);
            DataBase.LoadGenreFromDB(genres);
            DataBase.LoadBindingTypeFromDB(bindingTypes);
            DataBase.LoadBookFromDB(books);
            DataBase.LoadVidachaFromDB(vidachas);
            DataBase.LoadAuthorsFromDB(authors);
            DataBase.LoadPositionFromDB(positions);
        }



        string activeTable = DataBase.TABLE_SOTRUDNIKI;

        public MainWindow()
        {
            InitializeComponent();
            labelTest.Content = "Сотрудники";
            DataBase.connection = new SqlConnection(DataBase.connectString);
            DataBase.connection.Open();

            updateBD();



            ListViewWorkers.ItemsSource = workers;
            ListViewPosition.ItemsSource = positions;
            ListViewBooks.ItemsSource = books;
            ListGenreBooks.ItemsSource = genres;
            ListBindingTypeBooks.ItemsSource = bindingTypes;
            ListViewAutors.ItemsSource = authors;
            ListViewClaim.ItemsSource = vidachas;

            ComboBoxPossitionWorkers.ItemsSource = positions;
            ComboBoxPossitionWorkers.DisplayMemberPath = "Name";

            ComboBoxBookTypeBinding.ItemsSource = bindingTypes;
            ComboBoxBookTypeBinding.DisplayMemberPath = "Name";

            ComboBoxBookGenre.ItemsSource = genres;
            ComboBoxBookGenre.DisplayMemberPath = "Name";

            ComboBoxBookAutor.ItemsSource = authors;
            ComboBoxBookAutor.DisplayMemberPath = "Name";

            ComboBoxBookID.ItemsSource = books;
            ComboBoxBookID.DisplayMemberPath = "Name";

            ComboBoxWorkerID.ItemsSource = workers;
            ComboBoxWorkerID.DisplayMemberPath = "Name";


            ButtonAdd.BorderBrush = Brushes.Gray;
        }


       

      


        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            
            if (ButtonAdd.BorderBrush == Brushes.Gray)
            {
                ButtonAdd.BorderBrush = Brushes.Green;
                ButtonDelete.IsEnabled = false;
                ButtonUpdate.IsEnabled = false;
                clearAll();
            }
            else
            {
                ButtonAdd.BorderBrush = Brushes.Gray;
                switch (activeTable)
                {
                    case DataBase.TABLE_SOTRUDNIKI:


                        if (TextBoxNameWorkers.Text.Length > 0 && TextBoxSurnameWorkers.Text.Length > 0
                                && TextBoxLastnameWorkers.Text.Length > 0 && TextBoxPassport_SeriesWorkers.Text.Length > 0
                                && TextBoxPassport_NumberWorkers.Text.Length > 0 && ComboBoxPossitionWorkers.SelectedItem != null)
                        {
                            Worker worker = new Worker(
                            workers.Count + 1,
                            TextBoxNameWorkers.Text,
                            TextBoxSurnameWorkers.Text,
                            TextBoxLastnameWorkers.Text,
                            ComboBoxPossitionWorkers.Text,
                            TextBoxPassport_SeriesWorkers.Text,
                            TextBoxPassport_NumberWorkers.Text
                         );
                            workers.Add(worker);
                            Position pos = (Position)ComboBoxPossitionWorkers.SelectedItem;
                            DataBase.addWorkerInBD(pos, worker);

                        }

                        break;
                       

                    case DataBase.TABLE_AVTORA:
                        {
                            if (TextBoxNameAuthor.Text.Length > 0 && TextBoxSurnameAuthor.Text.Length > 0
                                && TextBoxLastnameAuthor.Text.Length > 0)
                            {
                                Author author = new Author
                                   (
                                   authors.Count + 1,
                                   TextBoxNameAuthor.Text, TextBoxSurnameAuthor.Text, TextBoxLastnameAuthor.Text
                                   );
                                authors.Add(author);
                                DataBase.addAuthorInBD(author);
                            }
                            break;
                        }
                    case DataBase.TABLE_DOLJNOSTI:

                        {

                            if (TextBoxposition.Text.Length > 0 && TextBoxSalary.Text.Length > 0)
                            {
                                Position position = new Position(
                                 positions.Count + 1,
                                 TextBoxposition.Text,
                                 TextBoxSalary.Text
                                 );
                                positions.Add(position);
                                DataBase.addPositionInBD(position);
                            }

                               
                            break;
                        }
                    case DataBase.TABLE_VIDACHA:

                        {
                            if (ComboBoxBookID.SelectedItem != null && ComboBoxWorkerID.SelectedItem != null
                                 && TextBoxDateClaim.Text.Length > 0)
                            {

                                Vidacha vidacha = new Vidacha
                               (
                               vidachas.Count + 1,
                               ComboBoxBookID.Text,
                               ComboBoxWorkerID.Text,
                               TextBoxDateClaim.Text
                               );
                                vidachas.Add(vidacha);

                                Book book = (Book)ComboBoxBookID.SelectedItem;
                                Worker worker = (Worker)ComboBoxWorkerID.SelectedItem;
                                DataBase.addVidachaInBD(book, worker, vidacha);

                            }
                               



                            break;
                        }
                    case DataBase.TABLE_KNIGA:

                        {
                            if (TextBoxBookName.Text.Length > 0 && ComboBoxBookAutor.SelectedItem != null
                                && TextBoxBookSheets.Text.Length > 0 && ComboBoxBookGenre.SelectedItem != null
                                 && ComboBoxBookTypeBinding.SelectedItem != null)
                            {
                                



                                Book book = new Book
                                   (
                                   books.Count + 1,
                                   TextBoxBookName.Text,
                                   ComboBoxBookAutor.Text,
                                   ComboBoxBookTypeBinding.Text,
                                   ComboBoxBookGenre.Text,
                                   TextBoxBookSheets.Text
                                   );
                                books.Add(book);

                                BindingType type = (BindingType)ComboBoxBookTypeBinding.SelectedItem;
                                Genre genre = (Genre)ComboBoxBookGenre.SelectedItem;
                                Author author = (Author)ComboBoxBookAutor.SelectedItem;
                                DataBase.addBookInBD(type, genre, author, book);

                            }

                            break;
                        }
                    case DataBase.TABLE_TIP_PEREPLETA:
                        {

                            if (TextBoxBookTypeBinding.Text.Length > 0)
                            {
                                BindingType bindingType = new BindingType
                                   (
                                   bindingTypes.Count + 1,
                                   TextBoxBookTypeBinding.Text
                                   );
                                bindingTypes.Add(bindingType);
                                DataBase.addBindingTypeInBD(bindingType);
                            }
                            break;
                        }
                    case DataBase.TABLE_JANR_KNIGI:
                        {

                            if (TextBoxBookGenre.Text.Length > 0)
                            {

                                Genre genre = new Genre
                                   (
                                   genres.Count + 1,
                                   TextBoxBookGenre.Text
                                   );
                                genres.Add(genre);
                                DataBase.addGenreInBD(genre);
                            }
                               
                            break;
                        }
                }
                updateBD();
            }
                
        }



        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
                ButtonDelete.IsEnabled = false;
                ButtonUpdate.IsEnabled = false;
                switch (activeTable)
                {
                    case DataBase.TABLE_DOLJNOSTI:
                        if (ListViewPosition.SelectedItem != null)
                        {
                            Position o = (Position)ListViewPosition.SelectedItem;
                            DataBase.deletePositionInBD(o);
                        }

                        break;
                    case DataBase.TABLE_SOTRUDNIKI:
                        if (ListViewWorkers.SelectedItem != null)
                        {
                            Worker o = (Worker)ListViewWorkers.SelectedItem;
                            DataBase.deleteWorkerInBD(o);
                        }

                        break;
                    case DataBase.TABLE_AVTORA:
                        if (ListViewAutors.SelectedItem != null)
                        {
                            Author o = (Author)ListViewAutors.SelectedItem;
                            DataBase.deleteAuthorInBD(o);
                        }

                        break;
                    case DataBase.TABLE_TIP_PEREPLETA:
                        if (ListBindingTypeBooks.SelectedItem != null)
                        {
                            BindingType o = (BindingType)ListBindingTypeBooks.SelectedItem;
                            DataBase.deleteBindingTypeInBD(o);
                        }

                        break;
                    case DataBase.TABLE_JANR_KNIGI:
                        if (ListGenreBooks.SelectedItem != null)
                        {
                            Genre o = (Genre)ListGenreBooks.SelectedItem;
                            DataBase.deleteGenreInBD(o);
                        }

                        break;
                    case DataBase.TABLE_KNIGA:

                        if (ListViewBooks.SelectedItem != null)
                        {
                            Book o = (Book)ListViewBooks.SelectedItem;
                            DataBase.deleteBookInBD(o);
                        }
                        break;
                    case DataBase.TABLE_VIDACHA:
                        if (ListViewClaim.SelectedItem != null)
                        {
                            Vidacha o = (Vidacha)ListViewClaim.SelectedItem;
                            DataBase.deleteVidachaInBD(o);
                        }
                        break;
                }
                updateBD();
        }


        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            if (ButtonUpdate.Content.Equals("Update"))
            {
                ButtonUpdate.Content = "Edit";
                switch (activeTable)
                {
                    case DataBase.TABLE_DOLJNOSTI:
                        if (ListViewPosition.SelectedItem != null &&
                            TextBoxposition.Text.Length > 0 && TextBoxSalary.Text.Length > 0)
                        {
                            Position o = (Position)ListViewPosition.SelectedItem;
                            o.Name = TextBoxposition.Text;
                            o.Salary = TextBoxSalary.Text;
                            DataBase.updatePositionInBD(o);
                           
                        }

                        break;

                         
                    case DataBase.TABLE_SOTRUDNIKI:
                        if (ListViewWorkers.SelectedItem != null
                            && TextBoxNameWorkers.Text.Length > 0 && TextBoxSurnameWorkers.Text.Length > 0
                            && TextBoxLastnameWorkers.Text.Length > 0 && TextBoxPassport_SeriesWorkers.Text.Length > 0
                            && TextBoxPassport_NumberWorkers.Text.Length > 0 && ComboBoxPossitionWorkers.SelectedItem != null)
                        {

                            Worker o = (Worker)ListViewWorkers.SelectedItem;
                            Position pos = (Position)ComboBoxPossitionWorkers.SelectedItem;
                            o.Surname = TextBoxSurnameWorkers.Text;
                            o.Name = TextBoxNameWorkers.Text;
                            o.Lastname = TextBoxLastnameWorkers.Text;
                            o.PassportSeries = TextBoxPassport_SeriesWorkers.Text;
                            o.PassportNumber = TextBoxPassport_NumberWorkers.Text;
                            o.Position = pos.Id.ToString();
                            DataBase.updateWorkerInBD(o);
                           
                        }

                        break;
                    case DataBase.TABLE_AVTORA:
                        if (ListViewAutors.SelectedItem != null && TextBoxSurnameAuthor.Text.Length > 0
                            && TextBoxNameAuthor.Text.Length > 0 && TextBoxLastnameAuthor.Text.Length > 0)
                        {
                            Author o = (Author)ListViewAutors.SelectedItem;

                            o.Surname = TextBoxSurnameAuthor.Text;
                            o.Name = TextBoxNameAuthor.Text;
                            o.Lastname = TextBoxLastnameAuthor.Text;
                            DataBase.updateAuthorInBD(o);
                           
                        }

                        break;
                    case DataBase.TABLE_TIP_PEREPLETA:
                        if (ListBindingTypeBooks.SelectedItem != null && TextBoxBookTypeBinding.Text.Length > 0)
                        {
                            BindingType o = (BindingType)ListBindingTypeBooks.SelectedItem;
                            o.Name = TextBoxBookTypeBinding.Text;
                            DataBase.updateBindingTypeInBD(o);
                            

                        }

                        break;
                    case DataBase.TABLE_JANR_KNIGI:
                        if (ListGenreBooks.SelectedItem != null && TextBoxBookGenre.Text.Length > 0)
                        {
                            Genre o = (Genre)ListGenreBooks.SelectedItem;

                            o.Name = TextBoxBookGenre.Text;
                            DataBase.updateGenreInBD(o);
                           
                        }


                        break;
                    case DataBase.TABLE_KNIGA:

                        if (ListViewBooks.SelectedItem != null
                            && ComboBoxBookAutor.SelectedItem != null && TextBoxBookSheets.Text.Length > 0
                            && ComboBoxBookTypeBinding.SelectedItem != null && ComboBoxBookGenre.SelectedItem != null &&
                            TextBoxBookName.Text.Length > 0
                            )
                        {
                            Book o = (Book)ListViewBooks.SelectedItem;
                            Author author = (Author) ComboBoxBookAutor.SelectedItem;
                            BindingType binding = (BindingType) ComboBoxBookTypeBinding.SelectedItem;
                            Genre genre = (Genre) ComboBoxBookGenre.SelectedItem;


                            o.Authorfio = ComboBoxBookAutor.Text;
                            o.Kolnumber_pages = TextBoxBookSheets.Text;
                            o.TypeBinding = ComboBoxBookTypeBinding.Text;
                            o.Genre = ComboBoxBookGenre.Text;
                            o.Name = TextBoxBookName.Text;

                            DataBase.updateBookInBD(o, author, binding, genre);
                            
                        }
                        break;
                    case DataBase.TABLE_VIDACHA:
                        if (ListViewClaim.SelectedItem != null)
                        {
                            Vidacha o = (Vidacha)ListViewClaim.SelectedItem;
                        
                            Book book = (Book)ComboBoxBookID.SelectedItem;
                            Worker worker = (Worker)ComboBoxWorkerID.SelectedItem;
                            o.DateOfVidachi = TextBoxDateClaim.Text;
                            o.NameBook = book.Name;
                            o.FioWorker = worker.Name;
                            DataBase.updateVidachaInBD(o, book, worker);
                          
                        }
                        break;
                }
                updateBD();

                viewModeForWorkers(false);
                viewModeForAuthor(false);
                viewModeForBindingType(false);
                viewModeForBook(false);
                viewModeForGenre(false);
                viewModeForVidacha(false);
                viewModeForPosition(false);
                ButtonAdd.IsEnabled = true;
                ButtonDelete.IsEnabled = true;
                ButtonUpdate.IsEnabled = false;
            }
            else
            {
                ButtonUpdate.Content = "Update";
                ButtonAdd.IsEnabled = false;
                ButtonDelete.IsEnabled = false;
                viewModeForWorkers(true);
                viewModeForAuthor(true);
                viewModeForBindingType(true);
                viewModeForBook(true);
                viewModeForGenre(true);
                viewModeForVidacha(true);
                viewModeForPosition(true);
            }

        }















        //-----------------------------------------UI--------------------------------------------------------------------------------------------



        public void clearAll()
        {

            TextBoxNameWorkers.Text = "";
            TextBoxSurnameWorkers.Text = "";
            TextBoxLastnameWorkers.Text = "";
            TextBoxPassport_SeriesWorkers.Text = "";
            TextBoxPassport_NumberWorkers.Text = "";
            ComboBoxPossitionWorkers.Text = "";
            TextBoxBookName.Text = "";
            ComboBoxBookAutor.Text = "";
            TextBoxBookSheets.Text = "";
            ComboBoxBookTypeBinding.Text = "";
            ComboBoxBookGenre.Text = "";
            TextBoxNameAuthor.Text = "";
            TextBoxSurnameAuthor.Text = "";
            TextBoxLastnameAuthor.Text = "";
            TextBoxposition.Text = "";
            TextBoxSalary.Text = "";
            ComboBoxBookID.Text = "";
            ComboBoxWorkerID.Text = "";
            TextBoxDateClaim.Text = "";
            TextBoxBookGenre.Text = "";
            TextBoxBookTypeBinding.Text = "";
            viewModeForWorkers(true);
            viewModeForAuthor(true);
            viewModeForBindingType(true);
            viewModeForBook(true);
            viewModeForGenre(true);
            viewModeForVidacha(true);
            viewModeForPosition(true);
            ButtonUpdate.Content = "Edit";
        }


        private void Button_Click_Autors_DB(object sender, RoutedEventArgs e)
        {
            viewModeForAuthor(false);
          
            ButtonDelete.IsEnabled = false;
            ButtonUpdate.IsEnabled = false;
            ButtonAdd.IsEnabled = true;
            ButtonUpdate.Content = "Edit";
            ButtonAdd.BorderBrush = Brushes.Gray;
            activeTable = DataBase.TABLE_AVTORA;
            labelTest.Content = "Авторы";


          
            WorkersItems(Visibility.Hidden);
            BookItems(Visibility.Hidden);
            AutorItems(Visibility.Visible);
            PositionItems(Visibility.Hidden);
            ClaimItems(Visibility.Hidden);
            TypeBookBookItems(Visibility.Hidden);
            GenreBookItems(Visibility.Hidden);
        }

        private void Button_Click_Position_DB(object sender, RoutedEventArgs e)
        {
            viewModeForPosition(false);
  
            activeTable = DataBase.TABLE_DOLJNOSTI;
            ButtonDelete.IsEnabled = false;
            ButtonUpdate.IsEnabled = false;
            ButtonAdd.IsEnabled = true;
            ButtonUpdate.Content = "Edit";
            ButtonAdd.BorderBrush = Brushes.Gray;
            labelTest.Content = "Должнасти";

            WorkersItems(Visibility.Hidden);
            BookItems(Visibility.Hidden);
            AutorItems(Visibility.Hidden);
            PositionItems(Visibility.Visible);
            ClaimItems(Visibility.Hidden);
            TypeBookBookItems(Visibility.Hidden);
            GenreBookItems(Visibility.Hidden);
        }

        private void Button_Click_Claim_DB(object sender, RoutedEventArgs e)
        {
            viewModeForVidacha(false);
  
            activeTable = DataBase.TABLE_VIDACHA;
            ButtonDelete.IsEnabled = false;
            ButtonUpdate.IsEnabled = false;
            ButtonAdd.IsEnabled = true;
            ButtonUpdate.Content = "Edit";
            ButtonAdd.BorderBrush = Brushes.Gray;
            labelTest.Content = "Выдача";

            WorkersItems(Visibility.Hidden);
            BookItems(Visibility.Hidden);
            AutorItems(Visibility.Hidden);
            PositionItems(Visibility.Hidden);
            ClaimItems(Visibility.Visible);
            TypeBookBookItems(Visibility.Hidden);
            GenreBookItems(Visibility.Hidden);
        }

        private void Button_Click_Worker_DB(object sender, RoutedEventArgs e)
        {
            viewModeForWorkers(false);
            activeTable = DataBase.TABLE_SOTRUDNIKI;
            ButtonDelete.IsEnabled = false;
            ButtonUpdate.IsEnabled = false;
            ButtonAdd.IsEnabled = true;
            ButtonUpdate.Content = "Edit";
            ButtonAdd.BorderBrush = Brushes.Gray;
            labelTest.Content = "Сотрудники";

            WorkersItems(Visibility.Visible);
            BookItems(Visibility.Hidden);
            AutorItems(Visibility.Hidden);
            PositionItems(Visibility.Hidden);
            ClaimItems(Visibility.Hidden);
            TypeBookBookItems(Visibility.Hidden);
            GenreBookItems(Visibility.Hidden);
        }

        private void Button_Click_Book_DB(object sender, RoutedEventArgs e)
        {
            viewModeForBook(false);
            activeTable = DataBase.TABLE_KNIGA;
            ButtonDelete.IsEnabled = false;
            ButtonUpdate.IsEnabled = false;
            ButtonAdd.IsEnabled = true;
            ButtonUpdate.Content = "Edit";
            ButtonAdd.BorderBrush = Brushes.Gray;
            labelTest.Content = "Книги";

            WorkersItems(Visibility.Hidden);
            BookItems(Visibility.Visible);
            AutorItems(Visibility.Hidden);
            PositionItems(Visibility.Hidden);
            ClaimItems(Visibility.Hidden);
            TypeBookBookItems(Visibility.Hidden);
            GenreBookItems(Visibility.Hidden);
        }

        private void Button_Click_Book_Genre_DB(object sender, RoutedEventArgs e)
        {
            viewModeForGenre(false);
            activeTable = DataBase.TABLE_JANR_KNIGI;
            ButtonDelete.IsEnabled = false;
            ButtonUpdate.IsEnabled = false;
            ButtonAdd.IsEnabled = true;
            ButtonUpdate.Content = "Edit";
            ButtonAdd.BorderBrush = Brushes.Gray;
            labelTest.Content = "Жанры";

            WorkersItems(Visibility.Hidden);
            BookItems(Visibility.Hidden);
            AutorItems(Visibility.Hidden);
            PositionItems(Visibility.Hidden);
            ClaimItems(Visibility.Hidden);
            TypeBookBookItems(Visibility.Hidden);
            GenreBookItems(Visibility.Visible);
        }
        private void Button_Click_Book_Binding_DB(object sender, RoutedEventArgs e)
        {
            viewModeForBindingType(false);
            activeTable = DataBase.TABLE_TIP_PEREPLETA;
            ButtonDelete.IsEnabled = false;
            ButtonUpdate.IsEnabled = false;
            ButtonAdd.IsEnabled = true;
            ButtonUpdate.Content = "Edit";
            ButtonAdd.BorderBrush = Brushes.Gray;
            labelTest.Content = "Типы переплета";


            WorkersItems(Visibility.Hidden);
            BookItems(Visibility.Hidden);
            AutorItems(Visibility.Hidden);
            PositionItems(Visibility.Hidden);
            ClaimItems(Visibility.Hidden);
            TypeBookBookItems(Visibility.Visible);
            GenreBookItems(Visibility.Hidden);
        }




        public void WorkersItems(Visibility visibility)
        {
            ListViewWorkers.Visibility = visibility;

            TextBoxNameWorkers.Visibility = visibility;
            TextBoxSurnameWorkers.Visibility = visibility;
            TextBoxLastnameWorkers.Visibility = visibility;
            TextBoxPassport_SeriesWorkers.Visibility = visibility;
            TextBoxPassport_NumberWorkers.Visibility = visibility;
            ComboBoxPossitionWorkers.Visibility = visibility;


            LabelNameWorkers.Visibility = visibility;
            LabelSurnameWorkers.Visibility = visibility;
            LabellastnameWorkers.Visibility = visibility;
            LabelSerPasWorkers.Visibility = visibility;
            LabelNumPasWorkers.Visibility = visibility;
            LabelTextBoxPossitionWorkers.Visibility = visibility;
        }

        public void BookItems(Visibility visibility)
        {
            ListViewBooks.Visibility = visibility;

            TextBoxBookName.Visibility = visibility;
            ComboBoxBookAutor.Visibility = visibility;
            TextBoxBookSheets.Visibility = visibility;
            ComboBoxBookTypeBinding.Visibility = visibility;
            ComboBoxBookGenre.Visibility = visibility;

            LabelBookName.Visibility = visibility;
            LabelBookAutor.Visibility = visibility;
            LabelBookSheets.Visibility = visibility;
            LabelBookTypeBinding.Visibility = visibility;
            LabelBookGenre.Visibility = visibility;
        }

        public void AutorItems(Visibility visibility)
        {
            ListViewAutors.Visibility = visibility;

            TextBoxNameAuthor.Visibility = visibility;
            TextBoxSurnameAuthor.Visibility = visibility;
            TextBoxLastnameAuthor.Visibility = visibility;

            LabelNameAuthor.Visibility = visibility;
            LabelSurnameAuthor.Visibility = visibility;
            LabelLastnameAuthor.Visibility = visibility;
        }

        public void PositionItems(Visibility visibility)
        {
            ListViewPosition.Visibility = visibility;

            TextBoxposition.Visibility = visibility;
            TextBoxSalary.Visibility = visibility;

            Labelposition.Visibility = visibility;
            LabelSalary.Visibility = visibility;
        }

        public void ClaimItems(Visibility visibility)
        {
            ListViewClaim.Visibility = visibility;

            ComboBoxBookID.Visibility = visibility;
            ComboBoxWorkerID.Visibility = visibility;
            TextBoxDateClaim.Visibility = visibility;

            LabelBookID.Visibility = visibility;
            LabelWorkerID.Visibility = visibility;
            LabelDateClaim.Visibility = visibility;
        }

        public void GenreBookItems(Visibility visibility)
        {
            ListGenreBooks.Visibility = visibility;

            TextBoxBookGenre.Visibility = visibility;

            LabelTXBookGenre.Visibility = visibility;
        }

        public void TypeBookBookItems(Visibility visibility)
        {
            ListBindingTypeBooks.Visibility = visibility;

            TextBoxBookTypeBinding.Visibility = visibility;

            LabelBookBinding.Visibility = visibility;
        }


        public void viewModeForWorkers(bool isEnabled)
        {
            TextBoxNameWorkers.IsEnabled = isEnabled;
            TextBoxSurnameWorkers.IsEnabled = isEnabled;
            TextBoxLastnameWorkers.IsEnabled = isEnabled;
            TextBoxPassport_SeriesWorkers.IsEnabled = isEnabled;
            TextBoxPassport_NumberWorkers.IsEnabled = isEnabled;
            ComboBoxPossitionWorkers.IsEnabled = isEnabled;
        }

        public void viewModeForBook(bool isEnabled)
        {
            TextBoxBookName.IsEnabled = isEnabled;
            ComboBoxBookAutor.IsEnabled = isEnabled;
            TextBoxBookSheets.IsEnabled = isEnabled;
            ComboBoxBookTypeBinding.IsEnabled = isEnabled;
            ComboBoxBookGenre.IsEnabled = isEnabled;
        }

        public void viewModeForAuthor(bool isEnabled)
        {
            TextBoxNameAuthor.IsEnabled = isEnabled;
            TextBoxSurnameAuthor.IsEnabled = isEnabled;
            TextBoxLastnameAuthor.IsEnabled = isEnabled;
        }

        public void viewModeForPosition(bool isEnabled)
        {
            TextBoxposition.IsEnabled = isEnabled;
            TextBoxSalary.IsEnabled = isEnabled;
        }

        public void viewModeForVidacha(bool isEnabled)
        {
            ComboBoxBookID.IsEnabled = isEnabled;
            ComboBoxWorkerID.IsEnabled = isEnabled;
            TextBoxDateClaim.IsEnabled = isEnabled;
        }

        public void viewModeForGenre(bool isEnabled)
        {
            TextBoxBookGenre.IsEnabled = isEnabled;

        }

        public void viewModeForBindingType(bool isEnabled)
        {
            TextBoxBookTypeBinding.IsEnabled = isEnabled;

        }
        //-------------------------------------------------------------------------SELECTLISTVIRE-----------------
        private void SelectWorker(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewWorkers.SelectedItem != null)
            {
                ButtonAdd.BorderBrush = Brushes.Gray;
                viewModeForWorkers(false);
                ButtonDelete.IsEnabled = true;
                ButtonUpdate.IsEnabled = true;

            
                Worker worker = (Worker)ListViewWorkers.SelectedItem;
                TextBoxNameWorkers.Text = worker.Name;
                TextBoxSurnameWorkers.Text = worker.Surname;
                TextBoxLastnameWorkers.Text = worker.Lastname;
                TextBoxPassport_SeriesWorkers.Text = worker.PassportSeries;
                TextBoxPassport_NumberWorkers.Text = worker.PassportNumber;
                ComboBoxPossitionWorkers.Text = worker.Position;
            }
        }

        private void SelectPosition(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewPosition.SelectedItem != null)
            {
                ButtonAdd.BorderBrush = Brushes.Gray;
                viewModeForPosition(false);
                ButtonDelete.IsEnabled = true;
                ButtonUpdate.IsEnabled = true;
                Position position = (Position)ListViewPosition.SelectedItem;
                TextBoxposition.Text = position.Name;
                TextBoxSalary.Text = position.Salary;
            }
        }
        private void SelectBook(object sender, SelectionChangedEventArgs e)
        {

            if (ListViewBooks.SelectedItem != null)
            {
                ButtonAdd.BorderBrush = Brushes.Gray;
                viewModeForBook(false);
                ButtonDelete.IsEnabled = true;
                ButtonUpdate.IsEnabled = true;


                Book book = (Book)ListViewBooks.SelectedItem;
                TextBoxBookName.Text = book.Name;
                TextBoxBookSheets.Text = book.Kolnumber_pages;

                ComboBoxBookAutor.Text = book.Authorfio;
                ComboBoxBookTypeBinding.Text = book.TypeBinding;
                ComboBoxBookGenre.Text = book.Genre;
            }
               

        }

       

        private void SelectVidacha(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewClaim.SelectedItem != null)
            {
                ButtonAdd.BorderBrush = Brushes.Gray;
                viewModeForVidacha(false);
                ButtonDelete.IsEnabled = true;
                ButtonUpdate.IsEnabled = true;


                Vidacha vidacha = (Vidacha)ListViewClaim.SelectedItem;
                ComboBoxBookID.Text = vidacha.NameBook;
                ComboBoxWorkerID.Text = vidacha.FioWorker;
                TextBoxDateClaim.Text = vidacha.DateOfVidachi;
            }
          
        }

        private void SelectAuthor(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewAutors.SelectedItem != null)
            {
                ButtonAdd.BorderBrush = Brushes.Gray;
                viewModeForAuthor(false);
                ButtonDelete.IsEnabled = true;
                ButtonUpdate.IsEnabled = true;

            
                Author author = (Author)ListViewAutors.SelectedItem;
                TextBoxNameAuthor.Text = author.Name;
                TextBoxSurnameAuthor.Text = author.Surname;
                TextBoxLastnameAuthor.Text = author.Lastname;

            }
        }


        private void SelectTypeBindingBook(object sender, SelectionChangedEventArgs e)
        {
            if (ListBindingTypeBooks.SelectedItem != null)
            {
                ButtonAdd.BorderBrush = Brushes.Gray;
                viewModeForBindingType(false);
                ButtonDelete.IsEnabled = true;
                ButtonUpdate.IsEnabled = true;
                BindingType t = (BindingType)ListBindingTypeBooks.SelectedItem;
                TextBoxBookTypeBinding.Text = t.Name;
            }
        }

        private void SelectGenreBook(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (ListGenreBooks.SelectedItem != null)
            {
                ButtonAdd.BorderBrush = Brushes.Gray;
                viewModeForGenre(false);
                ButtonDelete.IsEnabled = true;
                ButtonUpdate.IsEnabled = true;


                Genre genre = (Genre)ListGenreBooks.SelectedItem;
                TextBoxBookGenre.Text = genre.Name;
            }
         
           
        }


    }
}
