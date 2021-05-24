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
    public class CreateFilterView : ExternalCommandBase
    {
        protected override Result ProcessCommand(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Create Category
            var cats = new List<ElementId>();
            cats.Add(new ElementId(BuiltInCategory.OST_Sections));
            var sectionFilter = ParameterFilterRuleFactory.CreateContainsRule(new ElementId(BuiltInParameter.VIEW_NAME),"Section 1",false);
            var elementFilter = new ElementParameterFilter(sectionFilter);

            using (Transaction trans = new Transaction(_activeDocument, "Create Filter"))
            {
                trans.Start();

                var filter = ParameterFilterElement.Create(_activeDocument, "Wall Filter", cats, elementFilter);
                _activeDocument.ActiveView.AddFilter(filter.Id);
                _activeDocument.ActiveView.SetFilterVisibility(filter.Id,false);
                trans.Commit();
            }
            return Result.Succeeded;
        }
    }
}
