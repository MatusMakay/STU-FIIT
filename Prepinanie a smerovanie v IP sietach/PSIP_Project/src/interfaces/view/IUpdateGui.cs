using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSIP_Project.src.model
{
    public delegate void UpdateEth(string eth, List<string> protocols, List<int> counts);

    internal interface IUpdateGui
    {
        event UpdateEth updateInEth1;
    }
}
