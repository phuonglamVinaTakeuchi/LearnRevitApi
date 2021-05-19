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
    [TransactionAttribute(TransactionMode.ReadOnly)]
    public class GetParameterValue : IExternalCommand
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
                        if (element != null)
                        {
                            
                            var elementTypeId = element.GetTypeId();
                            var revitElementType = doc.GetElement(elementTypeId) as ElementType;

                            var param = revitElementType.Parameters.Cast<Parameter>().ToList().First(parameter => parameter.Definition.Name == "Width") as Parameter;
                            if (param != null)
                            {
                                TaskDialog.Show("Value of Width","Width = " + param.AsValueString());   
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
