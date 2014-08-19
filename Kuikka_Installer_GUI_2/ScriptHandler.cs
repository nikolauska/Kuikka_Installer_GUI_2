using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Kuikka_Installer_GUI_2
{
    class ScriptHandler
    {
        /**************************************************************
         * Variables
        ***************************************************************/
        private class Script
        {
            public string Name { get; set; } // Name of the script
            public string Description { get; set; } // Name of the script
            public string Comment { get; set; } // Comment above code
            public string Code { get; set; } // Code to activate script
            public string Variable { get; set; } // Changeable variable if needed
            public string GroupingIndex { get; set; } // Prevents running multiple same type of scripts
        };

        private List<Script> AllScripts { get; set; }
        private List<Script> UsedScripts { get; set; }

        /**************************************************************
         * Initialization
        ***************************************************************/
        public ScriptHandler()
        {
            //AllScripts = JsonConvert.DeserializeObject<List<Script>>(File.ReadAllText(@"c:\UDK\NewItems2.json"));
        }

        /**************************************************************
         * Functions
         ***************************************************************/
        // Add new to used script list
        public string AddScript(int Index)
        {
            foreach (Script script in UsedScripts)
            {
                if(script.GroupingIndex == AllScripts[Index].GroupingIndex)
                {
                    return "Sinulla on jo tämän tyyppinen skripti: " + script.Code + ". Poista kyseinen skripti lisätäksesi tämän skriptin";
                }
            }
            UsedScripts.Add(AllScripts[Index]);

            return "";
        }

        // Remove script from used script list
        public void RemoveScripts(int Index)
        {
            UsedScripts.RemoveAt(Index);
        }

        // Edit variable for script
        public void EditScript(int Index, string variable)
        {
            UsedScripts[Index].Variable = variable;
        }

        // Return all script names
        public List<string> ReturnAllScriptNames()
        {
            List<string> tempList = new List<string>();

            foreach (Script script in AllScripts)
            {
                tempList.Add(script.Name);
            }

            return tempList;
        }

        // Return all script description
        public List<string> ReturnAllScriptDescription()
        {
            List<string> tempList = new List<string>();

            foreach (Script script in AllScripts)
            {
                tempList.Add(script.Description);
            }

            return tempList;
        }

        // Return Used script names
        public List<string> ReturnUsedScriptNames()
        {
            List<string> tempList = new List<string>();

            foreach (Script script in UsedScripts)
            {
                tempList.Add(script.Name);
            }

            return tempList;
        }

        // Return used script description
        public List<string> ReturnUsedScriptDescription()
        {
            List<string> tempList = new List<string>();

            foreach (Script script in UsedScripts)
            {
                tempList.Add(script.Description);
            }

            return tempList;
        }


        // Return Text for all the scripts
        public string ReturnScriptText()
        {
            string tempText = "";

            foreach(Script script in UsedScripts)
            {
                tempText += "\n";
                tempText += script.Comment + "\n";
                tempText += script.Variable + " execVM " + script.Code + "\n";
            }

            return tempText;
        }

    }  
}
