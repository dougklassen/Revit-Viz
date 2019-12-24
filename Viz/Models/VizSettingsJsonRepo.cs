using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace DougKlassen.Revit.Viz
{
    public class VizSettingsJsonRepo : IVizSettingsRepo
    {
        private static String configFileName = "VizSettings.json";
        private static String configFilePath = FileLocations.AddInDirectory + configFileName;

        public VizSettings LoadSettings()
        {
            if (!File.Exists(configFilePath))
            {
                WriteSettings(new VizSettings());
            }

            try
            {
                String jsonText = File.ReadAllText(configFilePath);
                JsonConvert.DeserializeObject<VizSettings>(jsonText);
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't read config file", e);
            }

            VizSettings settings = new VizSettings();

            return settings;
        }

        public void WriteSettings(VizSettings settings)
        {
            if (!Directory.Exists(FileLocations.AddInDirectory))
            {
                Directory.CreateDirectory(FileLocations.AddInDirectory);
            }

            try
            {
                String jsonText = JsonConvert.SerializeObject(settings);
                File.WriteAllText(configFilePath, jsonText);
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't write config file", e);
            }
        }
    }
}
