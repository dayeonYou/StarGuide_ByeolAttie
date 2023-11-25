using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure_cali : MonoBehaviour
{
    public GameObject Cali_prefab;
    public GameObject player;
    public GameObject clock;
    public GameObject go;

    private void Start()
    {
        Cali_prefab.SetActive(false);
    }

    public void Click_cali()
    {
        Cali_prefab.SetActive(true);
        player.SetActive(false);
        clock.SetActive(false);
        go.SetActive(false);
    }

    public void Click_exit()
    {
        Cali_prefab.SetActive(false);
        player.SetActive(true);
        clock.SetActive(true);
        go.SetActive(true);
    }
}
