namespace Neural_Network_Test_2.Neural
{
    public interface IActivationFunction
    {
        float Function(float x);

        float Derivative(float x);

        float Derivative2(float x);

        string FunctionName();
    }
}
