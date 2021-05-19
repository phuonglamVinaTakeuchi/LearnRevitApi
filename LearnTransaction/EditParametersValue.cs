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
    public class EditParametersValue : IExternalCommand
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
                    using (var transaction = new Transaction(doc, "Change Element"))
                    {
                        transaction.Start();
                        var element = doc.GetElement(refObject.ElementId);

                        if(element!=null)
                        {
                            //var elementsParameters = element.Parameters.Cast<Parameter>().ToList();
                            var elementTypeId = element.GetTypeId();
                            var revitElementType = doc.GetElement(elementTypeId) as ElementType;

                            var param = revitElementType.Parameters.Cast<Parameter>().ToList().First(parameter => parameter.Definition.Name == "Cost");

                            //var elementsParameters = revitElementType.Parameters.Cast<Parameter>().ToList();
                            //var parameters = revitElementType.GetParameters("Cost");
                            //if (parameters!=null && parameters.Count()>0)
                            //{
                            //    var param = parameters[0];
                            //     param.Set(150);
                            //    transaction.Commit();
                            //}
                            if(param !=null)
                            {
                                param.Set(150);
                                transaction.Commit();
                            }
                        }
                        
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
