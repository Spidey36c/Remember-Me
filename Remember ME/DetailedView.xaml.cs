using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    /// Interaction logic for DetailedView.xaml
    /// </summary>
    public partial class DetailedView : Window
    {
        public DetailedView()
        {
            InitializeComponent();
            string selected = (string)Application.Current.Properties["Selected"];

            string query = "SELECT * FROM entry WHERE entry.name = '" + selected + "'";

            string server = "server=127.0.0.1;user id=root;database=rememberme;password=Hermiston2017!";
            MySqlConnection con = new MySqlConnection(server);

            string check = "SELECT * FROM entry WHERE entry.name = '" + selected + "'";
            MySqlCommand cmd = new MySqlCommand(check, con);

            con.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            EntryClass.ID = reader.GetInt32("ID");
            EntryClass.Name = reader.GetString("Name");
            EntryClass.Group = reader.GetString("Group");
            EntryClass.Description = reader.GetString("Description");
            if (!reader.IsDBNull(reader.GetOrdinal("Picture")))
                EntryClass.Picture = (byte[])reader["Picture"];
            else
                EntryClass.Picture = null;

            reader.Close();

            con.Close();

            EntryName.Text = EntryClass.Name;
            EntryGroup.Text = EntryClass.Group;
            EntryDesc.Text = EntryClass.Description;

            if (EntryClass.Picture != null)
            {
                BitmapImage bi = new BitmapImage();

                MemoryStream ms = new MemoryStream(EntryClass.Picture);

                ms.Position = 0;

                bi.BeginInit();
                bi.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.UriSource = null;
                bi.StreamSource = ms;
                bi.EndInit();
                bi.Freeze();

                TempImg.Visibility = Visibility.Collapsed;
                EntryImg.Source = bi;
                EntryImg.Visibility = Visibility.Visible;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); //just another way to close window
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Edit edit = new Edit();

            edit.ShowDialog();
            if ((bool)Application.Current.Properties["Edited"] == true)
            {
                EntryName.Text = EntryClass.Name;
                EntryGroup.Text = EntryClass.Group;
                EntryDesc.Text = EntryClass.Description;
            }
        }
    }
}
