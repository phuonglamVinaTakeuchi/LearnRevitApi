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
    public class CountDoorSimilar : IExternalCommand
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
                var refObject = uiDoc.Selection.PickObject(ObjectType.Element);

                if (refObject != null)
                {
                    var elementId = refObject.ElementId;
                    var element = doc.GetElement(elementId);
                    var elementID = element.Id; 

                    var elementTypeId = element.GetTypeId();
                    var elementType = element.GetType();
                    var revitElementType = doc.GetElement(elementTypeId) as ElementType;
                    var typeId = revitElementType.Id;

                    var categoryName = element.Category.Name;
                    if(categoryName == "Doors")
                    {
                        var familyInstanceFilter = new FamilyInstanceFilter(doc, elementId);
                        //var familyInstanceFilter = new FamilyInstanceFilter(doc, elementTypeId);
                        var collector = new FilteredElementCollector(doc);
                        var doors = collector.WherePasses(familyInstanceFilter).ToElements();
                        TaskDialog.Show("Count Similar Doors", doors.Count().ToString());
                        return Result.Succeeded;
                    }

                    TaskDialog.Show("Warning", "Please select a Doors");

                    return Result.Failed;

                }
            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }


            return Result.Succeeded;
        }
    }
}
