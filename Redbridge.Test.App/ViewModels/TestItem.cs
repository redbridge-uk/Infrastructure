using System;
using Redbridge.SDK;
using Redbridge.Forms;

namespace TesterApp
{
    public class TestItem : IDisplayText, IUnique<Guid>
    {
        public Guid Id { get; set; }

        public string DisplayText
        {
            get; set;
        }
    }
}
