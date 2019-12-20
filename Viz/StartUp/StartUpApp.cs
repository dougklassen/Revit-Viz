using System;
using System.IO;
using System.Windows.Media.Imaging;
using System.Reflection;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace DougKlassen.Revit.Viz
{
    public static class FileLocations
    {
        //AddInDirectory is initialized at runtime
        public static String AddInDirectory;
        //AssemblyName is initialized at runtime
        public static String AssemblyName;
        public static readonly String ResourceNameSpace = @"DougKlassen.Revit.Viz.Resources";
    }

    public class StartUpApp : IExternalApplication
    {
        Result IExternalApplication.OnStartup(UIControlledApplication application)
        {
            //initialize AssemblyName using reflection
            FileLocations.AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            //initialize AddInDirectory. The addin should be stored in a directory named after the assembly
            FileLocations.AddInDirectory = application.ControlledApplication.AllUsersAddinsLocation + "\\" + FileLocations.AssemblyName + "\\";

            //load image resources
            BitmapImage largeIcon = GetEmbeddedImageResource("iconLarge.png");
            BitmapImage smallIcon = GetEmbeddedImageResource("iconSmall.png");

            PushButtonData resetHiddenCommandPushButtonData = new PushButtonData(
                 "resetHiddenCommandButton", //name of the button
                 "Reset Hidden in View", //text on the button
                 FileLocations.AddInDirectory + FileLocations.AssemblyName + ".dll",
                 "DougKlassen.Revit.Viz.Commands.ResetHiddenCommand");
            resetHiddenCommandPushButtonData.LargeImage = largeIcon;
            resetHiddenCommandPushButtonData.ToolTip = "Unhide all elements hidden in the current view";

            PushButtonData resetGraphicsCommandPushButtonData = new PushButtonData(
                "resetGraphicsCommandButton", //name of the button
                "Reset Graphics in View", //text on the button
                FileLocations.AddInDirectory + FileLocations.AssemblyName + ".dll",
                "DougKlassen.Revit.Viz.Commands.ResetGraphicsCommand");
            resetGraphicsCommandPushButtonData.LargeImage = largeIcon;
            resetGraphicsCommandPushButtonData.ToolTip = "Reset all visibility graphics overrides in the current view";

            String tabName = "Viz";
            application.CreateRibbonTab(tabName);
            RibbonPanel VizRibbonPanel = application.CreateRibbonPanel(tabName, "Viz");
            VizRibbonPanel.AddItem(resetHiddenCommandPushButtonData);
            VizRibbonPanel.AddItem(resetGraphicsCommandPushButtonData);

            return Result.Succeeded;
        }

        Result IExternalApplication.OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        /// <summary>
        /// Utility method to retrieve an embedded image resource from the assembly
        /// </summary>
        /// <param name="resourceName">The name of the image, corresponding to the filename of the embedded resouce added to the solution</param>
        /// <returns>The loaded image represented as a BitmapImage</returns>
        BitmapImage GetEmbeddedImageResource(String resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream str = asm.GetManifestResourceStream(FileLocations.ResourceNameSpace + "." + resourceName);

            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = str;
            bmp.EndInit();

            return bmp;
        }
    }
}
