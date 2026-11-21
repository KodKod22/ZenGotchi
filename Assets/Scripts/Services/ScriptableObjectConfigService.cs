public class ScriptableObjectConfigService : IConfigService
{
    public EconomyConfig Economy { get; }
    public HoopsConfig Hoops { get; }

    public ScriptableObjectConfigService(EconomyConfig economy, HoopsConfig hoops)
    {
        Economy = economy;
        Hoops = hoops;
    }
}