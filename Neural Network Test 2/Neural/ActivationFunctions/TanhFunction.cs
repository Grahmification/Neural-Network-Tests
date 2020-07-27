using System;

namespace Neural_Network_Test_2.Neural
{
    class TanhFunction : IActivationFunction
    {
        public float Function(float x)
        {
            return (float)Math.Tanh(x);
        }
        public float Derivative(float x)
        {
            return 1 - (x * x); //derivative of tanh(x)
        }
        public float Derivative2(float x)
        {
            return -2 * x * (1 - (x * x));
        }
        public string FunctionName()
        {
            return "Tanh";
        }
    }
}
