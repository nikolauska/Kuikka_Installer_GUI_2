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
using System.Windows.Shapes;

namespace Kuikka_Installer_GUI_2
{
    /// <summary>
    /// Interaction logic for Briefing_Marker.xaml
    /// </summary>
    public partial class Briefing_Marker : Window
    {
        public Briefing_Marker()
        {
            InitializeComponent();
        }

        public string MarkerName
        {
            get { return TextBox_Marker_Name.Text; }
            set { TextBox_Marker_Name.Text = value; }
        }

        public string MarkerText
        {
            get { return TextBox_Marker_Text.Text; }
            set { TextBox_Marker_Text.Text = value; }
        }

        private void Button_Marker_OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Marker_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
