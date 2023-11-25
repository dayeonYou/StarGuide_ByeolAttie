using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class ClickSettingBtn : MonoBehaviour
{
    public GameObject gameAccountObjectG;
    public GameObject settingObjectG;
    public GameObject appObjectG;
    public CanvasGroup gameAccountObjectCanvasGroup;
    public CanvasGroup settingObjectCanvasGroup;
    public CanvasGroup appObjectCanvasGroup;

    public Button appBtn;
    public Button settingBtn;
    public Button gameAccountBtn;
    public Sprite clickedImage;
    public Sprite notClickedImage;
    public Sprite blackImage;
    public Sprite whiteImageApp;
    public Sprite whiteImageGameAccount;

    public GameObject ImageSettingIcon;
    public GameObject ImageAppIcon;
    public GameObject ImageGameAccountIcon;

    public TextMeshProUGUI TextApp;
    public TextMeshProUGUI TextGameAccount;
    public TextMeshProUGUI TextSetting;

    // Start is called before the first frame update
    void Start()
    {
        gameAccountObjectCanvasGroup = gameAccountObjectG.GetComponent<CanvasGroup>();
        settingObjectCanvasGroup = settingObjectG.GetComponent<CanvasGroup>();
        appObjectCanvasGroup = appObjectG.GetComponent<CanvasGroup>();
        gameAccountObjectCanvasGroup.alpha = 0f;
        settingObjectCanvasGroup.alpha = 0f;
        appObjectCanvasGroup.alpha = 0f;
        
    }

    public void ShowGameAccountWithFadeIn()
    {
        if(gameAccountObjectG.activeSelf){
            gameAccountObjectCanvasGroup.DOFade(0f, 1f).OnComplete(() => gameAccountObjectG.SetActive(false));
        }
        if(appObjectG.activeSelf){
            appObjectCanvasGroup.DOFade(0f, 1f).OnComplete(() => appObjectG.SetActive(false));
        }
        settingObjectG.SetActive(true);
        settingObjectCanvasGroup.DOFade(1f, 1f);

        //
        gameAccountBtn.interactable = true;
        appBtn.interactable = true;
        settingBtn.interactable = false;
        Image imageSetting = settingBtn.GetComponent<Image>();
        if (imageSetting != null && clickedImage != null)
        {
            imageSetting.sprite = clickedImage;
        }
        else{
            Debug.Log("null Image");
        }
        //
        Image imageGameAccount = gameAccountBtn.GetComponent<Image>();
        if (imageGameAccount != null && notClickedImage != null)
        {
            imageGameAccount.sprite = notClickedImage;
        }
        else{
            Debug.Log("null Image");
        }
        //
        Image imageApp = appBtn.GetComponent<Image>();
        if (imageApp != null && notClickedImage != null)
        {
            imageApp.sprite = notClickedImage;
        }
        else{
            Debug.Log("null Image");
        }
        //
        Image imageIcon = ImageSettingIcon.GetComponent<Image>();
        if (imageIcon != null && blackImage != null)
        {
            imageIcon.sprite = blackImage;
        }
        else{
            Debug.Log("null Image");
        }
        //
        Image imageIcon2 = ImageAppIcon.GetComponent<Image>();
        if (imageIcon2 != null && whiteImageApp != null)
        {
            imageIcon2.sprite = whiteImageApp;
        }
        else{
            Debug.Log("null Image");
        }
        //
        Image imageIcon3 = ImageGameAccountIcon.GetComponent<Image>();
        if (imageIcon3 != null && whiteImageGameAccount != null)
        {
            imageIcon3.sprite = whiteImageGameAccount;
        }
        else{
            Debug.Log("null Image");
        }
        //
        TextSetting.color = Color.black;
        TextApp.color = Color.white;
        TextGameAccount.color = Color.white;
    }

}
