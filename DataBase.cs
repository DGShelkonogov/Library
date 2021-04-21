    using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library2
{
    class DataBase
    {
        public const string TABLE_AVTORA = "AVTORA";
        public const string TABLE_DOLJNOSTI = "DOLJNOSTI";
        public const string TABLE_JANR_KNIGI = "JANR_KNIGI";
        public const string TABLE_KNIGA = "KNIGA";
        public const string TABLE_SOTRUDNIKI = "SOTRUDNIKI";
        public const string TABLE_TIP_PEREPLETA = "TIP_PEREPLETA";
        public const string TABLE_VIDACHA = "VIDACHA";

        public const string FAMILIYA = "Familiya";
        public const string COL_IMYA = "Imya";
        public const string COL_OTCHESTVO = "Otchestvo";
        public const string COL_NAIMENOVANIE = "Naimenovanie";

        public const string COL_ID_AVTORA = "ID_AVTORA";

        public const string COL_ID_DOLJNOSTI = "ID_DOLJNOSTI";
        public const string COL_OKLAD = "Oklad";

        public const string COL_ID_JANR_KNIGI = "ID_JANR_KNIGI";

        public const string COL_ID_KNIGI = "ID_KNIGI";
        public const string COL_AVTORA_ID = "AVTORA_ID";
        public const string COL_KOLICHESTVO_STRANIC = "Kolichestvo_stranic";
        public const string COL_TIP_PEREPLETA_ID = "TIP_PEREPLETA_ID";
        public const string COL_JANR_KNIGI_ID = "JANR_KNIGI_ID";
        public const string COL_IMYA_BOOK = "Imya_book";


        public const string COL_ID_SOTRUDNIKKA = "ID_SOTRUDNIKKA";
        public const string COL_SERIYA_PASPORTA = "Seriya_pasporta";
        public const string COL_NOMER_PASPORTA = "Nomer_Pasporta";
        public const string COL_ID_POSITION = "Doljnosti_ID";


        public const string COL_ID_TIP_PEREPLETA = "ID_TIP_PEREPLETA";
        public const string COL_TIP_PEREPLETA = "Naimenovanie_tip_perepleta";

        public const string COL_ID_VIDACHI = "ID_VIDACHI";
        public const string COL_DATA_VIDACHI = "Data_vidachi";
        public const string COL_KNIGI_ID = "KNIGI_ID";
        public const string CO_SOTRUDNIKKA_ID = "SOTRUDNIKKA_ID";






       public static string  cmdString;
       public static string  connectString = @"Data Source=HOME-PC\MSSQLSERVER_2;Initial Catalog=Labrary2;Integrated Security=True";
       public static SqlConnection connection;
       public static SqlCommand command;
       public static SqlDataReader dataReader;







        public static void LoadAuthorsFromDB(ObservableCollection<Author> authors)
        {
            authors.Clear();
            cmdString = "select * from " + DataBase.TABLE_AVTORA;

            command = new SqlCommand(cmdString, connection);

            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                authors.Add(new Author(
                Convert.ToInt32(dataReader[DataBase.COL_ID_AVTORA]),
                dataReader[DataBase.COL_IMYA].ToString(),
                dataReader[DataBase.FAMILIYA].ToString(),
                dataReader[DataBase.COL_OTCHESTVO].ToString()
               ));
            }
            cmdString = null;
            command = null;
            dataReader.Close();
        }




        public static void LoadGenreFromDB(ObservableCollection<Genre> genres)
        {
            genres.Clear();
            cmdString = "select * from " + DataBase.TABLE_JANR_KNIGI;

            command = new SqlCommand(cmdString, connection);

            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                genres.Add(new Genre(
                    Convert.ToInt32(dataReader[DataBase.COL_ID_JANR_KNIGI]),
                    dataReader[DataBase.COL_NAIMENOVANIE].ToString()));
            }
            cmdString = null;
            command = null;
            dataReader.Close();
        }


        public static void LoadBindingTypeFromDB(ObservableCollection<BindingType> bindingTypes)
        {
            bindingTypes.Clear();
            cmdString = "select * from " + DataBase.TABLE_TIP_PEREPLETA;

            command = new SqlCommand(cmdString, connection);

            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                bindingTypes.Add(new BindingType(
                     Convert.ToInt32(dataReader[DataBase.COL_ID_TIP_PEREPLETA]),
                    dataReader[DataBase.COL_TIP_PEREPLETA].ToString()));
            }
            cmdString = null;
            command = null;
            dataReader.Close();
        }



        public static void LoadWorkersFromDB(ObservableCollection<Worker> workers)
        {
            workers.Clear();
            cmdString = "select * from " +
                DataBase.TABLE_DOLJNOSTI + " inner join " + DataBase.TABLE_SOTRUDNIKI + " on " +
                DataBase.COL_ID_DOLJNOSTI + " = " + DataBase.COL_ID_POSITION;

            command = new SqlCommand(cmdString, connection);

            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                workers.Add(new Worker(
                Convert.ToInt32(dataReader[DataBase.COL_ID_SOTRUDNIKKA]),
                dataReader[DataBase.COL_IMYA].ToString(),
                dataReader[DataBase.FAMILIYA].ToString(),
                dataReader[DataBase.COL_OTCHESTVO].ToString(),
                dataReader[DataBase.COL_NAIMENOVANIE].ToString(),
                dataReader[DataBase.COL_SERIYA_PASPORTA].ToString(),
                dataReader[DataBase.COL_NOMER_PASPORTA].ToString()
               ));
            }
            cmdString = null;
            command = null;
            dataReader.Close();
        }

   


        public static void LoadBookFromDB(ObservableCollection<Book> books)
        {
            books.Clear();

            cmdString = "select * from " +
               DataBase.TABLE_AVTORA + " inner join " + DataBase.TABLE_KNIGA + " on " +
               DataBase.COL_ID_AVTORA + " = " + DataBase.COL_AVTORA_ID +
               " join " + DataBase.TABLE_JANR_KNIGI + " on " + DataBase.COL_ID_JANR_KNIGI + "=" + DataBase.COL_JANR_KNIGI_ID +
               " join " + DataBase.TABLE_TIP_PEREPLETA + " on " + DataBase.COL_ID_TIP_PEREPLETA + "=" + DataBase.COL_TIP_PEREPLETA_ID;


            command = new SqlCommand(cmdString, connection);

            dataReader = command.ExecuteReader();


            while (dataReader.Read())
            {
                string author = dataReader[DataBase.FAMILIYA].ToString()
                    + " " + dataReader[DataBase.COL_IMYA].ToString()
                     + " " + dataReader[DataBase.COL_OTCHESTVO].ToString();

                int IdBook = Convert.ToInt32(dataReader[DataBase.COL_ID_KNIGI]);
                string nameBook = dataReader[DataBase.COL_IMYA_BOOK].ToString();
                string authorName = dataReader[DataBase.COL_IMYA].ToString();
                string typeBinding = dataReader[DataBase.COL_TIP_PEREPLETA].ToString();
                string genre = dataReader[DataBase.COL_NAIMENOVANIE].ToString();
                 string kolstr = dataReader[DataBase.COL_KOLICHESTVO_STRANIC].ToString();


                books.Add(new Book(
                    IdBook, nameBook, authorName, typeBinding, genre, kolstr
               ));
            }
            cmdString = null;
            command = null;
            dataReader.Close();
        }



        public static void LoadVidachaFromDB(ObservableCollection<Vidacha> vidachas)
        {
            vidachas.Clear();

            cmdString = "select * from " +
               DataBase.TABLE_KNIGA + " inner join " + DataBase.TABLE_VIDACHA + " on " +
               DataBase.COL_ID_KNIGI + " = " + DataBase.COL_KNIGI_ID +
               " join " + DataBase.TABLE_SOTRUDNIKI + " on " + DataBase.COL_ID_SOTRUDNIKKA + "=" + DataBase.CO_SOTRUDNIKKA_ID;

            command = new SqlCommand(cmdString, connection);

            dataReader = command.ExecuteReader();



            while (dataReader.Read())
            {
                int idV = Convert.ToInt32(dataReader[DataBase.COL_ID_VIDACHI]);
                string nameBook = dataReader[DataBase.COL_IMYA_BOOK].ToString();
                string workerNameAuthor = dataReader[DataBase.COL_IMYA].ToString();
                string dateV = dataReader[DataBase.COL_DATA_VIDACHI].ToString();

                vidachas.Add(new Vidacha(idV, nameBook, workerNameAuthor, dateV));
            }
            cmdString = null;
            command = null;
            dataReader.Close();
        }

        public static void LoadPositionFromDB(ObservableCollection<Position> positions)
        {
            positions.Clear();
            cmdString = "select * from " + DataBase.TABLE_DOLJNOSTI;
            command = new SqlCommand(cmdString, connection);
            try
            {
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    positions.Add(new Position(
                         Convert.ToInt32(dataReader[DataBase.COL_ID_DOLJNOSTI]),
                        dataReader[DataBase.COL_NAIMENOVANIE].ToString(),
                        dataReader[DataBase.COL_OKLAD].ToString()));
            }
            catch { }
            finally
            {
                cmdString = null;
                command = null;
                dataReader.Close();
            }


        }









        public static void addWorkerInBD(Position pos, Worker worker)
        {
            try
            {
                cmdString = string.Format("insert into " +
                DataBase.TABLE_SOTRUDNIKI +

                "(" + DataBase.FAMILIYA + ", " + DataBase.COL_IMYA + ", " + DataBase.COL_OTCHESTVO
                + ", " + DataBase.COL_SERIYA_PASPORTA + ", " + DataBase.COL_NOMER_PASPORTA + ", "
                + DataBase.COL_ID_POSITION + ") " +

                "values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",

                worker.Surname, worker.Name,
                worker.Lastname, worker.PassportSeries,
                worker.PassportNumber, pos.Id);


                command = new SqlCommand(cmdString, connection);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch
                {

                }
            }
            catch (Exception e) { }

            command = null;
            cmdString = null;
        }


        public static void addBookInBD(BindingType type, Genre genre, Author author, Book book)
        {
           
            try
            {

               

                cmdString = string.Format("insert into " +

                    DataBase.TABLE_KNIGA +

                    "(" + DataBase.COL_AVTORA_ID + ", " + DataBase.COL_KOLICHESTVO_STRANIC + ", " + DataBase.COL_TIP_PEREPLETA_ID
                    + ", " + DataBase.COL_JANR_KNIGI_ID + ", " + DataBase.COL_IMYA_BOOK + ") " +

                    "values ('{0}', '{1}', '{2}', '{3}', '{4}')",

                    author.Id, book.Kolnumber_pages, type.Id, genre.Id, book.Name);
                command = new SqlCommand(cmdString, connection);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch{}
            }
            catch (Exception e) { }
            command = null;
            cmdString = null;
        }


        public static void addAuthorInBD(Author author)
        {
            try
            {
                cmdString = string.Format("insert into " +

                    DataBase.TABLE_AVTORA +

                    "(" + DataBase.FAMILIYA + ", " +
                    DataBase.COL_IMYA + ", " + DataBase.COL_OTCHESTVO + ") " +

                    "values ('{0}', '{1}', '{2}')",

                   author.Surname, author.Name,author.Lastname);


                command = new SqlCommand(cmdString, connection);
                try
                {
                    command.ExecuteNonQuery();


                }
                catch{}
            }
            catch (Exception e) { }
            command = null;
            cmdString = null;
        }


        public static void addVidachaInBD(Book book, Worker worker, Vidacha vidacha)
        {
           
            try
            {
                cmdString = string.Format("insert into " +

                DataBase.TABLE_VIDACHA +

                "(" + DataBase.COL_KNIGI_ID + ", " +
                DataBase.CO_SOTRUDNIKKA_ID + ", " + DataBase.COL_DATA_VIDACHI + ") " +

                "values ('{0}', '{1}', '{2}')",

                book.Id, worker.Id, vidacha.DateOfVidachi);
                command = new SqlCommand(cmdString, connection);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch{}
            }
            catch (Exception e) { }
            command = null;
            cmdString = null;
        }



        public static void addGenreInBD(Genre genre)
        {
            try
            {
                cmdString = string.Format("insert into " + DataBase.TABLE_JANR_KNIGI + "("
                    + DataBase.COL_NAIMENOVANIE + ") " +
                    "values ('{0}')", genre.Name);
                command = new SqlCommand(cmdString, connection);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch{}
            }
            catch (Exception e) { }
            command = null;
            cmdString = null;
        }


        public static void addBindingTypeInBD(BindingType type)
        {
            try
            {
                cmdString = string.Format("insert into " + DataBase.TABLE_TIP_PEREPLETA + "("
                    + DataBase.COL_TIP_PEREPLETA + ") " +
                    "values ('{0}')", type.Name);
                command = new SqlCommand(cmdString, connection);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch{}   
            }
            catch (Exception e) { }
            command = null;
            cmdString = null;
        }

        public static void addPositionInBD(Position position)
        {
           
            try
            {
                cmdString = string.Format("insert into " + DataBase.TABLE_DOLJNOSTI + "(" + DataBase.COL_NAIMENOVANIE + ", " + DataBase.COL_OKLAD + ") " +
                    "values ('{0}', '{1}')", position.Name, position.Salary);
                command = new SqlCommand(cmdString, connection);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch{}
            }
            catch (Exception e) { }
            command = null;
            cmdString = null;
        }









        public static void updateWorkerInBD(Worker worker)
        {

            cmdString = "UPDATE " + DataBase.TABLE_SOTRUDNIKI + " set " +
                 DataBase.FAMILIYA + " = '" + worker.Surname + "' , " +
                 DataBase.COL_IMYA + " = '" + worker.Name + "' , " +
                 DataBase.COL_OTCHESTVO + " = '" + worker.Lastname + "' , " +
                 DataBase.COL_SERIYA_PASPORTA + " = " + worker.PassportSeries + " , " +
                 DataBase.COL_NOMER_PASPORTA + " = " + worker.PassportNumber + " , " +
                 DataBase.COL_ID_POSITION + " = " + worker.Position +
                 " where " + DataBase.COL_ID_SOTRUDNIKKA + " = " + worker.Id;

            command = new SqlCommand(cmdString, connection);
            command.ExecuteNonQuery();


            command = null;
            cmdString = null;
        }


        public static void updateBookInBD(Book o, Author author, BindingType binding, Genre genre)
        {

            cmdString = "UPDATE " + DataBase.TABLE_KNIGA + " set " +
                DataBase.COL_AVTORA_ID + " = " + author.Id + " , " +
                DataBase.COL_KOLICHESTVO_STRANIC + " = '" + o.Kolnumber_pages + "' , " +
                DataBase.COL_TIP_PEREPLETA_ID + " = " + binding.Id + " , " +
                DataBase.COL_JANR_KNIGI_ID + " = " + genre.Id + " , " +
                DataBase.COL_IMYA_BOOK + " = '" + o.Name + "'" +
                " where " + DataBase.COL_ID_KNIGI + " = " + o.Id;

            command = new SqlCommand(cmdString, connection);
            command.ExecuteNonQuery();


            command = null;
            cmdString = null;
        }


        public static void updateAuthorInBD(Author o)
        {
            cmdString = "UPDATE " + DataBase.TABLE_AVTORA + " set " +
                DataBase.FAMILIYA + " = '" + o.Surname + "' , " +
                DataBase.COL_IMYA + " = '" + o.Name + "' , " +
                DataBase.COL_OTCHESTVO + " = '" + o.Lastname + "' " +
                " where " + DataBase.COL_ID_AVTORA + " = " + o.Id;

            command = new SqlCommand(cmdString, connection);
            command.ExecuteNonQuery();


            command = null;
            cmdString = null;
        }


        public static void updateVidachaInBD(Vidacha o, Book book, Worker worker)
        {

            cmdString = "UPDATE " + DataBase.TABLE_VIDACHA + " set " +
                DataBase.COL_KNIGI_ID + " = " + book.Id + " ," +
                DataBase.CO_SOTRUDNIKKA_ID + " = " + worker.Id + " ," + 
                DataBase.COL_DATA_VIDACHI + " = '" + o.DateOfVidachi + "' " +
                " where " + DataBase.COL_ID_VIDACHI + " = " + o.Id;

            command = new SqlCommand(cmdString, connection);
            command.ExecuteNonQuery();


            command = null;
            cmdString = null;
        }



        public static void updateGenreInBD(Genre o)
        {
            cmdString = "UPDATE " + DataBase.TABLE_JANR_KNIGI + " set " +
                DataBase.COL_NAIMENOVANIE + " = '" + o.Name + "' " +
                " where " + DataBase.COL_ID_JANR_KNIGI + " = " + o.Id;

            command = new SqlCommand(cmdString, connection);
            command.ExecuteNonQuery();


            command = null;
            cmdString = null;
        }


        public static void updateBindingTypeInBD(BindingType o)
        {
            cmdString = "UPDATE " + DataBase.TABLE_TIP_PEREPLETA + " set " +
                DataBase.COL_TIP_PEREPLETA + " = '" + o.Name + "' " +
                " where " + DataBase.COL_ID_TIP_PEREPLETA + " = " + o.Id;

            command = new SqlCommand(cmdString, connection);
            command.ExecuteNonQuery();


            command = null;
            cmdString = null;
        }

        public static void updatePositionInBD(Position o)
        {

            cmdString = "UPDATE " + DataBase.TABLE_DOLJNOSTI + " set " +
                DataBase.COL_NAIMENOVANIE + " = '" + o.Name + "' , " +
                DataBase.COL_OKLAD + " = " + o.Salary +
                " where " + DataBase.COL_ID_DOLJNOSTI + " = " + o.Id;

            command = new SqlCommand(cmdString, connection);
            command.ExecuteNonQuery();


            command = null;
            cmdString = null;
        }




        public static void deleteWorkerInBD(Worker o)
        {


            cmdString = "delete from " + DataBase.TABLE_SOTRUDNIKI + " where " + DataBase.COL_ID_SOTRUDNIKKA + " = " + o.Id;

            command = new SqlCommand(cmdString, connection);
            command.ExecuteNonQuery();
            command = null;
            cmdString = null;
        }


        public static void deleteBookInBD(Book o)
        {

            cmdString = "delete from " + DataBase.TABLE_KNIGA + " where " + DataBase.COL_ID_KNIGI + " = " + o.Id;

            command = new SqlCommand(cmdString, connection);
            command.ExecuteNonQuery();
            command = null;
            cmdString = null;
        }


        public static void deleteAuthorInBD(Author o)
        {
            cmdString = "delete from " + DataBase.TABLE_AVTORA + " where " + DataBase.COL_ID_AVTORA + " = " + o.Id;

            command = new SqlCommand(cmdString, connection);
            command.ExecuteNonQuery();
            command = null;
            cmdString = null;
        }


        public static void deleteVidachaInBD(Vidacha o)
        {
            cmdString = "delete from " + DataBase.TABLE_VIDACHA + " where " + DataBase.COL_ID_VIDACHI + " = " + o.Id;

            command = new SqlCommand(cmdString, connection);
            command.ExecuteNonQuery();
            command = null;
            cmdString = null;
        }



        public static void deleteGenreInBD(Genre o)
        {
            cmdString = "delete from " + DataBase.TABLE_JANR_KNIGI + " where " + DataBase.COL_ID_JANR_KNIGI + " = " + o.Id;

            command = new SqlCommand(cmdString, connection);
            command.ExecuteNonQuery();
            command = null;
            cmdString = null;
        }


        public static void deleteBindingTypeInBD(BindingType o)
        {
            cmdString = "delete from " + DataBase.TABLE_TIP_PEREPLETA + " where " + DataBase.COL_ID_TIP_PEREPLETA + " = " + o.Id;

            command = new SqlCommand(cmdString, connection);
            command.ExecuteNonQuery();
            command = null;
            cmdString = null;
        }

        public static void deletePositionInBD(Position o)
        {

            cmdString = "delete from " + DataBase.TABLE_DOLJNOSTI + " where " + DataBase.COL_ID_DOLJNOSTI + " = " + o.Id;

            command = new SqlCommand(cmdString, connection);
            command.ExecuteNonQuery();
            command = null;
            cmdString = null;
        }






    }
}
