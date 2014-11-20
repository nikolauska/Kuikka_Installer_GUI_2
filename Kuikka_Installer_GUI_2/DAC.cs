using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuikka_Installer_GUI_2
{
    class DAC
    {
        /***********Init*******************/
        public int ID { get; set; }
        // Default: 0
        // Default: 0
        /***********Infantry***************/
        public String InfGroupAmount { get; set; }
        public String InfGroupSize { get; set; }
        public String InfWaypointAmount { get; set; }
        public String InfGroupWaypointAmount { get; set; }
        /***********Vehicle***************/
        public String VehGroupAmount { get; set; }
        public String VehGroupSize { get; set; }
        public String VehWaypointAmount { get; set; }
        public String VehGroupWaypointAmount { get; set; }
        /***********Armored***************/
        public String ArmGroupAmount { get; set; }
        public String ArmGroupSize { get; set; }
        public String ArmWaypointAmount { get; set; }
        public String ArmGroupWaypointAmount { get; set; }
        /***********Air*******************/
        public String AirGroupAmount { get; set; }
        public String AirGroupSize { get; set; }
        public String AirWaypointAmount { get; set; }
        public String AirGroupWaypointAmount { get; set; }
        /***********Params*******************/
        public String Side { get; set; }
        public String Faction { get; set; }
        // Side again

        public DAC()
        {
            ID = 0;
            InfGroupAmount = "";
            InfGroupSize = "";
            InfWaypointAmount = "";
            InfGroupWaypointAmount = "";
            VehGroupAmount = "";
            VehGroupSize = "";
            VehWaypointAmount = "";
            VehGroupWaypointAmount = "";
            ArmGroupAmount = "";
            ArmGroupSize = "";
            ArmWaypointAmount = "";
            ArmGroupWaypointAmount = "";
            AirGroupAmount = "";
            AirGroupSize = "";
            AirWaypointAmount = "";
            AirGroupWaypointAmount = "";
            Side = "";
            Faction = "";
        }

        public DAC(DAC dac)
        {
            ID = dac.ID;
            InfGroupAmount = dac.InfGroupAmount;
            InfGroupSize = dac.InfGroupSize;
            InfWaypointAmount = dac.InfWaypointAmount;
            InfGroupWaypointAmount = dac.InfGroupWaypointAmount;
            VehGroupAmount = dac.VehGroupAmount;
            VehGroupSize = dac.VehGroupSize;
            VehWaypointAmount = dac.VehWaypointAmount;
            VehGroupWaypointAmount = dac.VehGroupWaypointAmount;
            ArmGroupAmount = dac.ArmGroupAmount;
            ArmGroupSize = dac.ArmGroupSize;
            ArmWaypointAmount = dac.ArmWaypointAmount;
            ArmGroupWaypointAmount = dac.ArmGroupWaypointAmount;
            AirGroupAmount = dac.AirGroupAmount;
            AirGroupSize = dac.AirGroupSize;
            AirWaypointAmount = dac.AirWaypointAmount;
            AirGroupWaypointAmount = dac.AirGroupWaypointAmount;
            Side = dac.Side;
            Faction = dac.Faction;
        }
    }
}
