using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;

namespace DougKlassen.Revit.Viz
{
    public static class Helpers
    {
        public static IEnumerable<Element> GetAllElements(this Document dbDoc)
        {
            IEnumerable<Element> allElements = new List<Element>();

            ElementFilter allElementsFilter = new LogicalOrFilter(new ElementIsElementTypeFilter(false), new ElementIsElementTypeFilter(true));
            allElements = new FilteredElementCollector(dbDoc).WherePasses(allElementsFilter);

            return allElements;
        }

        public static String GetVizDescription(this OverrideGraphicSettings settings)
        {
            String desc = String.Empty;

            desc +=
                "Detail Level: " + settings.DetailLevel.ToString() + '\n' +
                "Halftone: " + settings.Halftone.ToString() + '\n' +
                "Projection Lines:\n" +
                "- Pattern Id: " + settings.ProjectionLinePatternId.ToString() + '\n' +
                "- Color: " + settings.ProjectionLineColor.GetVizDescription() + '\n' +
                "- Weight: " + settings.ProjectionLineWeight.ToString() + '\n' +
                "Surface Patterns:\n" +
                "- Foreground Visible: " + settings.IsSurfaceForegroundPatternVisible.ToString() + '\n' +
                "- Foreground Pattern Id: " + settings.SurfaceForegroundPatternId.ToString() + '\n' +
                "- Foreground Color: " + settings.SurfaceForegroundPatternColor.GetVizDescription() + '\n' +
                "- Background Visible: " + settings.IsSurfaceBackgroundPatternVisible.ToString() + '\n' +
                "- Background Pattern Id: " + settings.SurfaceBackgroundPatternId.ToString() + '\n' +
                "- Background Color: " + settings.SurfaceBackgroundPatternColor.GetVizDescription() + '\n' +
                "Surface Transparency: " + settings.Transparency.ToString() + '\n' +
                "Cut Lines:\n" +
                "- Pattern Id: " + settings.CutLinePatternId.ToString() + '\n' +
                "- Color: " + settings.CutLineColor.GetVizDescription() + '\n' +
                "- Weight: " + settings.CutLineWeight.ToString() + '\n' +
                "Cut Patterns:\n" +
                "- Foreground Pattern Visibility: " + settings.IsCutForegroundPatternVisible.ToString() + '\n' +
                "- Foreground Pattern Id: " + settings.CutForegroundPatternId.ToString() + '\n' +
                "- Foreground Color: " + settings.CutForegroundPatternColor.GetVizDescription() + '\n' +
                "- Background Pattern Visibility: " + settings.IsCutBackgroundPatternVisible.ToString() + '\n' +
                "- Background Pattern Id: " + settings.CutBackgroundPatternId.ToString() + '\n' +
                "- Background Color: " + settings.CutBackgroundPatternColor.GetVizDescription() + '\n';

            return desc;
        }

        public static string GetVizDescription(this Autodesk.Revit.DB.Color color)
        {
            String desc = String.Empty;
            if (color.IsValid)
            {
                desc += String.Format("Red: {0} Green: {1} Blue: {2}", color.Red, color.Green, color.Blue);
            }
            else
            {
                desc += "Invalid";
            }
            return desc;
        }

        public static VizColor GetVizModel(this Color color)
        {
            if (color.IsValid)
            {
                return new VizColor(color);
            }
            else
            {
                return null;
            }
        }

        public static VizOverrides GetVizModel(this OverrideGraphicSettings settings)
        {
            return new VizOverrides(settings);
        }
    }
}
