using Newtonsoft.Json;
using System.IO;

public class Database
{
    private static string CONFIG_PATH => $"{Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}/config.json";
    public static Database instance;

    public static void Load()
    {
        if (File.Exists(CONFIG_PATH))
        {
            instance = JsonConvert.DeserializeObject<Database>(File.ReadAllText(CONFIG_PATH));
        }
        else
        {
            instance = new Database();
            Save();
        }
    }

    public static void Save()
    {
        File.WriteAllText(CONFIG_PATH, JsonConvert.SerializeObject(instance));
    }

    public string passwordHash;
}