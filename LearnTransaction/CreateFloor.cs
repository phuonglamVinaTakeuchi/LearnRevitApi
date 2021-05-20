using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnTransaction
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class CreateFloor : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var app = uiApp.Application;
            var doc = uiDoc.Document;
            try
            {
                // Tạo các đường curver bao đóng để tạo một floor
                var p1 = new XYZ(0,0,0);
                var p2 = new XYZ(100,0,0);
                var p3 = new XYZ(100,100,0);
                var p4 = new XYZ(0,100,0);
                var listP = new List<XYZ>() { p1,p2,p3,p4,p1 };

                // Tạo các đường curvers từ danh sách các point
                var curvers = new CurveArray();
                var countP = listP.Count;
                for (var i = 0; i < countP-1; i++)
                {
                    
                        curvers.Append(Line.CreateBound(listP[i], listP[i + 1]));
                    
                }

                // Bắt đầu sử dụng transaction trước khi thực hiện tạo ra tường, nếu không chương trình sẽ nén ra một ngoại lệ (Exception)
                using (var transaction = new Transaction(doc, "Create Floor Demo"))
                {
                    transaction.Start();
                    doc.Create.NewFloor(curvers,false);
                    //Tạo ra bức tường mới với các thông số đã được get ở phía trên và commit nó vào dự án.
                    //Do sử dụng phương thức tạo đơn giản nên tường được tạo sẽ là loại tường mặc định đang được active trong môi trường Revit.
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
