using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuikka_Installer_GUI_2
{
    class Side
    {
        public Side()
        {
            this.Tech = "";
            this.Tehtavat = "";
            this.Tiedustelu = "";
            this.Tilanne = "";
        }
        public Side(Side side)
        {
            this.Tech = side.Tech;
            this.Tehtavat = side.Tehtavat;
            this.Tiedustelu = side.Tiedustelu;
            this.Tilanne = side.Tilanne;
        }
        public String Tech { get; set; }
        public String Tiedustelu { get; set; }
        public String Tehtavat { get; set; }
        public String Tilanne { get; set; }
    }
}
