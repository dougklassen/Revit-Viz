using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DougKlassen.Revit.Viz
{
    public class VizSettingsRepo : IVizSettingsRepo
    {
        private static String configFileName = "VizSettings.json";
        private static String configFilePath = FileLocations.AddInDirectory + configFileName;

        public VizSettings LoadSettings()
        {
            if (!File.Exists(configFilePath))
            {
                throw new Exception("Viz config file not found");
            }

            try
            {
                String jsonText = File.ReadAllText(configFilePath);
                JsonSerializer.Deserialize<VizSettings>(jsonText);
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

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            try
            {
                String jsonText = JsonSerializer.Serialize(settings);
                File.WriteAllText(configFilePath, jsonText);
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't read config file", e);
            }
        }
    }
}
