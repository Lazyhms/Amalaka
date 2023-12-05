namespace System;

public class Snowflake
{
    private double inital;
    private readonly long _machingId;
    private readonly long _timestamp;
    private readonly long _dataCenterId;

    private long _sequence = 0L;
    private long _lastTimestamp = -1L;

    private const int DataCenterBit = 5;
    private const int MachingIdBits = 5;
    private const int SequenceBits = 12;
    private const int DateCenterIdShift = SequenceBits + MachingIdBits;
    private const int MachingIdShift = SequenceBits;
    private const long MaxDataCenterId = -1L ^ (-1L << DataCenterBit);
    private const long MaxMachingId = -1L ^ (-1L << MachingIdBits);
    private const long MaxSequence = -1L ^ (-1L << SequenceBits);
    private const long Timestamp = 1420041600000L;
    private const int TimestampLeftShift = SequenceBits + DataCenterBit + MachingIdBits;
    private readonly static DateTime LastDateTime = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public Snowflake(long dataCenterId, long machingId, long? sequence = null, long? timestamp = null)
    {
        if (dataCenterId > MaxDataCenterId || dataCenterId < 0)
        {
            throw new ArgumentException($"datacenter can't be greater than {MaxDataCenterId} or less than 0");
        }
        if (machingId > MaxMachingId || machingId < 0)
        {
            throw new ArgumentException($"maching can't be greater than {MaxMachingId} or less than 0 ");
        }

        _machingId = machingId;
        _dataCenterId = dataCenterId;
        _sequence = sequence ?? 0L;
        _timestamp = timestamp ?? Timestamp;
    }

    public long NextId()
    {
        while (Interlocked.CompareExchange(ref inital, 1, 0) == 1)
        {
            Thread.Sleep(1);
        }

        var timestamp = CurrentTimestamp();

        if (timestamp < _lastTimestamp)
        {
            throw new Exception($"Clock moved backwards. Refusing to generate id.");
        }

        if (_lastTimestamp == timestamp)
        {
            _sequence = (_sequence + 1) & MaxSequence;
            if (_sequence == 0)
            {
                timestamp = NextTimestamp(_lastTimestamp);
            }
        }
        else
        {
            _sequence = 0;
        }

        _lastTimestamp = timestamp;
        var result = ((timestamp - _timestamp) << TimestampLeftShift)
            | _dataCenterId << DateCenterIdShift
            | _machingId << MachingIdShift
            | _sequence;

        Interlocked.Exchange(ref inital, 0);

        return result;
    }

    private static long CurrentTimestamp()
        => (long)(DateTime.UtcNow - LastDateTime).TotalMilliseconds;

    private static long NextTimestamp(long lastTimestamp)
    {
        var timestamp = CurrentTimestamp();
        while (timestamp <= lastTimestamp)
        {
            timestamp = CurrentTimestamp();
        }
        return timestamp;
    }
}