using System;
using System.Collections.Generic;
using System.Linq;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace DougKlassen.Revit.Viz.Commands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class ResetGraphicsCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            String ttl = "Viz-Reset Graphics Overrides";
            String msg = String.Empty;
            Document dbDoc = commandData.Application.ActiveUIDocument.Document;
            View currentView = commandData.Application.ActiveUIDocument.ActiveView;
            msg += "Current view: " + currentView.Name + '\n';
            if (!currentView.AreGraphicsOverridesAllowed())
            {
                msg += "Graphic overrides are not allowed in the current view";
                TaskDialog.Show(ttl, msg);
                return Result.Failed;
            }

            List<Element> elementsToCheck = CommandHelpers.GetAllElements(dbDoc).ToList();
            msg += "All element count: " + elementsToCheck.Count + '\n';

            foreach (Element e in elementsToCheck)
            {
                using (Transaction t = new Transaction(dbDoc))
                {
                    t.Start(ttl);
                    currentView.SetElementOverrides(e.Id, new OverrideGraphicSettings());
                    t.Commit();
                }
            }

            TaskDialog.Show(ttl, msg);
            return Result.Succeeded;
        }
    }
}
