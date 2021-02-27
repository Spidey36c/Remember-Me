using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Remember_Me
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : Window
    {
        public CreateAccount()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string server = "server=127.0.0.1;user id=root;database=rememberme;password=Hermiston2017!";
            MySqlConnection con = new MySqlConnection(server);

            string check = "SELECT * FROM accounts WHERE accounts.username = @name";
            MySqlCommand cmd = new MySqlCommand(check, con);
            cmd.Parameters.AddWithValue("@name", UsernameBox.Text);

            con.Open();

            MySqlDataReader dataReader = cmd.ExecuteReader(); //check if it exists
            if (dataReader.HasRows)
            {
                con.Close();
                MessageBox.Show("Username already exists"); //don't want repeated usernames
            }
            else
            {
                con.Close();
                string query = "INSERT INTO `rememberme`.`accounts`(`username`,`password`) VALUES(@user, @userpass)";
                cmd.CommandText = query;

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@user", UsernameBox.Text); //hash later, test now
                cmd.Parameters.AddWithValue("@userpass", Password.Password);

                con.Open();

                int RowsAffected = cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    System.Windows.MessageBox.Show("User Successfully Added");
                }

                con.Close();
                this.Close();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
