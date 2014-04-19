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
        InitSQF Code_Init;
        DescEXT Code_Desc;

        string[] maps = { "Altis", "Stratis" };
        string[] gametypes = { "COOP", "TVT"};
        string[] respawn = { "Aalto", "Ei Mitään" };
        string[] medic = { "Kuikka Viikset", "Ei Mitään" };

        public MainWindow()
        {
            InitializeComponent();

            Profile_Settings_Canvas.Visibility = Visibility.Visible;
            Mission_Settings_Canvas.Visibility = Visibility.Hidden;
            Loading_Settings_Canvas.Visibility = Visibility.Hidden;           
            Script_Settings_Canvas.Visibility = Visibility.Hidden;
            RespawnWave_Additional_Canvas.Visibility = Visibility.Hidden;

            foreach (string text in maps)
            {
                Mission_Map_ComboBox.Items.Add(text);
            }
            foreach (string text in gametypes)
            {
                Mission_Gametype_ComboBox.Items.Add(text);
            }
            foreach (string text in respawn)
            {
                Script_Respawn_ComboBox.Items.Add(text);
            }
            foreach (string text in medic)
            {
                Script_Medic_ComboBox.Items.Add(text);
            }

            Code_Init = new InitSQF();
            Code_Init_TextBox.Text = Code_Init.getText();

            Code_Desc = new DescEXT();
            Code_Desc_TextBox.Text = Code_Desc.getText();

            Code_Init_TextBox.Visibility = Visibility.Visible;
            Code_Desc_TextBox.Visibility = Visibility.Hidden;
            Code_Init_TextBox.IsReadOnly = false;
            Code_Desc_TextBox.IsReadOnly = false;
            Code_Init_TextBox.AcceptsReturn = true;
            Code_Desc_TextBox.AcceptsReturn = true;

        }

        private void Center_Profile_Btn_Click(object sender, RoutedEventArgs e)
        {
            Profile_Settings_Canvas.Visibility = Visibility.Visible;
            Mission_Settings_Canvas.Visibility = Visibility.Hidden;
            Loading_Settings_Canvas.Visibility = Visibility.Hidden;
            Script_Settings_Canvas.Visibility = Visibility.Hidden;
            RespawnWave_Additional_Canvas.Visibility = Visibility.Hidden;
        }

        private void Center_Mission_Btn_Click(object sender, RoutedEventArgs e)
        {
            Profile_Settings_Canvas.Visibility = Visibility.Hidden;
            Mission_Settings_Canvas.Visibility = Visibility.Visible;
            Loading_Settings_Canvas.Visibility = Visibility.Hidden;
            Script_Settings_Canvas.Visibility = Visibility.Hidden;
            RespawnWave_Additional_Canvas.Visibility = Visibility.Hidden;
        }

        private void Center_Loading_Btn_Click(object sender, RoutedEventArgs e)
        {
            Profile_Settings_Canvas.Visibility = Visibility.Hidden;
            Mission_Settings_Canvas.Visibility = Visibility.Hidden;
            Loading_Settings_Canvas.Visibility = Visibility.Visible;
            Script_Settings_Canvas.Visibility = Visibility.Hidden;
            RespawnWave_Additional_Canvas.Visibility = Visibility.Hidden;
        }

        private void Center_Script_Btn_Click(object sender, RoutedEventArgs e)
        {
            Profile_Settings_Canvas.Visibility = Visibility.Hidden;
            Mission_Settings_Canvas.Visibility = Visibility.Hidden;
            Loading_Settings_Canvas.Visibility = Visibility.Hidden;
            Script_Settings_Canvas.Visibility = Visibility.Visible;
            RespawnWave_Additional_Canvas.Visibility = Visibility.Hidden;
        }

        private void Script_Respawn_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Script_Respawn_ComboBox.SelectedItem.ToString() == "Aalto")
            {
                RespawnWave_Additional_Canvas.Visibility = Visibility.Visible;

                Code_Init.updateWaveRespawnText(true);
                Code_Init_TextBox.Text = Code_Init.getText();
            }
            else
            {
                RespawnWave_Additional_Canvas.Visibility = Visibility.Hidden;

                Code_Init.updateWaveRespawnText(false);
                Code_Init_TextBox.Text = Code_Init.getText();
            }
        }

        private void Script_Medic_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Script_Medic_ComboBox.SelectedItem.ToString() == "Kuikka Viikset")
            {
                Code_Init.updateMedicText(true);
                Code_Init_TextBox.Text = Code_Init.getText();
            }
            else
            {
                Code_Init.updateMedicText(false);
                Code_Init_TextBox.Text = Code_Init.getText();
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
            Code_Init.updateWaveRespawnTime(WaveRespawn_Time_TextBox.Text);
            Code_Init_TextBox.Text = Code_Init.getText();
        }

        private void Mission_Gametype_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Code_Desc.updateMissionGameType(Mission_Gametype_ComboBox.SelectedItem.ToString());
            Code_Desc_TextBox.Text = Code_Desc.getText();
        }

        private void Mission_PlayerAmount_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Code_Desc.updateMissionMaxPlayers(Mission_PlayerAmount_TextBox.Text);
            Code_Desc_TextBox.Text = Code_Desc.getText();
        }

        private void Loading_Image_TextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Code_Desc.updateLoadingImage(Loading_Image_TextBox1.Text);
            Code_Desc_TextBox.Text = Code_Desc.getText();
        }

        private void Loading_Author_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Code_Desc.updateLoadingAuthor(Loading_Author_TextBox.Text);
            Code_Desc_TextBox.Text = Code_Desc.getText();
        }

        private void Loading_Name_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Code_Desc.updateLoadingName(Loading_Name_TextBox.Text);
            Code_Desc_TextBox.Text = Code_Desc.getText();
        }

        private void Loading_Info_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Code_Desc.updateLoadingInfo(Loading_Info_TextBox.Text);
            Code_Desc_TextBox.Text = Code_Desc.getText();
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
