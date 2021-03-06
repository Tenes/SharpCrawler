﻿using System;
using System.Collections.Generic;

namespace SharpCrawler
{
    public static class RandomExtension
    {

        #region Choose
        public static T Choose<T>(this Random r, List<T> list)
        {
            return list[r.Next(0, list.Count)];
        }

        #endregion

        #region Gaussian
        public static double NextGaussian(this Random r, double mean = 0, double stdDev = 1)
        {
            double u1 = r.NextDouble();
            double u2 = r.NextDouble();

            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                Math.Sin(2.0 * Math.PI * u2);

            double randNormal = mean + stdDev * randStdNormal;

            return randNormal;
        }
        #endregion
    }
}
