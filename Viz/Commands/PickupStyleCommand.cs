﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace DougKlassen.Revit.Viz.Commands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class PickupStyleCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            String ttl = "Viz-Style Eyedropper";
            String msg = String.Empty;
            VizSettingsJsonRepo repo = new VizSettingsJsonRepo();
            VizSettings vizSettings = repo.LoadSettings();

            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document dbDoc = commandData.Application.ActiveUIDocument.Document;
            View currentView = uiDoc.ActiveView;
            ElementId sourceElementId;

            if (uiDoc.Selection.GetElementIds().Count == 1)
            {
                sourceElementId = uiDoc.Selection.GetElementIds().First();
            }
            else
            {
                try
                {
                    sourceElementId = uiDoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element).ElementId;
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
            msg += String.Format(
                "Detail Level: {0}\n" +
                "Halftone: {1}\n" +
                "ProjectionLinePatternId: {2}\n" +
                "ProjectionLineColor: {3}\n" +
                "ProjectionLineWeight: {4}\n",
                vizSettings.CurrentOverrideStyle.DetailLevel,
                vizSettings.CurrentOverrideStyle.Halftone,
                vizSettings.CurrentOverrideStyle.ProjectionLinePatternId,
                vizSettings.CurrentOverrideStyle.ProjectionLineColor,
                vizSettings.CurrentOverrideStyle.ProjectionLineWeight);

            TaskDialog.Show(ttl, msg);
            return Result.Succeeded;
        }
    }
}

