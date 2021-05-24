using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using LearnAPIBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithView
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class CreateSheets : ExternalCommandBase
    {
        protected override Result ProcessCommand(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var titleBlockSymbol = new FilteredElementCollector(_activeDocument)
                                    .OfCategory(BuiltInCategory.OST_TitleBlocks)
                                    .WhereElementIsElementType()
                                    .Cast<FamilySymbol>()
                                    .First();
            
            var view = new FilteredElementCollector(_activeDocument)
                                  .OfCategory(BuiltInCategory.OST_Views)
                                  .WhereElementIsNotElementType()
                                  .Cast<ViewPlan>()
                                  .First(x => x.Name == "Level 1");
            

            using (Transaction trans = new Transaction(_activeDocument, "Create Sheet"))
            {
                trans.Start();
                var sheet = ViewSheet.Create(_activeDocument, titleBlockSymbol.Id);
                var boundingBox = sheet.Outline;
                var uX = (boundingBox.Max.U + boundingBox.Min.U) / 2;
                var uY = (boundingBox.Max.V + boundingBox.Min.V) / 2;
                var centerPoint = new XYZ(uX,uY, 0);
                sheet.Name = "VinaTakeuchi - Sheet";
                sheet.SheetNumber = "VNT-01";
                var viewPort = Viewport.Create(_activeDocument, sheet.Id, view.Id, centerPoint);
                trans.Commit();
            }
            return Result.Succeeded;
        }
    }
}
