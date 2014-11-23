﻿using System;
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
        DACHandler dacHandler;
        bool AddingInProgress, dacIdEmpty;

        public MainWindow()
        {
            InitializeComponent();

            briefing = new Briefing();
            dacHandler = new DACHandler();
            installHandler = new InstallHandler(this, briefing, dacHandler);
            VisibilityHandler = new VisibilityHandler(this);            
            VisibilityHandler.showProfileSettings();
            AddingInProgress = true;
            dacIdEmpty = true;

            TextBox_Briefing_Text.IsReadOnly = false;
            TextBox_Briefing_Text.AcceptsReturn = true;

            String arma3Folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Arma 3";
            String arma3OtherFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Arma 3 - Other Profiles";

            if (Directory.Exists(arma3Folder))
            {
                string[] files = Directory.GetFiles(arma3Folder, "*.Arma3Profile");

                string filename = new DirectoryInfo(files[0]).Name;
                string[] names = filename.Split('.');

                Combobox_Profile_Name.Items.Add(names[0]);
            }

            if (Directory.Exists(arma3OtherFolder))
            {
                string[] subdirectoryEntries = Directory.GetDirectories(arma3OtherFolder);
                foreach (string subdirectory in subdirectoryEntries)
                {
                    Combobox_Profile_Name.Items.Add(new DirectoryInfo(subdirectory).Name);
                }

            }

            ComboBox_Briefing_Side.SelectedIndex = 0;
            ComboBox_Briefing_Title.SelectedIndex = 0;
            Combobox_Mission_Gametype.SelectedIndex = 0;
            Combobox_Profile_Name.SelectedIndex = 0;
            Combobox_Mission_Map.SelectedIndex = 0;
            ComboBox_DAC_Faction.SelectedIndex = 0;
            ComboBox_DAC_Side.SelectedIndex = 0;

            AddingInProgress = false;
        }

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
                    Map = "Chernarus Summer";
                    break;
                case "Utes":
                    Map = "Utes";
                    break;
                case "Takistan":
                    Map = "Takistan";
                    break;
            }

            installHandler.MissionMap = Map;
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

        private void Center_Install_Btn_Click(object sender, RoutedEventArgs e)
        {
            VisibilityHandler.showSetup();
            TextBox_Setup_Text.Text = "";

            installHandler.StartInstall();
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

        private void TextBox_Mission_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            installHandler.missionName = TextBox_Mission_Name.Text;
        }

        private void Combobox_Profile_Name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            installHandler.NameIndex = Combobox_Profile_Name.SelectedIndex;
            installHandler.profileName = Combobox_Profile_Name.SelectedItem.ToString();
        }

        private void TextBox_Mission_PlayerMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            installHandler.MaxPlayers = TextBox_Mission_PlayerMax.Text;
        }

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

        private void DAC_TextEdited(object sender, TextChangedEventArgs e)
        {
            UpdateDAC("SET");
        }

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

        private void Button_Center_DAC_Click(object sender, RoutedEventArgs e)
        {
            VisibilityHandler.ShowDAC();
        }

        private void Button_DAC_New_Click(object sender, RoutedEventArgs e)
        {
            DAC dac = new DAC();
            int count = ComboBox_DAC_ID.Items.Count + 1;
            dac.ID = "z" + count.ToString();
            
            dacHandler.AddDac(dac);

            dacIdEmpty = false;

            ComboBox_DAC_ID.Items.Add(dac.ID);

            ComboBox_DAC_ID.SelectedIndex = ComboBox_DAC_ID.Items.Count - 1;


        }

        private void ComboBox_DAC_ID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDAC("GET");
        }

        private void ComboBox_DAC_Param_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDAC("SET");
        }

        private void UpdateDAC(String value)
        {
            if (AddingInProgress)
                return;

            if (!dacIdEmpty)
            {
                DAC dac = dacHandler.getSelected(ComboBox_DAC_ID.SelectedIndex);

                if (value.Equals("GET"))
                {
                    TextBox_DAC_InfGrpAmount.Text = dac.InfGroupAmount;
                    TextBox_DAC_InfGrpSize.Text = dac.InfGroupSize;
                    TextBox_DAC_InfWPAmount.Text = dac.InfGroupWaypointAmount;
                    TextBox_DAC_InfGrpWp.Text = dac.InfWaypointAmount;

                    TextBox_DAC_VehGrpAmount.Text = dac.VehGroupAmount;
                    TextBox_DAC_VehGrpSize.Text = dac.VehGroupSize;
                    TextBox_DAC_VehWPAmount.Text = dac.VehGroupWaypointAmount;
                    TextBox_DAC_VehGrpWp.Text = dac.VehWaypointAmount;

                    TextBox_DAC_ArmGrpAmount.Text = dac.ArmGroupAmount;
                    TextBox_DAC_ArmGrpSize.Text = dac.ArmGroupSize;
                    TextBox_DAC_ArmWPAmount.Text = dac.ArmGroupWaypointAmount;
                    TextBox_DAC_ArmGrpWp.Text = dac.ArmWaypointAmount;

                    TextBox_DAC_AirGrpAmount.Text = dac.AirGroupAmount;
                    TextBox_DAC_AirGrpSize.Text = dac.AirGroupSize;
                    TextBox_DAC_AirWPAmount.Text = dac.AirGroupWaypointAmount;
                    TextBox_DAC_AirGrpWp.Text = dac.AirWaypointAmount;

                    ComboBox_DAC_Side.SelectedIndex = Convert.ToInt32(dac.Side);
                    ComboBox_DAC_Faction.SelectedIndex = Convert.ToInt32(dac.Faction);
                }
                else
                {
                    dac.InfGroupAmount = TextBox_DAC_InfGrpAmount.Text;
                    dac.InfGroupSize = TextBox_DAC_InfGrpSize.Text;
                    dac.InfGroupWaypointAmount = TextBox_DAC_InfWPAmount.Text;
                    dac.InfWaypointAmount = TextBox_DAC_InfGrpWp.Text;

                    dac.VehGroupAmount = TextBox_DAC_VehGrpAmount.Text;
                    dac.VehGroupSize = TextBox_DAC_VehGrpSize.Text;
                    dac.VehGroupWaypointAmount = TextBox_DAC_VehWPAmount.Text;
                    dac.VehWaypointAmount = TextBox_DAC_VehGrpWp.Text;

                    dac.ArmGroupAmount = TextBox_DAC_ArmGrpAmount.Text;
                    dac.ArmGroupSize = TextBox_DAC_ArmGrpSize.Text;
                    dac.ArmGroupWaypointAmount = TextBox_DAC_ArmWPAmount.Text;
                    dac.ArmWaypointAmount = TextBox_DAC_ArmGrpWp.Text;

                    dac.AirGroupAmount = TextBox_DAC_AirGrpAmount.Text;
                    dac.AirGroupSize = TextBox_DAC_AirGrpSize.Text;
                    dac.AirGroupWaypointAmount = TextBox_DAC_AirWPAmount.Text;
                    dac.AirWaypointAmount = TextBox_DAC_AirGrpWp.Text;

                    dac.Side = ComboBox_DAC_Side.SelectedIndex.ToString();
                    dac.Faction = ComboBox_DAC_Faction.SelectedIndex.ToString();
                }
            }
            else
            {
                MessageBox.Show("Sinun täytyy lisätä uusi DAC ennekuin voit muokata arvoja!");
            }         
        }

        private void Button_DAC_Remove_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (ComboBox_DAC_ID.Items.Count != 0)
            {
                dacHandler.removeSelected(ComboBox_DAC_ID.SelectedItem.ToString());

                ComboBox_DAC_ID.Items.Clear();

                AddingInProgress = true;
                foreach (DAC dac in dacHandler.getList())
                {
                    ComboBox_DAC_ID.Items.Add(dac.ID);
                }
                AddingInProgress = false;

                if (dacHandler.getList().Count != 0)
                    ComboBox_DAC_ID.SelectedIndex = ComboBox_DAC_ID.Items.Count - 1;
                else
                {
                    dacIdEmpty = true;
                }

            }
            */

            MessageBox.Show("Tämä ominaisuus on WIP!");
        }
    }
}
