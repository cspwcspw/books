using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ThinkLib;
using System.Windows.Media;
using System.Windows;

namespace Fragments
{
    /// <summary>
    /// A fancy type of Turtle to illustrate some ideas about inheritance.
    /// </summary>
    public class TurtleGTX : Turtle, IComparable
    {
        Random rng;

        public TurtleGTX(Canvas playground, double homeX = 50, double homeY = 100)
            : base(playground, homeX, homeY)
        {
            rng = new Random();
            LineBrush = new RadialGradientBrush(Colors.LimeGreen, Colors.HotPink);
            BrushWidth = 10;
        }

        /// <summary>
        /// Move forward with the brush up, but leave the brush in the same
        /// state as it was.
        /// </summary>
        /// <param name="distance">How far to hop forward.</param>
        public void Jump(double distance)
        {
            if (BrushDown)
            {
                BrushDown = false;
                Forward(distance);
                BrushDown = true;
            }
            else
            {
                Forward(distance);
            }
        }

        /// <summary>
        /// Spin the turtle a few times and leave it facing in a random heading.
        /// </summary>
        public void Spin()
        {
            int theta = rng.Next(360);
            for (int t = 0; t < 199; t++)
            {
                Right(theta);
            }
        }

        public override void Left(double degrees)
        {
            base.Right(degrees*0.9);
        }

        public override void Right(double degrees)
        {
            base.Left(degrees*0.9);
        }

        public override void Forward(double distance)
        {
            base.Forward(-distance*0.9);
        }


        public int CompareTo(object obj)
        {
            Turtle other = obj as Turtle;
            if (other == null) return 1;
            double dOther = distance(other.Home, other.Position);
            double dMe = distance(Home, Position);
            return dMe.CompareTo(dOther);
        }

        public double distance(Point p1, Point p2)
        {
            double dx = p1.X - p2.X;
            double dy = p1.Y - p2.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public override string ToString()
        {
            string msg = String.Format("I'm a fancy turtle at position ({0:f0},{1:f0}) with heading {2:f0}",
                   this.Position.X, this.Position.Y, this.Heading);
            return msg;
        }
    }
}
