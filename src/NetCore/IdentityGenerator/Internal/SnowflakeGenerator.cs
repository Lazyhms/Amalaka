using Microsoft.Extensions.Options;

namespace System;

public class SnowflakeGenerator(IOptionsMonitor<SnowflakeOptions> snowflakeOptions)
    : Snowflake(snowflakeOptions.CurrentValue.DataCenterId, snowflakeOptions.CurrentValue.MachingId, snowflakeOptions.CurrentValue.Sequence, snowflakeOptions.CurrentValue.Timestamp), ISnowflakeGenerator
{
}