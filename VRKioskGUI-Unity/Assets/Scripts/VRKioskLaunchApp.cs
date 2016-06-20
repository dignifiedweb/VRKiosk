using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Diagnostics;
using System.IO;

public class VRKioskLaunchApp
{
    private string _workingDirOfLauncher;
    private string _launcherExeName;
    private string _workingDirOfLauncherCLI;
    private string _launcherCLIName;
    
    public Process ProcessRunning { get; set; }
    public bool LaunchedGame { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LaunchVRKioskCLI"/> class.
    /// </summary>
    public VRKioskLaunchApp()
    {
        _workingDirOfLauncher = Path.GetFullPath(".");
        _workingDirOfLauncherCLI = _workingDirOfLauncher + "\\VRKioskCLI";
        _launcherCLIName = "VRKioskCLI.exe";
        LaunchedGame = false;
    }

    /// <summary>
    /// Launchs the selected game.
    /// </summary>
    /// <param name="filenameFullPath">Filename full path.</param>
    /// <param name="arguments">Arguments.</param>
    /// <param name="sendKeysString">Send keys string.</param>
    /// <param name="sendKeysForAButton">Send keys for A button.</param>
    public void LaunchSelectedGame(string filenameFullPath, string arguments, string sendKeysForDialog, string sendKeysForAButton)
    {
        // Patht to CLI launcher
        string vrKioskCLIFilename = _workingDirOfLauncherCLI + "\\" + _launcherCLIName;

        // Launch the CLI executable, which launches the VR game and listens to gamepad for home button to be pressed
        ProcessStartInfo procInfo = new ProcessStartInfo();
        procInfo.FileName = vrKioskCLIFilename;
        procInfo.Arguments = "\"" + filenameFullPath + "\" \"" + arguments + "\" \"" + sendKeysForDialog + "\" \"" + sendKeysForAButton + "\" \"\"";
        procInfo.WindowStyle = ProcessWindowStyle.Minimized;

        ProcessRunning = new Process();
        ProcessRunning.StartInfo = procInfo;
        ProcessRunning.Start();
        LaunchedGame = true;
    }
}
