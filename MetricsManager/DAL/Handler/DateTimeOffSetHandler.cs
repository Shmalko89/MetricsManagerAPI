using Dapper;
using System.Data;
using System;

namespace MetricsAgent.Handler
{
    public class DateTimeOffsetHandler : SqlMapper.TypeHandler<DateTimeOffset>
    {
        public override DateTimeOffset Parse(object value) => DateTimeOffset.FromUnixTimeSeconds((long)value);

        public override void SetValue(IDbDataParameter parameter, DateTimeOffset value) => parameter.Value = value;

    }
}
