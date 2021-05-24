using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;

namespace LearnAPIBase
{
    public abstract class ExternalCommandBase : IExternalCommand
    {
        protected UIApplication _uiApplication;
        protected UIDocument _activeUIDocument;
        protected Application _revitApplication;
        protected Document _activeDocument;
        public virtual Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            InitCommandApplication(commandData);
            try
            {
                return ProcessCommand(commandData, ref message, elements);
            }
            catch(Exception e)
            {
                message = e.Message;
                return Result.Succeeded;
            }
        }
        protected void InitCommandApplication(ExternalCommandData commandData)
        {
            _uiApplication = commandData.Application;
            _activeUIDocument = _uiApplication.ActiveUIDocument;
            _revitApplication = _uiApplication.Application;
            _activeDocument = _activeUIDocument.Document;
        }
        protected abstract Result ProcessCommand(ExternalCommandData commandData, ref string message, ElementSet elements);
    }
}
