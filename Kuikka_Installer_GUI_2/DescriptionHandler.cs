using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kuikka_Installer_GUI_2
{
    class DescriptionHandler
    {
        /**************************************************************
         * Variables
         ***************************************************************/
        private List<string> DescText { get; set; }
        private MainWindow window { get; set; }

        /**************************************************************
         * Initialization
        ***************************************************************/
        public DescriptionHandler(MainWindow windowIn)
        {
            // Default text to be used in Description.ext
            DescText = new List<string>
            {
                "// Tämä Description.ext tiedosto on luotu Kuikan Missupohjan Asennusohjelmalla \n", // Info Text
                "\n",
                "// Tehtävän Asetukset \n",
                "{ \n",
                "", // Gametype #4
                "", // Minimum player amount #5
                "", // Maximum player amount #6
                "} \n",
                "\n",
                "// Latausruudun Asetukset \n",
                "", // Loading screen image #10
                "", // Loading screen author #11
                "", // Loading screen mission name #12
                "", // Loading screen info text #13
                "\n",
                "// Respawn Asetukset \n",
                "Respawn = BASE; \n",
                "RespawnDelay = 3; \n",
                "RespawnDialog = 0; \n",
                "RespawnOnStart = 1; \n",
                "RespawnTemplatesWest[] = {'Tickets','Counter','Wave','EndMission'}; \n",
                "\n",
                "\n// Muut Asetukset \n",
                "onLoadIntroTime = false; \n",
                "onLoadMissionTime = false; \n",
                "disabledAI = 1; \n",
                "debriefing = 1; \n",
                "showGPS = 1; \n"
            };

            // Save main window
            window = windowIn;
        }

        /**************************************************************
         * Functions
         ***************************************************************/
        // Update gametype
        public void updateMissionGameType(string type)
        {
            DescText[4] = "  GameType = " + type + "; \n";
        }

        // Update max players
        public void updateMissionMaxPlayers(string Amount)
        {
            DescText[5] = "  MinPlayers = 1; \n";
            DescText[6] = "  MaxPlayers = " + Amount + "; \n";
        }

        // Update loading screen image
        public void updateLoadingImage(string Image)
        {
            DescText[10] = @"LoadScreen = 'Images\" + Image + "'; \n";
        }

        // Update loading screen author
        public void updateLoadingAuthor(string Author)
        {
            DescText[11] = "Author = " + Author + "; \n";
        }

        // Update loading screen name
        public void updateLoadingName(string Name)
        {
            DescText[12] = "OnLoadName = " + Name + "; \n";
        }

        // Update loading screen info text
        public void updateLoadingInfo(string Info)
        {
            DescText[13] = "OnLoadMission = " + Info + "; \n";
        }

        // Update text to main windows
        public string ReturnDescriptionText()
        {
            string alltext = "";
            foreach (String text in DescText)
            {
                alltext += text;
            }
            return alltext;
        }

        // Check if anything is missing in description.ext
        public string missingCheck()
        {
            if (DescText[4] == "")
            {
                return "Gametype puuttuu";
            }
            if (DescText[5] == "")
            {
                return "Minimi pelaajamäärä puuttuu";
            }
            if (DescText[6] == "")
            {
                return "Maksimi pelaajamäärä puuttuu";
            }
            if (DescText[10] == "")
            {
                return "Latauskuva puuttuu";
            }
            if (DescText[11] == "")
            {
                return "Tekijän nimi puuttuu";
            }
            if (DescText[12] == "")
            {
                return "Tehtävän nimi puuttuu";
            }
            if (DescText[13] == "")
            {
                return "Infoteksti puuttuu";
            }
            return "";
        }
    }
}
