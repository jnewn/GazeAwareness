using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazeAwareness
{
    public class EyeGaze
    {
        private double gazeCoordinateX, gazeCoordinateY;
        private long timestamp;

        public long Timestamp
        {
            get
            {
                return timestamp;
            }

            set
            {
                timestamp = value;
            }
        }

        public EyeGaze(double x, double y)
        {
            this.gazeCoordinateX = x;
            this.gazeCoordinateY = y;
        }

        public EyeGaze(double x, double y, long TimeStamp)
        {
            this.gazeCoordinateX = x;
            this.gazeCoordinateY = y;
            this.Timestamp = TimeStamp;
        }

        public EyeGaze() { }    

        public double getGazeCoordinateX()
        {
            return this.gazeCoordinateX;
        }

        public double getGazeCoordinateY()
        {
            return this.gazeCoordinateY;
        }
    }
}
