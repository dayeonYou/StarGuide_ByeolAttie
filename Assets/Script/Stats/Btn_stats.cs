using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Drawing;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class Btn_stats : MonoBehaviour
{
    //main
    public GameObject G_findstarobject;
    public GameObject G_mousegameobject;
    public GameObject G_targetgameobject;
    public GameObject G_treasureobject;
    public GameObject G_closetobject;

    //gameplay
    public GameObject findstargameplayobjectG;
    public GameObject mousegamegameplayobjectG;
    public GameObject targetgamegameplayobjectG;
    public GameObject treasuregameplayobjectG;
    public GameObject closetgameplayobjectG;
    
    //eyetracking
    public GameObject finadstareyetrackingobjectG;
    public GameObject mousegameyetrackingobjectG;
    public GameObject targetgameeyetrackingobjectG;
    public GameObject treasureeyetrackingobjectG;
    public GameObject closeteyetrackingobjectG;

    //main
    public CanvasGroup findstarCanvasGroup;
    public CanvasGroup mousegameCanvasGroup;
    public CanvasGroup targetgameCanvasGroup;
    public CanvasGroup treasureCanvasGroup;
    public CanvasGroup closetCanvasGroup;

    //gameplay
    public CanvasGroup gameplayCanvasGroup;

    //eyeytacking
    public CanvasGroup eyetrackingCanvasGroup;

    public Text find_Color;
    public Text mouse_Color;
    public Text target_Color;
    public Text treasure_Color;
    public Text closet_Color;

    void Start()
    {

        findstarCanvasGroup = G_findstarobject.GetComponent<CanvasGroup>();
        mousegameCanvasGroup = G_mousegameobject.GetComponent<CanvasGroup>();
        targetgameCanvasGroup = G_targetgameobject.GetComponent<CanvasGroup>();
        treasureCanvasGroup = G_treasureobject.GetComponent<CanvasGroup>();
        closetCanvasGroup = G_closetobject.GetComponent<CanvasGroup>();
    }

    public void ShowfindstarObject()
    {
        find_Color.color = Color.yellow;
        mouse_Color.color = Color.white;
        target_Color.color = Color.white;
        treasure_Color.color = Color.white;
        closet_Color.color = Color.white;
        ActivateCanvasGroup(findstarCanvasGroup);
        DeactivateCanvasGroup(mousegameCanvasGroup);
        DeactivateCanvasGroup(targetgameCanvasGroup);
        DeactivateCanvasGroup(treasureCanvasGroup);
        DeactivateCanvasGroup(closetCanvasGroup);
    }
    public void ShowmousegameObject()
    {
        mouse_Color.color = Color.yellow;
        find_Color.color = Color.white;
        target_Color.color = Color.white;
        treasure_Color.color = Color.white;
        closet_Color.color = Color.white;
        ActivateCanvasGroup(mousegameCanvasGroup);
        DeactivateCanvasGroup(findstarCanvasGroup);
        DeactivateCanvasGroup(targetgameCanvasGroup);
        DeactivateCanvasGroup(treasureCanvasGroup);
        DeactivateCanvasGroup(closetCanvasGroup);
    }
    public void ShowtargetgameObject()
    {
        target_Color.color = Color.yellow;
        find_Color.color = Color.white;
        mouse_Color.color = Color.white;
        treasure_Color.color = Color.white;
        closet_Color.color = Color.white;
        ActivateCanvasGroup(targetgameCanvasGroup);
        DeactivateCanvasGroup(mousegameCanvasGroup);
        DeactivateCanvasGroup(findstarCanvasGroup);
        DeactivateCanvasGroup(treasureCanvasGroup);
        DeactivateCanvasGroup(closetCanvasGroup);
    }
    public void ShowtreasureObject()
    {
        treasure_Color.color = Color.yellow;
        find_Color.color = Color.white;
        mouse_Color.color = Color.white;
        target_Color.color = Color.white;
        closet_Color.color = Color.white;
        ActivateCanvasGroup(treasureCanvasGroup);
        DeactivateCanvasGroup(mousegameCanvasGroup);
        DeactivateCanvasGroup(targetgameCanvasGroup);
        DeactivateCanvasGroup(findstarCanvasGroup);
        DeactivateCanvasGroup(closetCanvasGroup);
    }
    public void ShowclosetObject()
    {
        closet_Color.color = Color.yellow;
        find_Color.color = Color.white;
        mouse_Color.color = Color.white;
        target_Color.color = Color.white;
        treasure_Color.color = Color.white;
        ActivateCanvasGroup(closetCanvasGroup);
        DeactivateCanvasGroup(mousegameCanvasGroup);
        DeactivateCanvasGroup(targetgameCanvasGroup);
        DeactivateCanvasGroup(treasureCanvasGroup);
        DeactivateCanvasGroup(findstarCanvasGroup);
    }
    public void ShowGameplayObject()
    {
        ActivateCanvasGroup(gameplayCanvasGroup);
        DeactivateCanvasGroup(eyetrackingCanvasGroup);
    }
    public void ShowEyetrackingObject()
    {
        ActivateCanvasGroup(eyetrackingCanvasGroup);
        DeactivateCanvasGroup(gameplayCanvasGroup);
    }
    // 캔버스 그룹을 활성화하는 함수
    void ActivateCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.DOFade(1f, 0.5f);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    // 캔버스 그룹을 비활성화하는 함수
    void DeactivateCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.gameObject.SetActive(false);
        canvasGroup.DOFade(0f, 0.5f);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

}
