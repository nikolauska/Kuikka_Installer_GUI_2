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
    /// Interaction logic for Briefing_Picture.xaml
    /// </summary>
    public partial class Briefing_Picture : Window
    {
        public Briefing_Picture()
        {
            InitializeComponent();
        }

        public string PictureLoc
        {
            get { return TextBox_Picture_Loc.Text; }
            set { TextBox_Picture_Loc.Text = value; }
        }

        private void Button_Picture_OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Picture_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Button_Picture_Find_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog() { Filter = "PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg" };
            var result = ofd.ShowDialog();
            if (result == false) return;

            BitmapImage img = new BitmapImage(new Uri(ofd.FileName));

            var imageHeight = img.Height;
            var imageWidth = img.Width;

            if (imageHeight == imageWidth)
            {
                if (imageHeight % 2 == 0 && imageWidth % 2 == 0)
                {
                    TextBox_Picture_Loc.Text = ofd.FileName;
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
    }
}
