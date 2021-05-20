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
    class CreateRebar : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var app = uiApp.Application;
            var doc = uiDoc.Document;
            try
            {
               
                using (var transaction = new Transaction(doc, "Create Rebar Demo"))
                {
                    transaction.Start();
                    
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
