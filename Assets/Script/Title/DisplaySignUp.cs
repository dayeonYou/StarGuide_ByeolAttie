using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase.Extensions;
using UnityEngine.UI;
public class DisplaySignUp : MonoBehaviour
{
    public GameObject squareOb;
    //public GameObject textToStart;
    public GameObject btnJoin;
    public GameObject btnLogin;
    public GameObject btnLogout;
    public GameObject textStatus;
    public GameObject textLogin;
    public GameObject fieldEmail;
    public GameObject fieldPassword;
    public GameObject titleObject;
    public GameObject titleText;
    public GameObject btnStart;

    public Image imageComponent;
    public CanvasGroup textToStartCanvasGroup;
    public CanvasGroup btnStartCanvasGroup;
    public CanvasGroup btnJoinCanvasGroup;
    public CanvasGroup btnLoginCanvasGroup;
    public CanvasGroup btnLogoutCanvasGroup;
    public CanvasGroup textStatusCanvasGroup;
    public CanvasGroup textLoginCanvasGroup;
    public CanvasGroup fieldEmailCanvasGroup;
    public CanvasGroup fieldPasswordCanvasGroup;

    private FirebaseAuth fbAuth;
    private FirebaseUser fbUser;
    private bool AutoLoginStatus;
    // Animator animator;

    public bool bLoginProgress { get; private set; }
    private FBManager fbManagerScript;

    public void SetFirebaseAuth(FirebaseAuth auth)
    {
        fbAuth = auth;
    }

    void Start()
    {
        fbManagerScript = GetComponent<FBManager>();
        imageComponent  = squareOb.GetComponent<Image>();

        // squareObRenderer = squareOb.GetComponent<SpriteRenderer>();
        //textToStartCanvasGroup = textToStart.GetComponent<CanvasGroup>();
        btnStartCanvasGroup = btnStart.GetComponent<CanvasGroup>();
        btnJoinCanvasGroup = btnJoin.GetComponent<CanvasGroup>();
        btnLoginCanvasGroup = btnLogin.GetComponent<CanvasGroup>();
        btnLogoutCanvasGroup = btnLogout.GetComponent<CanvasGroup>();
        textStatusCanvasGroup = textStatus.GetComponent<CanvasGroup>();
        textLoginCanvasGroup = textLogin.GetComponent<CanvasGroup>();
        fieldEmailCanvasGroup = fieldEmail.GetComponent<CanvasGroup>();
        fieldPasswordCanvasGroup = fieldPassword.GetComponent<CanvasGroup>();
        
        Color newColor = new Color(1f, 1f, 1f, 0f);
        imageComponent.color = newColor;

        // squareObRenderer.color = new Color(1f, 1f, 1f, 0f);
        btnJoinCanvasGroup.alpha = 0f;
        btnLoginCanvasGroup.alpha = 0f;
        btnLogoutCanvasGroup.alpha = 0f;
        textStatusCanvasGroup.alpha = 0f;
        textLoginCanvasGroup.alpha = 0f;
        fieldEmailCanvasGroup.alpha = 0f;
        fieldPasswordCanvasGroup.alpha = 0f;

        //textToStart.SetActive(true);
        textToStartCanvasGroup.alpha = 1f;

        SetFirebaseAuth(FirebaseAuth.DefaultInstance);

        Debug.Log("email: " + PlayerPrefs.GetString("UserEmail"));
        Debug.Log("pw: " + PlayerPrefs.GetString("UserPassword"));

        TryAutoLogin();

    }

