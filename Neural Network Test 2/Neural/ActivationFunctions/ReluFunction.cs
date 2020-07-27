namespace Neural_Network_Test_2.Neural
{
    public class ReluFunction : IActivationFunction
    {

        public float Derivative(float x)
        {
            return 1;
        }

        public float Derivative2(float y)
        {
            return 1;
        }

        public float Function(float x)
        {

            return x * 1;
        }

        public string FunctionName()
        {
            return "Relu";
        }
    }
}
