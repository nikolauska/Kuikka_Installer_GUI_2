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
        InitHandler Code_Init;
        DescriptionHandler Code_Desc;
        BriefingHandler Briefings;
        VisibilityHandler VisibilityHandler;

        string[] maps = { "Altis", "Stratis" };
        string[] gametypes = { "COOP", "TVT"};


        public MainWindow()
        {
            InitializeComponent();

            Code_Init = new InitHandler(this);
            Code_Desc = new DescriptionHandler(this);
            Briefings = new BriefingHandler(this);
            VisibilityHandler = new VisibilityHandler(this, Briefings);
            VisibilityHandler.showProfileSettings();
            

            foreach (string text in maps)
            {
                Mission_Map_ComboBox.Items.Add(text);
            }
            foreach (string text in gametypes)
            {
                Mission_Gametype_ComboBox.Items.Add(text);
            }
            /*foreach (string text in Code_Init.getRespawnSelections())
            {
                Script_Respawn_ComboBox.Items.Add(text);
            }
            foreach (string text in Code_Init.getMedicSelections())
            {
                Script_Medic_ComboBox.Items.Add(text);
            }
            */

            Code_TextBox.IsReadOnly = false;
            Code_TextBox.AcceptsReturn = true;
            Code_TextBox.Text = Code_Init.ReturnInitText();
        }

        private void Center_Profile_Btn_Click(object sender, RoutedEventArgs e)
        {
            VisibilityHandler.showProfileSettings();
        }

        private void Center_Mission_Btn_Click(object sender, RoutedEventArgs e)
        {
            VisibilityHandler.showMissionSettings();
        }

        private void Center_Loading_Btn_Click(object sender, RoutedEventArgs e)
        {
            VisibilityHandler.showLoadingSettings();
        }

        private void Center_Script_Btn_Click(object sender, RoutedEventArgs e)
        {
            VisibilityHandler.showScriptSettings();
        }


        private void Center_Briefing_Btn_Click(object sender, RoutedEventArgs e)
        {
            VisibilityHandler.showBriefingSettings();
            Briefing_Selection_ComboBox.Items.Clear();
            foreach (string title in Briefings.GetTitles())
            {
                Briefing_Selection_ComboBox.Items.Add(title);
            }
        }

        private void Script_Respawn_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Code_Init.updateRespawnText(Script_Respawn_ComboBox.SelectedItem.ToString());
        }

        private void Script_Medic_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Code_Init.updateMedicText(Script_Medic_ComboBox.SelectedItem.ToString());
        }

        private void Code_Init_Btn_Click(object sender, RoutedEventArgs e)
        {
            Code_TextBox.Text = Code_Init.ReturnInitText();
        }

        private void Code_Desc_Btn_Click(object sender, RoutedEventArgs e)
        {
            Code_TextBox.Text = Code_Desc.ReturnDescriptionText();
        }

        private void Code_Briefing_Btn_Click(object sender, RoutedEventArgs e)
        {
            Code_TextBox.Text = "Not yet implemented!";
        }

        private void Code_Music_Btn_Click(object sender, RoutedEventArgs e)
        {
            Code_TextBox.Text = "Not yet implemented!";
        }

        private void WaveRespawn_Time_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Code_Init.updateWaveRespawnTime(WaveRespawn_Time_TextBox.Text);
        }

        private void Mission_Gametype_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Code_Desc.updateMissionGameType(Mission_Gametype_ComboBox.SelectedItem.ToString());
        }

        private void Mission_PlayerAmount_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Code_Desc.updateMissionMaxPlayers(Mission_PlayerAmount_TextBox.Text);
        }

        private void Loading_Image_TextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Code_Desc.updateLoadingImage(Loading_Image_TextBox1.Text);
        }

        private void Loading_Author_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Code_Desc.updateLoadingAuthor(Loading_Author_TextBox.Text);
        }

        private void Loading_Name_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Code_Desc.updateLoadingName(Loading_Name_TextBox.Text);
        }

        private void Loading_Info_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Code_Desc.updateLoadingInfo(Loading_Info_TextBox.Text);
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

        private void Briefing_Settings_New_Btn_Click(object sender, RoutedEventArgs e)
        {
            VisibilityHandler.showBriefingEdit("new");           
        }

        private void Briefing_Settings_Edit_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (Briefing_Selection_ComboBox.SelectedItem != null)
            {
                VisibilityHandler.showBriefingEdit("edit");
            }
            else
            {
                MessageBox.Show("Et ole valinnut muokattavaa briefingiä!");
            }
        }

        private void Briefing_Save_Btn_Click(object sender, RoutedEventArgs e)
        {
            Briefings.EditBriefing(Briefing_Selection_ComboBox.SelectedIndex,
                                    Briefing_Title_TextBox.Text,
                                    Briefing_Code_TextBox.Text);

            VisibilityHandler.showBriefingSettings();
            Briefing_Selection_ComboBox.Items.Clear();
            foreach (string title in Briefings.GetTitles())
            {
                Briefing_Selection_ComboBox.Items.Add(title);
            }
        }

        private void Briefing_MarkerSave_Btn_Click(object sender, RoutedEventArgs e)
        {
            Briefings.EditMarker(Briefing_Selection_ComboBox.SelectedIndex,
                                Briefing_Marker_ComboBox.SelectedIndex,
                                Briefing_MarkerName_TextBox.Text,
                                Briefing_MarkerText_TextBox.Text);

            VisibilityHandler.showBriefingEdit("edit");
        }

        private void Briefing_Marker_New_Btn_Click(object sender, RoutedEventArgs e)
        {
            Briefing_Marker_ComboBox.Items.Add("Marker " + Briefing_Marker_ComboBox.Items.Count.ToString());
            Briefing_Marker_ComboBox.SelectedIndex = Briefing_Marker_ComboBox.Items.Count - 1;
            Briefings.AddMarker(Briefing_Selection_ComboBox.SelectedIndex,
                                "Marker " + Briefing_Marker_ComboBox.Items.Count.ToString(),
                                "");

            VisibilityHandler.showBriefingMarker();
        }

        private void Briefing_Marker_Edit_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (Briefing_Marker_ComboBox.SelectedItem != null)
            {
                VisibilityHandler.showBriefingMarker();
            }
            else
            {
                MessageBox.Show("Et ole valinnut muokattavaa markkeria!");
            }            
        }

        private void Center_HandEdit_Btn_Click(object sender, RoutedEventArgs e)
        {
            VisibilityHandler.showCodeEditCanvas();
        }
    }
}
