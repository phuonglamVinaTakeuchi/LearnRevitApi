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
    [TransactionAttribute(TransactionMode.ReadOnly)]
    public class CountDoorAndWindowInAWall : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var app = uiApp.Application;
            var doc = uiDoc.Document;
            var refObject = uiDoc.Selection.PickObject(ObjectType.Element);
            var element = doc.GetElement(refObject);
            try
            {
                TaskDialog.Show("Door and Window Count in Wall", "This wall containt " + GetOpenings((Wall)element).Count() + " Door and window"); ; ;
                return Result.Succeeded;   
            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            } 

        }
        static IList<ElementId> GetOpenings(Wall wall)
        {
            var listWindowCategoryID = new ElementId(BuiltInCategory.OST_Windows);
            var listDoorCategoryID = new ElementId(BuiltInCategory.OST_Doors);
            var listElementId = new List<ElementId>() { listDoorCategoryID, listWindowCategoryID };
            var emcf = new ElementMulticategoryFilter(listElementId);

            return wall.GetDependentElements(emcf);
        }
    }
}
