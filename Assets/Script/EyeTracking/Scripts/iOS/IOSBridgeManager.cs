using AOT;
using System;
using UnityEngine;
using System.Runtime.InteropServices;

public class IOSBridgeManager : SeeSoBridgeManager
{
    private static IOSBridgeManager manager;

    //callback
    private InitializationDelegate.onInitialized onInitialized;

    
#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void iOSInitGazeTracker(string licence, IOSDelegate.initialized_gaze_tracker callback, bool isUseAttention, bool isUseBlink, bool isUseDrowsiness);

    [DllImport("__Internal")]
    private static extern void iOSDeinitGazeTracker();

    [DllImport("__Internal")]
    private static extern void iOSStartTracking();

    [DllImport("__Internal")]
    private static extern void iOSStopTracking();

    [DllImport("__Internal")]
    private static extern bool iOSIsTracking();

    [DllImport("__Internal")]
    private static extern bool iOSSetTrackingFPS(int fps);

    [DllImport("__Internal")]
    private static extern void iOSSetCameraPreview(float left, float top, float right, float bottom);

    [DllImport("__Internal")]
    private static extern void iOSSetPreviewAlpha(float alpha);

    [DllImport("__Internal")]
    private static extern float iOSGetPreviewAlpha();


    [DllImport("__Internal")]
    private static extern void iOSRemoveCameraPreview();

    [DllImport("__Internal")]
    private static extern void iOSSetAttentionInterval(int interval);

    [DllImport("__Internal")]
    private static extern float iOSGetAttentionScore();

    [DllImport("__Internal")]
    private static extern void iOSSetAttentionRegion(float left, float top, float right, float bottom);

    [DllImport("__Internal")]
    private static extern IntPtr iOSGetAttentionRegion();

    [DllImport("__Internal")]
    private static extern void iOSRemoveAttentionRegion();

    [DllImport("__Internal")]
    private static extern void iOSSetForcedOrientation(int orientation);

    [DllImport("__Internal")]
    private static extern void iOSResetForcedOrientation();

    [DllImport("__Internal")]
    private static extern bool iOSStartCalibration(int mode, int criteria, float left, float top, float right, float bottom);

    [DllImport("__Internal")]
    private static extern void iOSStopCalibration();

    [DllImport("__Internal")]
    private static extern bool iOSStartCollectSamples();

    [DllImport("__Internal")]
    private static extern bool iOSSetCalibrationData(double[] calibrationData, int length);

    [DllImport("__Internal")]
    private static extern void iOSSetDelegates(IOSDelegate.status_on_start startCallback, IOSDelegate.status_on_stop stopCallback, IOSDelegate.gaze_info gazeCallbck, IOSDelegate.face_info faceCallback, IOSDelegate.calibration_on_next nextCallback, IOSDelegate.calibration_on_progress progressCallbck, IOSDelegate.calibration_on_finished finishedCallback, IOSDelegate.user_status_on_attention attentionCallback, IOSDelegate.user_status_on_blink blinkCallback, IOSDelegate.user_status_on_drowsiness drowsinessCallback);

    [DllImport("__Internal")]
    private static extern string iOSGetVersionName();

    private IOSBridgeManager(){}

    private static float FLOAT_NULL = float.NaN;

    public static new IOSBridgeManager SharedInstance()
    {
        if(manager == null)
        {
            manager = new IOSBridgeManager();
        }
        return manager;
    }

    public static new void InitGazeTracker(string license, InitializationDelegate.onInitialized onInitialized)
    {
        InitGazeTracker(license, onInitialized, new UserStatusOption());
    }

    public static new void InitGazeTracker(string license, InitializationDelegate.onInitialized onInitialized, UserStatusOption option)
    {
        SharedInstance().onInitialized = onInitialized;
        iOSInitGazeTracker(license, OnGazeTrackerInitailzed, option.isUseAttention(), option.isUseBlink(), option.isUseDrowsiness());
    }

    public static new void DeinitGazeTracker()
    {
        iOSDeinitGazeTracker();
    }


