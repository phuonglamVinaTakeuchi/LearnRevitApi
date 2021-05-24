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
    public class GetObjectIntersect : IExternalCommand
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
                var elementId = refObject.ElementId;
                var element = doc.GetElement(elementId);

                //var elementTypeId = element.GetTypeId();
                var elementType = element.GetType();

                // option để lọc ra geometry trên element
                // Két quả Geometry trả về tùy thuộc vào thiết lập trong option này
                var option = new Options();
                option.DetailLevel = ViewDetailLevel.Fine;
                option.ComputeReferences = true;
                
                // Lấy ra các đối tượng Solid trong element
                GetElementGeometry(element,option,out var curvers,out var solids);
                ICollection<ElementId> idsExclude = new List<ElementId>();
                idsExclude.Add(element.Id);
                var collector = new FilteredElementCollector(doc);
                var listFilter = new List<ElementFilter>();
                foreach(var solid in solids)
                {
                    var intersectFilter = new ElementIntersectsSolidFilter(solid);
                    listFilter.Add(intersectFilter);

                }
                var logicFilter = new LogicalOrFilter(listFilter);
                                
                

                //var faces = solids[0].Faces.Cast<Face>().ToList();
                //var edge = solids[0].Edges.Cast<Edge>().ToList();
                //var points = edge[0].Tessellate();
                //var faceArea = faces[0].Area;


                var intersections = collector
                                    .Excluding(idsExclude)
                                    //.OfClass(typeof(FamilyInstance))
                                    .WherePasses(logicFilter)
                                    .ToElementIds();
                uiDoc.Selection.SetElementIds(intersections);


            }

            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }
            return Result.Succeeded;
        }

        private void GetElementGeometry(Element element,Options option,out CurveArray curves, out List<Solid> solids) 
        {
            var geometryElement = element.get_Geometry(option);
            curves = new CurveArray();
            solids = new List<Solid>();

            AddElementGeometry(geometryElement, ref curves, ref solids);
        }

        private void AddElementGeometry(GeometryElement geomElem,ref CurveArray curves,ref List<Solid> solids)
        {
            foreach (var geomObj in geomElem)
            {
                var curve = geomObj as Curve;
                if (null != curve)
                {
                    curves.Append(curve);
                    continue;
                }
                var solid = geomObj as Solid;
                if (null != solid)
                {
                    solids.Add(solid);
                    continue;
                }
                //If this GeometryObject is Instance, call AddElementGeometry
                var geomInst = geomObj as GeometryInstance;
                if (null != geomInst)
                {
                    //GeometryElement transformedGeomElem = geomInst.GetInstanceGeometry(geomInst.Transform);
                    GeometryElement transformedGeomElem = geomInst.GetInstanceGeometry();
                    AddElementGeometry(transformedGeomElem, ref curves, ref solids);
                }
            }
        }
    }
}
