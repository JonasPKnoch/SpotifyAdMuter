using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Media;

namespace AdMuter
{
    static class Program
    {
        static void Main()
        {
            Process spotifyProcess = GetProcessName("Spotify");
            VolumeController.SetApplicationVolume(spotifyProcess.Id, 1f);
            bool muted = false;

            while (true)
            {
                spotifyProcess = GetProcessName("Spotify");
                if (spotifyProcess.MainWindowTitle == "Advertisement")
                {
                    VolumeController.SetApplicationVolume(spotifyProcess.Id, 0f);
                    muted = true;
                } else if(muted)
                {
                    VolumeController.SetApplicationVolume(spotifyProcess.Id, 1f);
                    muted = false;
                }
                Debug.Print($"Found {spotifyProcess.MainWindowTitle}");
                Thread.Sleep(1000);
            }

            Debug.Print("Done");
        }

        public static Process GetProcessName(string name)
        {
            Process result = null;
            foreach (var process in Process.GetProcesses())
            {
                if (process.ProcessName == name && !String.IsNullOrEmpty(process.MainWindowTitle))
                    result = process;
            }

            if (result == null)
                return null;

            return result;
        }
    }
}
