//ConcreteFactory2

namespace AbstractFactoryDesignPattern
{
    public sealed class OpticalInputFactory : InputFactory
    {
        public override Input GetInput(string inputType)
        {
            if (inputType.Equals("Mediapipe"))
            {
                return new Mediapipe();
            }
            else if (inputType.Equals("Kinect"))
            {
                return new Kinect();
            }
            else
                return null;
        }
    }
}