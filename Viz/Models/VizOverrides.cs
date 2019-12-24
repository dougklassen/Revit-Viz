using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;

namespace DougKlassen.Revit.Viz
{
    public class VizOverrides
    {
        public Int32 DetailLevel { get; set; }
        public Boolean Halftone { get; set; }
        public Int32 ProjectionLinePatternId { get; set; }
        public VizColor ProjectionLineColor { get; set; }
        public Int32 ProjectionLineWeight { get; set; }

        public VizOverrides()
        {
            OverrideGraphicSettings settings = new OverrideGraphicSettings();
            AssignValues(settings);
        }

        public VizOverrides(OverrideGraphicSettings settings)
        {
            AssignValues(settings);
        }

        private void AssignValues(OverrideGraphicSettings settings)
        {
            DetailLevel = (Int32)settings.DetailLevel;
            Halftone = settings.Halftone;
            ProjectionLinePatternId = settings.ProjectionLinePatternId.IntegerValue;
            ProjectionLineColor = settings.ProjectionLineColor.GetVizModel();
        }
    }
}
