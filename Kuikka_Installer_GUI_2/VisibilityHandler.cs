using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kuikka_Installer_GUI_2
{
    class VisibilityHandler
    {
        public VisibilityHandler(MainWindow windowIn, BriefingHandler BriefingIn)
        {
            window = windowIn;
            Briefings = BriefingIn;
        }

        private MainWindow window { get; set; }
        private BriefingHandler Briefings { get; set; }

        public void showProfileSettings()
        {
            this.hideAll();
            window.Profile_Settings_Canvas.Visibility = Visibility.Visible;
        }

        public void showMissionSettings()
        {
            this.hideAll();
            window.Mission_Settings_Canvas.Visibility = Visibility.Visible;
        }

        public void showLoadingSettings()
        {
            this.hideAll();
            window.Loading_Settings_Canvas.Visibility = Visibility.Visible;
        }

        public void showScriptSettings()
        {
            this.hideAll();
            window.Script_Settings_Canvas.Visibility = Visibility.Visible;
        }

        public void showRespawnWaveAdditional()
        {
            this.hideAll();
        }

        public void showBriefingSettings()
        {
            this.hideAll();
            window.Briefing_Settings_Canvas.Visibility = Visibility.Visible;
        }

        public void showBriefingEdit(string selection)
        {
            this.hideAll();
            window.Briefing_Edit_Canvas.Visibility = Visibility.Visible;
            window.Briefing_Code_Canvas.Visibility = Visibility.Visible;

            if (selection == "new")
            {
                window.Briefing_Selection_ComboBox.Items.Add("Otsikko " + window.Briefing_Selection_ComboBox.Items.Count.ToString());
                window.Briefing_Selection_ComboBox.SelectedIndex = window.Briefing_Selection_ComboBox.Items.Count - 1;
                Briefings.AddBriefing(window.Briefing_Title_TextBox.Text, window.Briefing_Code_TextBox.Text);
                window.Briefing_Title_TextBox.Text = "Otsikko " + window.Briefing_Selection_ComboBox.Items.Count.ToString();
                window.Briefing_Code_TextBox.Text = "";
            }
            else
            {
                Briefings.GetBriefing(window.Briefing_Selection_ComboBox.SelectedIndex);

                window.Briefing_Marker_ComboBox.Items.Clear();
                foreach (string title in Briefings.GetMarkerNames())
                {
                    window.Briefing_Marker_ComboBox.Items.Add(title);
                }
                window.Briefing_Marker_ComboBox.SelectedIndex = 0;

                Briefings.GetRawCode(window.Briefing_Selection_ComboBox.SelectedIndex);
            }
        }

        public void showBriefingMarker()
        {
            this.hideAll();
            window.Briefing_Marker_Canvas.Visibility = Visibility.Visible;
            window.Briefing_MarkerName_TextBox.Text = Briefings.getMarkerName(window.Briefing_Marker_ComboBox.SelectedIndex);
            window.Briefing_MarkerText_TextBox.Text = Briefings.getMarkerText(window.Briefing_Marker_ComboBox.SelectedIndex);
        }

        public void showCodeEditCanvas()
        {
            this.hideAll();
            window.Code_Edit_Canvas.Visibility = Visibility.Visible;
        }

        private void hideAll()
        {
            window.Profile_Settings_Canvas.Visibility = Visibility.Hidden;
            window.Mission_Settings_Canvas.Visibility = Visibility.Hidden;
            window.Loading_Settings_Canvas.Visibility = Visibility.Hidden;
            window.Script_Settings_Canvas.Visibility = Visibility.Hidden;
            window.Briefing_Settings_Canvas.Visibility = Visibility.Hidden;
            window.Briefing_Edit_Canvas.Visibility = Visibility.Hidden;
            window.Briefing_Code_Canvas.Visibility = Visibility.Hidden;
            window.Briefing_Marker_Canvas.Visibility = Visibility.Hidden;
            window.Code_Edit_Canvas.Visibility = Visibility.Hidden;
        }
    }
}
