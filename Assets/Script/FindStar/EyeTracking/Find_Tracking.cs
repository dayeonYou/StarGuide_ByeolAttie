using System.Collections;
using System;
using UnityEngine;
using static InitializationDelegate;
using static GazeDelegate;
using static StatusDelegate;
using static UserStatusDelegate;
using UnityEngine.UI;
using static CalibrationDelegate;
#if UNITY_ANDROID
using UnityEngine.Android;
#elif UNITY_IOS
using System.Runtime.InteropServices;
#endif
public class Find_Tracking : MonoBehaviour
{
    public string LisenseKey = "prod_p2w9yepluo1hojftnsh85wvkpczxm6gq6snsjhdc";

    public GameObject OverlayCanvas;

    bool isInitialized;
    public bool isTracking;
    bool isCalibrating;
    UserStatusOption Option;

    public GameObject StartTracking;
    public GameObject StopTracking;

    bool useFilteredGaze = true;
    GazeFilter gazeFilter = new GazeFilter();
    bool initializedEnable = true;
    public void setUseFilteredGaze(bool b)
    {
        useFilteredGaze = b;
    }

    public GameObject GazePoint;
    public bool isBlink;

    TrackingState trackingState;
    EyeMovementState eyeMovementState;
    ScreenState screenState;

    //User Status
    string userStatusAttention;
    public bool userStatusBlink;
    string userStatusDrowsiness;

    //DB
    public int count_face;
    public int count_eyes;

    private int previous_face;
    private int current_face;
    private int previous_eyes;
    private int current_eyes;

    // tracking data
    int fps;
    double[] calibrationData;

#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern bool hasIOSCameraPermission();
    [DllImport("__Internal")]
    private static extern void requestIOSCameraPermission();
#endif

    bool isNewGaze;
    float gazeX;
    float gazeY;

    float systemWidth;
    float systemHeight;

    ScreenOrientation orientation;

    void Start()
    {
        Debug.Log("SeeSo Core Version : " + GazeTracker.getVersionName());
        orientation = Screen.orientation;

        systemWidth = Mathf.Min(Display.main.systemWidth, Display.main.systemHeight);
        systemHeight = Mathf.Max(Display.main.systemWidth, Display.main.systemHeight);

        if (!HasCameraPermission())
        {
            RequestCameraPermission();
        }
        Initialized();

        count_face = 0;
        count_eyes = 0;

    }
    bool HasCameraPermission()
    {
#if UNITY_ANDROID
        return Permission.HasUserAuthorizedPermission(Permission.Camera);
#elif UNITY_IOS
        return hasIOSCameraPermission();
#endif
    }

    void RequestCameraPermission()
    {
#if UNITY_ANDROID
        Permission.RequestUserPermission(Permission.Camera);
#elif UNITY_IOS
        requestIOSCameraPermission();
#endif
    }

    void Update()
    {
        ScreenOrientation curOrientation = Screen.orientation;
        orientation = curOrientation;

        if (isTracking)
        {
            if (isNewGaze)
            {
                Vector2 overlayCanvasSizeDelta = OverlayCanvas.GetComponent<RectTransform>().sizeDelta;
                GazePoint.GetComponent<RectTransform>().anchoredPosition = new Vector2(gazeX * overlayCanvasSizeDelta.x, gazeY * overlayCanvasSizeDelta.y);
                isNewGaze = false;
            }
            GazePoint.SetActive(true);
        }
        else
        {
            GazePoint.SetActive(false);
        }
     
    }

    public void Initialized()
    {
        if (isInitialized) return;


        Option = new UserStatusOption();
        Option.useBlink();

        if (UsingUserStatus())
        {
            GazeTracker.initGazeTracker(LisenseKey, onInitialized, Option);
        }
        else
        {
            GazeTracker.initGazeTracker(LisenseKey, onInitialized);
        }

    }

    public void deInitialized()
    {
        GazeTracker.deinitGazeTracker();
        isInitialized = false;
        isTracking = false;
        isCalibrating = false;
    }

