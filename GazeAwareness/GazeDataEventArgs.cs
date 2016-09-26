using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GazeAwareness
{
    public class GazeDataEventArgs : EventArgs
    {
        public Point GazePoint {get; set; }

        public double TimeStamp {get; set; }
    }
}
