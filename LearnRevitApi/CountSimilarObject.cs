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
    public class CountSimilarObject : IExternalCommand
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

                    var elementTypeId = element.GetTypeId();
                    
                    var elementType = element.GetType();
                    var revitElementType = doc.GetElement(elementTypeId) as ElementType;
                    var familyTypeId = revitElementType.GetTypeId();
                    var categoryName = element.Category.Name;
                    if (revitElementType is FamilySymbol)
                    {
                        
                        var familyInstanceFilter = new FamilyInstanceFilter(doc, elementTypeId);
                        var collector = new FilteredElementCollector(doc);

                        var elementFilter = new FamilySymbolFilter(familyTypeId);
                        var collections = collector.WherePasses(familyInstanceFilter).ToElements();
                        TaskDialog.Show("Count Similar Object", collections.Count().ToString());
                        return Result.Succeeded;
                    }

                    TaskDialog.Show("Warning", "Your selected object can not be count!");

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
