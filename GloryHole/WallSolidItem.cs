using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloryHole
{
    public class WallSolidItem
    {
        public Solid WallSolid { get; set; }
        public XYZ WallOrientation { get; set; }
        public double Width { get; set; }
    }
}