    private void TryAutoLogin()
    {
        if (fbAuth != null)
        {
            if (fbAuth.CurrentUser != null)
            {
                FBManager fbManager = FindObjectOfType<FBManager>();
                if (fbManager != null)
                {
                    fbManager.SetCurrentUser(fbAuth.CurrentUser);
                }
                Debug.Log("자동로그인 성공:"+fbAuth.CurrentUser);
                //
                squareOb.SetActive(false);
                btnJoin.SetActive(false);
                btnLogin.SetActive(false);
                // btnLogout.SetActive(false);
                textStatus.SetActive(false);
                textLogin.SetActive(false);
                fieldEmail.SetActive(false);
                fieldPassword.SetActive(false);
                //
                AutoLoginStatus = true;
                btnLogoutCanvasGroup.alpha = 1f;


            }
            else
            {
                Debug.Log("자동로그인 실패");
                AutoLoginStatus = false;
            }
        }
        else
        {
            Debug.LogError("FirebaseAuth instance is not set.");
        }


        // if((PlayerPrefs.GetString("UserEmail") != null) && (AutoLoginStatus == false)){
        //     string email = PlayerPrefs.GetString("UserEmail");
        //     string pw = PlayerPrefs.GetString("UserPassword");
        //     if (bLoginProgress ||
        //         fbUser != null)
        //     {
        //         return;
        //     }

        //     bLoginProgress = true;

        //     fbAuth.SignInWithEmailAndPasswordAsync(email, pw).ContinueWithOnMainThread(task =>
        //     {
        //         bLoginProgress = false;

        //         if (task.IsFaulted || task.IsCanceled)
        //         {
        //             Debug.Log("Login Failure");
        //         }
        //         else
        //         {
        //             fbUser = task.Result;
        //             Debug.Log("Login Success");

        //             // DisplaySignUp DisplaySignUpScript = GetComponent<DisplaySignUp>();
        //             // DisplaySignUpScript.LoginSuccess();

        //         }
        //     });
        //     Debug.Log("자동로그인 성공:"+fbAuth.CurrentUser);
        //     //
        //     squareOb.SetActive(false);
        //     btnJoin.SetActive(false);
        //     btnLogin.SetActive(false);
        //     // btnLogout.SetActive(false);
        //     textStatus.SetActive(false);
        //     textLogin.SetActive(false);
        //     fieldEmail.SetActive(false);
        //     fieldPassword.SetActive(false);
        //     //
        //     AutoLoginStatus = true;
        //     btnLogoutCanvasGroup.alpha = 1f;
        // }
    }

    public void clickStart()
    {
        bool windowShow = fbManagerScript.loginStatusFB;
        Debug.Log("CLICK IN DSU");

        if ((AutoLoginStatus == false) && (!windowShow))
        // if ((Input.GetMouseButtonDown(0)))
        {
            squareOb.SetActive(true);
            btnJoin.SetActive(true);
            btnLogin.SetActive(true);
            //btnLogout.SetActive(true);
            textStatus.SetActive(true);
            textLogin.SetActive(true);
            fieldEmail.SetActive(true);
            fieldPassword.SetActive(true);
            imageComponent.DOFade(1f, 1f);
            // squareObRenderer.DOFade(1f, 1f);
            ShowButtonAndTextWithFadeIn();
            AutoLoginStatus = true;
            Debug.Log("자동로그인 실패 + 클릭" + AutoLoginStatus);

        }
        
        if((AutoLoginStatus == true))
        {
            Debug.Log("자동로그인 성공 + 클릭" + AutoLoginStatus);

            // 텍스트 메시 프로 컴포넌트에 접근하여 텍스트를 변경
            //TextMeshProUGUI textMeshPro = textToStart.GetComponent<TextMeshProUGUI>();
            //textMeshPro.text = "자동 로그인 되었습니다.";
            Debug.Log("이메일: " + fbAuth.CurrentUser.Email);
            Debug.Log("사용자 ID: " + fbAuth.CurrentUser.UserId);
            PlayerPrefs.SetString("UserID", fbAuth.CurrentUser.UserId);

            //화면 올라가는 애니메이션
            // Animator 컴포넌트에 액세스하여 애니메이션을 트리거
            Animator animator = titleObject.GetComponent<Animator>();
            Animator animator2 = titleText.GetComponent<Animator>();
            //textToStartCanvasGroup.DOFade(0f, 1f).OnComplete(() => textToStart.SetActive(false));
            btnStartCanvasGroup.DOFade(0f, 1f).OnComplete(() => btnStart.SetActive(false));

            animator.Play("moveTitle");
            animator2.Play("MOVETitleText");

        }
    }
    

