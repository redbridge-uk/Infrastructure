using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;

using Redbridge.Forms.Services;

namespace Redbridge.Xamarin.Forms.Droid.Services
{
    public class VibratorService : IVibratorService     {         public Task Vibrate(TimeSpan duration)         {             using (var vibrate = (Vibrator)Application.Context.GetSystemService(Context.VibratorService))             {                 if (vibrate.HasVibrator)                 {                     vibrate.Vibrate(VibrationEffect.CreateOneShot((long)duration.TotalMilliseconds, VibrationEffect.DefaultAmplitude));
                }             }             return Task.CompletedTask;         }     }
}
