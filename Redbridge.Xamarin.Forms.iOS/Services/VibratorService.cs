using System;
using System.Threading.Tasks;
using AudioToolbox;
using Redbridge.Forms.Services;

namespace Redbridge.Xamarin.Forms.iOS.Services
{
    public class VibratorService : IVibratorService     {         public Task Vibrate(TimeSpan duration)         {             SystemSound.Vibrate.PlaySystemSound();             return Task.CompletedTask;         }     }
}
