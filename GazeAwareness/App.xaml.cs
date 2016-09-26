//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

namespace GazeAwareness
{
    using System.Windows;
    using EyeXFramework.Wpf;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private WpfEyeXHost _eyeXHost;
        public static MainWindowModel _mainWindowModel;


        public App()
        {
            _eyeXHost = new WpfEyeXHost();
            _eyeXHost.Start();

            _mainWindowModel = new MainWindowModel(_eyeXHost);
            MainWindow = new MainWindow() { Visibility = Visibility.Visible, DataContext = _mainWindowModel };
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _eyeXHost.Dispose();
        }

        public static MainWindowModel GetMainWindowModel
        {
            get
            {
                return _mainWindowModel;
            }
        }
    }
}