    public override void StartTracking()
    {
        iOSStartTracking();
    }

    public override void StopTracking()
    {
        iOSStopTracking();
    }

    public override bool IsTracking()
    {
        
        return iOSIsTracking();
    }

    public override bool SetTrackingFPS(int fps)
    {
        return iOSSetTrackingFPS(fps);
    }

    public override void SetCameraPreview(float left, float top, float right, float bottom)
    {
        iOSSetCameraPreview(left, top, right, bottom);
    }

    public override void RemoveCameraPreview()
    {
        iOSRemoveCameraPreview();
    }

    public override void SetCameraPreviewAlpha(float alpha)
    {
        iOSSetPreviewAlpha(alpha);
    }

    public override void SetAttentionInterval(int interval)
    {
        iOSSetAttentionInterval(interval);
    }

    public override float GetAttentionScore()
    {
        return iOSGetAttentionScore();
    }

    public override void SetAttentionRegion(float left, float top, float right, float bottom)
    {
        iOSSetAttentionRegion(left, top, right, bottom);
    }

    public override float[] GetAttentionRegion()
    { 
        IntPtr intptr = iOSGetAttentionRegion();
        if(intptr != null)
        {
            int length = 4;
            byte[] managedData = new byte[length * 8];

            Marshal.Copy(intptr, managedData, 0, length * 8);
            float[] array = new float[length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = Convert.ToSingle(BitConverter.ToDouble(managedData, i * 8));
                
                if (float.IsInfinity(array[i]))
                {
                    return null;
                }
            }

            return array;
        }

        return null;
    }

    public override void RemoveAttentionRegion()
    {
        iOSRemoveAttentionRegion();
    }

    public override void SetForcedOrientation(UIInterfaceOrientation orientation)
    {
        iOSSetForcedOrientation((int)orientation);
    }

    public override void ResetForcedOrientation()
    {
        iOSResetForcedOrientation();
    }

    public override bool StartCalibration(CalibrationModeType mode, AccuracyCriteria criteria, float left, float top, float right, float bottom)
    {
        return iOSStartCalibration((int)mode, (int)criteria, left, top, right, bottom);
    }

    public override bool StartCalibration(CalibrationModeType mode, AccuracyCriteria criteria)
    {
        return iOSStartCalibration((int)mode, (int)criteria, FLOAT_NULL, FLOAT_NULL, FLOAT_NULL, FLOAT_NULL);
    }

    public override bool StartCalibration(float left, float top, float right, float bottom)
    {
        return iOSStartCalibration(0, 0, left, top, right, bottom);
    }

    public override bool StartCalibration()
    {
        return iOSStartCalibration(0, 0, FLOAT_NULL, FLOAT_NULL, FLOAT_NULL, FLOAT_NULL);
    }

    public override void StopCalibration()
    {
        iOSStopCalibration();
    }

    public override bool StartCollectSamples()
    {
        return iOSStartCollectSamples();
    }

    public override bool SetCalibrationData(double[] calibrationData)
    {
        return iOSSetCalibrationData(calibrationData, calibrationData.Length);
    }

    public new static string GetVersionName()
    {
        return iOSGetVersionName();
    }

    [MonoPInvokeCallback(typeof(IOSDelegate.initialized_gaze_tracker))]
    private static void OnGazeTrackerInitailzed(int error)
    {
        if (error == 0)
        {
            iOSSetDelegates(
                OnStarted,
                OnStopped,
                OnGazeInfo,
                OnFaceInfo,
                OnCalibrationNextPoint,
                OnCalibartionProgress,
                OnCalibrationFinished,
                OnAttention,
                OnBlink,
                OnDrowsiness);
        }
        Debug.Log("error :" + error);
        SharedInstance().onInitialized((InitializationErrorType)error);     
    }

    [MonoPInvokeCallback(typeof(IOSDelegate.status_on_start))]
    private static void OnStarted()
    {
        IOSBridgeManager manager = SharedInstance();
        if(manager.onStarted != null)
        {
            manager.onStarted();
        }
        
    }

