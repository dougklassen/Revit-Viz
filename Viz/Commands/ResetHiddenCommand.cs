using System;
using System.Collections.Generic;
using System.Linq;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace DougKlassen.Revit.Viz.Commands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class ResetHiddenCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            String msg = String.Empty;
            Document dbDoc = commandData.Application.ActiveUIDocument.Document;
            View currentView = commandData.Application.ActiveUIDocument.ActiveView;
            msg += "Current view: " + currentView.Name + '\n';

            List<Element> elementsToCheck = CommandHelpers.GetAllElements(dbDoc).ToList();
            msg += "All element count: " + elementsToCheck.Count + '\n';
            List<ElementId> elementIdsToUnhide = new List<ElementId>();

            foreach (Element e in elementsToCheck)
            {
                if (e.IsHidden(currentView))
                {
                    elementIdsToUnhide.Add(e.Id);
                }
            }

            if (elementIdsToUnhide.Count > 0)
            {
                using (Transaction t = new Transaction(dbDoc))
                {
                    t.Start("Viz-Reset hidden");
                    currentView.UnhideElements(elementIdsToUnhide);
                    msg += String.Format("{0} elements were unhidden in the current view\n", elementIdsToUnhide.Count);
                    t.Commit();
                }
            }
            else
            {
                msg += "No hidden elements were found in the view\n";
            }

            TaskDialog.Show("Elements Unhidden", msg);
            return Result.Succeeded;
        }
    }
}
