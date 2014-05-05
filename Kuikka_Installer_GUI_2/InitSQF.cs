using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuikka_Installer_GUI_2
{
    class InitSQF
    {        
        public InitSQF(MainWindow windowIn)
        {
            Respawn = new List<ScriptStruct>();
            ScriptStruct temp = new ScriptStruct();
            temp.Selection = "Aalto";
            temp.Text = "// Aalto respawn\n" +
                        "[] spawn KuikkaWave_fnc_Init;\n";
            Respawn.Add( temp );

            temp = new ScriptStruct();
            temp.Selection = "Ei Mitään";
            temp.Text = "";
            Respawn.Add( temp );

            Medic = new List<ScriptStruct>();
            temp = new ScriptStruct();
            temp.Selection = "Viikset";
            temp.Text = "// Kuikka lääkintäsysteemi\n" +
                        "[] spawn KuikkaMedic_fnc_Init;\n";
            Medic.Add( temp );

            temp = new ScriptStruct();
            temp.Selection = "Ei Mitään";
            temp.Text = "";
            Medic.Add( temp );

            InitText = new List<string>
            {
                "// Tämä Init.sqf tiedosto on luotu Kuikan Missupohjan Asennusohjelmalla \n",
                "\n",
                "// Odotamme JIP pelaajien saavan initialialisoinnin valmiiksi \n",
                "if (!isServer && isNull player) then {waitUntil {!(isNull player)}}; \n",
                "\n",
                "", // Respawn #5
                "\n",
                "", // Medic #7
            };

            window = windowIn;
            this.updateText();
        }

        struct ScriptStruct
        {
            public string Selection;
            public string Text;
        };

        private List<ScriptStruct> Respawn { get; set; }
        private List<ScriptStruct> Medic { get; set; }
        private List<string> InitText { get; set; }
        private MainWindow window { get; set; }

        public void updateRespawnText(string Selection)
        {
            foreach (ScriptStruct script in Respawn)
            {
                if (script.Selection == Selection)
                {
                    InitText[5] = script.Text;
                }
            };

            this.updateText();
        }

        public void updateWaveRespawnTime(string time)
        {
            int index = 0, IndexLoop = 0;
            foreach (ScriptStruct script in Respawn)
            {
                if (script.Selection == "Aalto")
                {
                    index = IndexLoop;
                }
                IndexLoop++;
            };

            ScriptStruct temp = new ScriptStruct();
            temp.Selection = "Aalto";
            temp.Text = "// Aalto respawn\n" +
                        "[" + time + "] spawn KuikkaWave_fnc_Init;\n";
            Respawn[index] = temp;

            this.updateText();
        }

        public void updateMedicText(string Selection)
        {
            foreach (ScriptStruct script in Medic)
            {
                if (script.Selection == Selection)
                {
                    InitText[7] = script.Text;
                }
            };

            this.updateText();
        }

        public void updateText() 
        {
            string alltext = "";
            foreach (String text in InitText)
            {
                alltext += text;
            }
            window.Code_Init_TextBox.Text = alltext;
        }

        public List<string> getRespawnSelections()
        {
            List<string> Selections = new List<string>();

            foreach (ScriptStruct script in Respawn)
            {
                Selections.Add(script.Selection);
            }

            return Selections;
        }

        public List<string> getMedicSelections()
        {
            List<string> Selections = new List<string>();

            foreach (ScriptStruct script in Medic)
            {
                Selections.Add(script.Selection);
            }

            return Selections;
        }
    }
}
