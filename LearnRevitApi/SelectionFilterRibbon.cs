
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LearnSelectionFilter
{
    public class SelectionFilterRibbon : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            // Add a new ribbon Panel
            var ribbon = application.CreateRibbonPanel("Learn-Selection-Panel");
            var thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            var folderPath = AssemblyDirection();
            PushButtonData buttonData = new PushButtonData("cmdHelloWorld", "Hello World!", thisAssemblyPath, "LearnSelectionFilter.SelectionFloor");
            var pustButton = ribbon.AddItem(buttonData) as PushButton;
            pustButton.ToolTip = "Say Hello to the revit API!";
            var iconImg = new Uri(folderPath + "\\About.ico");
            var bitmapImg = new BitmapImage(iconImg);
            pustButton.LargeImage = bitmapImg;
            return Result.Succeeded;
        }

        public string AssemblyDirection()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        public Result OnShutdown(UIControlledApplication application)
        {


            return Result.Succeeded;
        }
    }
}