    public void startTracking()
    {
        if (isTracking || !isInitialized) return;
        GazeTracker.setStatusCallback(onStarted, onStopped);
        GazeTracker.setGazeCallback(onGaze);

        if (UsingUserStatus())
        {
            GazeTracker.setUserStatusCallback(onAttention, onBlink, onDrowsiness);
        }

        if (GazeManager.instance != null)
        {
            // 설정한 FPS 가져오기
            fps = GazeManager.instance.getTrackingFPS();
            GazeTracker.setTrackingFPS(fps);

            // 설정한 calibration 가져오기
            calibrationData = GazeManager.instance.getCalibrationData();
            GazeTracker.setCalibrationData(calibrationData);
        }

        GazeTracker.startTracking();
        Debug.Log("isStart");
    }

    public void stopTracking()
    {
        if (!isInitialized || !isTracking) return;
        GazeTracker.stopTracking();
        isTracking = false;
    }

    public void onInitialized(InitializationErrorType error)
    {
        Debug.Log("onInitialized result : " + error);
        if (error == InitializationErrorType.ERROR_NONE)
        {
            isInitialized = true;
        }
        else
        {
            isInitialized = false;
        }
        initializedEnable = true;
    }

    void onGaze(GazeInfo gazeInfo)
    {
        Debug.Log("onGaze " + gazeInfo.timestamp + "," + gazeInfo.x + "," + gazeInfo.y + "," + gazeInfo.trackingState + "," + gazeInfo.screenState);
        //Debug.Log(IsBlinkLeft.ToString() + IsBlinkRight.ToString() + IsBlink.ToString());

        isNewGaze = true;


        // �� ���� SUCCESS = 0 / FACE_MISSING = 3
        current_face = (int)gazeInfo.trackingState;

        if (previous_face == 3 && current_face == 0)
        {
            count_face++;
            //Debug.Log("trackingState:" + gazeInfo.trackingState + " count_face:" + count_face);
        }

        // ���� �������� trackingState�� ���� �������� ������ ������Ʈ
        previous_face = current_face;

        // �ü� ���� INSIDE_OF_SCREEN = 0 / OUTSIDE_OF_SCREEN = 1
        current_eyes = (int)gazeInfo.screenState;

        if (previous_eyes == 1 && current_eyes == 0)
        {
            count_eyes++;
            //Debug.Log("screenState:" + gazeInfo.screenState + " count_eyes:" + count_eyes);
        }

        previous_eyes = current_eyes;

        if (!useFilteredGaze)
        {
            gazeX = _convertCoordinateX(gazeInfo.x);
            gazeY = _convertCoordinateY(gazeInfo.y);

        }
        else
        {
            gazeFilter.filterValues(gazeInfo.timestamp, _convertCoordinateX(gazeInfo.x), _convertCoordinateY(gazeInfo.y));
            gazeX = gazeFilter.getFilteredX();
            gazeY = gazeFilter.getFilteredY();
        }
        gazeInfo = null;
    }
    float _convertCoordinateX(float x)
    {
        float screenWidth = systemWidth;

        if (orientation == ScreenOrientation.LandscapeLeft || orientation == ScreenOrientation.LandscapeLeft || orientation == ScreenOrientation.LandscapeRight)
        {
            screenWidth = systemHeight;
        }

        return gazeX = x / screenWidth - 0.5f;
    }

    float _convertCoordinateY(float y)
    {
        float screenHeight = systemHeight;

        if (orientation == ScreenOrientation.LandscapeLeft || orientation == ScreenOrientation.LandscapeLeft || orientation == ScreenOrientation.LandscapeRight)
        {
            screenHeight = systemWidth;
        }
        return gazeY = 0.5f - y / screenHeight;
    }

    bool UsingUserStatus()
    {
        return Option.isUseBlink();
    }
    public void onBlink(long timestamp, bool isBlinkLeft, bool isBlinkRight, bool isBlink, float leftOpenness, float rightOpenness)
    {
        userStatusBlink = isBlink;
        //Debug.Log("onBlink " + isBlinkLeft + ", " + isBlinkRight + ", " + isBlink);
    }
    void onDrowsiness(long timestamp, bool isDrowsiness, double intensity)
    {
        userStatusDrowsiness = "" + intensity.ToString("F1");
        Debug.Log("onDrowsiness " + intensity.ToString("F1"));
    }
    void onAttention(long timestampBegin, long timestamEnd, float score)
    {
        userStatusAttention = "" + score;
        Debug.Log("onAttention " + score);
    }

    void onStarted()
    {
        isTracking = true;
    }
    void onStopped(StatusErrorType error)
    {
        isTracking = false;
    }
}
