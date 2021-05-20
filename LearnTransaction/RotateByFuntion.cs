using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeuchiUtils;

namespace LearnTransaction
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class RotateByFuntion : IExternalCommand
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
                    // trục tâm để đối tượng xoay quanh nó
                    var location = element.Location;
                    Line centerLine = null;
                    if(location is LocationPoint locationPoint)
                    {
                        var currentP = locationPoint.Point;
                        centerLine = Line.CreateBound(currentP, new XYZ(currentP.X, currentP.Y, currentP.Z + 10));
                    }
                    else if(location is LocationCurve locationCurve)
                    {
                        var currentCurve = locationCurve.Curve as Line;
                        var startP = currentCurve.GetEndPoint(0);
                        var endP = currentCurve.GetEndPoint(1);
                        var midPoint = PointUtils.GetMidPoint(startP, endP);
                        centerLine = Line.CreateBound(midPoint, new XYZ(midPoint.X, midPoint.Y, midPoint.Z + 10));
                    }
                    
                    // Góc xoay, lưu ý, góc phải được đổi từ độ sang Radial
                    var angle = UnitUtils.Convert(45, DisplayUnitType.DUT_DECIMAL_DEGREES, DisplayUnitType.DUT_RADIANS);

                    // Có thể sử dụng phương thức  ElementTransformUtils.RotateElement() hoặc phương thức rotate trong location của đối tượng đều được kết quả như nhau
                    //ElementTransformUtils.RotateElement(doc, elementId, centerLine,angle);
                    element.Location.Rotate(centerLine, angle);
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
