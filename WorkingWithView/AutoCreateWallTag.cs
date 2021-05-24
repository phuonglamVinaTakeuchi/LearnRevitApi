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
    public class AutoCreateWallTag : ExternalCommandBase
    {
        protected override Result ProcessCommand(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Tag All Wall

            var listCategory = new List<BuiltInCategory>() { BuiltInCategory.OST_Walls };

            var filter = new ElementMulticategoryFilter(listCategory);
            var elementLists = new FilteredElementCollector(_activeDocument, _activeDocument.ActiveView.Id)
                                .WherePasses(filter)
                                .WhereElementIsNotElementType()
                                .ToElements();
            using (Transaction trans = new Transaction(_activeDocument, "Create View Plan"))
            {
                trans.Start();
                foreach (var element in elementLists)
                {
                    var reference = new Reference(element);
                    var loc = element.Location as LocationCurve;
                    var startPos = loc.Curve.GetEndPoint(0);
                    var endPos = loc.Curve.GetEndPoint(1);
                    var midPos = (startPos + endPos) / 2;

                    var tagNode = IndependentTag.Create(_activeDocument, _activeDocument.ActiveView.Id, reference, true,
                                                        TagMode.TM_ADDBY_CATEGORY, TagOrientation.Horizontal, midPos);

                }
                trans.Commit();
            }
            return Result.Succeeded;
        }
    }
}
