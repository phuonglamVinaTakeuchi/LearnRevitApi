using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
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
    public class MoveByLocation : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var app = uiApp.Application;
            var doc = uiDoc.Document;
            try
            {
                // Sử dụng PickObject để chọn đối tượng trên giao diện Revit
                // các dòng sau hàm này sẽ chưa được thực thi cho đến khi người dùng chọn một đối tượng trên Revit

                var refObject = uiDoc.Selection.PickObject(ObjectType.Element);

                var element = doc.GetElement(refObject.ElementId);
                var elementId = element.Id;

                using (var transaction = new Transaction(doc, "Change Location"))
                {
                    transaction.Start();

                    // hiệu chỉnh trực tiếp tọa độ của đối tượng nếu đối tượng là một family instance
                    //var locationPoint = element.Location as LocationPoint;
                    //var currentPoint = locationPoint.Point;
                    //var newPoint = new XYZ(currentPoint.X + 4, currentPoint.Y, currentPoint.Z);
                    //locationPoint.Point = newPoint;
                    
                    //var locationPoint = element.Location as LocationCurve;
                    //var curentCurve = locationPoint.Curve;
                    //var newLine = Line.CreateBound(new XYZ(0, 0, 0), new XYZ(100, 0, 0));
                    //locationPoint.Curve = newLine;

                    var locationPoint = element.Location ;
                    var moveVector = new XYZ(10, 10, 0);
                    locationPoint.Move(moveVector);
                    
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
