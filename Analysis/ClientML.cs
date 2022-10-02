using UnityEngine;

namespace StrategyDesignPattern
{
    public class ClientML : MonoBehaviour
    {
        // Start is called before the first frame update
        // Start Algorithm 1 and 2 - ML
        void Start()
        {
            Context ctx = new Context(new Algorithm1());
            ctx.ExecuteMachineLearning("data1");
            ctx.SetStrategy(new Algorithm2());
            ctx.ExecuteMachineLearning("data2");
        }
    }
}
