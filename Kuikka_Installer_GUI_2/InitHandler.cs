using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuikka_Installer_GUI_2
{
    class InitHandler
    {
        /**************************************************************
         * Variables
         ***************************************************************/
        private MainWindow window { get; set; }
        string InitText;
        private ScriptHandler Scripts;

        /**************************************************************
         * Initialization
        ***************************************************************/
        public InitHandler(MainWindow windowIn)
        {
            InitText = "// Tämä Init.sqf tiedosto on luotu Kuikan Missupohjan Asennusohjelmalla \n" +
                "\n" +
                "// Odotamme JIP pelaajien saavan initialialisoinnin valmiiksi \n" +
                "if (!isServer && isNull player) then {waitUntil {!(isNull player)}}; \n";

            window = windowIn;

            Scripts = new ScriptHandler();
        }

        /**************************************************************
         * Functions
         ***************************************************************/
        // Update all combo and text boxes
        private void UpdataWindowData()
        {

        }

        public void AddScript()
        {
            //Scripts.AddScript() // Add combobox index
        }

        public string ReturnInitText()
        {
            return InitText;
        }
    }
}
