using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using LearnAPIBase;

namespace LearnSelectionFilter
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class CreateAModelCurve : ExternalCommandBase
    {
        protected override Result ProcessCommand(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            return Result.Succeeded;

        }
    }
}
