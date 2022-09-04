using UnityEngine;

namespace StrategyDesignPattern
{
    public class ClientML : MonoBehaviour
    {
        // Start is called before the first frame update
        // Start method 1 and 2 - ML
        void Start()
        {
            Context ctx = new Context(new Method1());
            ctx.ExecuteMachineLearning("data1");
            ctx.SetStrategy(new Method2());
            ctx.ExecuteMachineLearning("data2");
        }
    }
}