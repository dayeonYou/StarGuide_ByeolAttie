using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class getInfo : MonoBehaviour
{
    public TextMeshProUGUI userEmail;

    // Start is called before the first frame update
    void Start()
    {
        string emailGet = "";
        emailGet = PlayerPrefs.GetString("UserEmail");
        userEmail = userEmail.GetComponent<TextMeshProUGUI>();
        if(userEmail==null){
            Debug.Log("nullText");
        }
        else{
            userEmail.text = emailGet;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
