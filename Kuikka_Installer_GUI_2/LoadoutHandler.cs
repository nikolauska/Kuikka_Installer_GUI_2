using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kuikka_Installer_GUI_2
{
    class LoadoutHandler
    {
        /**************************************************************
         * Variables
        ***************************************************************/
        private class Magazine
        {
            public string name { get; set; }
            public string displayname { get; set; }
        }

        private class Item
        {
            public string name { get; set; }
            public string displayname { get; set; }
        }

        private class Rifle
        {
            public string name { get; set; }
            public string displayname { get; set; }
            public List<Magazine> magazines { get; set; }
        }

        private class Pistol
        {
            public string name { get; set; }
            public string displayname { get; set; }
            public List<Magazine> magazines { get; set; }
        }

        private class All
        {
            public List<Item> items { get; set; }
            public List<Rifle> rifles { get; set; }
            public List<Pistol> pistols { get; set; }
        }

        private All allitems { get; set; }
        private MainWindow window { get; set; }

        /**************************************************************
         * Initialization
        ***************************************************************/
        public LoadoutHandler(MainWindow windowIn)
        {
            allitems = JsonConvert.DeserializeObject<All>(File.ReadAllText(@"c:\UDK\NewItems2.json"));

            window = windowIn;
        }

        /**************************************************************
         * Functions
         ***************************************************************/
    }
}
