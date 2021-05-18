using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnSelectionFilter
{
    [Transaction(TransactionMode.Manual)]
    public class CountWalls : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var app = uiApp.Application;
            var doc = uiDoc.Document;

            // Start code here

            // Lay nhung doi tuong dang chon

            try
            {
                var collector = new FilteredElementCollector(doc);
                var filter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
                var collections = collector.WherePasses(filter).WhereElementIsNotElementType();
                TaskDialog.Show("Count Similar Object", collections.Count().ToString());
                return Result.Succeeded;
            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }
        }
    }
}
