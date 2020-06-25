using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkLib;

namespace BouncingFootprints
{
    class MovingFootprint
    {
        int timeToLive;
        double dx, dy;
        public Footprint myFp { get; private set; }

        public MovingFootprint(Footprint fp)
        {
            myFp = fp;

            Random rng = new Random();
            timeToLive = rng.Next(400);
            dx = rng.NextDouble() * 10 - 5;
            dy = rng.NextDouble() * 10 - 5;
        }

        internal void Update()
        {
            timeToLive--;
            myFp.Offset(dx, dy);
        }

        public int getTimeToLive()
        {
            return timeToLive;
        }
    }
}
