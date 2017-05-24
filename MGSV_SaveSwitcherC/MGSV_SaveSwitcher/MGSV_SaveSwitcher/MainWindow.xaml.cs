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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SteamScan;

namespace MGSV_SaveSwitcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySteamScanner mySteamScan = new MySteamScanner();

        public MainWindow()
        {
            InitializeComponent();
            SteamScanner();
            
        }

        private void SteamScanner()
        {
            string steamPath = this.mySteamScan.ScanSteam();
            List<string> username = this.mySteamScan.UserScan();
            this.steamPath.Text = steamPath;
            foreach (string x in username)
            {
                this.userList.Items.Add(x);
            }
            /*
            if (username.Count < 2)
            {
                this.currentUser.Text = username[0];
            }
            */
            UserCheck();
        }
        /// <summary>
        /// When user is selected, enable save options
        /// </summary>
        private void UserCheck()
        {
            if (this.currentUser.Text != "")
            {
                this.newSaveName.IsReadOnly = false;
                this.newSaveName.Background = Brushes.White;
                this.saveDelList.IsEnabled = true;
                this.saveChangeList.IsEnabled = true;
                SaveListing();
            }
        }
        /// <summary>
        /// Add saves to the dropdown menus
        /// </summary>
        private void SaveListing()
        {
            List<string> saves = this.mySteamScan.SaveScan(this.currentUser.Text);
            foreach (string x in saves)
            {
                this.saveDelList.Items.Add(x);
                this.saveChangeList.Items.Add(x);
            }
        }
        /// <summary>
        /// Apply selected user from dropdown menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void applyUser_Click(object sender, RoutedEventArgs e)
        {
            string userSelection = this.userList.Text;
            this.userList.Text = "";
            this.currentUser.Text = userSelection;
            UserCheck();
        }

        /// <summary>
        /// Apply selected save file (save switching)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void applySaveChange_Click(object sender, RoutedEventArgs e)
        {
            string saveSelection = this.saveChangeList.Text;
            this.saveChangeList.Text = "";
            this.currentSave.Text = saveSelection;
        }

        /// <summary>
        /// Apply the name for new save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void applyNewSaveName_Click(object sender, RoutedEventArgs e)
        {
            string newSave = this.newSaveName.Text;
            this.newSaveName.Text = "";
            MessageBox.Show($"Create a new save with name '{newSave}'?", "Confirm", MessageBoxButton.YesNo);
            
            this.currentSave.Text = newSave;

        }

        /// <summary>
        /// Apply selected save to be deleted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void applyDelSave_Click(object sender, RoutedEventArgs e)
        {
            string saveSelection = this.saveDelList.Text;
            this.saveDelList.Text = "";
            String message = $"Delete save {saveSelection}?";
            
            MessageBox.Show($"Save '{saveSelection}' Deleted.", "Confirm",  MessageBoxButton.YesNo);
        }

        
    }
}
