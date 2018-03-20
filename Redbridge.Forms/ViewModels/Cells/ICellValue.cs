using System;

namespace Redbridge.Forms
{
    public interface ICellValue<TValue>
    {
        IObservable<TValue> Values
        {
            get;
        }

        TValue Value { get; set; }
    }
}
