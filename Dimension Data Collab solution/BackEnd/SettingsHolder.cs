
namespace BackEnd
{
    internal static class SettingsHolder
    {
        internal static string ConnectionString { get; } = "mongodb+srv://mainUser:BtPCEIb8e3Dbb06n@323projectdatacluster.k94r8.azure.mongodb.net/MainData?retryWrites=true&w=majority";
        internal static string DataBaseName { get; } = "MainData";
        internal static string CollectionName { get; } = "Test";

        internal static string LoginDataBaseName { get; } = "Login";
        internal static string LoginCollectionName { get; } = "Users";

        internal static int PageSize { get; } = 15;
    }
}
