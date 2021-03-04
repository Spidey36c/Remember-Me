using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

namespace Remember_Me
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Autolog();
        }

        private void Autolog()
        {
            bool success = false;
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null))
            {
                if (isoStore.FileExists("Autolog.dat")) //check if file exists
                {
                    IsolatedStorageFileStream fs = isoStore.OpenFile("Autolog.dat", System.IO.FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();
                    account Account = new account();
                    Account = (account)bf.Deserialize(fs);
                    string server = "server=127.0.0.1;user id=root;database=rememberme;password=Hermiston2017!";
                    MySqlConnection con = new MySqlConnection(server);

                    string check = "SELECT * FROM accounts WHERE accounts.username = @name AND accounts.password = @pass";
                    MySqlCommand cmd = new MySqlCommand(check, con);
                    cmd.Parameters.AddWithValue("@name", Account.User);
                    cmd.Parameters.AddWithValue("@pass", Account.Password);

                    fs.Close();
                    con.Open();

                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        con.Close();
                        ViewEntry viewEntry = new ViewEntry();
                        viewEntry.Show();
                        success = true;
                    }
                    else
                    {
                        con.Close();
                        MessageBox.Show("Autolog Failed Please log in manually"); //just in case a messup on iso storage
                    }
                }
            }

            if(success)
            {
                this.Close();
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string server = "server=127.0.0.1;user id=root;database=rememberme;password=Hermiston2017!";
            MySqlConnection con = new MySqlConnection(server);

            string check = "SELECT * FROM accounts WHERE accounts.username = @name AND accounts.password = @pass";
            MySqlCommand cmd = new MySqlCommand(check, con);
            cmd.Parameters.AddWithValue("@name", UsernameBox.Text); //hash this later, just for testing now
            cmd.Parameters.AddWithValue("@pass", Password.Password);

            con.Open();

            MySqlDataReader dataReader = cmd.ExecuteReader();
            if(dataReader.HasRows)
            {
                con.Close();
                account User = new account();
                User.User = UsernameBox.Text;
                User.Password = Password.Password; //needed for auto login later
                using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null))
                {
                    using (IsolatedStorageFileStream fs = isoStore.CreateFile("Autolog.dat")) //Create the new file
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(fs, User);
                    }
                }
                ViewEntry viewEntry = new ViewEntry();
                viewEntry.Show();
                this.Close();
            }
            else
            {
                con.Close();
                MessageBox.Show("Incorrect Username Or Password");// maybe spead it out and let them know if one or the other failed
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            CreateAccount createaccout = new CreateAccount();
            createaccout.ShowDialog(); 
        }
    }
}
