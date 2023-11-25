using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCalibration : MonoBehaviour
{
    public GameObject Cali_prefab;

    private void Start()
    {
        Cali_prefab.SetActive(false);
    }

    public void Click_cali()
    {
        Cali_prefab.SetActive(true);
    }

    public void Click_exit()
    {
        Cali_prefab.SetActive(false);
    }
}
