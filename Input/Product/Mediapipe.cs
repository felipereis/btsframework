//Concrete Product

namespace AbstractFactoryDesignPattern
{
    public class Mediapipe : Input
    {
        private static bool _isMediaPipeReady;

        public bool Start()
        {
            return _isMediaPipeReady = true;
        }

        public bool Stop()
        {
            return _isMediaPipeReady = false;
        }

        public bool IsReady()
        {
            return _isMediaPipeReady;
        }
    }
}