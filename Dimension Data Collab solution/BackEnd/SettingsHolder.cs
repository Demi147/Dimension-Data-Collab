
namespace BackEnd
{
    public static class SettingsHolder
    {
        public static string ConnectionString { get; } = "mongodb+srv://mainUser:BtPCEIb8e3Dbb06n@323projectdatacluster.k94r8.azure.mongodb.net/MainData?retryWrites=true&w=majority";
        public static string DataBaseName { get; } = "MainData";
        public static string CollectionName { get; } = "Test";
    }
}
