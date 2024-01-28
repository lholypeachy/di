﻿using System;
using System.Drawing;

namespace TagsCloudVisualization.PointDistributors
{
    public class Spiral : IPointDistributor
    {
        private readonly int step;
        private double angle;
        private double deltaAngle;
        private Point center;
        public bool centerOnPoint = false;
       
        public Spiral(Point center, int step = 1, double deltaAngle = 0.1)
        {
            this.step = step;
            this.center = center;
            this.deltaAngle = deltaAngle;
        }
         
        public Point GetPosition()
        {
            if (!centerOnPoint)
            {
                centerOnPoint = true;
                return center;
            }

            angle += deltaAngle;

            var k = step / (2 * Math.PI);
            var radius = k * angle;

            var position = new Point(
                center.X + (int)(Math.Cos(angle) * radius),
                center.Y + (int)(Math.Sin(angle) * radius));

            return position;
        }
    }
}