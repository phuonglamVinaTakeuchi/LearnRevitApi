using Autodesk.Revit.UI;
using LearnTransaction;
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
            //CreateRibbonButton(application,ribbon, "GetObjectInfo", "Get Selected Object Info!", @"\About.ico", "LearnSelectionFilter");
            //CreateRibbonButton(application, ribbon, "CountDoor", "Count all Door in Current Document!", @"\About.ico", "LearnSelectionFilter");
            //CreateRibbonButton(application, ribbon, "CountDoorSimilar", "Count Similar Selected Door in Current Document!", @"\About.ico", "LearnSelectionFilter");
            //CreateRibbonButton(application, ribbon, "CountSimilarObject", "Count Similar Selected Object in Current Document!", @"\About.ico", "LearnSelectionFilter");
            //CreateRibbonButton(application, ribbon, "CountWalls", "Count Walls Object in Current Document!", @"\About.ico", "LearnSelectionFilter");
            //CreateRibbonButton(application, ribbon, "TransactionDemo", "Demo Using Transaction!", @"\About.ico", "LearnTransaction");
            //CreateRibbonButton(application, ribbon, "EditParametersValue", "Demo Using Transaction!", @"\About.ico", "LearnTransaction");
            //CreateRibbonButton(application, ribbon, "AddInstanceFamily", "Demo Using Transaction!", @"\About.ico", "LearnTransaction");
            //CreateRibbonButton(application, ribbon, "GetParameterValue", "Demo Using Transaction!", @"\About.ico", "LearnTransaction");
            //CreateRibbonButton(application, ribbon, "SetWallThicknessSample", "Demo Using Transaction!", @"\About.ico", "LearnTransaction");
            //CreateRibbonButton(application, ribbon, "EditTNFFootingParam", "Demo Using Transaction!", @"\About.ico", "LearnTransaction");
            //CreateRibbonButton(application, ribbon, nameof(CreateWall), "Demo Using Transaction!", @"\About.ico", "LearnTransaction");
            //CreateRibbonButton(application, ribbon, nameof(CreateFloor), "Demo Using Transaction!", @"\About.ico", "LearnTransaction");
            //CreateRibbonButton(application, ribbon, nameof(MoveByLocation), "Demo Using Transaction!", @"\About.ico", "LearnTransaction");
            //CreateRibbonButton(application, ribbon, nameof(MoveByMoveFunction), "Demo Move Object By Element Move Funcion!", @"\About.ico", "LearnTransaction");
            //CreateRibbonButton(application, ribbon, nameof(RotateByFuntion), "Demo Move Object By Element Move Funcion!", @"\About.ico", "LearnTransaction");
            
            return Result.Succeeded;
        }
        private void CreateRibbonButton(UIControlledApplication application, RibbonPanel ribbon,string commandName,string decripptionTooltip,string iconStringPath,string nameSpace)
        {
            //var thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            var folderPath = AssemblyDirection();
            var assenblyPath = folderPath + @"\" + nameSpace+".dll";
            PushButtonData buttonData = new PushButtonData("cmd"+commandName, commandName, assenblyPath, nameSpace + "."+commandName);
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
