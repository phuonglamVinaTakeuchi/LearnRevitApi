using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using LearnAPIBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnTransaction
{
    [TransactionAttribute(TransactionMode.ReadOnly)]
    public class GetElementIntersect : ExternalCommandBase
    {
        protected override Result ProcessCommand(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Reference r = _activeUIDocument.Selection.PickObject(ObjectType.Element);
            var element = _activeDocument.GetElement(r.ElementId);
            var boundingBox = element.get_BoundingBox(_activeDocument.ActiveView);
            var outLine = new Outline(boundingBox.Min, boundingBox.Max);
            var bbFilter = new BoundingBoxIntersectsFilter(outLine);
            FilteredElementCollector collector = new FilteredElementCollector(_activeDocument, _activeDocument.ActiveView.Id);
            ICollection<ElementId> idsExclude = new List<ElementId>();
            idsExclude.Add(element.Id);
            collector.Excluding(idsExclude).WherePasses(bbFilter);
            int nCount = 0;
            string report = string.Empty;
            foreach (Element e in collector)
            {
                string name = e.Name;
                report += "\nName = " + name
                  + " Element Id: " + e.Id.ToString();
                nCount++;
            }

            TaskDialog.Show(
              "Bounding Box + View + Exclusion Filter","Found " + nCount.ToString()+ " elements whose bounding box intersects" +report);
            return Result.Succeeded;
        }
    }
}
