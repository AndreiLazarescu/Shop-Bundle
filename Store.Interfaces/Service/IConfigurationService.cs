namespace Store.Interfaces.Service
{
    public interface IConfigurationService
    {
        string ReadString(string key);
        int ReadInt(string key);
        string ReadConnectionString(string key);
    }
}
