using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuikka_Installer_GUI_2
{
    class DescEXT
    {
        public DescEXT()
        {
            infoText = "// Tämä Description.ext tiedosto on luotu Kuikan Missupohjan Asennusohjelmalla";

            MissionSettingsText = "";
            MissionGameTypeText = "";
            MissionMinPlayersText = "";
            MissionMaxPlayersText = "";
            
            LoadingSettingsText = "";
            LoadingImageText = "";
            LoadingAuthorText = "";
            LoadingNameText = "";
            LoadingInfoText = "";

            RespawnSettingsText = "\n// Respawn Asetukset \n" +
                                  "Respawn = BASE; \n" +
                                  "RespawnDelay = 3; \n" +
                                  "RespawnDialog = 0; \n" +
                                  "RespawnOnStart = 1; \n" +
                                  "RespawnTemplatesWest[] = {'Tickets','Counter','Wave','EndMission'}; \n";

            OtherSettingsText = "\n// Muut Asetukset \n" +
                                "onLoadIntroTime = false; \n" +
                                "onLoadMissionTime = false; \n" +
                                "disabledAI = 1; \n" +
                                "debriefing = 1; \n" +
                                "showGPS = 1; \n";

            allText = "";
        }

        private string infoText { get; set; }

        private string MissionSettingsText { get; set; }
        private string MissionGameTypeText { get; set; }
        private string MissionMinPlayersText { get; set; }
        private string MissionMaxPlayersText { get; set; }

        private string LoadingSettingsText { get; set; }
        private string LoadingImageText { get; set; }
        private string LoadingAuthorText { get; set; }
        private string LoadingNameText { get; set; }
        private string LoadingInfoText { get; set; }

        private string RespawnSettingsText { get; set; }
        private string OtherSettingsText { get; set; }

        private string allText { get; set; }

        public void updateMissionGameType(string type)
        {
            MissionGameTypeText = "  GameType = " + type + "; \n";
            MissionSettingsText = " \n\n// Tehtävän Asetukset \n" +
                                "class Header \n" +
                                "{ \n" +
                                MissionGameTypeText +
                                MissionMinPlayersText +
                                MissionMaxPlayersText +
                                "} \n";
        }

        public void updateMissionMaxPlayers(string Amount)
        {
            MissionMinPlayersText = "  MinPlayers = 1; \n";
            MissionMaxPlayersText = "  MaxPlayers = " + Amount + "; \n";
            MissionSettingsText = " \n\n// Tehtävän Asetukset \n" +
                                "class Header \n" +
                                "{ \n" +
                                MissionGameTypeText +
                                MissionMinPlayersText +
                                MissionMaxPlayersText +
                                "} \n";
        }

        public void updateLoadingImage(string Image)
        {
            LoadingSettingsText = "\n// Latausruudun Asetukset \n";
            LoadingImageText = @"LoadScreen = 'Images\" + Image + "'; \n";
        }

        public void updateLoadingAuthor(string Author)
        {
            LoadingSettingsText = "\n// Latausruudun Asetukset \n";
            LoadingAuthorText = "Author = " + Author + "; \n";          
        }

        public void updateLoadingName(string Name)
        {
            LoadingSettingsText = "\n// Latausruudun Asetukset \n";
            LoadingNameText = "OnLoadName = " + Name + "; \n";        
        }

        public void updateLoadingInfo(string Info)
        {
            LoadingSettingsText = "\n// Latausruudun Asetukset \n";
            LoadingInfoText = "OnLoadMission = " + Info + "; \n";
        }

        public string getText()
        {
            allText = infoText;

            allText += MissionSettingsText;

            allText += LoadingSettingsText;
            allText += LoadingImageText;
            allText += LoadingAuthorText;
            allText += LoadingNameText;
            allText += LoadingInfoText;

            allText += RespawnSettingsText;

            allText += OtherSettingsText;
            return allText;
        }

        public string missingCheck()
        {
            if (MissionGameTypeText == "")
            {
                return "Gametype puuttuu";
            }
            if (MissionMinPlayersText == "")
            {
                return "Minimi pelaajamäärä puuttuu";
            }
            if (MissionMaxPlayersText == "")
            {
                return "Maksimi pelaajamäärä puuttuu";
            }
            if (LoadingImageText == "")
            {
                return "Latauskuva puuttuu";
            }
            if (LoadingAuthorText == "")
            {
                return "Tekijän nimi puuttuu";
            }
            if (LoadingNameText == "")
            {
                return "Tehtävän nimi puuttuu";
            }
            if (LoadingInfoText == "")
            {
                return "Infoteksti puuttuu";
            }
            return "";
        }
    }
}
