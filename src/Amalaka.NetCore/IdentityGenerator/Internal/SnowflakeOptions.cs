namespace Amalaka.AspNetCore.IdentityGenerator.Internal;

public class SnowflakeOptions
{
    public long DataCenterId { get; set; }

    public long MachingId { get; set; }

    public long? Sequence { get; set; }

    public long? Timestamp { get; set; }
}
