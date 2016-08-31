using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication4
{
   public class Busstop
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Busstop(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        public override string ToString()
        {
            string named = ID + " " + Name;
            return named;
        }
    }
}
