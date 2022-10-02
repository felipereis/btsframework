using UnityEngine;

namespace StrategyDesignPattern
{
    public class Algorithm1 : IMachineLearning
    {
        // Implement Algorithm 1
        public void ExecuteMachineLearning(string data)
        {
            Debug.Log("Data: '" + data);
        }
    }
}
