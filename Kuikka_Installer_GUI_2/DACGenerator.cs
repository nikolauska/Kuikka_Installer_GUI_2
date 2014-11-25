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

            int index = 1;
            foreach(DAC dac in dacList)
            {
                String DacArray = @"0 = ['z" + index + @"', [[" + index + ", 0, 0], " + GetInfString(dac) + ", " + GetVehString(dac) + ", " + GetArmString(dac) + ", " + GetAirString(dac) + ", " + GetParamString(dac) + "]] spawn DAC_Zone";
                
                returnString += "if(!isNull " + "z" + index + ") then {" + "\n" +
                                "   z" + index + @" setTriggerStatements[""time > 0"",""" + DacArray + @""", """"];" + "\n" +
                                "} else {" + "\n" +
                                @"   titleText[""z" + index + @" triggeriä ei löydetty!"", ""PLAIN""];" + "\n" +
                                "};" + "\n" +
                                "\n";
                ++index;               
            }

            return returnString;
        }

        private String EmptyCheck(String value) 
        {
            if(value.Equals(""))
                return "0";

            return value;
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