    [MonoPInvokeCallback(typeof(IOSDelegate.status_on_stop))]
    private static void OnStopped(int error)
    {
        IOSBridgeManager manager = SharedInstance();
        if (manager.onStopped != null)
        {
            manager.onStopped((StatusErrorType)error);
        }
    }

    [MonoPInvokeCallback(typeof(IOSDelegate.gaze_info))]
    private static void OnGazeInfo(long timeStamp, float x, float y, float fixationX, float fixationY, float leftOpenness, float rightOpenness, int trackingState, int eyeMovementState, int screenState)
    {
        IOSBridgeManager manager = SharedInstance();
        
        if(manager.onGaze != null)
        {
            GazeInfo info = new GazeInfo(timeStamp, x, y, fixationX, fixationY, leftOpenness, rightOpenness, (TrackingState)trackingState, (EyeMovementState)eyeMovementState, (ScreenState)screenState);
            manager.onGaze(info);
        }
       
    }

    [MonoPInvokeCallback(typeof(IOSDelegate.face_info))]
    private static void OnFaceInfo(long timestamp, float score, float frameWidth, float frameHeight, float left, float top, float width, float height, float pitch, float yaw, float roll, float centerX, float centerY, float centerZ)
    {
        IOSBridgeManager manager = SharedInstance();
        if (manager.onFace != null)
        {
            Rect rect = new Rect(left, top, width, height);
            FaceInfo info = new FaceInfo(timestamp, score, frameWidth, frameHeight, rect, pitch, yaw, roll, new Vector3(centerX, centerY, centerZ));

            manager.onFace(info);
        }

    }

    [MonoPInvokeCallback(typeof(IOSDelegate.calibration_on_next))]
    private static void OnCalibrationNextPoint(float x, float y)
    {
        IOSBridgeManager manager = SharedInstance();
        if(manager.onNextPoint != null)
        {
            manager.onNextPoint(x, y);
        }
        
    }

    [MonoPInvokeCallback(typeof(IOSDelegate.calibration_on_progress))]
    private static void OnCalibartionProgress(float progress)
    {
        IOSBridgeManager manager = SharedInstance();
        if(manager.onProgress != null)
        {
            manager.onProgress(progress);
        }
    }

    [MonoPInvokeCallback(typeof(IOSDelegate.calibration_on_finished))]
    private static void OnCalibrationFinished(IntPtr calibrationData,int length)
    {
        IOSBridgeManager manager = SharedInstance();

        if(manager.onFinished != null)
        {
            byte[] managedData = new byte[length * 8];

            Marshal.Copy(calibrationData, managedData, 0, length * 8);
            double[] array = new double[length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = BitConverter.ToDouble(managedData, i * 8);
            }
            Marshal.FreeHGlobal(calibrationData);
            manager.onFinished(array);
        }
        
       
    }

    [MonoPInvokeCallback(typeof(IOSDelegate.user_status_on_attention))]
    private static void OnAttention(long timestampBegin, long timestampEnd, float score)
    {
        IOSBridgeManager manager = SharedInstance();
        if (manager.onAttention != null)
        {
            manager.onAttention(timestampBegin, timestampEnd, score);
        }
    }
    [MonoPInvokeCallback(typeof(IOSDelegate.user_status_on_blink))]
    private static void OnBlink(long timestamp, bool isBlinkLeft, bool isBlinkRight, bool isBlink, float leftOpenness, float rightOpenness)
    {
        IOSBridgeManager manager = SharedInstance();
        if (manager.onBlink != null)
        {
            manager.onBlink(timestamp, isBlinkLeft, isBlinkRight, isBlink, leftOpenness, rightOpenness);
        }
    }

    [MonoPInvokeCallback(typeof(IOSDelegate.user_status_on_drowsiness))]
    private static void OnDrowsiness(long timestamp, bool isDrowsiness, double intensity)
    {
        IOSBridgeManager manager = SharedInstance();
        if (manager.onDrowsiness != null) {
            manager.onDrowsiness(timestamp, isDrowsiness, intensity);
        }
    }
#endif
}
