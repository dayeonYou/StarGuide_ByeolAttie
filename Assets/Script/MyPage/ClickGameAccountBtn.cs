using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class ClickGameAccountBtn : MonoBehaviour
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
    public Sprite whiteImageSetting;

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
        if(settingObjectG.activeSelf){
            settingObjectCanvasGroup.DOFade(0f, 1f).OnComplete(() => settingObjectG.SetActive(false));
        }
        if(appObjectG.activeSelf){
            appObjectCanvasGroup.DOFade(0f, 1f).OnComplete(() => appObjectG.SetActive(false));
        }
        gameAccountObjectG.SetActive(true);
        gameAccountObjectCanvasGroup.DOFade(1f, 1f);
        //
        appBtn.interactable = true;
        settingBtn.interactable = true;
        gameAccountBtn.interactable = false;
        Image imageGameAccount = gameAccountBtn.GetComponent<Image>();
        if (imageGameAccount != null && clickedImage != null)
        {
            imageGameAccount.sprite = clickedImage;
        }
        else{
            Debug.Log("null Image");
        }
        //
        Image imageSetting = settingBtn.GetComponent<Image>();
        if (imageSetting != null && notClickedImage != null)
        {
            imageSetting.sprite = notClickedImage;
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
        Image imageIcon = ImageGameAccountIcon.GetComponent<Image>();
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
        Image imageIcon3 = ImageSettingIcon.GetComponent<Image>();
        if (imageIcon3 != null && whiteImageSetting != null)
        {
            imageIcon3.sprite = whiteImageSetting;
        }
        else{
            Debug.Log("null Image");
        }
        //
        TextGameAccount.color = Color.black;
        TextSetting.color = Color.white;
        TextApp.color = Color.white;
    }

}
