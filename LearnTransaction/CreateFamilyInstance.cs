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
    public class CreateFamilyInstance : IExternalCommand
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
                //var level = collector.OfCategory(BuiltInCategory.OST_Levels)
                //            .WhereElementIsNotElementType()
                //            .Cast<Level>();
                var symbols = collector.OfClass(typeof(FamilySymbol)).WhereElementIsElementType().ToElements();
                //var family = collector.OfClass(typeof(FamilyInstance)).ToElements();
                FamilySymbol symbol = null;
                foreach (var sym in symbols)
                {
                    if (sym.Name == "1525 x 762mm")
                    {
                        symbol = sym as FamilySymbol;
                    }
                }
                if (symbol == null)
                {
                    message = "Can not find that family type";
                    return Result.Cancelled;
                }
                    


                using (var transaction = new Transaction(doc, "Create Wall Demo"))
                {
                    transaction.Start();

                    var newSymbol = symbol.Duplicate("1525 x 800mm") as FamilySymbol;

                    //newSymbol.Activate();
                    
                    //var insertPoint = new XYZ(0, 0, 0);

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
