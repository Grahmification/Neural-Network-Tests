namespace Neural_Network_Test_2.Neural
{
    public class TrainingUpdate
    {
        public int currentTrainingStep;
        public int currentEpoch;
        public int maxTrainingSteps;
        public int maxEpochs;

        public float[] currentError;

        public float ProgressPercent()
        {
            var totalSteps = maxTrainingSteps * maxEpochs;
            var currentSteps = maxTrainingSteps * (currentEpoch-1) + (currentTrainingStep-1);
            return currentSteps / totalSteps;
        }
    }
}
