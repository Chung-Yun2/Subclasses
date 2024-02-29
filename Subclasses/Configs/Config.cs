using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.Loader;
using Subclasses.API.Features;
using YamlDotNet.Serialization;

namespace Subclasses.Configs
{
    public class Config : IConfig
    {
        [YamlIgnore]
        public List<SubclassTemplate> Subclasses { get; set; } = new()
        {
            new SubclassTemplate()
        };
        
        [Description("Whether or not plugin is enabled")]
        public bool IsEnabled { get; set; } = true;
        
        [Description("Whether or not debug is enabled")]
        public bool Debug { get; set; } = false;
        
        [Description("ConfigFolderPath")]
        public string ConfigFolder { get; set; } = Path.Combine(Paths.Configs, "Subclasses");

        [Description("ConfigFileName")]
        public string ConfigFile { get; set; } = "global.yml";

        public void LoadConfigs()
        {
            Directory.CreateDirectory(ConfigFolder);

            string filePath = Path.Combine(ConfigFolder, ConfigFile);
            
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, Loader.Serializer.Serialize(Subclasses));
            }
            else
            {
                Subclasses = Loader.Deserializer.Deserialize<List<SubclassTemplate>>(File.ReadAllText(filePath));
                File.WriteAllText(filePath, Loader.Serializer.Serialize(Subclasses));
            }
        }
    }
}