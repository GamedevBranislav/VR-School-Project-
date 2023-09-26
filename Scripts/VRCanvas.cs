using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCanvas : MonoBehaviour
{
    public RayCastButton currentActiveButton;

    public Color unselectedColor = Color.gray;

    public Color selectedColor = Color.green;

    public void SetActiveButton(RayCastButton activeButton)
    {
        //if there was previously an active button,disable it.
        if(currentActiveButton != null)
        {
            currentActiveButton.SetButtonColor(unselectedColor);
        }
        if(activeButton != null && currentActiveButton != activeButton)
        {
            currentActiveButton = activeButton;
            currentActiveButton.SetButtonColor(selectedColor);
        }
        else
        {
            Debug.Log("Reseting");
            currentActiveButton = null;
            Player.instance.activeMode = InputMode.NONE;
        }
    }
}
