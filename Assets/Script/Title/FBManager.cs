using Firebase.Auth;
using Firebase;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using Firebase.Extensions;
using Firebase.Database;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.Text;
public class FBManager : MonoBehaviour
{
    [SerializeField]
    TMP_InputField fieldEmail;
    [SerializeField]
    TMP_InputField fieldPassword;
    [SerializeField]
    Button btnJoin;
    [SerializeField]
    Button btnLogin;
    [SerializeField]
    Button btnLogout;
    [SerializeField]
    TMP_Text textStatus;

    [SerializeField]
    Button btnDeleteAccount;

    public bool bFirebaseOn { get; private set; }
    public bool bLoginProgress { get; private set; }

    private FirebaseApp fbApp;
    private FirebaseAuth fbAuth;
    private FirebaseUser fbUser;
    private DatabaseReference reference;

    private static FBManager instance;
    private static object lockObject = new object();
    public bool loginStatusFB = false;

    private FBManager()
    {
        // 생성자를 private로 설정하여 외부에서 직접 인스턴스를 생성할 수 없도록 하기.
    }

    public static FBManager Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        GameObject fbManagerObj = new GameObject("FBManager");
                        instance = fbManagerObj.AddComponent<FBManager>();
                        DontDestroyOnLoad(fbManagerObj);
                    }
                }
            }
            return instance;
        }
    }

    public void SetCurrentUser(FirebaseUser user)
    {
        fbUser = user;
    }
    void Start()
    {
        Init();

        btnJoin.onClick.AddListener(() =>
        {
            Join(fieldEmail.text, fieldPassword.text);
        });

        btnLogin.onClick.AddListener(() =>
        {
            Login(fieldEmail.text, fieldPassword.text);
        });

        // PlayerPrefs.SetString("UserEmail", null);
        // PlayerPrefs.SetString("UserPassword", null);

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            reference = FirebaseDatabase.DefaultInstance.RootReference;
        });

    }
    
    public void Init()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result != DependencyStatus.Available)
            {
                bFirebaseOn = false;
                textStatus.text = " Connection Failure";
            }
            else
            {
                bFirebaseOn = true;

                fbApp = FirebaseApp.DefaultInstance;
                fbAuth = FirebaseAuth.DefaultInstance;

                textStatus.text = " Connection Success";
                // 자동 로그인 시도
                string savedEmail = PlayerPrefs.GetString("UserEmail");
                string savedPassword = PlayerPrefs.GetString("UserPassword");

                if (!string.IsNullOrEmpty(savedEmail) && !string.IsNullOrEmpty(savedPassword))
                {
                    loginStatusFB = true;
                }
            }
        });
        // DisplaySignUp 스크립트를 초기화합니다
        DisplaySignUp displaySignUp = GetComponent<DisplaySignUp>();

        // FirebaseAuth 인스턴스를 전달합니다
        displaySignUp.SetFirebaseAuth(fbAuth);

    }

    // void Update()
    // {
    //     string savedEmail = PlayerPrefs.GetString("UserEmail");
    //     string savedPassword = PlayerPrefs.GetString("UserPassword");
    //     if ((Input.GetMouseButtonDown(0)) && (loginStatusFB == true))
    //     {
    //         Login(savedEmail, savedPassword);
    //     }
    // }
    public void clickStartL()
    {
        Debug.Log("CLICK IN FBM");
        string savedEmail = PlayerPrefs.GetString("UserEmail");
        string savedPassword = PlayerPrefs.GetString("UserPassword");
        if ((loginStatusFB == true))
        {
            Login(savedEmail, savedPassword);
        }
    }

    public void Join(string _email, string _passward)
    {
        fbAuth.CreateUserWithEmailAndPasswordAsync(_email, _passward).ContinueWithOnMainThread(task =>
        {
            bLoginProgress = false;

            if (task.IsFaulted || task.IsCanceled)
            {
                textStatus.text = "Join Failure";
            }
            else
            {
                fbUser = task.Result;
                textStatus.text = "Join Success";
                // 사용자 정보를 로컬에 저장
                PlayerPrefs.SetString("UserEmail", _email);
                PlayerPrefs.SetString("UserPassword", _passward);

                //firebase에 새로운 user 추가
                Debug.Log(fbUser.UserId+"!join!");
                PlayerPrefs.SetString("UserID", fbUser.UserId);
                AddDataToDatabase(fbUser.UserId, _email, _passward);
            }

        });
    }

    public void Login(string _email, string _passward)
    {
        if (bFirebaseOn == false ||
            bLoginProgress ||
            fbUser != null)
        {
            return;
        }

        bLoginProgress = true;

        fbAuth.SignInWithEmailAndPasswordAsync(_email, _passward).ContinueWithOnMainThread(task =>
        {
            bLoginProgress = false;

            if (task.IsFaulted || task.IsCanceled)
            {
                textStatus.text = "Login Failure";
            }
            else
            {
                fbUser = task.Result;
                textStatus.text = "Login Success";
                // 사용자 정보를 로컬에 저장
                PlayerPrefs.SetString("UserEmail", _email);
                PlayerPrefs.SetString("UserPassword", _passward);
                PlayerPrefs.SetString("UserID", fbUser.UserId);
                Debug.Log(fbUser.UserId+"!login!");

                DisplaySignUp DisplaySignUpScript = GetComponent<DisplaySignUp>();
                DisplaySignUpScript.LoginSuccessAuto();

            }
        });
    }

    public void Logout()
    {
        fbAuth.SignOut();

        if (fbUser.Email == "")
        {
            fbUser = null;
            textStatus.text = "Logout";
            PlayerPrefs.SetString("UserEmail", null);
            PlayerPrefs.SetString("UserPassword", null);
        }
    }

    // 이 메서드를 호출하여 데이터를 업로드
    public int AddDataToDatabase(string id, string gmail, string pw)
    {
        string[] constellationAdd = {"Aquarius", "Pisces", "Aries", "Taurus", "Gemini", "Cancer", "Leo", "Virgo", "Libra", "Scorpio", "Sagittarius", "Capricorn"};

        // 데이터를 추가할 경로 설정
        string pathID = "User+"+id+"/Info/id";
        string pathGmail = "User+"+id+"/Info/gmail";
        // string pathPw = "User+"+id+"/Info/pw";
        string pathHashedPw = "User+"+id+"/Info/Hashedpw";
        Debug.Log("function call.");

        // 추가할 데이터 설정
        // string data = id;
        if(reference!=null){   
            Debug.Log("reference is not null2.");
        }
        else{
            reference = FirebaseDatabase.DefaultInstance.RootReference;
            Debug.Log("reference is null2.");
        }
        // 데이터베이스에 데이터 추가
        reference.Child(pathID).SetValueAsync(id).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("id 추가 성공!");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("id 추가 실패: " + task.Exception);
            }
        });
        reference.Child(pathGmail).SetValueAsync(gmail).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("gmail 추가 성공!");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("gmail 추가 실패: " + task.Exception);
            }
        });
        // reference.Child(pathPw).SetValueAsync(pw).ContinueWithOnMainThread(task =>
        // {
        //     if (task.IsCompleted)
        //     {
        //         Debug.Log("pw 추가 성공!");
        //     }
        //     else if (task.IsFaulted)
        //     {
        //         Debug.LogError("pw 추가 실패: " + task.Exception);
        //     }
        // });
        for(int i=0;i<constellationAdd.Length;i++){
            string pathConstellation = "User+"+id+"/Constellation/"+constellationAdd[i];
            
            reference.Child(pathConstellation).SetValueAsync(0).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("추가 성공!");
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError("추가 실패: " + task.Exception);
                }
            });
        }
        string hashedPassword = HashPassword(pw);
        Debug.Log("Original Password: " + pw);
        Debug.Log("Hashed Password: " + hashedPassword);

        reference.Child(pathHashedPw).SetValueAsync(hashedPassword).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("pw 추가 성공!");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("pw 추가 실패: " + task.Exception);
            }
        });

        SceneManager.LoadScene("ADHDTest");
        return 1;
    }
    // 다른 스크립트에서 Firebase 초기화 및 참조 설정을 사용할 수 있도록 메서드 제공
    public DatabaseReference GetDatabaseReference()
    {
        return reference;
    }
    public static string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // 문자열을 바이트 배열로 변환 후 해시 계산
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            // 바이트 배열을 16진수 문자열로 변환하여 반환
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashedBytes.Length; i++)
            {
                builder.Append(hashedBytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}