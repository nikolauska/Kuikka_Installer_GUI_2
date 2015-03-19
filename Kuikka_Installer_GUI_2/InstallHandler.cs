using System;
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
        public int EditorIndex { get; set; }
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

        // Update text on main window
        private void UpdateText(string message)
        {
            window.TextBox_Setup_Text.AppendText(message);
        }     

        // Start install thread
        public void StartInstall() 
        {
            Thread installThread = new Thread(() => Installer());
            installThread.Start();
        }

        // Main installer method
        private void Installer()
        {
            String basePath = "";
            // Get base folder 
            if (NameIndex == 0)
            {
                basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Arma 3\";
            }
            else
            {
                basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Arma 3 - Other Profiles\" + profileName + @"\";
            }

            // Set base folder to editor type
            if (EditorIndex == 0)
            {
                basePath += @"MPmissions\";
            }
            else
            {
                basePath += @"missions\";
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

            // Main install function (should be run on different thread than GUI)
            if (startInstall)
            {
                // Create mission folder
                Directory.CreateDirectory(basePath);
                GUIUpdate("INFO: Tehtävä kansio luotu! \n");

                // Create editable folder
                Directory.CreateDirectory(basePath + @"\Muokattavat");
                GUIUpdate("INFO: Muokattavat kansio luotu! \n");

                // Create pictures folder
                Directory.CreateDirectory(basePath + @"\Muokattavat\Kuvat");
                GUIUpdate("INFO: Kuvat kansio luotu! \n");

                // Load mission.sqm
                GUIUpdate("INFO: Ladataan mission.sqm tiedostoa! \n");
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

                        // Download failed on needed file so remove whole mission folder and try again
                        DeleteDirectory(basePath);
                        return;
                    }
                }
                GUIUpdate("INFO: mission.sqm ladattu! \n");

                // Download scripts file
                GUIUpdate("INFO: Ladataan scripts.zip tiedostoa! \n");
                using (WebClient wb = new WebClient())
                {
                    try
                    {
                        wb.DownloadFile("https://dl.dropboxusercontent.com/u/43689307/scripts.zip", System.IO.Path.GetTempPath() + @"\scripts.zip");
                    }
                    catch (WebException webEx)
                    {                  
                        GUIUpdate("ERROR: scripts.zip tiedoston lataus epäonnistui. Asennus keskeytettiin! \n");
                        GUIUpdate("ERROR MESSGAGE: " + webEx.ToString() + "\n");

                        // Download failed on needed folder so remove whole mission folder and try again
                        DeleteDirectory(basePath);
                        return;
                    }
                }
                GUIUpdate("INFO: scripts.zip ladattu! \n");

                // Extract zip file
                ZipFile.ExtractToDirectory(System.IO.Path.GetTempPath() + @"\scripts.zip", basePath + @"\");
                GUIUpdate("INFO: scripts.zip purettu tehtävä kansioon! \n");

                // Create init.sqf
                CreateFile(basePath + @"\init.sqf", 
                        "/***************************************************************************************************************************** \n" +
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
                        @"[] execVM ""Muokattavat\Briefing.sqf"";" + "\n" +
                        "\n" +
                        @"[] execVM ""Muokattavat\OmaInit.sqf"";" + "\n" +
                        "\n" +
                        "//TASK FORCE ASETUKSET" + "\n" +
                        "tf_same_sw_frequencies_for_side = true;" + "\n" +
                        "tf_no_auto_long_range_radio = true;" + "\n" +
                        "tf_same_lr_frequencies_for_side = true;" + "\n" +
                        "TF_give_personal_radio_to_regular_soldier = false;" + "\n" +
                        "\n" +
                        "//Init UPSMON script" + "\n" +
                        @"call compile preprocessFileLineNumbers ""scripts\Init_UPSMON.sqf"";");
                GUIUpdate("INFO: init.sqf luotu! \n");

                // Create Description.ext
                CreateFile(basePath + @"\Description.ext", 
                        "/*****************************************************************************************************************************\n" +
                        "* Älä muokkaa tätä tiedostoa ellet tiedä varmaksi mitä olet tekemässä\n" +
                        "*****************************************************************************************************************************/\n" +
                        "\n" +
                        @"#include ""Muokattavat\MissionSettings.ext""" + "\n" +
                        "\n" +
                        "Respawn = 3;\n" +
                        "enableDebugConsole = 1;");
                GUIUpdate("INFO: Description.ext luotu! \n");

                // Create Briefing.sqf
                CreateFile(basePath + @"\Muokattavat\Briefing.sqf",
                        "/***************************************************************************************************************************** \n" +
                        "* Tämä tiedosto sisältää briefing tekstin \n" +
                        "*****************************************************************************************************************************/ \n" +
                        "\n" +
                        briefing.GenerateBriefing());
                GUIUpdate("INFO: Briefing.sqf luotu! \n");

                // Loading screen picture
                string picture = "";
                if (LoadingImage.Equals(""))
                {
                    // Download default if loadscreen image is not set
                    picture = @"Muokattavat\Kuvat\kuikka.jpg";

                    GUIUpdate("INFO: Ladataan Kuikka.jpg tiedostoa! \n");
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
                    // Copy set loading screen image to mission folder
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

                // Create MissionSettings.ext
                CreateFile(basePath + @"\Muokattavat\MissionSettings.ext", 
                        "/***************************************************************************************************************************\n" +
                        "* Tehtävän asetukset esim latausruudun tekstit/kuvat, pelimuoto, tehtävän nimi\n" +
                        "***************************************************************************************************************************/\n" +
                        "\n" +
                        "// Monipelivalikko\n" +
                        @"overviewText = """ + replaceAO(missionName) + @"""; // Teksti moninpelivalikossa" + "\n" +
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

                // Create omaInit file
                CreateFile(basePath + @"\Muokattavat\OmaInit.sqf", 
                        "/***************************************************************************************************************************\n" +
                        "* Täällä voit ajaa haluamasi omat skriptit\n" +
                        "***************************************************************************************************************************/\n");
                GUIUpdate("INFO: OmaInit.sqf luotu! \n");

                // Copy briefing images
                GUIUpdate(briefing.CopyImages(basePath));
                GUIUpdate("INFO: Briefing kuvat kopioitu! \n");

                GUIUpdate("INFO: Valmis!");

                // Set base folder to editor type
                if (EditorIndex == 0)
                {
                    MessageBox.Show("Tehtävä kansio on nyt luotu. \n\n" +
                                "Aloittaaksesi tehtävän editoinnin käynnistä ArmA 3 ja luo local serveri multiplayer valikosta ja valitse kartta johon loit tehtäväsi.\n\n" + 
                                "Oikealla puolella olevassa tehtävä valikossa pitäisi nyt näkyä luomasi tehtävä sinisellä. Valitse se ja avaa alhaalla olevasta edit napista");
                }
                else
                {
                    MessageBox.Show("Tehtävä kansio on nyt luotu. \n\n" +
                                "Aloittaaksesi tehtävän editoinnin valitse vain päävalikosta Editor ja kartta johon loit tehtäväsi. Load napin alta pitäisi löytyä luomasi tehtävä.");
                }
                
            }
        }

        // Create file function
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

        // Update text on main window invoker
        private void GUIUpdate(String message) 
        {
            window.TextBox_Setup_Text.Dispatcher.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { message });
        }

        // Replaces ä and ö with a and o 
        private String replaceAO(String text)
        {
            text = text.Replace('ä', 'a');
            text = text.Replace('ö', 'o');

            return text;
        }

        // Delete folder and everything in it
        private static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            // Delete all files inside 
            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            // Delete every folder recursively 
            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            try
            {
                Directory.Delete(target_dir, false);
            }
            catch (DirectoryNotFoundException)
            {
                return; // Exit when everything is deleted
            }
            catch (IOException)
            {  
                // Folder could not be deleted
                MessageBox.Show("Kansiota " + Path.GetDirectoryName(target_dir) + " ei voitu poistaa! \n\nVarmista, että muut prosessit eivät käytä kansiota");
                DeleteDirectory(target_dir);
            }
            
        }
    }
}
