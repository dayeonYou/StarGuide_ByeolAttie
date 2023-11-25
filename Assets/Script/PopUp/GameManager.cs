using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public spawn2 spawn2;
    public Movement movement;
    public GameObject Panel;
    public Button stBtn;

    // Start is called before the first frame update
    void Start()
    {
        stBtn = GameObject.Find("StartButton").GetComponent<Button>();
    }

    public void onClickStartButton () { 
    
        Panel.SetActive(false);
        movement.canMove = true;
    }
}
