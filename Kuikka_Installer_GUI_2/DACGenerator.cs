using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuikka_Installer_GUI_2
{
    class DACGenerator
    {
        public DACGenerator(){}

        public String GeneratedCode(List<DAC> dacListIn)
        {
            List<DAC> dacList = new List<DAC>(dacListIn);

            String returnString = "";

            foreach(DAC dac in dacList)
            {
                String DacArray = @"0 = [""z" + dac.ID.ToString() + @""", [" + GetInfString(dac) + ", " + GetVehString(dac) + GetArmString(dac) + GetAirString(dac) + GetParamString(dac) + "]] spawn DAC_Zone";
                returnString += @"_trg1=createTrigger[""EmptyDetector"",getmarkerpos ""z" + dac.ID.ToString() + @"""];" + "\n" +
                                @"_shape = if (markershape ""z" + dac.ID.ToString() + @""" == ""Rectangle"") then {true} else {false};" + "\n" +
                                @"_trg1 setTriggerArea [ getMarkerSize ""z" + dac.ID.ToString() + @""" select 0, getMarkerSize ""z" + dac.ID.ToString() + @""" select 1,markerdir ""z" + dac.ID.ToString() + @""",_shape];" + "\n" +
                                @"_trg1 setTriggerActivation[""WEST"",""NOT PRESENT"",FALSE];" + "\n" +
                                @"_trg1 setTriggerStatements[""time > 0"",""" + DacArray + @"""];";
                                
            }

            return returnString;
        }

        private String GetInfString(DAC dac)
        {
            if (dac.InfGroupAmount.Equals("") && dac.InfGroupSize.Equals("") && dac.InfWaypointAmount.Equals("") && dac.InfGroupWaypointAmount.Equals(""))
                return "[]";
            else
                return "[" + dac.InfGroupAmount + ", " + dac.InfGroupSize + ", " + dac.InfWaypointAmount + ", " + dac.InfGroupWaypointAmount + "]";

        }

        private String GetVehString(DAC dac)
        {
            if (dac.VehGroupAmount.Equals("") && dac.VehGroupSize.Equals("") && dac.VehWaypointAmount.Equals("") && dac.VehGroupWaypointAmount.Equals(""))
                return "[]";
            else
                return "[" + dac.VehGroupAmount + ", " + dac.VehGroupSize + ", " + dac.VehWaypointAmount + ", " + dac.VehGroupWaypointAmount + "]";
        }

        private String GetArmString(DAC dac)
        {
            if (dac.ArmGroupAmount.Equals("") && dac.ArmGroupSize.Equals("") && dac.ArmWaypointAmount.Equals("") && dac.ArmGroupWaypointAmount.Equals(""))
                return "[]";
            else
                return "[" + dac.ArmGroupAmount + ", " + dac.ArmGroupSize + ", " + dac.ArmWaypointAmount + ", " + dac.ArmGroupWaypointAmount + "]";
        }

        private String GetAirString(DAC dac)
        {
            if (dac.AirGroupAmount.Equals("") && dac.AirGroupSize.Equals("") && dac.AirWaypointAmount.Equals("") && dac.AirGroupWaypointAmount.Equals(""))
                return "[]";
            else
                return "[" + dac.AirGroupAmount + ", " + dac.AirGroupSize + ", " + dac.AirWaypointAmount + ", " + dac.AirGroupWaypointAmount + "]";
        }

        private String GetParamString(DAC dac)
        {
                return "[" + dac.Side + ", " + dac.Faction + ", 0, 0]";
        }
    }
}
