using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection _connect;
        private ObservableCollection<User> users = new ObservableCollection<User>();
        private string conStr = ConfigurationManager.AppSettings["conStr"];
        public MainWindow()
        {
            InitializeComponent();
            _connect = new SqlConnection(conStr);
            //users.Add(new User() { FName = "qwe", LName = "rty" });
            //users.Add(new User() { FName = "asd", LName = "fgh" });

            //DataTable dt = new DataTable();
            //DataColumn id = new DataColumn("id", typeof(int));
            //DataColumn firstname = new DataColumn("firstname", typeof(string));
            //dt.Columns.Add(id);
            //dt.Columns.Add(firstname);


            ////lbUsers.ItemsSource = users;
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    _connect.Open();
            //    SqlCommand cmd = new SqlCommand("SELECT [Id],[FirstName]FROM[yurkissdb].[dbo].[exam_Students]", _connect);
            //    SqlDataReader rdr = cmd.ExecuteReader();
            //    while (rdr.Read())
            //    {
            //        DataRow row = dt.NewRow();
            //        row[0] = rdr["Id"];
            //        row[1] = rdr["FirstName"].ToString();
            //        dt.Rows.Add(row);
            //    }

            //    _connect.Close();
            //    scope.Complete();
            //}
            //DG.ItemsSource = dt.DefaultView;

            DG_Load();
        }
        public void DG_Load()
        {
            DataTable dt = new DataTable();
            DataColumn id = new DataColumn("id", typeof(int));
            DataColumn firstname = new DataColumn("firstname", typeof(string));
            dt.Columns.Add(id);
            dt.Columns.Add(firstname);

            using (TransactionScope scope = new TransactionScope())
            {
                _connect.Open();
                SqlCommand cmd = new SqlCommand("SELECT [Id],[FirstName]FROM[yurkissdb].[dbo].[exam_Students]", _connect);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    DataRow row = dt.NewRow();
                    row[0] = rdr["Id"];
                    row[1] = rdr["FirstName"].ToString();
                    dt.Rows.Add(row);
                }

                _connect.Close();
                scope.Complete();
            }
            DG.ItemsSource = dt.DefaultView;
        }
        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            users.Add(new User() { Name = "New user" });
        }

        private void btnChangeUser_Click(object sender, RoutedEventArgs e)
        {
            if (DG.SelectedItem != null)
            {
                (DG.SelectedItem as User).Name = "Random Name";
                using (TransactionScope sc =new TransactionScope())
                {
                    _connect.Open();
                    SqlCommand cmd = new SqlCommand($"UPDATE [dbo].[exam_Students]SET[FirstName] = '{DG.SelectedItems[1]}' WHERE [Id] = '{DG.SelectedItems[0]}'", _connect);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("update user");

                    _connect.Close();
                    sc.Complete();
                }
            }
            DG_Load();
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (DG.SelectedItem != null)
                users.Remove(DG.SelectedItem as User);
        }
    }
    public class User : INotifyPropertyChanged
    {
        private string name;
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
