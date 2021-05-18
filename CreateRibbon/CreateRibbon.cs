using Autodesk.Revit.UI;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace LearnCreateRibbon
{
    public class CreateRibbon : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            var ribbon = application.CreateRibbonPanel("Learn-Selection-Panel");
            CreateRibbonButton(application,ribbon, "GetObjectInfo", "Get Selected Object Info!", @"\About.ico");
            CreateRibbonButton(application, ribbon, "CountDoor", "Count all Door in Current Document!", @"\About.ico");
            CreateRibbonButton(application, ribbon, "CountDoorSimilar", "Count Similar Selected Door in Current Document!", @"\About.ico");
            CreateRibbonButton(application, ribbon, "CountSimilarObject", "Count Similar Selected Object in Current Document!", @"\About.ico");
            CreateRibbonButton(application, ribbon, "CountWalls", "Count Walls Object in Current Document!", @"\About.ico");

            return Result.Succeeded;
        }
        private void CreateRibbonButton(UIControlledApplication application, RibbonPanel ribbon,string commandName,string decripptionTooltip,string iconStringPath)
        {
            //var thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            var folderPath = AssemblyDirection();
            var assenblyPath = folderPath + @"\LearnSelectionFilter.dll";
            PushButtonData buttonData = new PushButtonData("cmd"+commandName, commandName, assenblyPath, "LearnSelectionFilter."+commandName);
            var pustButton = ribbon.AddItem(buttonData) as PushButton;
            pustButton.ToolTip = decripptionTooltip;
            var iconImg = new Uri(folderPath + iconStringPath);
            var bitmapImg = new BitmapImage(iconImg);
            pustButton.LargeImage = bitmapImg;
        }
        public string AssemblyDirection()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}
