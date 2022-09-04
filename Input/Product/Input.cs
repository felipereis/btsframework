//Abstract Product

namespace AbstractFactoryDesignPattern
{
    public interface Input
    {
        public bool Start();
        public bool Stop();
        public bool IsReady();
    }
}