    public void ShowButtonAndTextWithFadeIn()
    {
        btnJoinCanvasGroup.DOFade(1f, 1f);
        btnLoginCanvasGroup.DOFade(1f, 1f);
        //btnLogoutCanvasGroup.DOFade(1f, 1f);
        textStatusCanvasGroup.DOFade(1f, 1f);
        textLoginCanvasGroup.DOFade(1f, 1f);
        fieldEmailCanvasGroup.DOFade(1f, 1f);
        fieldPasswordCanvasGroup.DOFade(1f, 1f);

        btnStartCanvasGroup.DOFade(0f, 1f).OnComplete(() => btnStart.SetActive(false));
        //textToStartCanvasGroup.DOFade(0f, 1f).OnComplete(() => textToStart.SetActive(false));
    }
    public void ShowButtonAndTextWithFadeOut()
    {
        /*
        btnJoinCanvasGroup.DOFade(0f, 1f).OnComplete(() => textToStart.SetActive(false));
        btnLoginCanvasGroup.DOFade(0f, 1f).OnComplete(() => textToStart.SetActive(false));
        textStatusCanvasGroup.DOFade(0f, 1f).OnComplete(() => textToStart.SetActive(false));
        textLoginCanvasGroup.DOFade(0f, 1f).OnComplete(() => textToStart.SetActive(false));
        fieldEmailCanvasGroup.DOFade(0f, 1f).OnComplete(() => textToStart.SetActive(false));
        fieldPasswordCanvasGroup.DOFade(0f, 1f).OnComplete(() => textToStart.SetActive(false));
        btnStartCanvasGroup.DOFade(0f, 1f).OnComplete(() => btnStart.SetActive(false));
        textToStartCanvasGroup.DOFade(0f, 1f).OnComplete(() => textToStart.SetActive(false));
        imageComponent.DOFade(0f, 1f).OnComplete(() => textToStart.SetActive(false));
        */
    } 

    public void LoginSuccess()
    {
        squareOb.SetActive(false);
        btnJoin.SetActive(false);
        btnLogin.SetActive(false);
        // btnLogout.SetActive(false);
        textStatus.SetActive(false);
        textLogin.SetActive(false);
        fieldEmail.SetActive(false);
        fieldPassword.SetActive(false);
        //

        ShowButtonAndTextWithFadeOut();

        //화면 올라가는 애니메이션
        // Animator 컴포넌트에 액세스하여 애니메이션을 트리거.
        Animator animator = titleObject.GetComponent<Animator>();
        Animator animator2 = titleText.GetComponent<Animator>();
        //textToStartCanvasGroup.DOFade(0f, 1f).OnComplete(() => textToStart.SetActive(false));
        btnStartCanvasGroup.DOFade(0f, 1f).OnComplete(() => btnStart.SetActive(false));

        animator.Play("moveTitle");
        animator2.Play("MOVETitleText");

        Debug.Log("로그인_이메일: " + fbAuth.CurrentUser.Email);
        Debug.Log("로그인_사용자 ID: " + fbAuth.CurrentUser.UserId);
    }
    public void LoginSuccessAuto()
    {
        squareOb.SetActive(false);
        btnJoin.SetActive(false);
        btnLogin.SetActive(false);
        // btnLogout.SetActive(false);
        textStatus.SetActive(false);
        textLogin.SetActive(false);
        fieldEmail.SetActive(false);
        fieldPassword.SetActive(false);
        
        Animator animator2 = titleText.GetComponent<Animator>();
        Animator animator = titleObject.GetComponent<Animator>();
        //textToStartCanvasGroup.DOFade(0f, 1f).OnComplete(() => textToStart.SetActive(false));
        btnStartCanvasGroup.DOFade(0f, 1f).OnComplete(() => btnStart.SetActive(false));

        animator2.Play("MOVETitleText");
        animator.Play("moveTitle");

    }

}
