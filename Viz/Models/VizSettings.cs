using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;

namespace DougKlassen.Revit.Viz
{
    public class VizSettings
    {
        public IEnumerable<BuiltInCategory> ModelCategories;
        public IEnumerable<BuiltInCategory> AnnotationCategories;
        public IEnumerable<BuiltInCategory> ViewBugCategories;
        public IEnumerable<BuiltInCategory> AnalyticalCategories;

        public VizOverrides CurrentOverrideStyle;

        public VizSettings()
        {
            ModelCategories = new List<BuiltInCategory>();
            AnnotationCategories = new List<BuiltInCategory>();
            ViewBugCategories = new List<BuiltInCategory>();
            AnalyticalCategories = new List<BuiltInCategory>();

            CurrentOverrideStyle = new VizOverrides();
        }
    }
}
