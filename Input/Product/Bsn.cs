//Concrete Product

namespace AbstractFactoryDesignPattern
{
    public class Bsn : Input
    {
        private static bool _isBsnReady;

        public bool Start()
        {
            return _isBsnReady = true;
        }

        public bool Stop()
        {
            return _isBsnReady = false;
        }

        public bool IsReady()
        {
            return _isBsnReady;
        }
    }
}