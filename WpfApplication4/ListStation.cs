using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication4
{
    public class ListStation: Busstop
    {
        public ListStation(int id, string name):base(id,name)
        {
           
        }

        public override string ToString()
        {
            string named = Name;
            return named;
        }
    }
}
