using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuikka_Installer_GUI_2
{
    class Briefing
    {
        private Side west;
        private Side east;
        private Side independent;
        private Side Civilian;
        private BriefingGenerator generator;
        private List<String> Images;

        public Briefing()
        {
            west = new Side();
            east = new Side();
            independent = new Side();
            Civilian = new Side();
            generator = new BriefingGenerator();
            Images = new List<String>();
        }

        public void setText(String Side, String Title, String Text)
        {
            switch (Side)
            {
                case "WEST":
                    {
                        switch (Title)
                        {
                            case "Tech/Info":
                                west.Tech = Text;
                                break;
                            case "Tiedustelu":
                                west.Tiedustelu = Text;
                                break;
                            case "Tehtävät":
                                west.Tehtavat = Text;
                                break;
                            case "Tilanne":
                                west.Tilanne = Text;
                                break;
                        }
                        break;
                    }
                case "EAST":
                    {
                        switch (Title)
                        {
                            case "Tech/Info":
                                east.Tech = Text;
                                break;
                            case "Tiedustelu":
                                east.Tiedustelu = Text;
                                break;
                            case "Tehtävät":
                                east.Tehtavat = Text;
                                break;
                            case "Tilanne":
                                east.Tilanne = Text;
                                break;
                        }
                        break;
                    }
                case "INDEPENDENT":
                    {
                        switch (Title)
                        {
                            case "Tech/Info":
                                independent.Tech = Text;
                                break;
                            case "Tiedustelu":
                                independent.Tiedustelu = Text;
                                break;
                            case "Tehtävät":
                                independent.Tehtavat = Text;
                                break;
                            case "Tilanne":
                                independent.Tilanne = Text;
                                break;
                        }
                        break;
                    }
                case "CIVILIAN":
                    {
                        switch (Title)
                        {
                            case "Tech/Info":
                                Civilian.Tech = Text;
                                break;
                            case "Tiedustelu":
                                Civilian.Tiedustelu = Text;
                                break;
                            case "Tehtävät":
                                Civilian.Tehtavat = Text;
                                break;
                            case "Tilanne":
                                Civilian.Tilanne = Text;
                                break;
                        }
                        break;
                    }
            }
        }

        public String getText(String Side, String Title)
        {
            switch (Side)
            {
                case "WEST":
                    {
                        switch (Title)
                        {
                            case "Tech/Info":
                                return west.Tech;
                            case "Tiedustelu":
                                return west.Tiedustelu;
                            case "Tehtävät":
                                return west.Tehtavat;
                            case "Tilanne":
                                return west.Tilanne;
                        }
                        break;
                    }
                case "EAST":
                    {
                        switch (Title)
                        {
                            case "Tech/Info":
                                return east.Tech;
                            case "Tiedustelu":
                                return east.Tiedustelu;
                            case "Tehtävät":
                                return east.Tehtavat;
                            case "Tilanne":
                                return east.Tilanne;
                        }
                        break;
                    }
                case "INDEPENDENT":
                    {
                        switch (Title)
                        {
                            case "Tech/Info":
                                return independent.Tech;
                            case "Tiedustelu":
                                return independent.Tiedustelu;
                            case "Tehtävät":
                                return independent.Tehtavat;
                            case "Tilanne":
                                return independent.Tilanne;
                        }
                        break;
                    }
                case "CIVILIAN":
                    {
                        switch (Title)
                        {
                            case "Tech/Info":
                                return Civilian.Tech;
                            case "Tiedustelu":
                                return Civilian.Tiedustelu;
                            case "Tehtävät":
                                return Civilian.Tehtavat;
                            case "Tilanne":
                                return Civilian.Tilanne;
                        }
                        break;
                    }
            }
            return "ERROR " + Side + ", " + Title;
        }

        public String GenerateBriefing()
        {
            String returnText = "";

            returnText = "switch (playerSide) do {\n" +
                         "  case WEST: { \n" +
                         generator.Generate(west) + "\n" +
                         "  }; \n" +
                         "  case EAST: { \n" +
                         generator.Generate(east) + "\n" +
                         "  }; \n" +
                         "  case INDEPENDENT: { \n" +
                         generator.Generate(independent) + "\n" +
                         "  }; \n" +
                         "  case CIVILIAN: { \n" +
                         generator.Generate(Civilian) + "\n" +
                         "  }; \n" +
                         "};";

            return returnText;
        }

        public void AddMarker(String Side, String Title, String Name, String Text)
        {
            this.setText(Side, Title, this.getText(Side, Title) + @" <marker name=""" + Name + @""">" + Text + @"<marker>");
        }

        public void AddPicture(String Side, String Title, String Location)
        {
            this.Images.Add(Location);
            this.setText(Side, Title, this.getText(Side, Title) + @" <img image=""Muokattavat\Kuvat\" + new DirectoryInfo(Location).Name + @" ""/>");
            
        }

        public String getExplanation(String Title)
        {
            switch (Title)
            {
                case "Tech/Info":
                    return "Tietoa missionisi käyttämistäsi skripteistä esim Respawn, JIP jne.";
                case "Tiedustelu":
                    return "Mitä tietoa haluat antaa pelaajille ennen missionin aloitusta.";
                case "Tehtävät":
                    return "Mitä tehtäviä pelaajilla on missionissa.";
                case "Tilanne":
                    return "Missionisi tarina";
            }
            return "";
        }

        public String CopyImages(String basePath)
        {
            String Errors = "";
            foreach(String Image in Images) 
            {
                try
                {
                    File.Copy(Image, basePath + @"\Muokattavat\Kuvat\" + new DirectoryInfo(Image).Name);
                }
                catch (IOException copyError)
                {
                    Errors += "ERROR: Latauskuvan kopiointi epäonnistui. Latausruudun kuva jätetään tyhjäksi! \n";
                    Errors += "ERROR MESSAGE: " + copyError.ToString() + "\n";
                }  
            }

            return Errors;
        }
    }
}
