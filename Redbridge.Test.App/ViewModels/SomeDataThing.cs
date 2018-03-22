using System;

namespace TesterApp
{
    public class SomeDataThing
    {
        public decimal MyDecimalValue
        {
            get;
            set;
        }

        public decimal? MyNullableDecimalValue
        {
            get;
            set;
        }

        public bool MySwitchValue
        {
            get;
            set;
        }

        public bool? MyNullableSwitchValue
        {
            get;
            set;
        }

        public string MyTextValue
        {
            get;
            set;
        }

        public DateTime? MyDateTimeValue
        {
            get;
            set;
        }

        public MyTestEnum? MyEnumValue
        {
            get; set;
        }

        public MyOtherTestEnum? MyOtherEnumValue
        {
            get; set;
        }
    }
}
