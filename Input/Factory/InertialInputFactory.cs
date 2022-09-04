//ConcreteFactory1

namespace AbstractFactoryDesignPattern
{
    public sealed class InertialInputFactory : InputFactory
    {
        public override Input GetInput(string inputType)
        {
            if (inputType.Equals("Bsn"))
            {
                return new Bsn();
            }
            else
                return null;
        }
    }
}