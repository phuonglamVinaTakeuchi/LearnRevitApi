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
    [Transaction(TransactionMode.Manual)]
    public class GetIntersectPointOfElement : IExternalCommand
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
                var elementId = refObject.ElementId;
                var element = doc.GetElement(elementId);

                //Bộ lọc các đối tượng cần tính toán giao điểm
                var filter = new ElementCategoryFilter(BuiltInCategory.OST_Floors);
                // là đối tượng hướng dẫn để tìm giao điểm 
                var refInter = new ReferenceIntersector(filter, FindReferenceTarget.Face, (View3D)doc.ActiveView);
                
                // điểm cơ sở của vector bạn cần tính toán giao điểm.
                var basePoint = ((LocationPoint)element.Location).Point;
                // điểm chỉ hướng của vector bạn cần tính toán
                var ray = new XYZ(0, 0, 1);

                // trong trường hợp tia chỉ hướng là ngược hướng
                // với hướng mà lẽ ra theo hướng đó, vector mới cắt với mặt phẳng thì sẽ không trả về kế quả nào.
                var refContext = refInter.FindNearest(basePoint, ray);
                var intersectP = refContext.GetReference().GlobalPoint;

                var distacne = UnitUtils.ConvertFromInternalUnits(basePoint.DistanceTo(intersectP), DisplayUnitType.DUT_MILLIMETERS);
                // Lưu ý element location point không nhất thiết là điểm thấp nhất mà là điểm cơ sở của đối tượng 
                TaskDialog.Show("Tinh Khoang cach", "Khoỏng cách từ chân cột đến mái là " +distacne);


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
