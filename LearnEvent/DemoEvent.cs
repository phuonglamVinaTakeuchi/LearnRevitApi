using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEvent
{
    public class DemoEvent : IExternalDBApplication
    {
        public ExternalDBApplicationResult OnShutdown(ControlledApplication application)
        {
            application.DocumentChanged -= ElementChangedEvent;
            return ExternalDBApplicationResult.Succeeded;
        }

        public ExternalDBApplicationResult OnStartup(ControlledApplication application)
        {
            application.DocumentChanged += ElementChangedEvent;
            return ExternalDBApplicationResult.Succeeded;
        }
        public void ElementChangedEvent(object sender, DocumentChangedEventArgs args)
        {
            var elementFilter = new ElementCategoryFilter(BuiltInCategory.OST_Doors);
            var elementId = args.GetModifiedElementIds(elementFilter).First();
            var nameTrans = args.GetTransactionNames().First();
            TaskDialog.Show("Warning Changed", "Ban da thay doi doi tuong " + elementId + " Trans name la " + nameTrans);
        }
    }
}
