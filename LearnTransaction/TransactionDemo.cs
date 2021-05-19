using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;

namespace LearnTransaction
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class TransactionDemo : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var app = uiApp.Application;
            var doc = uiDoc.Document;
            try
            {
                var refObject = uiDoc.Selection.PickObject(ObjectType.Element);
                if (refObject !=null)
                {
                    using(var transaction = new Transaction(doc,"Change Element"))
                    {
                        transaction.Start();

                        doc.Delete(refObject.ElementId);

                        var taskDialog = new TaskDialog("Delete Element Confirm");
                        taskDialog.MainContent = "Do you want delete this element";
                        taskDialog.CommonButtons = TaskDialogCommonButtons.Ok | TaskDialogCommonButtons.Cancel;
                        if(taskDialog.Show() == TaskDialogResult.Ok)
                        {
                            transaction.Commit();
                            TaskDialog.Show("Deleted Confirm", "Ban da xoa phan tu " + refObject.ElementId);
                        }
                        else
                        {
                            transaction.RollBack();
                            TaskDialog.Show("Deleted Confirm", "You Do not Delete anything");
                        }
                    }
                }
            }
            catch(Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }
            return Result.Succeeded;
        }
    }
}
