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
using System.Diagnostics;
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
            Console.WriteLine("Line 42");
            if (username.Count < 2)
            {
                this.currentUser.Text = username[0];
                this.currentSave.Text = this.mySteamScan.CurrentSave(this.currentUser.Text);
                this.userList.IsEnabled = false;
                this.applyUser.IsEnabled = false;
            }
            Console.WriteLine("Line 50");
            if (!this.mySteamScan.SaveScan(this.currentUser.Text).Any())
            {
                Console.WriteLine("Line 53");
                if (this.currentUser.Text == "")
                {
                    Console.WriteLine("Line 55");
                    this.currentUser.Text = "Chooce your username from the user list below.";
                } else
                {
                    Console.WriteLine("Line 59");
                    this.mySteamScan.FirstSave("Original", this.currentUser.Text);
                    UserCheck();
                }
            } else if (this.mySteamScan.CurrentSave(this.currentUser.Text) != this.currentSave.Text)
            {
                Console.WriteLine("Line 65");
                this.mySteamScan.SaveSwitch(this.mySteamScan.SaveScan(this.currentUser.Text)[0], this.currentUser.Text);
                UserCheck();
            } else
            {
                Console.WriteLine("Line 70");
                UserCheck();
            }   
        }

        /// <summary>
        /// When user is selected, enable save options
        /// </summary>
        private void UserCheck()
        {
            Console.WriteLine("User check");
            if (this.currentUser.Text != "")
            {
                this.newSaveName.IsReadOnly = false;
                this.newSaveName.Background = Brushes.White;
                
                this.currentSave.Text = this.mySteamScan.CurrentSave(this.currentUser.Text);
                this.RenameSaveName.IsReadOnly = false;
                this.RenameSaveName.Background = Brushes.White;
                this.ApplyRename.IsEnabled = true;
                SaveListing();
            }
        }
        /// <summary>
        /// Add saves to the dropdown menus
        /// </summary>
        private void SaveListing()
        {
            List<string> saves = this.mySteamScan.SaveScan(this.currentUser.Text);

            if (saves.Count < 2)
            {
                this.saveDelList.IsEnabled = false;
                this.applyDelSave.IsEnabled = false;
                this.applySaveChange.IsEnabled = false;
                this.saveChangeList.IsEnabled = false;
            } else
            {
                this.saveDelList.IsEnabled = true;
                this.applyDelSave.IsEnabled = true;
                this.applySaveChange.IsEnabled = true;
                this.saveChangeList.IsEnabled = true;
            }

            List<string> delList = new List<string>();
            foreach (string y in this.saveDelList.Items)
            {
                if (!saves.Contains(y)) {
                    delList.Add(y);
                }
            }
            foreach (string x in delList)
            {
                this.saveDelList.Items.Remove(x);
            }

            delList = new List<string>();
            foreach (string y in this.saveChangeList.Items)
            {
                if (!saves.Contains(y))
                {
                    delList.Add(y);
                }
            }
            foreach (string x in delList)
            {
                this.saveChangeList.Items.Remove(x);
            }


            foreach (string x in saves)
            {
                if (!this.saveDelList.Items.Contains(x))
                {
                    this.saveDelList.Items.Add(x);
                }
                if (!this.saveChangeList.Items.Contains(x))
                {
                    this.saveChangeList.Items.Add(x);
                }
            }

            foreach (string x in this.saveChangeList.Items)
            {
                Console.WriteLine(x);
            }
        }
        /// <summary>
        /// Apply selected user from dropdown menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyUser_Click(object sender, RoutedEventArgs e)
        {
            string userSelection = this.userList.Text;
            this.userList.Text = "";
            this.currentUser.Text = userSelection;
            if (this.currentUser.Text == "Chooce your username from the user list below.")
            {
                this.mySteamScan.FirstSave("Original", this.currentUser.Text);
            }
            
            UserCheck();
        }

        /// <summary>
        /// Apply selected save file (save switching)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplySaveChange_Click(object sender, RoutedEventArgs e)
        {
            string saveSelection = this.saveChangeList.Text;
            if (saveSelection.Trim() != "")
            {
                this.saveChangeList.Text = "";
                this.currentSave.Text = saveSelection;
                this.mySteamScan.SaveSwitch(this.currentSave.Text, this.currentUser.Text);
            }
            
            SaveListing();
        }

        /// <summary>
        /// Apply the name for new save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyNewSaveName_Click(object sender, RoutedEventArgs e)
        {

            string newSave = this.newSaveName.Text;
            
            if (newSave.Trim() == "")
            {
                MessageBox.Show("Please enter a valid name.");
            } else
            {
                MessageBoxResult msgResult = MessageBox.Show($"Create a new save with name '{newSave}'?", "Confirm", MessageBoxButton.YesNo);
                if (msgResult == MessageBoxResult.Yes)
                {
                    this.currentSave.Text = newSave;
                    this.mySteamScan.NewSave(newSave, this.currentUser.Text);
                    this.newSaveName.Text = "";
                }
                else if (msgResult == MessageBoxResult.No)
                {
                    MessageBox.Show("Operation cancelled, no changes made.");
                }
            }

            SaveListing();
  
        }

        /// <summary>
        /// Apply selected save to be deleted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyDelSave_Click(object sender, RoutedEventArgs e)
        {
            string saveSelection = this.saveDelList.Text;
            if (this.saveDelList.Text.Trim() == "")
            {
                MessageBox.Show("No save selected.");
            } else
            {
                this.saveDelList.Text = "";
                string message = $"Delete save {saveSelection}?";

                MessageBoxResult msgResult = MessageBox.Show($"Delete '{saveSelection}' ?", "Confirm", MessageBoxButton.YesNo);
                if (msgResult == MessageBoxResult.Yes)
                {
                    this.mySteamScan.DeleteSave(saveSelection, this.currentUser.Text);
                    this.currentSave.Text = this.mySteamScan.CurrentSave(this.currentUser.Text);

                }
                else if (msgResult == MessageBoxResult.No)
                {
                    MessageBox.Show("Operation cancelled, no changes made.");
                }
            }
            SaveListing();
        }

        private void SaveScan_Click(object sender, RoutedEventArgs e)
        {
            SaveListing();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void ApplyRename_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgResult = MessageBox.Show($"Rename '{this.currentSave.Text}' to '{this.RenameSaveName}' ?", "Confirm", MessageBoxButton.YesNo);
            if (msgResult == MessageBoxResult.Yes)
            {
                this.mySteamScan.RenameSave(this.currentSave.Text, this.currentUser.Text, this.RenameSaveName.Text);
                this.currentSave.Text = this.mySteamScan.CurrentSave(this.currentUser.Text);
            }
            else if (msgResult == MessageBoxResult.No)
            {
                MessageBox.Show("Operation cancelled, no changes made.");
            }
            this.RenameSaveName.Text = "";
            
        }
    }
}
