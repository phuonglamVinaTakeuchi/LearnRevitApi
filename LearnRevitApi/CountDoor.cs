using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnSelectionFilter
{
    [Transaction(TransactionMode.Manual)]
    public class CountDoor : IExternalCommand
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
                var familyInstanceFilter = new ElementClassFilter(typeof(FamilyInstance));
                var doorsCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_Doors);
                var listFilter = new List<ElementFilter>() { new ElementCategoryFilter(BuiltInCategory.OST_Doors) };

                //var doorInstancesFilters = new LogicalAndFilter(listFilter);
                var doorInstancesFilters = new LogicalAndFilter(familyInstanceFilter, doorsCategoryFilter);
                var collector = new FilteredElementCollector(doc);
                var doors = collector.WherePasses(doorInstancesFilters).ToElements();
                TaskDialog.Show("Door Count", doors.Count().ToString());
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
