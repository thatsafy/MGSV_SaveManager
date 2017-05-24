using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace SteamScan
{
    public class MySteamScanner
    {
        private string localDir = "C:\\MGSV_saves";
        private string steamPath = "";
        private List<string> userNames = new List<string>();
        private string userID = "";

        private string MGSV1 = "287700";
        private string MGSV2 = "311340";

        private List<string> MGSV_Saves = new List<string>();
        
        
        

        public MySteamScanner()
        {
            
            this.MGSV_Saves.Add($"{this.MGSV1}\\*");
            this.MGSV_Saves.Add($"{this.MGSV2}\\TPP_CONFIG_DATA");
            this.MGSV_Saves.Add($"{this.MGSV2}\\TPP_GAME_DATA");
            this.MGSV_Saves.Add($"{this.MGSV2}\\PERSONAL_DATA");
        }
        /// <summary>
        /// Find Steam install directory
        /// </summary>
        public string ScanSteam()
        {

            try
            {
                this.steamPath = File.ReadAllLines($"{this.localDir}\\steam_path.txt")[0];
                Console.WriteLine("File found");
                return this.steamPath;
            } catch 
            {
                FirstRun();
                string driveInfo = $"wmic LOGICALDISK LIST BRIEF > {this.localDir}\\drvs.txt";
                CommandPrompter(driveInfo);
                string[] drives = File.ReadAllLines($"{this.localDir}\\drvs.txt");
                string findSteam;
                string clearPathFile = $"{localDir[0]}: && del /Q {this.localDir}\\temp_path.txt 2> nul";
                CommandPrompter(clearPathFile);
                for (int i = 1; i < drives.Length; i++)
                {
                    drives[i] = drives[i][0] + ":";
                    /// Put proper path here before release!
                    findSteam = $"{drives[i]} && dir /s /b Steam.exe >> {this.localDir}\\temp_path.txt";
                    CommandPrompter(findSteam);
                }
                string[] temp_path = File.ReadAllLines($"{this.localDir}\\temp_path.txt");
                ///File.Open($"{localDir}\\steam_path.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
                foreach (string x in temp_path)
                {
                    if (x.Contains("Steam.exe"))
                    {
                        Console.WriteLine("Found steam!");
                        this.steamPath = x.Replace("Steam.exe", "");
                        CommandPrompter($"echo {this.steamPath} > {this.localDir}\\steam_path.txt");
                        return this.steamPath;
                    }
                }
                return "Path not found, try running closing app and making sure Steam is installed.";
            }
        }

        /// <summary>
        /// Use Windows Command prompt
        /// </summary>
        /// <param name="command"></param>
        private void CommandPrompter(string command)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = $"/C {command}";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }

        /// <summary>
        /// Scan for users
        /// </summary>
        /// <returns></returns>
        public List<string> UserScan()
        {
            string userScan = $"{this.steamPath[0]}: && cd {this.steamPath}\\userdata && dir /b /AD > {this.localDir}\\users.txt";
            CommandPrompter(userScan);
            string[] users = File.ReadAllLines($"{this.localDir}\\users.txt");
            foreach (string userid in users)
            {
                string[] userData = File.ReadAllLines($"{this.steamPath}\\userdata\\{userid}\\config\\localconfig.vdf");
                foreach (string line in userData)
                {
                    if (line.Contains("PersonaName"))
                    {
                        this.userNames.Add(line.Replace("PersonaName", "").Replace("		", "").Replace("\"", ""));
                        return this.userNames;
                    }
                }
            }
            return this.userNames;
        }

        /// <summary>
        /// First run, create the local folder
        /// </summary>
        public void FirstRun()
        {
            string command = $"{this.localDir[0]}: && mkdir {this.localDir} 2> nul";
            CommandPrompter(command);
        }

        /// <summary>
        /// Scan local folder for save files
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<string> SaveScan(string username)
        {
            try
            {
                string command = $"{this.localDir[0]}: && dir /b /AD {this.localDir}\\{username} > {this.localDir}\\{username}\\saves.txt";
                CommandPrompter(command);
            } catch
            {
                string command = $"mkdir {this.localDir}\\{username} 2> nul";
                CommandPrompter(command);
                command = $"{this.localDir[0]}: && dir /b /AD {this.localDir}\\{username} > {this.localDir}\\{username}\\saves.txt";
                CommandPrompter(command);
            }

            string[] saves_temp = File.ReadAllLines($"{this.localDir}\\{ username}\\saves.txt");
            List<string> saves = new List<string>();
            foreach (string x in saves_temp)
            {
                saves.Add(x);
            }
            return saves;
        }

        public void SteamToLocal(string currentSave, string username)
        {
            string command = $"xcopy / E / Y {this.steamPath}{this.MGSV1} {localDir}\\{username}\\{currentSave}\\{this.MGSV1} 2> nul";
            CommandPrompter(command);
            command = $"xcopy / E / Y {this.steamPath}{this.MGSV2} {localDir}\\{username}\\{currentSave}\\{this.MGSV2} 2> nul";
            CommandPrompter(command);
        }

        public void LocalToSteam(string currentSave, string username)
        {
            string command = $"xcopy / E / Y {this.localDir}\\{username}\\{currentSave}\\{this.MGSV1} {this.steamPath}{this.MGSV1} 2> nul";
            CommandPrompter(command);
            command = $"xcopy / E / Y {this.localDir}\\{username}\\{currentSave}\\{this.MGSV2} {this.steamPath}{this.MGSV2} 2> nul";
            CommandPrompter(command);
        }


        public string CurrentSave()
        {
            return "";
        }

        public void SaveSwitch(string currentsave, string nextsave, string username)
        {
            this.SteamToLocal(currentsave, username);
            this.LocalToSteam(nextsave, username);
        }


        public void NewSave(string savename, string username)
        {
            string command = $"mkdir {localDir}\\{username}\\{savename}";
            CommandPrompter(command);
        }

    }
}

