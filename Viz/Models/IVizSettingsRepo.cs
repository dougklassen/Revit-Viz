using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DougKlassen.Revit.Viz
{
    public interface IVizSettingsRepo
    {
        VizSettings LoadSettings();

        void WriteSettings(VizSettings settings);
    }
}
