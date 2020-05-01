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

            String tabName = "DK";
            application.CreateRibbonTab(tabName);

            #region Reset Panel
            RibbonPanel ResetRibbonPanel = application.CreateRibbonPanel(tabName, "Reset View Overrides");

            PushButtonData resetHiddenCommandPushButtonData = new PushButtonData(
                 "resetHiddenCommandButton", //name of the button
                 "Reset Hidden", //text on the button
                 FileLocations.AddInDirectory + FileLocations.AssemblyName + ".dll",
                 "DougKlassen.Revit.Viz.Commands.ResetHiddenCommand");
            resetHiddenCommandPushButtonData.LargeImage = largeIcon;
            resetHiddenCommandPushButtonData.ToolTip = "Unhide all elements hidden in the current view";

            PushButtonData resetGraphicsCommandPushButtonData = new PushButtonData(
                "resetGraphicsCommandButton", //name of the button
                "Reset Graphics", //text on the button
                FileLocations.AddInDirectory + FileLocations.AssemblyName + ".dll",
                "DougKlassen.Revit.Viz.Commands.ResetGraphicsCommand");
            resetGraphicsCommandPushButtonData.LargeImage = largeIcon;
            resetGraphicsCommandPushButtonData.ToolTip = "Reset all visibility graphics overrides in the current view";

            ResetRibbonPanel.AddItem(resetHiddenCommandPushButtonData);
            ResetRibbonPanel.AddItem(resetGraphicsCommandPushButtonData);
            #endregion Reset Panel

            #region Apply Styles Panel
            RibbonPanel ApplyStylesPanel = application.CreateRibbonPanel(tabName, "Apply Override Styles");

            PushButtonData pickupStyleCommandPushButtonData = new PushButtonData(
                 "pickupStyleCommandButton", //name of the button
                 "Style Eyedropper", //text on the button
                 FileLocations.AddInDirectory + FileLocations.AssemblyName + ".dll",
                 "DougKlassen.Revit.Viz.Commands.PickupStyleCommand");
            pickupStyleCommandPushButtonData.LargeImage = largeIcon;
            pickupStyleCommandPushButtonData.ToolTip = "Choose an override style to apply to other elements";
            pickupStyleCommandPushButtonData.AvailabilityClassName = "DougKlassen.Revit.Viz.OverrideableViewCommandAvailability";
            ApplyStylesPanel.AddItem(pickupStyleCommandPushButtonData);

            PushButtonData applyStyleCommandPushButtonData = new PushButtonData(
                 "applyStyleCommandButton", //name of the button
                 "Apply Style", //text on the button
                 FileLocations.AddInDirectory + FileLocations.AssemblyName + ".dll",
                 "DougKlassen.Revit.Viz.Commands.ApplyStyleCommand");
            applyStyleCommandPushButtonData.LargeImage = largeIcon;
            applyStyleCommandPushButtonData.ToolTip = "Apply an override style to selected elements";
            applyStyleCommandPushButtonData.AvailabilityClassName = "DougKlassen.Revit.Viz.OverrideableViewCommandAvailability";
            ApplyStylesPanel.AddItem(applyStyleCommandPushButtonData);
            #endregion Apply Styles Panel

            #region Manage Callouts Panel
            RibbonPanel ManageCalloutsPanel = application.CreateRibbonPanel(tabName, "Manage View Callouts");

            PushButtonData filterBugsCommandPushButtonData = new PushButtonData(
                 "filterBugsCommandButton", //name of the button
                 "Filter View Callouts", //text on the button
                 FileLocations.AddInDirectory + FileLocations.AssemblyName + ".dll",
                 "DougKlassen.Revit.Viz.Commands.FilterBugsCommand");
            filterBugsCommandPushButtonData.LargeImage = largeIcon;
            filterBugsCommandPushButtonData.ToolTip = "Filter Callouts for the current view";
            filterBugsCommandPushButtonData.AvailabilityClassName = "DougKlassen.Revit.Viz.OverrideableViewCommandAvailability";
            ManageCalloutsPanel.AddItem(filterBugsCommandPushButtonData);

            #endregion Manage Callouts Panel

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

    #region Command Availability
    public class OverrideableViewCommandAvailability : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            return applicationData.ActiveUIDocument.ActiveView.AreGraphicsOverridesAllowed();
        }
    }
    #endregion Command Availability
}
