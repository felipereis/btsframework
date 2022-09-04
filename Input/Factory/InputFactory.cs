//AbstractFactory

namespace AbstractFactoryDesignPattern
{
    public abstract class InputFactory
    {
        public abstract Input GetInput(string inputType);

        public static InputFactory CreateInputFactory(string factoryType)
        {
            if (factoryType.Equals("Optical"))
                return new OpticalInputFactory();
            else
                return new InertialInputFactory();
        }
    }
}