using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Microsoft.Build.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnSelectionFilter
{
    [Transaction(TransactionMode.Manual)]
    public class GetObjectInfo : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var app = uiApp.Application;
            var doc = uiDoc.Document;

            // Start code here

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

                    var categoryName = element.Category.Name;
                    var elementName = element.Name;
                    var elementTypeName = revitElementType.Name;
                    var familyName = revitElementType.FamilyName;

                    var infor = "Category Name: " + categoryName + Environment.NewLine +
                                "Family Name: " + familyName + Environment.NewLine +
                                "Element Type Name: " + elementTypeName + Environment.NewLine +
                                "Element Name: " + elementName;

                    TaskDialog.Show("Element Information", infor);

                    return Result.Succeeded;

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
