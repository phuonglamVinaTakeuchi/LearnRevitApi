using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnTransaction
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class CreateRebar : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var app = uiApp.Application;
            var doc = uiDoc.Document;
            var rebarShape = new FilteredElementCollector(doc)
                                .OfClass(typeof(RebarShape))
                                .Cast<RebarShape>()
                                .First(x=>x.Name == "M_00");
            var rebarType = new FilteredElementCollector(doc)
                                .OfClass(typeof(RebarBarType))
                                .Cast<RebarBarType>().
                                First(x=>x.Name == "22M");
            var reference = uiDoc.Selection.PickObject(ObjectType.Element,"Select element to put Rebar");
            var element = doc.GetElement(reference.ElementId);
            var locationPoint = element.Location as LocationPoint;
            var centerPoint = locationPoint.Point;

            var xVec = new XYZ(0, 0, 1);
            var yVec = new XYZ(0, 1, 0);
            try
            {
               
                using (var transaction = new Transaction(doc, "Create Rebar Demo"))
                {
                    transaction.Start();
                    var rebar = Rebar.CreateFromRebarShape(doc, rebarShape, rebarType, element, centerPoint, xVec, yVec);
                    transaction.Commit();
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
