using System;

namespace ScriptsUtilities
{
    public class Timer
    {
        public bool started { get; private set; }
        private DateTime _start;

        public Timer()
        {
            started = false;
        }

        public void Stop()
        {
            started = false;
        }

        public void Start()
        {
            started = true;
            _start = DateTime.Now;
        }

        public float Current()
        {
            return (float)(DateTime.Now - _start).TotalSeconds;
        }

        public float Interrupt()
        {
            float current = Current();
            Start();
            return current;
        }
    }
}