using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VRKioskAppListItem
{
    public string Name { get; set; }
    public Sprite Image { get; set; }
    public string ImageFilePath { get; set; }
    public string FilePath { get; set; }
    public string Parameters { get; set; }
    public string SendKeysForDialog { get; set; }
    public string SendKeysForAButton { get; set; }
    public string Notes { get; set; }
    
    // Constructor
    public VRKioskAppListItem()
    {

    }
}
