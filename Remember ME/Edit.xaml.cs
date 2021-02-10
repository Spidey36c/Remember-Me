using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        public Edit()
        {
            InitializeComponent();

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

            if (EntryClass.FilePath != null)
            {
                if (File.Exists(EntryClass.FilePath))
                {
                    Video.Source = new Uri(EntryClass.FilePath);
                    PlayVideo.Visibility = Visibility.Visible;
                    PauseVideo.Visibility = Visibility.Visible;
                }
            }
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            string server = "server=127.0.0.1;user id=root;database=rememberme;password=Hermiston2017!";
            MySqlConnection con = new MySqlConnection(server);


            string check = "SELECT * FROM entry WHERE entry.name = '" + EntryName.Text + "'"; //SQL injection issue
            MySqlCommand cmd = new MySqlCommand(check, con);

            con.Open();

            MySqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.HasRows && ((string)dataReader["Name"] != EntryClass.Name))
            {
                MessageBox.Show("That Entry Name Already Exists");
                con.Close();
            }
            else
            {
                con.Close(); //SQL injection issue
                check = "UPDATE `rememberme`.`entry` SET `Name` = '" + EntryName.Text /* make this @name*/ + "', `Group` = '" + EntryGroup.Text + "', `Description` = '" + EntryDesc.Text + "' WHERE (`ID` = '" + EntryClass.ID /*I think this MAY be fine*/ + "')";
                cmd.CommandText = check;

                // just use this to fix above injection issue,
                cmd.Parameters.AddWithValue("@Name", EntryName.Text);
                cmd.Parameters.AddWithValue("@Group", EntryGroup.Text);
                cmd.Parameters.AddWithValue("@Description", EntryDesc.Text);

                EntryClass.Name = EntryName.Text;
                EntryClass.Group = EntryGroup.Text;
                EntryClass.Description = EntryDesc.Text;

                con.Open();

                int RowsAffected = cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    MessageBox.Show("Entry Successfully Edited");
                }

                con.Close();

                Application.Current.Properties["Edited"] = true;
                this.Close();
            }
        }

        private void PlayVideo_Click(object sender, RoutedEventArgs e)
        {
            Video.Play(); //Simple but needed, I think
        }

        private void PauseVideo_Click(object sender, RoutedEventArgs e)
        {
            Video.Pause();
        }
    }
}
