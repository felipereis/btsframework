using UnityEngine;

namespace StrategyDesignPattern
{
    public class Method1 : IMachineLearning
    {
        // Implement method 1
        public void ExecuteMachineLearning(string data)
        {
            Debug.Log("Data: '" + data);
        }
    }
}