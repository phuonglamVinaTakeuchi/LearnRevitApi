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
    public class AutoCreateDoorAndWindowTag : ExternalCommandBase
    {
        protected override Result ProcessCommand(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Tag All Door

            var listCategory = new List<BuiltInCategory>() { BuiltInCategory.OST_Doors, BuiltInCategory.OST_Windows };

            var filter = new ElementMulticategoryFilter(listCategory);
            var elementLists = new FilteredElementCollector(_activeDocument, _activeDocument.ActiveView.Id)
                                .WherePasses(filter)
                                .WhereElementIsNotElementType()
                                .ToElements();
            using (Transaction trans = new Transaction(_activeDocument, "Create View Plan"))
            {
                trans.Start();
                foreach(var element in elementLists)
                {
                    var reference = new Reference(element);
                    var loc = element.Location as LocationPoint;
                    var pos = loc.Point;
                    var tagNode = IndependentTag.Create(_activeDocument, _activeDocument.ActiveView.Id, reference, true, TagMode.TM_ADDBY_CATEGORY, TagOrientation.Horizontal, pos);

                }
                trans.Commit();
            }
            return Result.Succeeded;
        }
    }
}
