using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuikka_Installer_GUI_2
{
    class Briefing
    {
        public Briefing(MainWindow windowIn)
        {
            Briefings = new List<BriefingStruct>();
            PictureData = new List<PictureStruct>();
            MarkerData = new List<MarkerStruct>();

            window = windowIn;
        }

        private List<BriefingStruct> Briefings { get; set; }
        private List<PictureStruct> PictureData { get; set; }
        private List<MarkerStruct> MarkerData { get; set; }
        private MainWindow window { get; set; }
        

        private struct BriefingStruct
        {
            public string Title;
            public string Text;
        }

        private struct PictureStruct
        {
            public string Location;
            public string FileName;
            public string ID;
        }

        private struct MarkerStruct
        {
            public string MarkerName;
            public string MarkerText;
            public string ID;
        }

        /*
         * Add Functions
         */ 
        public void AddBriefing(string Title, string Text) 
        {
            BriefingStruct temp = new BriefingStruct();
            temp.Title = Title;
            temp.Text = Text;
            Briefings.Add(temp);

            window.Briefing_Title_TextBox.Text = temp.Title;
            window.Briefing_Code_TextBox.Text = temp.Text;
        }

        public void EditBriefing(int index, string Title, string Text)
        {
            BriefingStruct temp = new BriefingStruct();
            temp.Title = Title;
            temp.Text = Text;
            Briefings[index] = temp;

            window.Briefing_Title_TextBox.Text = temp.Title;
            window.Briefing_Code_TextBox.Text = temp.Text;
        }

        public void GetBriefing(int index)
        {
            window.Briefing_Title_TextBox.Text = Briefings[index].Title;
            window.Briefing_Code_TextBox.Text = Briefings[index].Text;
        }

        public void AddPicture(int BriefingIndex, string Location, string FileName)
        {
            PictureStruct picture = new PictureStruct();
            picture.Location = Location;
            picture.FileName = FileName;
            picture.ID = " {picture " + FileName + "} ";
            PictureData.Add(picture);

            BriefingStruct briefing = new BriefingStruct();
            briefing.Title = Briefings[BriefingIndex].Title;
            briefing.Text += picture.ID;
            Briefings[BriefingIndex] = briefing;
        }

        public void EditPicture(int BriefingIndex, int PictureIndex, string Location, string FileName)
        {          
            PictureStruct picture = new PictureStruct();
            picture.Location = Location;
            picture.FileName = FileName;
            picture.ID = " {picture " + FileName + "} ";

            BriefingStruct briefing = new BriefingStruct();
            briefing.Title = Briefings[BriefingIndex].Title;
            briefing.Text.Replace(PictureData[PictureIndex].ID, picture.ID);
            Briefings[BriefingIndex] = briefing;

            PictureData[PictureIndex] = picture;
        }


        public void AddMarker(int BriefingIndex, string MarkerName, string MarkerText)
        {
            MarkerStruct marker = new MarkerStruct();
            marker.MarkerName = MarkerName;
            marker.MarkerText = MarkerText;
            marker.ID = " {marker " + MarkerName + "} ";
            MarkerData.Add(marker);

            BriefingStruct briefing = new BriefingStruct();
            briefing.Title = Briefings[BriefingIndex].Title;
            briefing.Text = Briefings[BriefingIndex].Text + " {marker " + MarkerName + "} ";
            Briefings[BriefingIndex] = briefing;
        }

        public string getMarkerName(int MarkerIndex)
        {
            return MarkerData[MarkerIndex].MarkerName;
        }

        public string getMarkerText(int MarkerIndex)
        {
            return MarkerData[MarkerIndex].MarkerText;
        }

        public void EditMarker(int BriefingIndex, int MarkerIndex, string MarkerName, string MarkerText)
        {
            MarkerStruct marker = new MarkerStruct();
            marker.MarkerName = MarkerName;
            marker.MarkerText = MarkerText;
            marker.ID = " {marker " + MarkerName + "} ";

            BriefingStruct briefing = new BriefingStruct();
            briefing.Title = Briefings[BriefingIndex].Title;
            briefing.Text = MarkerData[MarkerIndex].MarkerText.Replace(MarkerData[MarkerIndex].ID, " {marker " + MarkerName + "} ");
            Briefings[BriefingIndex] = briefing;

            MarkerData[MarkerIndex] = marker;
        }

        /*
         * Remove Functions
         */
        public void RemovePicture(int BriefingIndex, int PictureIndex)
        {
            BriefingStruct briefing = new BriefingStruct();
            briefing.Title = Briefings[BriefingIndex].Title;
            briefing.Text.Replace(PictureData[PictureIndex].ID, " ");
            Briefings[BriefingIndex] = briefing;

            PictureData.RemoveAt(PictureIndex);
        }

        public void RemoveMarker(int BriefingIndex, int MarkerIndex)
        {
            BriefingStruct briefing = new BriefingStruct();
            briefing.Title = Briefings[BriefingIndex].Title;
            briefing.Text.Replace(MarkerData[MarkerIndex].ID, " ");
            Briefings[BriefingIndex] = briefing;

            MarkerData.RemoveAt(MarkerIndex);
        }

        /*
         * Return Functions
         */ 
        public List<string> GetTitles()
        {
            List<string> Titles = new List<string>();

            foreach (BriefingStruct briefing in Briefings)
            {
                Titles.Add(briefing.Title);
            }

            return Titles;
        }

        public List<string> GetPictureLocations()
        {
            List<string> PictureLocations = new List<string>();

            foreach (PictureStruct pair in PictureData)
            {
                PictureLocations.Add(pair.Location);
            }

            return PictureLocations;
        }

        public List<string> GetPictureNames()
        {
            List<string> PictureNames = new List<string>();

            foreach (PictureStruct pair in PictureData)
            {
                PictureNames.Add(pair.FileName);
            }

            return PictureNames;
        }

        public List<string> GetMarkerNames()
        {
            List<string> MarkerNames = new List<string>();

            foreach (MarkerStruct pair in MarkerData)
            {
                MarkerNames.Add(pair.MarkerName);
            }

            return MarkerNames;
        }

        public string GetRawCode(int BriefingIndex)
        {
            window.Briefing_Code_TextBox.Text = Briefings[BriefingIndex].Text;
            return Briefings[BriefingIndex].Text;
        }

        /*
        public string GetProcessedCode()
        {
            string ReturnCode = "";

            foreach (PictureStruct picture in PictureData)
            {
                ReturnCode = Code.Replace(picture.ID,
                                        @"<img image='Images\" + picture.FileName + "/>");
            }

            foreach (MarkerStruct marker in MarkerData)
	        {
                ReturnCode = Code.Replace(marker.ID,
                                        "<marker name=" + marker.MarkerName + ">" + marker.MarkerText + "</marker>");
	        }

            return ReturnCode;
        }
        */
    }
}
