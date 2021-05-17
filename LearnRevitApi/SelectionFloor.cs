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
    public class SelectionFloor : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var app = uiApp.Application;
            var doc = uiDoc.Document;

            // Start code here

            // Lay nhung doi tuong dang chon

            //var selections = uiDoc.Selection.GetElementIds().ToList();
            //var elementList = new List<Element>();
            //foreach (var elementid in selections)
            //{
            //    //elementList.Add(doc.GetElement(elementid));
            //    var element = doc.GetElement(elementid);
            //    TaskDialog.Show("Revit", element.Name);
            //}

            try
            {
                var refObject = uiDoc.Selection.PickObject(ObjectType.Element);
                if (refObject!=null)
                {
                    TaskDialog.Show("Element Id", refObject.ElementId.ToString());
                }

            }
            catch(Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }
            

            return Result.Succeeded;
        }
    }
}
