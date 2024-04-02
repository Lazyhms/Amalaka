namespace System;

public class SnowflakeOptions
{
    public long DataCenterId { get; set; }

    public long MachingId { get; set; }

    public long? Sequence { get; set; }

    public long? Timestamp { get; set; }
}
