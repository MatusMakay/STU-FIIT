using PSIP_Project.src.service.traffic;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSIP_Project.src.model;

namespace PSIP_Project.src.service.guitimer
{
    internal class GuiTimer
    {
        
        DataModel model;
        private bool countdown = false;

        // janciho metoda thread a vo while cykle bude sleep a odcitaj minus jednotku

        public GuiTimer(DataModel model)
        {
            this.model = model;
        }

        public void startMinusOne()
        {
            countdown = true;

            while (countdown) 
            {
                Thread.Sleep(1000);

                model.minusOne();

            }
        }
       
    }
}
