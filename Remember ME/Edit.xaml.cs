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
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;

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
            dataReader.Read();

            if (dataReader.HasRows && ((string)dataReader["Name"] != EntryClass.Name))
            {
                MessageBox.Show("That Entry Name Already Exists");
                con.Close();
            }
            else
            {
                con.Close(); //SQL injection issue

                byte[] ImgData = null;
                if (EntryImg.Source != null && EntryClass.Picture == null) //if there doesn't already exist a picture(or none) and if there is a new picture instead
                {
                    FileStream fs = new FileStream(((BitmapImage)EntryImg.Source).UriSource.OriginalString, FileMode.Open, FileAccess.Read);

                    BinaryReader br = new BinaryReader(fs);
                    ImgData = br.ReadBytes((int)fs.Length);

                    br.Close();
                    fs.Close();
                }
                else if (EntryClass.Picture != null)
                {
                    ImgData = EntryClass.Picture;
                }

                check = "UPDATE `rememberme`.`entry` SET `Name` = @Name, `Group` = @Group, `Description` = @Description, `Picture` = @Picture WHERE (`ID` = '" + EntryClass.ID /*I think this MAY be fine*/ + "')";
                cmd.CommandText = check;

                // just use this to fix above injection issue,
                cmd.Parameters.AddWithValue("@Name", EntryName.Text);
                cmd.Parameters.AddWithValue("@Group", EntryGroup.Text);
                cmd.Parameters.AddWithValue("@Description", EntryDesc.Text);
                cmd.Parameters.AddWithValue("@Picture", ImgData);

                EntryClass.Name = EntryName.Text;
                EntryClass.Group = EntryGroup.Text;
                EntryClass.Description = EntryDesc.Text;
                EntryClass.Picture = ImgData;

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

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select an Entry";
            op.Filter = "XML Files|*.xml";
            op.InitialDirectory = SettingsClass.ImportF;
            if (op.ShowDialog() == true)
            {
                XmlSerializer Deser = new XmlSerializer(typeof(ExportClass));
                TextReader reader = new StreamReader(op.FileName);
                object obj = Deser.Deserialize(reader);
                ExportClass ex = (ExportClass)obj; //unsure if this will work if xml deserialized is not of the same class
                reader.Close();
                EntryName.Text = ex.Name;
                EntryGroup.Text = ex.Group;
                EntryDesc.Text = ex.Description;

                if (ex.Picture != null)
                {
                    BitmapImage bi = new BitmapImage();

                    MemoryStream ms = new MemoryStream(ex.Picture);

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

                EntryClass.Picture = ex.Picture;
            }
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a Picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Portable Network Graphic (*.png)|*.png";
            //Might expand filter if needed, I.E. weird camera image file, needs testing
            if (op.ShowDialog() == true)
            {
                TempImg.Visibility = Visibility.Collapsed;
                EntryImg.Source = new BitmapImage(new Uri(op.FileName));
                EntryImg.Visibility = Visibility.Visible;
                EntryClass.Picture = null;
            }
        }
    }
}
