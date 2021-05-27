using Autodesk.Revit.UI;
using RevitWPF.Views;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace RevitWPF
{
    public class WPFApp : IExternalApplication
    {
        public static WPFApp WpfApp { get; private set; }
        private MainWindowView _mainWindow;
        public Result OnShutdown(UIControlledApplication application)
        {
           return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            WpfApp = this;
            _mainWindow = null;

            application.CreateRibbonTab("MVVM");
            var ribbon = application.CreateRibbonPanel("MVVM", "MVVMPanel");
            CreateRibbonButton(application, ribbon, nameof(LoadWindowCommand), "Demo MVVM !", @"\AddSheet.ico", "RevitWPF");
            return Result.Succeeded;
        }
        private void CreateRibbonButton(UIControlledApplication application, RibbonPanel ribbon, string commandName, string decripptionTooltip, string iconStringPath, string nameSpace)
        {
            //var thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            var folderPath = AssemblyDirection();
            var assenblyPath = folderPath + @"\" + nameSpace + ".dll";
            PushButtonData buttonData = new PushButtonData("cmd" + commandName, commandName, assenblyPath, nameSpace + "." + commandName);
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
        public void Show(UIApplication uiApp, ExternalEvent exEvent)
        {
            if (_mainWindow != null && _mainWindow == null) return;
            _mainWindow = new MainWindowView(uiApp,exEvent);
            _mainWindow.Show();
        }
    }
}
