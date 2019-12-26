﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace DougKlassen.Revit.Viz.Commands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class PickupStyleCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            String ttl = "Style Eyedropper";
            String msg = String.Empty;
            IVizSettingsRepo repo = new VizSettingsJsonRepo();
            VizSettings vizSettings = repo.LoadSettings();

            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document dbDoc = commandData.Application.ActiveUIDocument.Document;
            View currentView = commandData.Application.ActiveUIDocument.ActiveView;
            ElementId sourceElementId;

            if (uiDoc.Selection.GetElementIds().Count == 1)
            {
                sourceElementId = uiDoc.Selection.GetElementIds().First();
            }
            else
            {
                try
                {
                    sourceElementId = uiDoc.Selection.PickObject(ObjectType.Element).ElementId;
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException e)
                {
                    return Result.Cancelled;
                }
            }
            OverrideGraphicSettings selectedOverrides = currentView.GetElementOverrides(sourceElementId);
            msg += selectedOverrides.GetVizDescription();

            vizSettings.CurrentOverrideStyle = selectedOverrides.GetVizModel();
            repo.WriteSettings(vizSettings);

            TaskDialog.Show(ttl, msg);
            return Result.Succeeded;
        }
    }
}

