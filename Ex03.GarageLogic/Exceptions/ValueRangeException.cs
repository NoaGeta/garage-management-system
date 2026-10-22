using System;

namespace Ex03.GarageLogic
{
    public class ValueRangeException : Exception
    {
        public float MinValue { get; }
        public float MaxValue { get; }

        public ValueRangeException(float i_MinValue, float i_MaxValue)
            : base(string.Format(
                "Value is out of range. Allowed range: {0} - {1}",
                i_MinValue,
                i_MaxValue))
        {
            MinValue = i_MinValue;
            MaxValue = i_MaxValue;
        }

        public ValueRangeException(float i_MinValue, float i_MaxValue, string i_Message)
            : base(i_Message)
        {
            MinValue = i_MinValue;
            MaxValue = i_MaxValue;
        }
    }
}
