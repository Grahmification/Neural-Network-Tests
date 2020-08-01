using System;

namespace Neural_Network_Test_2.Neural 
{
    public class BipolarSigmoidFunction : IActivationFunction
    {
        public float Alpha { get; set; } = 2;

        public BipolarSigmoidFunction() { }
        public BipolarSigmoidFunction(double alpha)
        {
            this.Alpha = (float)alpha;
        }

        public float Derivative(float x)
        {
            float y = Function(x);

            return (Alpha * (1 - y * y) / 2);
        }
        public float Derivative2(float x)
        {
            throw new NotImplementedException();
        }
        public float Function(float x)
        {
            return (float)((2 / (1 + Math.Exp(-Alpha * x))) - 1);
        }
        public string FunctionName()
        {
            return "Bipolar Sigmoid";
        }
    }
}
