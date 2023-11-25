using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase.Extensions;
using Firebase.Auth;
using Firebase;
using System;
using UnityEngine.UI;
using Firebase.Database;

public class ClickBtnWithdrawal : MonoBehaviour
{
    public GameObject withdrawalG;
    public CanvasGroup withdrawalCanvasGroup;

    public bool bFirebaseOn { get; private set; }
    public bool bLoginProgress { get; private set; }

    private FirebaseApp fbApp;
    private FirebaseAuth fbAuth;
    private FirebaseUser fbUser;

    [SerializeField]
    public TMP_InputField fieldCheckPassword;
    private DatabaseReference reference;

    void Start()
    {
        withdrawalCanvasGroup = withdrawalG.GetComponent<CanvasGroup>();
        withdrawalCanvasGroup.alpha = 0f;

        // Firebase 데이터베이스 참조 가져오기
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            reference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    public void clickBtnWithdrawal()
    {
        withdrawalG.SetActive(true);
        withdrawalCanvasGroup.DOFade(1f, 1f);
    }
    public void clickCancelBtnWithdrawal()
    {
        withdrawalCanvasGroup.DOFade(0f, 1f).OnComplete(() => withdrawalG.SetActive(false));
    }
    public void clickBtnLogout()
    {
        StartCoroutine(LogoutAfterInit());
    }
    public void clickCheckBtnWithdrawal()
    {
        StartCoroutine(DeleteAccountAfterInit());
    }
    public IEnumerator LogoutAfterInit()
    {
        yield return Init();
        yield return StartCoroutine(Logout());
        SceneManager.LoadScene("RFst");
    }
    public IEnumerator DeleteAccountAfterInit()
    {
        yield return Init();
        yield return StartCoroutine(DeleteAccount(fieldCheckPassword.text));
        SceneManager.LoadScene("RFst");
    }
    public IEnumerator Logout()
    {
        if (fbUser != null)
        {
            // Debug.Log("s!!");
            string savedPassword = PlayerPrefs.GetString("UserPassword");
            var signInTask = fbAuth.SignInWithEmailAndPasswordAsync(fbUser.Email, savedPassword);
            yield return new WaitUntil(() => signInTask.IsCompleted);

            if (signInTask.IsCompleted && !signInTask.IsFaulted)
            {
                // 로그인 성공
                fbUser = signInTask.Result;
                fbAuth.SignOut();

                if (fbUser.Email == "")
                {
                    fbUser = null;
                    Debug.Log("LOGOUT Success");
                    PlayerPrefs.SetString("UserEmail", null);
                    PlayerPrefs.SetString("UserPassword", null);
                }
            }
            else
            {
                // 로그인 실패
                Debug.Log("로그인 실패");
            }
        }
    }

    public IEnumerator DeleteAccount(string pw)
    {
        if (fbUser != null)
        {
            // Debug.Log("s!!");
            var signInTask = fbAuth.SignInWithEmailAndPasswordAsync(fbUser.Email, pw);
            yield return new WaitUntil(() => signInTask.IsCompleted);

            if (signInTask.IsCompleted && !signInTask.IsFaulted)
            {
                // 로그인 성공
                fbUser = signInTask.Result;
                var deleteTask = fbUser.DeleteAsync();
                yield return new WaitUntil(() => deleteTask.IsCompleted);
                string id = PlayerPrefs.GetString("UserID");

                if (deleteTask.IsCompleted && !deleteTask.IsFaulted)
                {
                    // 회원 탈퇴 성공
                    fbUser = null;
                    Debug.Log("Account Deletion Success");
                    DeleteAllData(id);

                    PlayerPrefs.SetString("UserEmail", null);
                    PlayerPrefs.SetString("UserPassword", null);


                    // Debug.Log("s");
                }
                else
                {
                    // 회원 탈퇴 실패
                    Debug.Log("Account Deletion Failed");
                    // Debug.Log("sx");
                    Debug.Log(deleteTask.Exception);
                }
            }
            else
            {
                // 로그인 실패
                Debug.Log("로그인 실패");
            }
        }
        else
        {
            Debug.Log("s!");
        }
    }


    public IEnumerator Init()
    {
        var task = FirebaseApp.CheckAndFixDependenciesAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Result != DependencyStatus.Available)
        {
            bFirebaseOn = false;
            Debug.Log("Firebase Connection Failure");
        }
        else
        {
            bFirebaseOn = true;

            fbApp = FirebaseApp.DefaultInstance;
            fbAuth = FirebaseAuth.DefaultInstance;
            Debug.Log("Firebase Connection Success");
        }

        // 로그인 되어있는지 확인
        string savedEmail = PlayerPrefs.GetString("UserEmail");
        string savedPassword = PlayerPrefs.GetString("UserPassword");

        if (!string.IsNullOrEmpty(savedEmail) && !string.IsNullOrEmpty(savedPassword))
        {
            yield return Login(savedEmail, savedPassword);
        }
    }

    public IEnumerator Login(string _email, string _password)
    {
        if (bFirebaseOn == false || bLoginProgress || fbUser != null)
        {
            yield break;
        }

        bLoginProgress = true;

        var task = fbAuth.SignInWithEmailAndPasswordAsync(_email, _password);
        yield return new WaitUntil(() => task.IsCompleted);

        bLoginProgress = false;

        if (task.IsFaulted || task.IsCanceled)
        {
            Debug.Log("Login Failure");
        }
        else
        {
            fbUser = task.Result;
            Debug.Log("Login Success");

            // 사용자 정보를 로컬에 저장
            PlayerPrefs.SetString("UserEmail", _email);
            PlayerPrefs.SetString("UserPassword", _password);
            // Debug.Log("Login email: "+_email);
            // Debug.Log("Login pw: "+_password);

        }
    }
    // 이 메서드를 호출하여 데이터를 삭제
    public void DeleteAllData(string id)
    {
        // 삭제할 경로 설정
        string path = "User+"+id;
        Debug.Log(path+"path");

        // 데이터베이스에서 경로 아래의 모든 데이터 삭제
        reference.Child(path).RemoveValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("데이터 삭제 성공!");

            }
            else if (task.IsFaulted)
            {
                Debug.LogError("데이터 삭제 실패: " + task.Exception);
            }
        });
    }
   
}
