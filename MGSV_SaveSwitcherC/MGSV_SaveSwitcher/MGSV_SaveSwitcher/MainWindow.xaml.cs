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
        public MainWindow()
        {
            InitializeComponent();
            SteamScanner();
        }

        private void SteamScanner()
        {
            MySteamScanner mySteamScan = new MySteamScanner();
            mySteamScan.ScanSteam();
        }

        private void applyUser_Click(object sender, RoutedEventArgs e)
        {
            string userSelection = this.userList.Text;
            this.userList.Text = "";
            this.currentUser.Text = userSelection;
        }

        private void applySaveChange_Click(object sender, RoutedEventArgs e)
        {
            string saveSelection = this.saveChangeList.Text;
            this.saveChangeList.Text = "";
            this.currentSave.Text = saveSelection;
        }

        private void applyNewSaveName_Click(object sender, RoutedEventArgs e)
        {
            string newSave = this.newSaveName.Text;
            this.newSaveName.Text = "";
            MessageBox.Show($"Create a new save with name '{newSave}'?", "Confirm", MessageBoxButton.YesNo);
            
            this.currentSave.Text = newSave;

        }

        private void applyDelSave_Click(object sender, RoutedEventArgs e)
        {
            string saveSelection = this.saveDelList.Text;
            this.saveDelList.Text = "";
            String message = $"Delete save {saveSelection}?";
            
            MessageBox.Show($"Save '{saveSelection}' Deleted.", "Confirm",  MessageBoxButton.YesNo);
        }
    }
}
