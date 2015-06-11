using Microsoft.Band.Sensors;
using System;
using System.Linq;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace BandStreamProcessor
{
    public class WaveGestureDetector
    {
        public event EventHandler WaveDetected;
        public event EventHandler ReverseWaveDetected;
        Subject<IBandAccelerometerReading> rx = new Subject<IBandAccelerometerReading>();

        public WaveGestureDetector()
        {
            var wave = rx.Where(x => x.AccelerationY > 3);
            wave.Subscribe(x =>
            {
                var eh = WaveDetected;
                if(eh != null)
                {
                    eh(this, EventArgs.Empty);
                }
            });

            var reverseWave = rx.Where(x => x.AccelerationY < -3);
            reverseWave.Subscribe(x =>
            {
                var eh = ReverseWaveDetected;
                if (eh != null)
                {
                    eh(this, EventArgs.Empty);
                }
            });
        }

        public void AddAccelerometerReading(IBandAccelerometerReading accelerometerReading)
        {
            rx.OnNext(accelerometerReading);
        }
    }
}
