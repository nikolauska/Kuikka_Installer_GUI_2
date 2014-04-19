using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuikka_Installer_GUI_2
{
    class InitSQF
    {        
        public InitSQF()
        {
            infoText = "// Tämä Init.sqf tiedosto on luotu Kuikan Missupohjan Asennusohjelmalla\n\n";

            jipText = "// Odotamme JIP pelaajien saavan initialialisoinnin valmiiksi\n" +
                      "if (!isServer && isNull player) then {waitUntil {!(isNull player)}};\n\n";

            waveRespawnText = "";
            waveRespawnTime = "";

            medicText = "";

            allText = "";
        }
        private string infoText { get; set; }

        private string jipText { get; set; }

        private string waveRespawnText { get; set; }
        private string waveRespawnTime { get; set; }

        private string medicText { get; set; }

        private string allText { get; set; }

        public void updateWaveRespawnText(bool visibility)
        {
            if (visibility)
            {
                waveRespawnText =   "// Aalto respawn\n" +
                                    "[" + waveRespawnTime + "] spawn KuikkaWave_fnc_Init;\n\n";
            }
            else
            {
                waveRespawnText = "";
            }
        }

        public void updateWaveRespawnTime(string time)
        {
            waveRespawnTime = time;
            updateWaveRespawnText(true);
        }

        public void updateMedicText(bool visibility)
        {
            if (visibility)
            {
                medicText = "// Kuikka lääkintäsysteemi\n" +
                            "[] spawn KuikkaMedic_fnc_Init;\n\n";
            }
            else
            {
                medicText = "";
            }
        }

        public string getText() 
        {
            allText = infoText;
            allText += jipText;
            allText += waveRespawnText;
            allText += medicText;
            return allText;
        }
    
    }
}
