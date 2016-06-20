using UnityEngine;
using System.Collections;

// Some Code from here:
// http://answers.unity3d.com/questions/24785/how-to-use-the-xbox-360-controller-d-pad-pc-.html
public class DPadButtons : MonoBehaviour
{
    //public VRKioskGUIMain _vrKioskGUIMain;

	//private bool left;
	//private bool right;
 //   private bool dpadXInUse;

 //   float lastX;
	//float lastY;
	
	public DPadButtons() 
	{
        //left = false;
        //right = false;
        //dpadXInUse = false;
    }
	
	void Update()
    {
        //// Left and Right on Dpad
        //if (Input.GetAxisRaw("DPadX") != 0)
        //{
        //    if (dpadXInUse == false)
        //    {
        //        dpadXInUse = true;

        //        // Dpad Right
        //        if (Input.GetAxis("DPadX") == 1)
        //        {
        //            right = true;
        //            left = false;

        //            _vrKioskGUIMain.DpadRightButtonPressed();

        //        }
        //        // Dpad Left
        //        else if (Input.GetAxis("DPadX") == -1)
        //        {
        //            right = false;
        //            left = true;

        //            _vrKioskGUIMain.DpadLeftButtonPressed();
        //        }
        //    }
        //}
        //if (Input.GetAxisRaw("DPadX") == 0)
        //{
        //    dpadXInUse = false;
        //    right = false;
        //    left = false;
        //}
    }
}
