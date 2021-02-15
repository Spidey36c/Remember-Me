using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Remember_Me
{
    /// <summary>
    /// Interaction logic for Settigns.xaml
    /// </summary>
    public partial class Settigns : Window
    {
        public Settigns() //Honestly I don't exacly know how this works without issue
        {
            InitializeComponent();
            ImportFolder.Text = SettingsClass.ImportF;
            ExportFolder.Text = SettingsClass.ExportF;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(ImportFolder.Text) || string.IsNullOrEmpty(ExportFolder.Text))
            {
                System.Windows.MessageBox.Show("Default Folders can't be empty"); //ensure that there exists some default folder
            }
            else
            {
                if(ImportFolder.Text != SettingsClass.ImportF)
                {
                    SettingsClass.ImportF = ImportFolder.Text; //assign default folders to class
                }

                if(ExportFolder.Text != SettingsClass.ExportF)
                {
                    SettingsClass.ExportF = ExportFolder.Text;
                }

                SaveSettings(); //save settings to file
            }
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e) //needed in case user closes using the X button instead
        {
            if (string.IsNullOrEmpty(ImportFolder.Text) || string.IsNullOrEmpty(ExportFolder.Text))
            {
                e.Cancel = true; //stop window from closing if empty defaults
                System.Windows.MessageBox.Show("Default Folders can't be empty");
            }
            else
            {
                if (ImportFolder.Text != SettingsClass.ImportF)
                {
                    SettingsClass.ImportF = ImportFolder.Text;
                }

                if (ExportFolder.Text != SettingsClass.ExportF)
                {
                    SettingsClass.ExportF = ExportFolder.Text;
                }
                SaveSettings();
            }
        }

        private void SaveSettings()
        {
            SettingsSave save = new SettingsSave(); //Can't Serialize a Static file,
            save.ExportF = SettingsClass.ExportF;
            save.ImportF = SettingsClass.ImportF;
            using(IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null))
            { 
                if(isoStore.FileExists("settings.dat")) //check if file exists
                {
                    isoStore.DeleteFile("settings.dat"); //delete file to make space for new settings
                }
                using(IsolatedStorageFileStream fs = isoStore.CreateFile("settings.dat")) //Create the new file
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, save);
                }
            }
        }

        private void ImportBrowse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog(); //browse folders
            DialogResult dialogResult = new DialogResult();
            dialogResult = folderBrowser.ShowDialog();
            if(dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                ImportFolder.Text = folderBrowser.SelectedPath;
            }
        }

        private void ExportBrowse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult dialogResult = new DialogResult();
            dialogResult = folderBrowser.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                ExportFolder.Text = folderBrowser.SelectedPath;
            }
        }

        private void Test_Click(object sender, RoutedEventArgs e)//test starting app without settings 
        {
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null))
            {
                if (isoStore.FileExists("settings.dat")) 
                {
                    isoStore.DeleteFile("settings.dat");
                }
            }
        }
    }
}
