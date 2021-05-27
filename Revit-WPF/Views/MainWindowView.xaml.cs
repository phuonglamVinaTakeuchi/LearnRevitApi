using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitWPF.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RevitWPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        private UIApplication _app;
        private ExternalEvent _exEvent;
        public MainWindowView(UIApplication uiApp, ExternalEvent exEvent)
        {
            InitializeComponent();
            _app = uiApp;
            _exEvent = exEvent;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _exEvent.Raise();
        }
    }
}

