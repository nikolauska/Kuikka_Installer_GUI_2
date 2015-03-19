using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Drawing;
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

namespace Kuikka_Installer_GUI_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {       
        Briefing briefing;
        VisibilityHandler VisibilityHandler;
        InstallHandler installHandler;

        public MainWindow()
        {
            InitializeComponent();

            briefing = new Briefing();
            installHandler = new InstallHandler(this, briefing);
            VisibilityHandler = new VisibilityHandler(this);            
            VisibilityHandler.showProfileSettings();

            TextBox_Briefing_Text.IsReadOnly = false;
            TextBox_Briefing_Text.AcceptsReturn = true;

            // Get arma profilefolders
            String arma3Folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Arma 3";
            String arma3OtherFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Arma 3 - Other Profiles";

            // Find default arma profilename
            if (Directory.Exists(arma3Folder))
            {
                string[] files = Directory.GetFiles(arma3Folder, "*.Arma3Profile");

                string filename = new DirectoryInfo(files[0]).Name;
                string[] names = filename.Split('.');

                Combobox_Profile_Name.Items.Add(names[0]);
            }

            // Find all custom arma profilenames
            if (Directory.Exists(arma3OtherFolder))
            {
                string[] subdirectoryEntries = Directory.GetDirectories(arma3OtherFolder);
                foreach (string subdirectory in subdirectoryEntries)
                {
                    Combobox_Profile_Name.Items.Add(new DirectoryInfo(subdirectory).Name);
                }

            }

            // Set all combobox to first value
            ComboBox_Briefing_Side.SelectedIndex = 0;
            ComboBox_Briefing_Title.SelectedIndex = 0;
            Combobox_Mission_Gametype.SelectedIndex = 0;
            Combobox_Profile_Name.SelectedIndex = 0;
            Combobox_Mission_Map.SelectedIndex = 0;
            Combobox_Profile_Editor.SelectedIndex = 0;
        }

        /***************************************************************************************************/
        // Menu buttons
        private void Button_Center_Profile_Click(object sender, RoutedEventArgs e)
        {
            VisibilityHandler.showProfileSettings();
        }

        private void Button_Center_Mission_Click(object sender, RoutedEventArgs e)
        {
            VisibilityHandler.showMissionSettings();
        }

        private void Button_Center_Loading_Click(object sender, RoutedEventArgs e)
        {
            VisibilityHandler.showLoadingSettings();
        }

        private void Center_Briefing_Btn_Click(object sender, RoutedEventArgs e)
        {
            VisibilityHandler.showBriefingSettings();
        }

        private void Center_Install_Btn_Click(object sender, RoutedEventArgs e)
        {
            VisibilityHandler.showSetup();
            TextBox_Setup_Text.Text = "";

            installHandler.StartInstall();
        }

