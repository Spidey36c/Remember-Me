using Microsoft.Win32;
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
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.IO;

namespace Remember_Me
{
    /// <summary>
    /// Interaction logic for ViewEntry.xaml
    /// </summary>
    public partial class ViewEntry : Window
    {
        public ViewEntry()
        {
            InitializeComponent();
        }

        private void LoadImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a Picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Portable Network Graphic (*.png)|*.png"; 
            //Might expand filter if needed, I.E. weird camera image file, needs testing
            if(op.ShowDialog() == true)
            {
                EntryImg.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            //Find a better way to do this if possible
            if (string.IsNullOrEmpty(EntryName.Text)) //Check for required data
            {
                MessageBox.Show("Missing Name of Entry");
            }
            else if (string.IsNullOrEmpty(EntryGroup.Text))
            {
                MessageBox.Show("Missing Group of Entry");
            }
            else if (string.IsNullOrEmpty(EntryDesc.Text))
            {
                MessageBox.Show("Missing Description of Entry");
            }
            else //Clear data
            {
                FileStream fs = new FileStream(((BitmapImage)EntryImg.Source).UriSource.OriginalString, FileMode.Open, FileAccess.Read);

                BinaryReader br = new BinaryReader(fs);
                byte[] ImgData = br.ReadBytes((int)fs.Length);

                br.Close();
                fs.Close();

                string server = "server=127.0.0.1;user id=root;database=rememberme;password=Hermiston2017!";
                MySqlConnection con = new MySqlConnection(server);
                string query = "INSERT INTO `rememberme`.`entry`(`Name`,`Group`,`Description`,`Picture`) VALUES(@Name, @Group, @Description, @Picture)";
                MySqlCommand cmd = new MySqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Name", EntryName.Text);
                cmd.Parameters.AddWithValue("@Group", EntryGroup.Text);
                cmd.Parameters.AddWithValue("@Description", EntryDesc.Text);
                cmd.Parameters.AddWithValue("@Picture", ImgData);

                con.Open();

                int RowsAffected = cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    MessageBox.Show("Entry Successfully Added");
                }

                con.Close();

                this.Clear_Click(sender, e); //it works for now, might be simpler to just make a non-event function
                ViewTab.IsSelected = true; //Switch views
            }


        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        { //Clear all data
            EntryName.Text = "";
            EntryGroup.Text = "";
            EntryDesc.Text = "";
            EntryImg.Source = null;
        }
    }
}
