using UnityEngine;

namespace StrategyDesignPattern
{
    public class Method2 : IMachineLearning
    {
        // Implement method 2
        public void ExecuteMachineLearning(string data)
        {
            Debug.Log("Data: '" + data);
        }
    }
}