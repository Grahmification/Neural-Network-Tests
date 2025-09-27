namespace Neural_Network_Test_2.Neural
{
    /// <summary>
    /// Generic definition for a neural network activation function
    /// </summary>
    public interface IActivationFunction
    {
        float Function(float x);

        float Derivative(float x);

        float Derivative2(float x);

        string FunctionName();
    }
}
