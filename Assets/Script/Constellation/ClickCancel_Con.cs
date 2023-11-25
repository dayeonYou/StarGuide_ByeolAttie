using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCancel_Con : MonoBehaviour
{
    public GameObject WindowOb;

    public void clickCancel()
    {
        WindowOb.SetActive(false);
    }

}
