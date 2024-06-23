using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.MODEL
{
    public class SingleBuilding
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Lat {  get; set; }
        public string? Lng {  get; set; }
        public bool IsDel { get; set; }
        public int Number {  get; set; }
    }
}
