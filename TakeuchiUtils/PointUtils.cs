using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeuchiUtils
{
    public static class PointUtils
    {
        public static  XYZ GetMidPoint(XYZ p1,XYZ p2)
        {
            //var x = (p1.X + p2.X) / 2;
            //var y = (p1.Y + p2.Y) / 2;
            //var z = (p1.Z + p2.Z) / 2;
            return (p1 + p2) / 2;
            //return new XYZ(x, y,z);
        }
    }
}
