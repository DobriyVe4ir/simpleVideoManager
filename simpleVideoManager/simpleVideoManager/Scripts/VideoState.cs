using Microsoft.Extensions.Logging;

namespace simpleVideoManager.Scripts

{
    public class VideoState
    {
        public event Action? OnVideoChanged;
        private string? _currentVideoPath;

        public string? CurrentVideoPath
        {
            get => _currentVideoPath;
            set
            {
                Console.WriteLine(value + " selected");
                if (_currentVideoPath != value)
                {
                    _currentVideoPath = value;
                    OnVideoChanged?.Invoke();
                }
            }
        }
    }

}
