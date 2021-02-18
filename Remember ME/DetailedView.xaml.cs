using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
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
using System.Xml.Serialization;

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
            LoadEntry(); //Just to have the constructor less cluttered
        }

        private void LoadEntry()
        {
            string selected = (string)Application.Current.Properties["Selected"]; //Get Entry Name

            string server = "server=127.0.0.1;user id=root;database=rememberme;password=Hermiston2017!";
            MySqlConnection con = new MySqlConnection(server);

            string check = "SELECT * FROM entry WHERE entry.name = '" + selected + "'"; //Get entry from DB
            MySqlCommand cmd = new MySqlCommand(check, con);

            con.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            EntryClass.ID = reader.GetInt32("ID");
            EntryClass.Name = reader.GetString("Name");
            EntryClass.Group = reader.GetString("Group");
            EntryClass.Description = reader.GetString("Description");
            if (!reader.IsDBNull(reader.GetOrdinal("Picture"))) //Check if entry contains picture
                EntryClass.Picture = (byte[])reader["Picture"];
            else
                EntryClass.Picture = null;

            if (!reader.IsDBNull(reader.GetOrdinal("AVPath"))) //Check if entry contains Audio/Video
                EntryClass.FilePath = reader.GetString("AVPath");
            else
                EntryClass.FilePath = null;

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

            if (EntryClass.FilePath != null)
            {
                if (File.Exists(EntryClass.FilePath)) //try to find video
                {
                    Video.Source = new Uri(EntryClass.FilePath);
                    PlayVideo.Visibility = Visibility.Visible;
                    PauseVideo.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Video or Audio File could not be found, try editing the entry and reselecting the desired file");
                }
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
        }

        private void PlayVideo_Click(object sender, RoutedEventArgs e)
        {
            Video.Play(); //Simple but needed, I think
        }

        private void PauseVideo_Click(object sender, RoutedEventArgs e)
        {
            Video.Pause();
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            ExportClass export = new ExportClass();
            export.Name = EntryClass.Name;
            export.Description = EntryClass.Description;
            export.Group = EntryClass.Group;
            export.Picture = EntryClass.Picture; //whether this is null or not this should work

            string path = SettingsClass.ExportF + "\\" + export.Name + ".xml"; //create path to export file
            XmlSerializer serializer = new XmlSerializer(typeof(ExportClass));
            using(TextWriter write = new StreamWriter(@path)) //using avoids having to open and close the writer
            {
                serializer.Serialize(write, export);
            }


            if (File.Exists(path))
            {
                MessageBox.Show("Entry Successfully Exported");
            }
            else
            {
                MessageBox.Show("Failed to export entry");
            }
        }
    }
}
