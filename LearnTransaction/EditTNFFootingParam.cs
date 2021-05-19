using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnTransaction
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class EditTNFFootingParam : IExternalCommand
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
                if (refObject != null)
                {
                    var element = doc.GetElement(refObject.ElementId);
                    var elementTypeId = element.GetTypeId();
                    var elementType = doc.GetElement(elementTypeId);

                    //if (element is FamilyInstance familyInstance)
                    //{
                    //    var elementSymbol = familyInstance.Symbol;
                    //}

                    var parameters = elementType.Parameters.Cast<Parameter>().ToList();
                    
                   
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
