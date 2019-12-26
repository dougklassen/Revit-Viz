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
        public IEnumerable<BuiltInCategory> ModelCategories { get; set; }
        public IEnumerable<BuiltInCategory> AnnotationCategories { get; set; }
        public IEnumerable<BuiltInCategory> ViewBugCategories { get; set; }
        public IEnumerable<BuiltInCategory> AnalyticalCategories { get; set; }

        public VizOverrides CurrentOverrideStyle { get; set; }

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
