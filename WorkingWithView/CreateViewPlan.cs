using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using LearnAPIBase;
using System.Linq;

namespace WorkingWithView
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class CreateViewPlan : ExternalCommandBase
    {
        protected override Result ProcessCommand(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var collector = new FilteredElementCollector(_activeDocument);
            var level = collector.OfCategory(BuiltInCategory.OST_Levels)
                                 .WhereElementIsNotElementType()
                                 .Cast<Level>()
                                 .First(x => x.Name == "Level 1");
                                            
            var viewFamily = new FilteredElementCollector(_activeDocument)
                                  .OfClass(typeof(ViewFamilyType))
                                  .Cast<ViewFamilyType>()
                                  .First(x => x.ViewFamily == ViewFamily.FloorPlan);

            using(Transaction trans = new Transaction(_activeDocument,"Create View Plan"))
            {
                trans.Start();
                var viewPlan = ViewPlan.Create(_activeDocument, viewFamily.Id, level.Id);
                viewPlan.Name = "My Floor Plan";
                trans.Commit();
            }
            return Result.Succeeded;
        }
    }
}
