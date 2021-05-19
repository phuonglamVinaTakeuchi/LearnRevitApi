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
    public class SetWallThicknessSample : IExternalCommand
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
                if(refObject!=null)
                {
                    var element = doc.GetElement(refObject.ElementId);
                    if (element is Wall wall)
                    {
                        var compoundStructure = wall.WallType.GetCompoundStructure();
                        int layerIndex = compoundStructure.GetFirstCoreLayerIndex();
                        var csLayers = compoundStructure.GetLayers();

                        var wallThickness = 90*0.0032808;


                        using (var transaction = new Transaction(doc, "Edit Wall Thickness"))
                        {
                            transaction.Start();
                            compoundStructure.SetLayerWidth(layerIndex,wallThickness);
                            wall.WallType.SetCompoundStructure(compoundStructure);
                            transaction.Commit();

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
