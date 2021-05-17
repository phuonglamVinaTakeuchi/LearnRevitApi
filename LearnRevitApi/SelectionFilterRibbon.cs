using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnSelectionFilter
{
    public class SelectionFilterRibbon : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {


            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
