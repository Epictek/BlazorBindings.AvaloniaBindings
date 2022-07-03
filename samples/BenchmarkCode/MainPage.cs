using System.Diagnostics;

namespace BenchmarkCode
{
    public class MainPage : ContentPage
    {
        private MovingCircle[] _circles = new MovingCircle[] {
            new(15, Colors.AliceBlue),
            new(20, Colors.AntiqueWhite),
            new(25, Colors.Aqua),
            new(30, Colors.Azure),
            new(40, Colors.Beige),
            new(45, Colors.Fuchsia),
            new(50, Colors.Green),
            new(53, Colors.Yellow),
            new(57, Colors.DarkSalmon),
            new(60, Colors.Magenta)
        };

        private Label _fpsLabel = new() { TextColor = Colors.Red };

        private double x;
        double _speed = 0.1;
        List<double> _frameTimes = new();
        double _fps = 0;
        Stopwatch _stopWatch = new();
        TimeSpan _waitTime = TimeSpan.FromMilliseconds(1);


        public MainPage()
        {
            var layout = new AbsoluteLayout();
            foreach (var circle in _circles)
                layout.Children.Add(circle);

            layout.Children.Add(_fpsLabel);

            Content = layout;
            UpdateLoop();
        }

        async void UpdateLoop()
        {
            while (true)
            {
                x += (_stopWatch.Elapsed.TotalSeconds * _speed) % 1;
                UpdatePosition();

                if (_frameTimes.Count > 50)
                {
                    _fps = 1 / _frameTimes.Average();
                    _frameTimes.Clear();
                }

                _frameTimes.Add(_stopWatch.Elapsed.TotalSeconds);
                _stopWatch.Restart();

                await Task.Delay(_waitTime);
            }
        }

        private void UpdatePosition()
        {
            _fpsLabel.Text = $"FPS: {_fps}";

            _circles[0].SetPosition(x % 1, (-Math.Cos(x * 10) + 1) / 2);
            _circles[1].SetPosition(2 * x % 1, (-Math.Cos(x * 15) + 1) / 3);
            _circles[2].SetPosition(3 * x % 1, (-Math.Cos(x * 20) + 1) / 4);
            _circles[3].SetPosition(4 * x % 1, (-Math.Cos(x * 25) + 1) / 5);
            _circles[4].SetPosition(5 * x % 1, (-Math.Cos(x * 30) + 1) / 6);
            _circles[5].SetPosition(6 * x % 1, (-Math.Cos(x * 35) + 1) / 2);
            _circles[6].SetPosition(7 * x % 1, (-Math.Cos(x * 40) + 1) / 2);
            _circles[7].SetPosition(8 * x % 1, (-Math.Cos(x * 45) + 1) / 2);
            _circles[8].SetPosition(9 * x % 1, (-Math.Cos(x * 50) + 1) / 2);
            _circles[9].SetPosition(10 * x % 1, (-Math.Cos(x * 55) + 1) / 2);
        }
    }
}
