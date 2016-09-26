//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

namespace GazeAwareness
{
    using EyeXFramework;
    using EyeXFramework.Wpf;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using Tobii.EyeX.Framework;

    public class MainWindowModel : INotifyPropertyChanged
    {
        private bool _showInstruction;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowModel()
        {
            ShowInstruction = false;
        }

        public string InstructionTeaser
        {
            get { return "Look here for instruction..."; }
        }

        public string Instruction
        {
            get { return "Instruction: The visual elements above respond to your eye-gaze. " +
                         "First, move away the mouse cursor. " +
                         "Now, look at one of the colored surfaces or the 'Hello!', and after a " +
                         "pre-defined delay they will change color from a dim to a clear color. " +
                         "As long as the eye-gaze falls within a child element its parent element " +
                         "will be considered looked at as well. " +
                         "Open MainWindow.xaml to see how it is done. (C)lose instruction. (Q)uit application."; }
        }

        public bool ShowInstruction
        {
            get { return _showInstruction; }
            private set
            {
                if (_showInstruction != value)
                {
                    _showInstruction = value;
                    NotifyPropertyChanged("ShowInstruction");
                }
            }
        }

        private readonly WpfEyeXHost _eyeXHost;

        public event EventHandler<GazeDataEventArgs> GazeDataTriggered;


        public MainWindowModel(WpfEyeXHost eyeXHost)
        {
            _eyeXHost = eyeXHost;

            var stream = _eyeXHost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered);
            stream.Next += (s, e) => EyeXHost_GazePointDataStream(s, e);

        }

        private void EyeXHost_GazePointDataStream(object sender, GazePointEventArgs e)
        {
            var tempPoint = new Point(e.X, e.Y);
            var args = new GazeDataEventArgs
            {
                GazePoint = tempPoint,
                TimeStamp = e.Timestamp
            };

     //       Console.Out.WriteLine("coordinates:" + tempPoint.ToString());


            var gazeTriggeredEvent = GazeDataTriggered;
            if (gazeTriggeredEvent != null) gazeTriggeredEvent(this, args);

            //        RunOnMainThread(new Action(() => Update_GazeData(eyeGazePoint)));
        }

        public void NotifyInstructionHasGazeChanged(bool hasGaze)
        {
            if (hasGaze)
            {
                ShowInstruction = true;
            }
        }

        public void CloseInstruction()
        {
            ShowInstruction = false;
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        private void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// Marshals the given operation to the UI thread.
        /// </summary>
        /// <param name="action">The operation to be performed.</param>
        private static void RunOnMainThread(Action action)
        {
            if (Application.Current != null)
            {
                Application.Current.Dispatcher.BeginInvoke(action);
            }
        }
    }
}
