using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB.MODEL
{
    public class Floor
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public bool IsDel {  get; set; }
        public int BuildingId {  get; set; } // 楼房id
    }
}
