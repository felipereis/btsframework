using UnityEngine;

namespace AbstractFactoryDesignPattern
{
    class ClientInput : MonoBehaviour
    {
        private Input input;
        private InputFactory inputFactory;

        // Start is called before the first frame update
        void Start()
        {
            // Create the OpticalInput factory object by passing the factory type as Optical
            inputFactory = InputFactory.CreateInputFactory("Optical");

            // Get Mediapipe Input object by passing the input type as Mediapipe
            input = inputFactory.GetInput("Mediapipe");
            input.Start();

            // Get Kinect Input object by passing the input type as Kinect
            input = inputFactory.GetInput("Kinect");
            input.Start();
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}