using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuikka_Installer_GUI_2
{
    class DACHandler
    {
        private List<DAC> dacList;

        public DACHandler()
        {
            dacList = new List<DAC>();
        }

        public void AddDac(DAC dac)
        {
            dacList.Add(dac);
        }

        public void EditDac(DAC dac, int index)
        {
            dacList[index] = dac;
        }

        public List<DAC> getList()
        {
            return dacList;
        }

        public DAC getSelected(int index)
        {
            return dacList[index];
        }

        public void removeSelected(String ID)
        {     
            List<DAC> tempList = new List<DAC>();

            foreach (DAC dac in dacList)
            {
                if (!dac.ID.Equals(ID))
                    tempList.Add(dac);
            }

            dacList = new List<DAC>(tempList);
        }

        public String GenerateCode()
        {
            return new DACGenerator().GeneratedCode(dacList);
        }
    }
}
