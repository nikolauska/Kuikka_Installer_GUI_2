﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Kuikka_Installer_GUI_2
{
    class InstallHandler
    {
        public int NameIndex { get; set; }
        public String profileName { get; set; }
        public String missionName { get; set; }
        public String MaxPlayers { get; set; }
        public String gameType { get; set; }
        public String LoadingText { get; set; }
        public String LoadingAuthor { get; set; }
        public String LoadingImage { get; set; }
        public String MissionMap { get; set; }

        private MainWindow window { get; set; }
        private Briefing briefing { get; set; }

        public InstallHandler(MainWindow window, Briefing briefing) 
        {
            this.window = window;
            this.briefing = briefing;

            NameIndex = 0;
            profileName = "";
            missionName = "";
            MaxPlayers = "";
            gameType = "";
            LoadingText = "";
            LoadingAuthor = "";
            LoadingImage = "";
            MissionMap = "";
        }

        private delegate void UpdateTextCallback(string message);

        private void UpdateText(string message)
        {
            window.TextBox_Setup_Text.AppendText(message);
        }     

        public void StartInstall() 
        {
            Thread installThread = new Thread(() => Installer());
            installThread.Start();
        }

        private void Installer()
        {
            String basePath = "";
            // Get base folder 
            if (NameIndex == 0)
            {
                basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Arma 3\MPmissions\";
            }
            else
            {
                basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Arma 3 - Other Profiles\" + profileName + @"\MPmissions\";
            }

            // Check if mission name exists
            if (missionName.Equals(""))
            {
                GUIUpdate("ERROR: Tehtävän nimi ei voi olla tyhjä!");
                return;
            }

            // Check if max player exists
            if (MaxPlayers.Equals(""))
            {
                GUIUpdate("ERROR: Pelaajien maksimimäärä ei voi olla tyhjä!");
                return;
            }

            // Check if max player is number
            try
            {
                Convert.ToInt32(MaxPlayers);
            }
            catch (FormatException eror)
            {
                GUIUpdate("ERROR: Annettu maksimi pelaajamäärä ei ole numero!");
                return;
            }

            // Create mission directory
            basePath += replaceAO(missionName.Replace(' ', '_') + "." + MissionMap);

            bool startInstall = true;

            if (Directory.Exists(basePath))
            {
                var result = MessageBox.Show("Kyseinen tehtävä on jo olemassa. Halautko korvata sen uudella?", "", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.No)
                {
                    startInstall = false;
                }
                else
                {
                    DeleteDirectory(basePath);
                }
            }

            if (startInstall)
            {
                Directory.CreateDirectory(basePath);
                GUIUpdate("INFO: Tehtävä kansio luotu! \n");
                Directory.CreateDirectory(basePath + @"\Muokattavat");
                GUIUpdate("INFO: Muokattavat kansio luotu! \n");
                Directory.CreateDirectory(basePath + @"\Muokattavat\Kuvat");
                GUIUpdate("INFO: Kuvat kansio luotu! \n");

                using (WebClient wb = new WebClient())
                {
                    try
                    {
                        wb.DownloadFile("https://dl.dropboxusercontent.com/u/43689307/mission.sqm", basePath + @"\mission.sqm");
                    }
                    catch (WebException webEx)
                    {
                        GUIUpdate("ERROR: mission.sqm tiedoston lataus epäonnistui. Asennus keskeytettiin!\n");
                        GUIUpdate("ERROR MESSGAGE: " + webEx.ToString() + "\n");

                        DeleteDirectory(basePath);
                        return;
                    }
                }

                GUIUpdate("INFO: mission.sqm ladattu! \n");

                using (WebClient wb = new WebClient())
                {
                    try
                    {
                        wb.DownloadFile("https://dl.dropboxusercontent.com/u/43689307/DAC.zip", System.IO.Path.GetTempPath() + @"\DAC.zip");
                    }
                    catch (WebException webEx)
                    {
                        GUIUpdate("ERROR: DAC modin lataus epäonnistui. Asennus keskeytettiin! \n");
                        GUIUpdate("ERROR MESSGAGE: " + webEx.ToString() + "\n");

                        DeleteDirectory(basePath);
                        return;
                    }
                }

                GUIUpdate("INFO: DAC.zip ladattu! \n");

                ZipFile.ExtractToDirectory(System.IO.Path.GetTempPath() + @"\DAC.zip", basePath + @"\");
                GUIUpdate("INFO: DAC.zip purettu tehtävä kansioon! \n");

                // Create init.sqf
                CreateFile(basePath + @"\init.sqf", "/***************************************************************************************************************************** \n" +
                        "* Älä muokkaa tätä tiedostoa ellet tiedä varmasti mitä olet tekemässä \n" +
                        "*****************************************************************************************************************************/ \n" +
                        "\n" +
                        "//JIP initialisointi \n" +
                        "if (!isDedicated && (player != player)) then \n" +
                        "{ \n" +
                        "    waitUntil {player == player}; \n" +
                        "    waitUntil {time > 10}; \n" +
                        "}; \n" +
                        "\n" +
                        "if(paramsArray select 0 == 1) then{\n" +
                        "    if(isServer) then{\n" +
                        "        HCPresent = true;\n" +
                        "        publicVariable 'HCPresent';\n" +
                        "    };\n" +
                        "    if (!hasInterface && !isServer) then{\n" +
                        "        HCName = name player; \n" +
                        "        publicVariable 'HCName';\n" +
                        "    };\n" +
                        "} else{\n" +
                        "    if(isServer) then{\n" +
                        "        HCPresent = false;\n" +
                        "        HCName = 'NOONE';\n" +
                        "        publicVariable 'HCPresent';\n" +
                        "        publicVariable 'HCName';\n" +
                        "    };\n" +
                        "};\n" +
                        "\n" +
                        "//DAC Init\n" +
                        "DAC_Basic_Value = 0;\n" +
                        @"execVM 'DAC\DAC_Config_Creator.sqf';" + "\n" +
                        "\n" +
                        @"[] execVM 'Muokattavat\Briefing.sqf';" + "\n" +
                        "\n" +
                        @"[] execVM 'Muokattavat\OmaInit.sqf';");
                GUIUpdate("INFO: init.sqf luotu! \n");

                // Create Description.ext
                CreateFile(basePath + @"\Description.ext", "/*****************************************************************************************************************************\n" +
                        "* Älä muokkaa tätä tiedostoa ellet tiedä varmaksi mitä olet tekemässä\n" +
                        "*****************************************************************************************************************************/\n" +
                        "\n" +
                        @"#include ""Muokattavat\MissionSettings.ext""" + "\n" +
                        "\n" +
                        "//CLASS PARAMS HC CLIENTILLE\n" +
                        "class Params\n" +
                        "{\n" +
                        "    class HeadlessClient\n" +
                        "    {\n" +
                        @"        title = ""Headless Client""" + "\n" +
                        "        values[]= {0,1};\n" +
                        @"        texts[] = {""OFF"",""ON""};" + "\n" +
                        "        default = 0;\n" +
                        "    };\n" +
                        "};\n" +
                        "\n" +
                        "Respawn = 3;\n" +
                        "enableDebugConsole = 1;");
                GUIUpdate("INFO: Description.ext luotu! \n");

                // Create Briefing.sqf
                CreateFile(basePath + @"\Muokattavat\Briefing.sqf", briefing.GenerateBriefing());
                GUIUpdate("INFO: Briefing.sqf luotu! \n");

                // Create MissionSettings.ext
                string picture = "";
                if (LoadingImage.Equals(""))
                {
                    picture = @"Muokattavat\Kuvat\kuikka.jpg";

                    using (WebClient wb = new WebClient())
                    {
                        try
                        {
                            wb.DownloadFile("https://dl.dropboxusercontent.com/u/43689307/Kuikka.jpg", basePath + @"\Muokattavat\Kuvat\kuikka.jpg");
                        }
                        catch (WebException webEx)
                        {
                            GUIUpdate("ERROR: Vakio latauskuvan lataaminen epäonnistui. Latausruudun kuva jätetään tyhjäksi! \n");
                            GUIUpdate("ERROR MESSGAGE: " + webEx.ToString() + "\n");
                        }
                    }
                }
                else
                {
                    try
                    {
                        File.Copy(LoadingImage, basePath + @"\Muokattavat\Kuvat\" + new DirectoryInfo(LoadingImage).Name);
                        picture = @"Muokattavat\Kuvat\" + new DirectoryInfo(LoadingImage).Name;
                    }
                    catch (IOException copyError)
                    {
                        GUIUpdate("ERROR: Latauskuvan kopiointi epäonnistui. Latausruudun kuva jätetään tyhjäksi! \n");
                        GUIUpdate("ERROR MESSGAGE: " + copyError.ToString() + "\n");
                    }
                }
                CreateFile(basePath + @"\Muokattavat\MissionSettings.ext", 
                        "/***************************************************************************************************************************\n" +
                        "* Tehtävän asetukset esim latausruudun tekstit/kuvat, pelimuoto, tehtävän nimi\n" +
                        "***************************************************************************************************************************/\n" +
                        "\n" +
                        "// Monipelivalikko\n" +
                        @"overviewText = """ + replaceAO(missionName) + @"""; // Teksti moninpelivalikossa" + "\n" +
                        @"overviewPicture = """ + picture + @"""; // Kuva moninpelivalikossa" + "\n" +
                        "\n" +
                        "// Latausruutu\n" +
                        @"onLoadName = """ + replaceAO(missionName) + @"""; // Missun nimi latausruudussa" + "\n" +
                        @"onLoadMission = """ + replaceAO(LoadingText) + @"""; // Latausruudussa alhalla näkyvä teksti" + "\n" +
                        @"author = """ + replaceAO(LoadingAuthor) + @"""; // Tekijä latausruudussa" + "\n" +
                        @"loadScreen = """ + picture + @""";  // Kuva latausruudussa" + "\n" +
                        "\n" +
                        "// Tehtävän asetukset\n" +
                        "class Header\n" +
                        "{\n" +
                        "    gameType = " + gameType + "; // Pelimuoto\n" +
                        "    minPlayers = 1;  // Minimi pelaajamäärä\n" +
                        "    maxPlayers = " + MaxPlayers + "; // Maksimi pelaajamäärä\n" +
                        "};\n" +
                        "\n" +
                        "// Respawn asetukset\n" +
                        "RespawnDelay = 5; 	// Kuinka pitkään kunnes pelaaja respawnaa");
                GUIUpdate("INFO: MissionSettings.ext luotu! \n");


                CreateFile(basePath + @"\Muokattavat\OmaInit.sqf", "");
                GUIUpdate("INFO: OmaInit.sqf luotu! \n");

                GUIUpdate(briefing.CopyImages(basePath));
                GUIUpdate("INFO: Briefing kuvat kopioitu!");

                MessageBox.Show("Tehtävä kansio on nyt luotu. Aloittaaksesi tehtävän editoinnin käynnistä ArmA 3 ja luo local serveri multiplayer valikosta.\n\nSieltä löydät luodun tehtävän ja voit alkaa muokkaamaan sitä.");
            }
        }

        private void CreateFile(String FileLoc, String text)
        {
            // Create Briefing.sqf
            using (FileStream fs = File.Create(FileLoc))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(text);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
        }

        private void GUIUpdate(String message) 
        {
            window.TextBox_Setup_Text.Dispatcher.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { message });
        }

        private String replaceAO(String text)
        {
            text = text.Replace('ä', 'a');
            text = text.Replace('ö', 'o');

            return text;
        }

        private static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }
    }
}