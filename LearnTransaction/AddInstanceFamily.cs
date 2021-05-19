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
    public class AddInstanceFamily : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var app = uiApp.Application;
            var doc = uiDoc.Document;
            try
            {
                var collector = new FilteredElementCollector(doc);
                var symbols = collector.OfClass(typeof(FamilySymbol)).WhereElementIsElementType().ToElements();
                FamilySymbol symbol = null;
                foreach(var sym in symbols)
                {
                    if(sym.Name =="1525 x 762mm")
                    {
                        symbol = sym as FamilySymbol;
                    }
                }
                if(symbol!=null)
                {
                    
                    using (Transaction transaction = new Transaction(doc,"Add New Symbol"))
                    {
                        transaction.Start();
                        if (!symbol.IsActive) symbol.Activate();
                        doc.Create.NewFamilyInstance(new XYZ(0, 0, 4000), symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                        transaction.Commit();

                    }
                        

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
