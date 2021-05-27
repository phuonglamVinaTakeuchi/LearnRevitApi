using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitWPF.Event;
using RevitWPF.Views;

namespace RevitWPF
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class LoadWindowCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var externalEvent = new AddNewFamilyEvent();
            var externalEventAdd = ExternalEvent.Create(externalEvent);
            WPFApp.WpfApp.Show(commandData.Application,externalEventAdd);
            return Result.Succeeded;
        }
    }
}