        /***************************************************************************************************/
        // Profile settings
        private void Combobox_Profile_Editor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            installHandler.EditorIndex = Combobox_Profile_Editor.SelectedIndex;
        }

        private void Combobox_Profile_Name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            installHandler.NameIndex = Combobox_Profile_Name.SelectedIndex;
            installHandler.profileName = Combobox_Profile_Name.SelectedItem.ToString();
        }

        /***************************************************************************************************/
        // Missions settings
        private void Combobox_Mission_Gametype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeCombo = (ComboBoxItem)Combobox_Mission_Gametype.SelectedItem;

            switch (typeCombo.Content.ToString())
            {
                case "COOP":
                    Label_Mision_Gametype.Content = "Cooperative Mission";
                    break;
                case "DM":
                    Label_Mision_Gametype.Content = "Deathmatch";
                    break;
                case "TDM":
                    Label_Mision_Gametype.Content = "Team Deathmatch";
                    break;
                case "CTF":
                    Label_Mision_Gametype.Content = "Capture The Flag";
                    break;
                case "SC":
                    Label_Mision_Gametype.Content = "Sector Control";
                    break;
                case "CTI":
                    Label_Mision_Gametype.Content = "Capture The Island";
                    break;
                case "RPG":
                    Label_Mision_Gametype.Content = "Role-Playing Game";
                    break;
                case "SANDBOX":
                    Label_Mision_Gametype.Content = "Sandbox";
                    break;
                case "SEIZE":
                    Label_Mision_Gametype.Content = "Seize";
                    break;
                case "DEFEND":
                    Label_Mision_Gametype.Content = "Defend";
                    break;
                case "ZDM":
                    Label_Mision_Gametype.Content = "Zeus - Deathmatch";
                    break;
                case "ZCTF":
                    Label_Mision_Gametype.Content = "Zeus - Capture The Flag";
                    break;
                case "ZCOOP":
                    Label_Mision_Gametype.Content = "Zeus - Cooperative Mission";
                    break;
                case "ZSC":
                    Label_Mision_Gametype.Content = "Zeus - Sector Control";
                    break;
                case "ZCTI":
                    Label_Mision_Gametype.Content = "Zeus - Capture The Island";
                    break;
                case "ZTDM":
                    Label_Mision_Gametype.Content = "Zeus - Team Deathmatch";
                    break;
                case "ZRPG":
                    Label_Mision_Gametype.Content = "Zeus - Role Playing Game";
                    break;
                case "ZGM":
                    Label_Mision_Gametype.Content = "Zeus - Game Master";
                    break;
                case "ZVZ":
                    Label_Mision_Gametype.Content = "Zeus vs. Zeus";
                    break;
                case "ZVP":
                    Label_Mision_Gametype.Content = "Zeus vs. Players";
                    break;
            }

            installHandler.gameType = typeCombo.Content.ToString();
        }

        private void Combobox_Mission_Map_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeCombo = (ComboBoxItem)Combobox_Mission_Map.SelectedItem;

            String Map = "";
            switch (typeCombo.Content.ToString())
            {
                case "Altis":
                    Map = "Altis";
                    break;
                case "Stratis":
                    Map = "Stratis";
                    break;
                case "Chernarus":
                    Map = "Chernarus";
                    break;
                case "Chernarus Summer":
                    Map = "Chernarus_Summer";
                    break;
                case "Utes":
                    Map = "Utes";
                    break;
                case "Takistan":
                    Map = "Takistan";
                    break;
                case "Takistan Mountains":
                    Map = "Mountains_ACR";
                    break;
                case "Shapur":
                    Map = "Shapur_BAF";
                    break;
                case "Proving Grounds":
                    Map = "ProvingGrounds_PMC";
                    break;
                case "Desert":
                    Map = "Desert_E";
                    break;
                case "Bukovina":
                    Map = "Bootcamp_ACR";
                    break;
                case "Bystrica":
                    Map = "Woodland_ACR";
                    break;
                case "Zargabad":
                    Map = "Zargabad";
                    break;
                case "Rahmadi":
                    Map = "Intro";
                    break;
                case "Porto":
                    Map = "Porto";
                    break;
                case "United Sahrani":
                    Map = "Sara_dbe1";
                    break;
                case "Southern Sahrani":
                    Map = "SaraLite";
                    break;
                case "Sahrani":
                    Map = "Sara";
                    break;
                case "Thirsk":
                    Map = "Thirsk";
                    break;
                case "Thirsk Winter":
                    Map = "ThirskW";
                    break;
                case "Virtual Reality":
                    Map = "VR";
                    break;
            }

            installHandler.MissionMap = Map;
        }

        private void TextBox_Mission_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            installHandler.missionName = TextBox_Mission_Name.Text;
        }

        private void TextBox_Mission_PlayerMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            installHandler.MaxPlayers = TextBox_Mission_PlayerMax.Text;
        }

        /***************************************************************************************************/
        // Loadscreen settings
        private void TextBox_Loading_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            installHandler.LoadingText = TextBox_Loading_Text.Text;
        }

        private void TextBox_Loading_Author_TextChanged(object sender, TextChangedEventArgs e)
        {
            installHandler.LoadingAuthor = TextBox_Loading_Author.Text;
        }

        private void TextBox_Loading_Image_TextChanged(object sender, TextChangedEventArgs e)
        {
            installHandler.LoadingImage = TextBox_Loading_Image.Text;
        }

        private void Button_Loading_Picture_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog() { Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png" };
            var result = ofd.ShowDialog();
            if (result == false) return;

            BitmapImage img = new BitmapImage(new Uri(ofd.FileName));

            var imageHeight = img.Height;
            var imageWidth = img.Width;

            if (imageHeight == imageWidth)
            {
                if (imageHeight % 2 == 0 && imageWidth % 2 == 0)
                {
                    TextBox_Loading_Image.Text = ofd.FileName;
                }
                else
                {
                    MessageBox.Show("Kuvan korkeus ja leveys pitää olla kahdella jaollinen! Esim 128x128, 256x256, 512x512");
                }
            }
            else
            {
                MessageBox.Show("Kuvan korkeus ja leveys pitää olla sama! Esim 128x128, 256x256, 512x512");
            }
        }

        /***************************************************************************************************/
        // Briefing settings
        private void TextBox_Briefing_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.BriefingTextEdit("SET"); 
        }

        private void ComboBox_Briefing_Title_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_Briefing_Title.SelectedValue != null && ComboBox_Briefing_Side.SelectedValue != null)
                this.BriefingTextEdit("GET");
        }

        private void ComboBox_Briefing_Side_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_Briefing_Title.SelectedValue != null && ComboBox_Briefing_Side.SelectedValue != null)
                this.BriefingTextEdit("GET"); 
        }

        private void BriefingTextEdit(String type)
        {

            ComboBoxItem sideCombo = (ComboBoxItem)ComboBox_Briefing_Side.SelectedItem;
            ComboBoxItem briefingCombo = (ComboBoxItem)ComboBox_Briefing_Title.SelectedItem;

            if (type.Equals("GET"))
            {
                TextBox_Briefing_Text.Text = briefing.getText(sideCombo.Content.ToString(), briefingCombo.Content.ToString());
                Label_Briefing_Explanation.Content = briefing.getExplanation(briefingCombo.Content.ToString());
            }
            else
            {
                briefing.setText(sideCombo.Content.ToString(), briefingCombo.Content.ToString(), TextBox_Briefing_Text.Text);
            }
        }

        private void Button_Briefing_Marker_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Briefing_Marker();
            if (dialog.ShowDialog() == true)
            {
                briefing.AddMarker(ComboBox_Briefing_Side.Text, ComboBox_Briefing_Title.Text, dialog.MarkerName, dialog.MarkerText);
                this.BriefingTextEdit("GET");
            }
        }

        private void Button_Briefing_Picture_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Briefing_Picture();
            if (dialog.ShowDialog() == true)
            {
                briefing.AddPicture(ComboBox_Briefing_Side.Text, ComboBox_Briefing_Title.Text, dialog.PictureLoc);
                this.BriefingTextEdit("GET");
            }
        }

        /***************************************************************************************************/
        // Misc functions
        private String CheckInt(String value)
        {
            if(value.Equals(""))
                return "0";

            try
            {
                Convert.ToInt32(value);
                return value;
            }
            catch (FormatException eror)
            {
                MessageBox.Show("Sinun pitää antaa numero!");
                return "0";
            }
        }
    }
}
