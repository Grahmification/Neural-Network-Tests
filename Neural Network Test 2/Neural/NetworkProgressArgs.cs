using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
namespace Neural_Network_Test_2.Neural
{
    public class NetworkProgressArgs
    {
        public NetworkStatus Status { get; set; } = NetworkStatus.Idle;
        public string StatusString 
        {
            get
            {
                switch (Status) 
                {
                    case NetworkStatus.Complete:
                        return "Complete";

                    case NetworkStatus.Cancelled:
                        return "Cancelled";

                    case NetworkStatus.Idle:
                        return "Idle";

                    case NetworkStatus.LoadingData:
                        return "Loading Data";

                    case NetworkStatus.PreparingData:
                        return "Preparing Data";

                    case NetworkStatus.Processing:
                        return "Processing";

                    case NetworkStatus.Training:
                        return "Training";
                    default:
                        return "";
                }

            } 
        }
        
        public string StatusStringArgs = "";
        public double Progress { get; set; } = 0; //ranges from 0-1
        public int PercentProgress { get { return (int)(Progress * 100.0); } } //for progressbar

        public NetworkProgressArgs(double progress, NetworkStatus status, string statusStringArgs = "")
        {
            Progress = progress;
            Status = status;
            StatusStringArgs = statusStringArgs;
        }
    }

    public enum NetworkStatus { LoadingData, PreparingData, Training, Processing, Idle, Cancelled, Complete}

}
