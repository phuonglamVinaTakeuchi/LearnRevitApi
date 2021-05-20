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
    public class CreateWall : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var app = uiApp.Application;
            var doc = uiDoc.Document;
            try
            {
                // Tạo ra một collector để lấy tập hợp các element trong dự án hiện hành.
                var collector = new FilteredElementCollector(doc);

                // Lấy ra level đầu tiên trong danh sách các Level có trong dự án hiện hành
                var level = collector.OfCategory(BuiltInCategory.OST_Levels)
                            .WhereElementIsNotElementType()
                            .Cast<Level>()
                            .First();
                // Bạn có thể lấy ra một level đặc biệt với tên của nó bằng cách sau nếu đã biết tên Level đó
                /*
                var level = collector.OfCategory(BuiltInCategory.OST_Levels)
                            .WhereElementIsNotElementType()
                            .Cast<Level>()
                            .First(x=>x.Name == "Level 1");
                Lưu ý, Phải kiểm tra xem level đó có tồn tại không trước khi thực hiện bước kế tiếp nếu không chương trình sẽ ném ra một ngoại lệ (Exception).
                if(level1=null)
                {
                    // Khối code bạn muốn thực hiện nếu level !=null
                }
                */

                // tạo ra một bức tường có chiều dài 10000 mm
                // Bạn cần thực hiện convert qua đơn vị Feet trước khi thực hiện lệnh vì trong Revit API đa phần các đối tượng sử dụng chiều dài length đều là ở đơn vị feet
                // Ở đây sử dụng một phương thức được xây dựng sẵn trong Revit API nếu không, bạn có thể xây dựng phương thức Convert của riêng bạn.
                var wallLength = UnitUtils.Convert(10000, DisplayUnitType.DUT_MILLIMETERS, DisplayUnitType.DUT_DECIMAL_FEET);
                var startWallPoint = new XYZ(0, 0, 0);
                var endWallPoint = new XYZ(wallLength, 0, 0);
                var wallLine = Line.CreateBound(startWallPoint, endWallPoint);

                // Bắt đầu sử dụng transaction trước khi thực hiện tạo ra tường, nếu không chương trình sẽ nén ra một ngoại lệ (Exception)
                using (var transaction = new Transaction(doc,"Create Wall Demo"))
                {
                    transaction.Start();
                    //Tạo ra bức tường mới với các thông số đã được get ở phía trên và commit nó vào dự án.
                    //Do sử dụng phương thức tạo đơn giản nên tường được tạo sẽ là loại tường mặc định đang được active trong môi trường Revit.
                    var wall = Wall.Create(doc, wallLine, level.Id, false);
                    
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
