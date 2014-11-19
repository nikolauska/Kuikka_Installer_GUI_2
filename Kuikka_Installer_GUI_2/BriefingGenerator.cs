using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuikka_Installer_GUI_2
{
    class BriefingGenerator
    {
        private Side EditedSide;

        public String Generate(Side side)
        {
            this.EditedSide = new Side(side);
          
            this.ReplaceLineChange();
            this.EditToSqfFormat();
            this.AddBeginningEmpty();

            String returnText = this.EditedSide.Tech + "\n" +
                                this.EditedSide.Tehtavat + "\n" +
                                this.EditedSide.Tiedustelu + "\n" +
                                this.EditedSide.Tilanne;

            return returnText;
        }

        private void ReplaceLineChange()
        {
            this.EditedSide.Tech = this.EditedSide.Tech.Replace("\r\n", "<br/>");
            this.EditedSide.Tehtavat = this.EditedSide.Tehtavat.Replace("\r\n", "<br/>");
            this.EditedSide.Tiedustelu = this.EditedSide.Tiedustelu.Replace("\r\n", "<br/>");
            this.EditedSide.Tilanne = this.EditedSide.Tilanne.Replace("\r\n", "<br/>");
        }

        private void EditToSqfFormat()
        {
            this.EditedSide.Tech = "player createDiaryRecord['Diary', ['Tech&Info', '" + this.EditedSide.Tech + "']];";
            this.EditedSide.Tehtavat = "player createDiaryRecord['Diary', ['Tehtävät', '" + this.EditedSide.Tehtavat + "']];";
            this.EditedSide.Tiedustelu = "player createDiaryRecord['Diary', ['Tiedustelu', '" + this.EditedSide.Tiedustelu + "']];";
            this.EditedSide.Tilanne = "player createDiaryRecord['Diary', ['Tilanne', '" + this.EditedSide.Tilanne + "']];";
        }

        private void AddBeginningEmpty()
        {
            this.EditedSide.Tech = "     " + this.EditedSide.Tech;
            this.EditedSide.Tehtavat = "     " + this.EditedSide.Tehtavat;
            this.EditedSide.Tiedustelu = "     " + this.EditedSide.Tiedustelu;
            this.EditedSide.Tilanne = "     " + this.EditedSide.Tilanne;
        }
    }
}
