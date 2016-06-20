using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XInputDotNetPure;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using System.Management;

namespace VRKioskCLINS
{
    public class Program
    {
        static Process _procToLaunch = new Process();
        static string _vrKioskFilePath;
        static int _vrKioskGUIProcessID;

        public static void Main(string[] args)
        {
            // Arguments
            // args[0] = string filename (full path)
            // args[1] = string arguments (for executable we are launching)
            // args[2] = string sendkeys (if not blank, these keys will be sent to executable)
            // args[3] = string sendkeys to bind to gamepad button a
            // args[4] = Unity launcher (VR app) exe full path


            bool appClosedNormal = false;

            Process[] processNamedVRKioskGUI = Process.GetProcessesByName("VRKioskGUI");
            bool vrKioskGUIOpen = false;
            foreach (Process p in processNamedVRKioskGUI)
            {
                vrKioskGUIOpen = true;
                _vrKioskFilePath = p.MainModule.FileName;
                _vrKioskGUIProcessID = p.Id;
            }

            Console.WriteLine("VR Kiosk CLI - Running application - please don't close this window, it is important.");

            // Must be 4 arguments
            if (args.Length > 3)
            {
                string filenameFullPath = args[0];
                string argumentsForExecutable = args[1];
                string sendKeysString = args[2];
                string sendKeysForAButton = args[3];

                // Get name of executable file
                string executableName = Path.GetFileName(filenameFullPath);

                // Status to user what's running
                Console.WriteLine("\r\n---------------\r\nRunning: " + executableName + "\r\n---------------\r\n");

                // Get working directory of exe:
                string workingDirectoryOfExe = filenameFullPath.Replace("\\" + executableName, "");

                // Start the VR game the user wanted
                _procToLaunch.StartInfo.WorkingDirectory = workingDirectoryOfExe;
                _procToLaunch.StartInfo.FileName = filenameFullPath;
                _procToLaunch.StartInfo.Arguments = argumentsForExecutable;
                _procToLaunch.StartInfo.UseShellExecute = false;
                _procToLaunch.Start();
                
                // If the sendkeys contains anything, send the keys
                if (sendKeysString.Length > 0)
                {
                    // Wait until input is idle before sendkeys works
                    _procToLaunch.WaitForInputIdle();

                    // Send the keys to the dialog window
                    SendKeys.SendWait(sendKeysString);
                }

                Console.WriteLine("Game should now be open...");

                // Check for gamepad middle button press - if pressed exit game, then launch the launcher again
                while (_procToLaunch.HasExited == false)
                {
                    if (GamePadStartBackPressed() == true)
                    {
                        Console.WriteLine("[Exit proc] - Start and back button pressed. Game will close and VR Kiosk GUI will now launch.");
                        KillProcessAndChildren(_procToLaunch.Id);
                        appClosedNormal = true;
                    }

                    if (sendKeysForAButton.Length > 0)
                    {
                        GamePadState state = GamePad.GetState(PlayerIndex.One);
                        if (state.Buttons.A.Equals(XInputDotNetPure.ButtonState.Pressed))
                        {
                            // Send keys such as " " for spacebar or "{ENTER}" for enter or "^" for CTRL
                            Console.WriteLine("A button pressed - Trying to send keys - Keys sent: " + sendKeysForAButton);
                            SendKeys.SendWait(sendKeysForAButton);
                        }
                    }

                    // Sleep the while loop 32 milliseconds
                    Thread.Sleep(32);
                }

                // If the app didn't close normal, force kill VR Kiosk if open
                // This is a work around for oculus home killing open app, leaves an orphan VR Kiosk open
                if (!appClosedNormal && vrKioskGUIOpen)
                {
                    KillProcessAndChildren(_vrKioskGUIProcessID);
                    Thread.Sleep(32);
                    LaunchVRKioskGUI();
                }
            }
            else
            {
                Console.WriteLine("You must provide 4 arguments to this file.");
            }

            Console.WriteLine("\r\n-----------\r\nApplication has now closed");

            // Keep console window open after execution (used for debugging)
            //Console.ReadLine(); 

        }

        public static bool GamePadStartBackPressed()
        {
            GamePadState state = GamePad.GetState(PlayerIndex.One);
            bool backButton = state.Buttons.Back.Equals(XInputDotNetPure.ButtonState.Pressed);
            bool startButton = state.Buttons.Start.Equals(XInputDotNetPure.ButtonState.Pressed);

            if (backButton == true && startButton == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static void LaunchVRKioskGUI()
        {
            // Working directory of vr launcher app
            string vrLauncherWorkingDirectory = _vrKioskFilePath.Replace("\\" + Path.GetFileName(_vrKioskFilePath), "");

            // Launch the app
            Process vrKioskLauncherProc = new Process();
            vrKioskLauncherProc.StartInfo.FileName = _vrKioskFilePath;
            vrKioskLauncherProc.StartInfo.WorkingDirectory = vrLauncherWorkingDirectory;
            vrKioskLauncherProc.StartInfo.UseShellExecute = false;
            vrKioskLauncherProc.Start();
        }


        /// <summary>
        /// Kill a process, and all of its children, grandchildren, etc.
        /// </summary>
        /// <param name="pid">Process ID.</param>
        private static void KillProcessAndChildren(int pid)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher
              ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection moc = searcher.Get();
            foreach (ManagementObject mo in moc)
            {
                KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
            }
            try
            {
                Process proc = Process.GetProcessById(pid);
                proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
        }
    }
}
