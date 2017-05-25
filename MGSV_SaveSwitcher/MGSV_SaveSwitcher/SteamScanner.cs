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
            this.MGSV_Saves.Add($"{this.MGSV1}\\local\\*");
            this.MGSV_Saves.Add($"{this.MGSV2}\\remote\\TPP_CONFIG_DATA");
            this.MGSV_Saves.Add($"{this.MGSV2}\\remote\\TPP_GAME_DATA");
            this.MGSV_Saves.Add($"{this.MGSV2}\\remote\\PERSONAL_DATA");
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
                        this.steamPath = x.Replace("Steam.exe", "").Trim();
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
                        this.userID = userid;
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
            Console.WriteLine("Save scanning");
            string command = $"mkdir {this.localDir}\\{username} 2> nul";
            CommandPrompter(command);
            command = $"{this.localDir[0]}: && dir /b /AD {this.localDir}\\{username} > {this.localDir}\\{username}\\saves.txt";
            CommandPrompter(command);

            string[] saves_temp = File.ReadAllLines($"{this.localDir}\\{username}\\saves.txt");
            Console.WriteLine("Line 135");
            List<string> saves = new List<string>();
            foreach (string x in saves_temp)
            {
                saves.Add(x);
            }
            Console.WriteLine("Line 141");
            Console.WriteLine(saves.Count);
            
            if (saves.Count <= 1)
            {
                Console.WriteLine("Line 144");
                this.FirstSave("Original", username);
            } else if (!saves.Contains(CurrentSave(username)))
            {
                Console.WriteLine("Line 148");
                ChangeCurrentSave(saves[0], username);
            }
            Console.WriteLine("Lien 151");
            return saves;
        }

        public void SteamToLocal(string currentSave, string username)
        {
            string command = $"xcopy /E /Y {this.steamPath.Trim()}userdata\\{this.userID}\\{this.MGSV1} {localDir}\\{username}\\{currentSave}\\{this.MGSV1} 2> nul";
            Console.WriteLine(command);
            this.CommandPrompter(command);
            command = $"xcopy /E /Y {this.steamPath.Trim()}userdata\\{this.userID}\\{this.MGSV2} {localDir}\\{username}\\{currentSave}\\{this.MGSV2} 2> nul";
            Console.WriteLine(command);
            this.CommandPrompter(command);
        }

        public void LocalToSteam(string currentSave, string username)
        {
            string command = $"xcopy /E /Y {this.localDir}\\{username}\\{currentSave}\\{this.MGSV1} {this.steamPath.Trim()}userdata\\{this.userID}\\{this.MGSV1} 2> nul";
            this.CommandPrompter(command);
            command = $"xcopy /E /Y {this.localDir}\\{username}\\{currentSave}\\{this.MGSV2} {this.steamPath.Trim()}userdata\\{this.userID}\\{this.MGSV2} 2> nul";
            this.CommandPrompter(command);
        }

        public void SteamToOld(string username)
        {
            string command = $"mkdir {this.localDir}\\{username}\\OLD\\{this.MGSV1} 2> nul";
            this.CommandPrompter(command);
            command = $"mkdir {this.localDir}\\{username}\\OLD\\{this.MGSV2} 2> nul";
            this.CommandPrompter(command);

            command = $"xcopy /E /Y {this.steamPath.Trim()}userdata\\{this.userID}\\{this.MGSV1} {this.localDir}\\{username}\\OLD\\{this.MGSV1} 2> nul";
            Console.WriteLine(command);
            this.CommandPrompter(command);
            command = $"xcopy /E /Y {this.steamPath.Trim()}userdata\\{this.userID}\\{this.MGSV2} {this.localDir}\\{username}\\OLD\\{this.MGSV2} 2> nul";
            Console.WriteLine(command);
            this.CommandPrompter(command);
        }

        public string CurrentSave(string username)
        {
            try
            {
                string currentsave = File.ReadAllLines($"{localDir}\\{username}\\current_save.txt")[0];
                return currentsave.Trim();
            } catch
            {
                string currentsave = "none";
                string command = $"echo {currentsave} > {this.localDir}\\{username}\\current_save.txt";
                this.CommandPrompter(command);
                return currentsave.Trim();
            }
                
        }

        public void FirstSave(string savename, string username)
        {
            string command = $"mkdir {this.localDir}\\{username}\\{savename}";
            this.CommandPrompter(command);
            command = $"mkdir {this.localDir}\\{username}\\{savename}\\{this.MGSV1}";
            this.CommandPrompter(command);
            command = $"mkdir {this.localDir}\\{username}\\{savename}\\{this.MGSV2}";
            this.CommandPrompter(command);
            this.SteamToLocal(savename, username);
            this.ChangeCurrentSave(savename, username);
        }

        public void SaveSwitch(string nextsave, string username)
        {
            string currentsave = CurrentSave(username);
            if (!this.SaveScan(username).Contains(currentsave))
            {
                this.SteamToOld(username);
            } else
            {
                this.SteamToLocal(currentsave, username);
            }
            this.DeleteSteamFiles();
            this.LocalToSteam(nextsave, username);
            this.ChangeCurrentSave(nextsave, username);
        }

        public void DeleteSteamFiles()
        {
            foreach (string data in this.MGSV_Saves)
            {
                string command = $"del /Q {this.steamPath.Trim()}userdata\\{this.userID}\\{data}";
                Console.WriteLine(command);
                this.CommandPrompter(command);
            }
        }

        public void ChangeCurrentSave(string save, string username)
        {
            string command = $"echo {save} > {this.localDir}\\{username}\\current_save.txt";
            this.CommandPrompter(command);
        }

        public void NewSave(string savename, string username)
        {
            string current = CurrentSave(username);
   
            this.SteamToLocal(current, username);
            string command = $"mkdir {this.localDir}\\{username}\\{savename}";
            this.CommandPrompter(command);
            command = $"mkdir {this.localDir}\\{username}\\{savename}\\{this.MGSV1} 2> nul";
            this.CommandPrompter(command);
            command = $"mkdir {this.localDir}\\{username}\\{savename}\\{this.MGSV2} 2> nul";
            this.CommandPrompter(command);
            this.DeleteSteamFiles();
            this.SteamToLocal(savename, username);
            
            this.ChangeCurrentSave(savename, username);
        }

        public void RenameSave(string save, string username, string newname)
        {
            string command = $"rename {this.localDir}\\{username}\\{save.Trim()} {newname}";
            Console.WriteLine(command);
            this.CommandPrompter(command);
            ChangeCurrentSave(newname, username);
        }

        public void DeleteSave(string save, string username)
        {
            string command = $"rmdir /S /Q {localDir}\\{username}\\{save}";
            this.CommandPrompter(command);
            
            if (save == CurrentSave(username))
            {
                this.DeleteSteamFiles();
                string cursave = this.SaveScan(username)[0];
                this.ChangeCurrentSave(cursave, username);
                this.LocalToSteam(cursave, username);
            }
        }

    }
}

