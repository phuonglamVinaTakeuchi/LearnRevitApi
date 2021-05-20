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
    public class MoveByMoveFunction : IExternalCommand
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
                    var vectorMove = new XYZ(10, 10, 0);
                    ElementTransformUtils.MoveElement(doc,elementId,vectorMove);
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
