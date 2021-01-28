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
using System.Data;
using System.Collections;

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
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            string server = "server=127.0.0.1;user id=root;database=rememberme;password=Hermiston2017!";
            MySqlConnection con = new MySqlConnection(server);
            string query = "SELECT entry.Name, entry.Group, entry.Description FROM entry";

            MySqlDataAdapter entryadapt = new MySqlDataAdapter(query,con);

            MySqlCommandBuilder Entrycmd = new MySqlCommandBuilder(entryadapt);

            DataTable dt = new DataTable("Entries");

            entryadapt.Fill(dt);

            Entries.ItemsSource = dt.DefaultView;
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView selected = (DataRowView)Entries.SelectedItems[0];

            string str = Convert.ToString(selected.Row.ItemArray[0]); //wonky but works, 

            Application.Current.Properties["Selected"] = str;
            Application.Current.Properties["Edited"] = false;

            DetailedView view = new DetailedView();
            view.ShowDialog();
        }

        private void LoadImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a Picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Portable Network Graphic (*.png)|*.png"; 
            //Might expand filter if needed, I.E. weird camera image file, needs testing
            if(op.ShowDialog() == true)
            {
                TempImg.Visibility = Visibility.Collapsed;
                EntryImg.Source = new BitmapImage(new Uri(op.FileName));
                EntryImg.Visibility = Visibility.Visible;
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
                byte[] ImgData = null;
                if (EntryImg.Source != null)
                {
                    FileStream fs = new FileStream(((BitmapImage)EntryImg.Source).UriSource.OriginalString, FileMode.Open, FileAccess.Read);

                    BinaryReader br = new BinaryReader(fs);
                    ImgData = br.ReadBytes((int)fs.Length);

                    br.Close();
                    fs.Close();
                }
                string server = "server=127.0.0.1;user id=root;database=rememberme;password=Hermiston2017!";
                MySqlConnection con = new MySqlConnection(server);

                string check = "SELECT * FROM entry WHERE entry.name = '" + EntryName.Text + "'";
                MySqlCommand cmd = new MySqlCommand(check, con);

                con.Open();

                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    MessageBox.Show("That Entry Name Already Exists");
                    con.Close();
                }
                else
                {
                    con.Close();
                    string query = "INSERT INTO `rememberme`.`entry`(`Name`,`Group`,`Description`,`Picture`) VALUES(@Name, @Group, @Description, @Picture)";
                    cmd.CommandText = query;

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

                    FillDataGrid();

                    this.Clear_Click(sender, e); //it works for now, might be simpler to just make a non-event function
                    ViewTab.IsSelected = true; //Switch views
                }
            }


        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        { //Clear all data
            EntryName.Text = "";
            EntryGroup.Text = "";
            EntryDesc.Text = "";
            EntryImg.Source = null;
            EntryImg.Visibility = Visibility.Collapsed;
            TempImg.Visibility = Visibility.Visible;
        }
    }
}
