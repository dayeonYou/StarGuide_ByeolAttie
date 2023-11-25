using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClosetShow : MonoBehaviour
{
    public string scene;
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Not_ShowCloset()
    {
        gameObject.SetActive(false);
    }
}
