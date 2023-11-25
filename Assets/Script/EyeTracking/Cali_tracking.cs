using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class Cali_tracking : MonoBehaviour
{
    public string LisenseKey = "prod_p2w9yepluo1hojftnsh85wvkpczxm6gq6snsjhdc";

    public GameObject OverlayCanvas;

    bool isInitialized;
    public bool isTracking;
    bool isCalibrating;
    UserStatusOption Option;
    
    public GameObject StartTracking;
    public GameObject StopTracking;
    public GameObject StartCalibration;
    public GameObject StopCalibration;
    public GameObject Btn_exit;

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

    //User Status
    string userStatusAttention;
    public bool userStatusBlink;
    string userStatusDrowsiness;

    // For Calibration
    public GameObject CalibrationPoint;
    bool isNextStepReady;
    bool isCalibrationFinished;
    float calibrationProgress;
    float calibrationX;
    float calibrationY;

#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern bool hasIOSCameraPermission();
    [DllImport("__Internal")]
    private static extern void requestIOSCameraPermission();
#endif

    // Tracking FPS
    public Slider silder;
    int targetFPS;

    public void setCameraFpsRateFromSlider(float fps)
    {
        targetFPS = (int) fps;
        Debug.Log(targetFPS);
        GazeManager.instance.setTrackingFPS(targetFPS);
    }
    /*
    public void lazySetCameraFpsRateFromSlider()
    {
        GazeTracker.setTrackingFPS(targetFPS);
    }
    */
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

        // 슬라이더의 값이 변경될 때마다 호출되는 이벤트 리스너 추가
        silder.onValueChanged.AddListener(setCameraFpsRateFromSlider);
        // 초기 값을 슬라이더의 값으로 설정
        targetFPS = (int) silder.value;

        Initialized();

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

            if (isCalibrating)
            {
                GazePoint.SetActive(false);
                Btn_exit.SetActive(false);
            }
            else
            {
                GazePoint.SetActive(true);
                Btn_exit.SetActive(true);
            }
        }
        else
        {
            GazePoint.SetActive(false);
        }

        // Calibrate Progress
        if (isCalibrating)
        {
            CalibrationPoint.SetActive(true);

            if (isNextStepReady)
            {
                Vector2 overlayCanvasSizeDelta = OverlayCanvas.GetComponent<RectTransform>().sizeDelta;
                CalibrationPoint.GetComponent<RectTransform>().anchoredPosition = new Vector2(calibrationX * overlayCanvasSizeDelta.x, calibrationY * overlayCanvasSizeDelta.y);
                isNextStepReady = false;
                GazeTracker.startCollectSamples();
            }

            if (isCalibrationFinished)
            {
                isCalibrationFinished = false;
                isNextStepReady = false;
                isCalibrating = false;
            }

            if (CalibrationPoint.activeSelf)
            {
                CalibrationPoint.GetComponent<RawImage>().color = new Color(calibrationProgress, 0f, 0f);
            }

        }
        else
        {
            CalibrationPoint.SetActive(false);
        }

        // Button Visibility
        if (isCalibrating)
        {
            StartCalibration.SetActive(false);
            StopCalibration.SetActive(true);
        }
        else
        {
            StartCalibration.SetActive(true);
            StopCalibration.SetActive(false);
        }
   
        if (isTracking)
        {
            StartTracking.SetActive(false);
            StopTracking.SetActive(true);
        }
        else
        {
            StartTracking.SetActive(true);
            StopTracking.SetActive(false);
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

    public void deinitialize()
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

        // 설정한 FPS 가져오기
        int fps = GazeManager.instance.getTrackingFPS();
        GazeTracker.setTrackingFPS(fps);

        // 설정한 calibration 가져오기
        double[] calibrationData = GazeManager.instance.getCalibrationData();
        GazeTracker.setCalibrationData(calibrationData);

        if (UsingUserStatus())
        {
            GazeTracker.setUserStatusCallback(onAttention, onBlink, onDrowsiness);
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
    public void startCalibration()
    {
        if (!isTracking) return;
        if (isCalibrating) return;


        GazeTracker.setCalibrationCallback(onCalibrationNextPoint, onCalibrationProgress, onCalibrationFinished);
        bool success = GazeTracker.startCalibration();

        if (!success)
        {
            Debug.Log("startCalibration() fail, please check camera permission OR call startTracking() First");
        }
        else
        {
            isCalibrating = true;
        }
    }

    public void stopCalibration()
    {
        if (!isInitialized || !isCalibrating) return;

        GazeTracker.stopCalibration();

        isCalibrating = false;
        isNextStepReady = false;
        isCalibrationFinished = false;
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
        //Debug.Log("onGaze " + gazeInfo.timestamp + "," + gazeInfo.x + "," + gazeInfo.y + "," + gazeInfo.trackingState + "," + gazeInfo.screenState);
        //Debug.Log(IsBlinkLeft.ToString() + IsBlinkRight.ToString() + IsBlink.ToString());
        isNewGaze = true;
        trackingState = gazeInfo.trackingState;

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
    //보정값 저장 
    public void setCalibrationData(double[] calibrationData)
    {
        bool result = GazeManager.instance.setCalibrationData(calibrationData);
        //Debug.Log("setCalibrationData : " + result);
    }
    void onCalibrationNextPoint(float x, float y)
    {
        //Debug.Log("onCalibrationNextPoint" + x + "," + y);

        calibrationX = _convertCoordinateX(x);
        calibrationY = _convertCoordinateY(y);
        isNextStepReady = true;
    }

    void onCalibrationProgress(float progress)
    {
        //Debug.Log("onCalibrationProgress" + progress);
        calibrationProgress = progress;
    }

    void onCalibrationFinished(double[] calibrationData)
    {
        //Debug.Log("OnCalibrationFinished" + calibrationData.Length);
        setCalibrationData(calibrationData);
        isCalibrationFinished = true;
    }
    public void setTrackingFps(int fps)
    {
        GazeManager.instance.setTrackingFPS(fps);
    }
}
