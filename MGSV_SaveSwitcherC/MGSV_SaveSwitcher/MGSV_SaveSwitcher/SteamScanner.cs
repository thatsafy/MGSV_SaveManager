using System;
using System.IO;
using System.Text;
namespace SteamScan
{
    public class MySteamScanner
    {
        public MySteamScanner()
        {
        }

        public void ScanSteam()
        {
            string localDir = "C:\\MGSV_saves";
            string driveInfo = $"wmic LOGICALDISK LIST BRIEF > {localDir}\\drvs.txt";
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = $"/C {driveInfo}";
            process.StartInfo = startInfo;
            process.Start();

            using (FileStream fs = File.Open($"{localDir}\\drvs.txt", FileMode.Open))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    Console.WriteLine(temp.GetString(b));
                }
            }

        }

    }
}

