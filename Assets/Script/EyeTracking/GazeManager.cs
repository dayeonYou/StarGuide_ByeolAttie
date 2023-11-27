using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GazeManager : MonoBehaviour
{
    public static GazeManager instance;
    public double[] calibrationData;
    public int trackingFPS;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void Update()
    {
        Debug.Log("GazeManager alive!");
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        Debug.Log("GazeManager instance: " + (instance != null ? "Exists" : "Does not exist"));
        Debug.Log("Saved trackingFPS: " + trackingFPS);
    }
    public bool setCalibrationData(double[] data)
    {
        Debug.Log("setCalibrationData called");
        calibrationData = data;
        return GazeTracker.setCalibrationData(data);
    }

    public bool setTrackingFPS(int fps)
    {
        Debug.Log("setTrackingFPS called");
        trackingFPS = fps;
        return GazeTracker.setTrackingFPS(fps);
    }
    public int getTrackingFPS()
    {
        return trackingFPS;
    }
    public double[] getCalibrationData()
    {
        return calibrationData;
    }
}