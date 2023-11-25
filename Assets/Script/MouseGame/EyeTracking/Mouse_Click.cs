using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Mouse_Click : MonoBehaviour, IPointerClickHandler
{
    pause pause;
    Mouse_EyeTrackingScript Mouse_EyeTrackingScript;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (pause != null)
        {
            Debug.Log("Click");
            pause.onClickStartButton();
            Mouse_EyeTrackingScript.stopTracking();
        }

    }

}