using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuikka_Installer_GUI_2
{
    class DACGenerator
    {
        private List<DAC> dacList;
        private MainWindow window;

        public DACGenerator(MainWindow window)
        {
            dacList = new List<DAC>();
            this.window = window;
        }

        public String GeneratedCode(List<DAC> dacList)
        {
            dacList = new List<DAC>(dacList);

            String returnString = "";

            foreach(DAC dac in dacList)
            {
                String DacArray = "[" + GetInfString(dac) + ", " + GetVehString(dac) + "] spawn DAC_Zone";
                returnString += @"_trg1=createTrigger[""EmptyDetector"",getmarkerpos """ + dac.Marker + @"""];" + "\n" +
                                @"_shape = if (markershape """ + dac.Marker + @""" == ""Rectangle"") then {true} else {false};" + "\n" +
                                @"_trg1 setTriggerArea [ getMarkerSize """ + dac.Marker + @""" select 0, getMarkerSize """ + dac.Marker + @""" select 1,markerdir """ + dac.Marker + @""",_shape];" + "\n" +
                                @"_trg1 setTriggerActivation[""WEST"",""NOT PRESENT"",FALSE];" + "\n" +
                                @"_trg1 setTriggerStatements[""time > 0"",""" + DacArray + @"""];";
                                
            }

            return returnString;
        }

        private String GetInfString(DAC dac)
        {
            if (dac.InfGroupAmount.Equals("") && dac.InfGroupSize.Equals("") && dac.InfWaypointAmount.Equals("") && dac.InfGroupWaypointAmount.Equals(""))
                return "[]";

            if (!dac.InfGroupAmount.Equals("") || !dac.InfGroupSize.Equals("") || !dac.InfWaypointAmount.Equals("") || !dac.InfGroupWaypointAmount.Equals(""))
            {
                return "[" + dac.InfGroupAmount + ", " + dac.InfGroupSize + ", " + dac.InfWaypointAmount + ", " + dac.InfGroupWaypointAmount + "]";
            }
            return "";
        }

        private String GetVehString(DAC dac)
        {
            if (dac.VehGroupAmount.Equals("") && dac.VehGroupSize.Equals("") && dac.VehWaypointAmount.Equals("") && dac.VehGroupWaypointAmount.Equals(""))
                return "[]";

            if (!dac.VehGroupAmount.Equals("") || !dac.VehGroupSize.Equals("") || !dac.VehWaypointAmount.Equals("") || !dac.VehGroupWaypointAmount.Equals(""))
            {
                return "[" + dac.VehGroupAmount + ", " + dac.VehGroupSize + ", " + dac.VehWaypointAmount + ", " + dac.VehGroupWaypointAmount + "]";
            }
            return "";
        }
    }
}
