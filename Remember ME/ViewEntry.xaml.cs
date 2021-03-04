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
using System.Xml.Serialization;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization.Formatters.Binary;
using System.Speech.Recognition;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace Remember_Me
{
    /// <summary>
    /// Interaction logic for ViewEntry.xaml
    /// </summary>
    public partial class ViewEntry : Window
    {
        private account Account = new account();
        static CultureInfo ci = new CultureInfo("en-us");
        static SpeechRecognitionEngine speech = new SpeechRecognitionEngine(ci);
        public ViewEntry()
        {
            InitializeComponent();
            FillDataGrid();
            LoadSettings();
            speech.SetInputToDefaultAudioDevice();
            speech.SpeechRecognized += sre_SpeechRecognized;
        }

        private void LoadSettings()
        {
            SettingsSave save = new SettingsSave();
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null))
            {
                if (!isoStore.FileExists("settings.dat"))
                {
                    System.Windows.MessageBox.Show("Settings not found please add settings"); //Make sure settings exist
                    Settigns settigns = new Settigns();
                    settigns.ShowDialog(); // no need to re run after return from settings view as settings class should be filled.
                }
                else
                {
                    IsolatedStorageFileStream fs = isoStore.OpenFile("settings.dat", System.IO.FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();
                    save = (SettingsSave)bf.Deserialize(fs);
                    SettingsClass.ExportF = save.ExportF;
                    SettingsClass.ImportF = save.ImportF;
                    fs.Close();
                }

            }
        }

        private void FillDataGrid()
        {
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null))
            {
                if (isoStore.FileExists("Autolog.dat"))
                {
                    IsolatedStorageFileStream fs = isoStore.OpenFile("Autolog.dat", System.IO.FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();
                    Account = (account)bf.Deserialize(fs);
                    fs.Close();
                }
            }
            string server = "server=127.0.0.1;user id=root;database=rememberme;password=Hermiston2017!";
            MySqlConnection con = new MySqlConnection(server);
            string query = "SELECT entry.Name, entry.Group FROM entry WHERE entry.username = @name";

            MySqlDataAdapter entryadapt = new MySqlDataAdapter(query,con);

            entryadapt.SelectCommand.Parameters.AddWithValue("@name", Account.User);

            MySqlCommandBuilder Entrycmd = new MySqlCommandBuilder(entryadapt);

            DataTable dt = new DataTable("Entries");

            entryadapt.Fill(dt);

            dt.DefaultView.Sort = "Group desc";
            dt = dt.DefaultView.ToTable();


            Entries.ItemsSource = dt.DefaultView;

            GrammarBuilder grammarBuilder = new GrammarBuilder();


            List<string> grammerList = new List<string>();
            
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                grammerList.Add("open " + dt.Rows[i][0].ToString());
            }

            grammerList.Add("import");
            grammerList.Add("exit");

            Choices values = new Choices(grammerList.ToArray());

            grammarBuilder.Append(values);

            speech.LoadGrammar(new Grammar(grammarBuilder));

        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView selected = (DataRowView)Entries.SelectedItems[0];

            string str = Convert.ToString(selected.Row.ItemArray[0]); //wonky but works, 

            System.Windows.Application.Current.Properties["Selected"] = str;
            System.Windows.Application.Current.Properties["Edited"] = false;

            DetailedView view = new DetailedView();
            view.ShowDialog();
            EntryClass.Picture = null; //Needed for import to work, without it, it probably wouldn't

            bool edit = (bool)System.Windows.Application.Current.Properties["Edited"];

            if(edit)
            {
                speech.UnloadAllGrammars();
                FillDataGrid();
            }
        }

        private void LoadImg_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog op = new Microsoft.Win32.OpenFileDialog();
            op.Title = "Select a Picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Portable Network Graphic (*.png)|*.png"; 
            //Might expand filter if needed, I.E. weird camera image file, needs testing
            if(op.ShowDialog() == true)
            {
                TempImg.Visibility = Visibility.Collapsed;
                EntryImg.Source = new BitmapImage(new Uri(op.FileName));
                EntryImg.Visibility = Visibility.Visible;
                EntryClass.Picture = null; 
            }
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            //Find a better way to do this if possible
            if (string.IsNullOrEmpty(EntryName.Text)) //Check for required data
            {
                System.Windows.MessageBox.Show("Missing Name of Entry");
            }
            else if (string.IsNullOrEmpty(EntryGroup.Text))
            {
                System.Windows.MessageBox.Show("Missing Group of Entry");
            }
            else if (string.IsNullOrEmpty(EntryDesc.Text))
            {
                System.Windows.MessageBox.Show("Missing Description of Entry");
            }
            else //Clear data
            {
                byte[] ImgData = null;
                string VidFile = null;
                if (EntryImg.Source != null && EntryClass.Picture == null)
                {
                    FileStream fs = new FileStream(((BitmapImage)EntryImg.Source).UriSource.OriginalString, FileMode.Open, FileAccess.Read);

                    BinaryReader br = new BinaryReader(fs);
                    ImgData = br.ReadBytes((int)fs.Length);

                    br.Close();
                    fs.Close();
                }
                else if(EntryClass.Picture != null)
                {
                    ImgData = EntryClass.Picture;
                }
                if (Video.Source != null && File.Exists(Video.Source.OriginalString))
                {
                    VidFile = Video.Source.OriginalString;
                }
                string server = "server=127.0.0.1;user id=root;database=rememberme;password=Hermiston2017!";
                MySqlConnection con = new MySqlConnection(server);

                string check = "SELECT * FROM entry WHERE entry.name = '" + EntryName.Text + "'"; //injection issue
                MySqlCommand cmd = new MySqlCommand(check, con);

                con.Open();

                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows) //even if this fails the data will still be sent, data problem
                {
                    System.Windows.MessageBox.Show("That Entry Name Already Exists");
                    con.Close();
                }
                else
                {
                    con.Close();
                    string query = "INSERT INTO `rememberme`.`entry`(`Name`,`Group`,`Description`,`Picture`,`AVPath`) VALUES(@Name, @Group, @Description, @Picture,@Path)";
                    cmd.CommandText = query;



                    cmd.Parameters.AddWithValue("@Name", EntryName.Text);
                    cmd.Parameters.AddWithValue("@Group", EntryGroup.Text);
                    cmd.Parameters.AddWithValue("@Description", EntryDesc.Text);
                    cmd.Parameters.AddWithValue("@Picture", ImgData); //could break maybe
                    cmd.Parameters.AddWithValue("@Path", VidFile);

                    con.Open();

                    int RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0)
                    {
                        System.Windows.MessageBox.Show("Entry Successfully Added");
                    }

                    con.Close();

                    FillDataGrid();

                    this.Clear_Click(sender, e); //it works for now, might be simpler to just make a non-event function
                    EntryClass.Picture = null;
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
            Video.Source = null;
            EntryImg.Visibility = Visibility.Collapsed;
            TempImg.Visibility = Visibility.Visible; //show default img
            PlayVideo.Visibility = Visibility.Hidden;
            PauseVideo.Visibility = Visibility.Hidden;
        }

        private void LoadVideo_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Do not delete or move the selected video after creating entry, or errors may occur"); //irritating for testing, only show once per new entry?
            Microsoft.Win32.OpenFileDialog op = new Microsoft.Win32.OpenFileDialog();
            op.Title = "Select a Picture";
            op.Filter = "*All Media Files|*.mp3;*.mp4;*.wmv";
            //WMV may not work for everything
            if (op.ShowDialog() == true)
            {
                Video.Source = new Uri(op.FileName);
                PlayVideo.Visibility = Visibility.Visible;
                PauseVideo.Visibility = Visibility.Visible;
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

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog op = new Microsoft.Win32.OpenFileDialog();
            op.Title = "Select an Entry";
            op.Filter = "XML Files|*.xml";
            op.InitialDirectory = SettingsClass.ImportF;
            if(op.ShowDialog() == true)
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

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Settigns settigns = new Settigns();
            settigns.Show();
        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string txt = e.Result.Text;
            float conf = e.Result.Confidence;
            bool edit = false;
            bool import = false;
            bool exit = false;
            if (conf >= 0.65) //Trying to avoid bad commands
            {
                VoiceText.Text = txt;

                if(txt.IndexOf("open") == 0)
                {
                    string selected = txt.Substring(5, txt.Length - 5); //it works

                    System.Windows.Application.Current.Properties["Selected"] = selected;
                    System.Windows.Application.Current.Properties["Edited"] = false;

                    DetailedView view = new DetailedView();
                    view.ShowDialog();
                    EntryClass.Picture = null;

                    edit = (bool)System.Windows.Application.Current.Properties["Edited"];
                }
                else if(txt.IndexOf("import") == 0)
                {
                    import = true;
                }
                else if(txt.IndexOf("exit") == 0)
                {
                    exit = true;
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Did not hear you clearly try again");
            }
            if (edit)
            {
                speech.UnloadAllGrammars(); //need to be put out here to avoid lag after closing detailed view. Why, I have no idea.
                FillDataGrid();
            }
            else if(import)
            {
                CreateTab.IsSelected = true;
                ButtonAutomationPeer peer = new ButtonAutomationPeer(Import);
                IInvokeProvider invoke = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invoke.Invoke();
            }
            else if(exit)
            {
                App.Current.Shutdown();
            }
        }

        private void VoiceCmd_Unchecked(object sender, RoutedEventArgs e)
        {
            speech.RecognizeAsyncStop();
        }            

        private void VoiceCmd_Checked(object sender, RoutedEventArgs e)
        {

            speech.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Account.Clear();
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null))
            {
                if (isoStore.FileExists("Autolog.dat")) //check if file exists
                {
                    isoStore.DeleteFile("Autolog.dat"); //delete file to make space for new Autolog
                }
            }
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }
    }
}
