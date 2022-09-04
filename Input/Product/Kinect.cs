//Concrete Product

namespace AbstractFactoryDesignPattern
{
    public class Kinect : Input
    {
        private static bool _isKinectReady;

        public bool Start()
        {
            return _isKinectReady = true;
        }

        public bool Stop()
        {
            return _isKinectReady = false;
        }

        public bool IsReady()
        {
            return _isKinectReady;
        }
    }
}