namespace StrategyDesignPattern
{
    // Strategy Pattern Context
    public class Context
    {
        private IMachineLearning ml;

        public Context(IMachineLearning ml)
        {
            this.ml = ml;
        }

        public void SetStrategy(IMachineLearning ml)
        {
            this.ml = ml;
        }

        public void ExecuteMachineLearning(string data)
        {
            ml.ExecuteMachineLearning(data);
        }
    }
}