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

namespace Kuikka_Installer_GUI_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string oldWaveLine = "", newWaveLine = "";
        string oldLoadImg = "LoadScreen = ''";
        string oldLoadAuthor = "Author = ''";
        string oldLoadName = "OnLoadName = ''";
        string oldLoadInfo = "OnLoadMission = ''";
        string oldMissionGameType = "GameType = ''";
        string oldMissionPlayerAmount = "MaxPlayers = ''";

        public MainWindow()
        {
            InitializeComponent();

            Profile_Settings_Canvas.Visibility = Visibility.Visible;
            Mission_Settings_Canvas.Visibility = Visibility.Hidden;
            Loading_Settings_Canvas.Visibility = Visibility.Hidden;           
            Script_Settings_Canvas.Visibility = Visibility.Hidden;
            RespawnWave_Additional_Canvas.Visibility = Visibility.Hidden;

            Mission_Map_ComboBox.Items.Add("Altis");
            Mission_Map_ComboBox.Items.Add("Stratis");

            Mission_Gametype_ComboBox.Items.Add("COOP");
            Mission_Gametype_ComboBox.Items.Add("TVT");

            Script_Respawn_ComboBox.Items.Add("Aalto");
            Script_Respawn_ComboBox.Items.Add("Ei Mitään");

            Script_Medic_ComboBox.Items.Add("Kuikka Viikset");
            Script_Medic_ComboBox.Items.Add("Ei Mitään");

            Code_Init_TextBox.AppendText("// Tämä Init.sqf tiedosto on luotu Kuikan Missupohjan Asennusohjelmalla" + Environment.NewLine);
            Code_Init_TextBox.AppendText(Environment.NewLine);
            Code_Init_TextBox.AppendText("// Odotamme JIP pelaajien saavan initialialisoinnin valmiiksi" + Environment.NewLine);
            Code_Init_TextBox.AppendText("if (!isServer && isNull player) then {waitUntil {!(isNull player)}};" + Environment.NewLine);
            Code_Init_TextBox.AppendText(Environment.NewLine);
            Code_Init_TextBox.AppendText("// Aalto respawn" + Environment.NewLine);
            Code_Init_TextBox.AppendText("//[] spawn KuikkaWave_fnc_Init;" + Environment.NewLine);
            Code_Init_TextBox.AppendText(Environment.NewLine);
            Code_Init_TextBox.AppendText("// Kuikka lääkintäsysteemi" + Environment.NewLine);
            Code_Init_TextBox.AppendText("//[] spawn KuikkaMedic_fnc_Init;" + Environment.NewLine);

            Code_Desc_TextBox.AppendText("// Tämä Description.ext tiedosto on luotu Kuikan Missupohjan Asennusohjelmalla" + Environment.NewLine);
            Code_Desc_TextBox.AppendText(Environment.NewLine);
            Code_Desc_TextBox.AppendText("// Tehtävän Asetukset" + Environment.NewLine);
            Code_Desc_TextBox.AppendText("class Header" + Environment.NewLine);
            Code_Desc_TextBox.AppendText("{" + Environment.NewLine);
            Code_Desc_TextBox.AppendText("  GameType = '';" + Environment.NewLine);
            Code_Desc_TextBox.AppendText("  MinPlayers = 1;" + Environment.NewLine);
            Code_Desc_TextBox.AppendText("  MaxPlayers = '';" + Environment.NewLine);
            Code_Desc_TextBox.AppendText("};" + Environment.NewLine);
            Code_Desc_TextBox.AppendText(Environment.NewLine);
            Code_Desc_TextBox.AppendText("// Respawn Asetukset" + Environment.NewLine);
            Code_Desc_TextBox.AppendText("Respawn = BASE;" + Environment.NewLine);
            Code_Desc_TextBox.AppendText("RespawnDelay = 3;" + Environment.NewLine);
            Code_Desc_TextBox.AppendText("RespawnDialog = 0;" + Environment.NewLine);
            Code_Desc_TextBox.AppendText("RespawnOnStart = 1;" + Environment.NewLine);
            Code_Desc_TextBox.AppendText("RespawnTemplatesWest[] = {'Tickets','Counter','Wave','EndMission'};" + Environment.NewLine);
            Code_Desc_TextBox.AppendText(Environment.NewLine);
            Code_Desc_TextBox.AppendText("// Latausruudun Asetukset" + Environment.NewLine);
            Code_Desc_TextBox.AppendText("LoadScreen = '';" + Environment.NewLine);
            Code_Desc_TextBox.AppendText("Author = '';" + Environment.NewLine);
            Code_Desc_TextBox.AppendText("OnLoadName = '';" + Environment.NewLine);
            Code_Desc_TextBox.AppendText("OnLoadMission = '';" + Environment.NewLine);
            Code_Desc_TextBox.AppendText(Environment.NewLine);
            Code_Desc_TextBox.AppendText("// Muut Asetukset" + Environment.NewLine);
            Code_Desc_TextBox.AppendText("onLoadIntroTime = false;" + Environment.NewLine);
            Code_Desc_TextBox.AppendText("onLoadMissionTime = false;" + Environment.NewLine);
            Code_Desc_TextBox.AppendText("disabledAI = 1;" + Environment.NewLine);
            Code_Desc_TextBox.AppendText("debriefing = 1;" + Environment.NewLine);
            Code_Desc_TextBox.AppendText("showGPS = 1;" + Environment.NewLine);

            Code_Init_TextBox.Visibility = Visibility.Visible;
            Code_Desc_TextBox.Visibility = Visibility.Hidden;
        }

        private void Center_Profile_Btn_Click(object sender, RoutedEventArgs e)
        {
            Profile_Settings_Canvas.Visibility = Visibility.Visible;
            Mission_Settings_Canvas.Visibility = Visibility.Hidden;
            Loading_Settings_Canvas.Visibility = Visibility.Hidden;
            Script_Settings_Canvas.Visibility = Visibility.Hidden;
        }

        private void Center_Mission_Btn_Click(object sender, RoutedEventArgs e)
        {
            Profile_Settings_Canvas.Visibility = Visibility.Hidden;
            Mission_Settings_Canvas.Visibility = Visibility.Visible;
            Loading_Settings_Canvas.Visibility = Visibility.Hidden;
            Script_Settings_Canvas.Visibility = Visibility.Hidden;
        }

        private void Center_Loading_Btn_Click(object sender, RoutedEventArgs e)
        {
            Profile_Settings_Canvas.Visibility = Visibility.Hidden;
            Mission_Settings_Canvas.Visibility = Visibility.Hidden;
            Loading_Settings_Canvas.Visibility = Visibility.Visible;
            Script_Settings_Canvas.Visibility = Visibility.Hidden;
        }

        private void Center_Script_Btn_Click(object sender, RoutedEventArgs e)
        {
            Profile_Settings_Canvas.Visibility = Visibility.Hidden;
            Mission_Settings_Canvas.Visibility = Visibility.Hidden;
            Loading_Settings_Canvas.Visibility = Visibility.Hidden;
            Script_Settings_Canvas.Visibility = Visibility.Visible;
        }

        private void Script_Respawn_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Script_Respawn_ComboBox.SelectedItem.ToString() == "Aalto")
            {
                RespawnWave_Additional_Canvas.Visibility = Visibility.Visible;

                newWaveLine = "[" + WaveRespawn_Time_TextBox.Text.ToString() + "] spawn KuikkaWave_fnc_Init;";
                if (oldWaveLine != "")
                {
                    Code_Init_TextBox.Text = Code_Init_TextBox.Text.Replace(oldWaveLine, newWaveLine);
                }
                else
                {
                    Code_Init_TextBox.Text = Code_Init_TextBox.Text.Replace("//" + newWaveLine, newWaveLine);
                }
                oldWaveLine = newWaveLine;
            }
            else
            {
                RespawnWave_Additional_Canvas.Visibility = Visibility.Hidden;

                newWaveLine = "[" + WaveRespawn_Time_TextBox.Text.ToString() + "] spawn KuikkaWave_fnc_Init;";
                if (oldWaveLine != "")
                {
                    Code_Init_TextBox.Text = Code_Init_TextBox.Text.Replace(oldWaveLine, "//" + newWaveLine);
                }
                else
                {
                    Code_Init_TextBox.Text = Code_Init_TextBox.Text.Replace(newWaveLine, "//" + newWaveLine);
                }
                oldWaveLine = "//" + newWaveLine;
            }
        }

        private void Script_Medic_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Script_Medic_ComboBox.SelectedItem.ToString() == "Kuikka Viikset")
            {
                Code_Init_TextBox.Text = Code_Init_TextBox.Text.Replace("//[] spawn KuikkaMedic_fnc_Init;", "[] spawn KuikkaMedic_fnc_Init;");
            }
            else
            {
                Code_Init_TextBox.Text = Code_Init_TextBox.Text.Replace("[] spawn KuikkaMedic_fnc_Init;", "//[] spawn KuikkaMedic_fnc_Init;");
            }
        }

        private void Code_Init_Btn_Click(object sender, RoutedEventArgs e)
        {
            Code_Init_TextBox.Visibility = Visibility.Visible;
            Code_Desc_TextBox.Visibility = Visibility.Hidden;
        }

        private void Code_Desc_Btn_Click(object sender, RoutedEventArgs e)
        {
            Code_Init_TextBox.Visibility = Visibility.Hidden;
            Code_Desc_TextBox.Visibility = Visibility.Visible;
        }

        private void WaveRespawn_Time_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Code_Init_TextBox.Text = Code_Init_TextBox.Text.Replace(oldWaveLine, "[" + WaveRespawn_Time_TextBox.Text.ToString() + "] spawn KuikkaWave_fnc_Init;");
            oldWaveLine = "[" + WaveRespawn_Time_TextBox.Text.ToString() + "] spawn KuikkaWave_fnc_Init;";
        }

        private void Mission_Gametype_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Code_Desc_TextBox.Text = Code_Desc_TextBox.Text.Replace(oldMissionGameType, "GameType = " + Mission_Gametype_ComboBox.SelectedItem.ToString());
            oldMissionGameType = "GameType = " + Mission_Gametype_ComboBox.SelectedItem.ToString();
        }

        private void Mission_PlayerAmount_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Code_Desc_TextBox.Text = Code_Desc_TextBox.Text.Replace(oldMissionPlayerAmount, "MaxPlayers = " + Mission_PlayerAmount_TextBox.Text);
            oldMissionPlayerAmount = "MaxPlayers = " + Mission_PlayerAmount_TextBox.Text;
        }

        private void Loading_Author_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Code_Desc_TextBox.Text = Code_Desc_TextBox.Text.Replace(oldLoadAuthor, "Author = " + Loading_Author_TextBox.Text);
            oldLoadAuthor = "Author = " + Loading_Author_TextBox.Text;
        }

        private void Loading_Name_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Code_Desc_TextBox.Text = Code_Desc_TextBox.Text.Replace(oldLoadName, "OnLoadName = " + Loading_Name_TextBox.Text);
            oldLoadName = "OnLoadName = " + Loading_Name_TextBox.Text;
        }

        private void Loading_Info_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Code_Desc_TextBox.Text = Code_Desc_TextBox.Text.Replace(oldLoadInfo, "OnLoadMission = " + Loading_Info_TextBox.Text);
            oldLoadInfo = "OnLoadMission = " + Loading_Info_TextBox.Text;
        }

        private void Loading_Image_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Loading_Image_TextBox1.IsEnabled = false;
            Loading_Image_Btn.IsEnabled = false;
        }

        private void Loading_Image_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Loading_Image_TextBox1.IsEnabled = true;
            Loading_Image_Btn.IsEnabled = true;
        }
    }
}
