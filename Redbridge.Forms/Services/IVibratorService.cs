using System;
using System.Threading.Tasks;

namespace Redbridge.Forms.Services
{
    public interface IVibratorService     {         Task Vibrate(TimeSpan duration);     }
}
