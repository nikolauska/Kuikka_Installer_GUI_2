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
        public VisibilityHandler(MainWindow windowIn)
        {
            window = windowIn;
        }

        private MainWindow window { get; set; }

        public void showProfileSettings()
        {
            this.hideAll();
            window.Canvas_Profile.Visibility = Visibility.Visible;
        }

        public void showMissionSettings()
        {
            this.hideAll();
            window.Canvas_Mission.Visibility = Visibility.Visible;
        }

        public void showLoadingSettings()
        {
            this.hideAll();
            window.Canvas_Loading.Visibility = Visibility.Visible;
        }

        public void showBriefingSettings()
        {
            this.hideAll();
            window.Canvas_Briefing.Visibility = Visibility.Visible;
        }

        public void showSetup()
        {
            this.hideAll();
            window.Canvas_Setup.Visibility = Visibility.Visible;
        }

        public void ShowDAC()
        {
            this.hideAll();
            window.Canvas_DAC.Visibility = Visibility.Visible;
        }

        private void hideAll()
        {
            window.Canvas_Profile.Visibility = Visibility.Hidden;
            window.Canvas_Mission.Visibility = Visibility.Hidden;
            window.Canvas_Loading.Visibility = Visibility.Hidden;
            window.Canvas_Briefing.Visibility = Visibility.Hidden;
            window.Canvas_Setup.Visibility = Visibility.Hidden;
            window.Canvas_DAC.Visibility = Visibility.Hidden;
        }
    }
}
