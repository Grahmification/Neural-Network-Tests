namespace Neural_Network_Test_2.Neural
{
    public class NetworkProgressArgs(double progress, NetworkStatus status, string statusStringArgs = "")
    {
        public NetworkStatus Status { get; private set; } = status;
        public string StatusString 
        {
            get
            {
                return Status switch
                {
                    NetworkStatus.Complete => "Complete",
                    NetworkStatus.Cancelled => "Cancelled",
                    NetworkStatus.Idle => "Idle",
                    NetworkStatus.LoadingData => "Loading Data",
                    NetworkStatus.PreparingData => "Preparing Data",
                    NetworkStatus.Processing => "Processing",
                    NetworkStatus.Training => "Training",
                    _ => "",
                };
            } 
        }

        public string StatusStringArgs { get; private set; } = statusStringArgs;

        /// <summary>
        /// Progress from 0 to 1
        /// </summary>
        public double Progress { get; private set; } = progress;

        /// <summary>
        /// Progress from 0 to 100 for progress bar
        /// </summary>
        public int PercentProgress => (int)(Progress * 100.0);
    }

    public enum NetworkStatus { LoadingData, PreparingData, Training, Processing, Idle, Cancelled, Complete}

}
