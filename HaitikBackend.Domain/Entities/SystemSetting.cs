namespace HaitikBackend.Domain.Entities;

public partial class SystemSetting
{

    public string Key { get; private set; } = null!;

    public string Value { get; private set; } = null!;

    private SystemSetting()
    {
    }

    public SystemSetting(string key, string value)
    {
        Key = key;
        Value = value;
    }

    public static SystemSetting Create(string key, string value)
    {
        return new SystemSetting(key, value);
    }
}
