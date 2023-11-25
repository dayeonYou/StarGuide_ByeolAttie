using System;

public class IOSDelegate
{
    public delegate void initialized_gaze_tracker(int error);
    public delegate void status_on_start();
    public delegate void status_on_stop(int error);
    public delegate void gaze_info(long timestamp, float x, float y, float fixationX, float fixationY, float leftOpenness, float rightOpenness, int trackingState, int eyeMovementState, int screenState);
    public delegate void face_info(long timestamp, float score, float frameWidth, float frameHeight, float left, float top, float width, float height, float pitch, float yaw, float roll, float centerX, float centerY, float centerZ);
    public delegate void user_status_on_attention(long timestampBegin, long timestampEnd, float score);
    public delegate void user_status_on_blink(long timestamp, bool isBlinkLeft, bool isBlinkRight, bool isBlink, float leftOpenness, float rightOpenness);
    public delegate void user_status_on_drowsiness(long timestamp, bool isDrowsiness, double intensity);
    public delegate void calibration_on_next(float x, float y);
    public delegate void calibration_on_progress(float progress);
    public delegate void calibration_on_finished(IntPtr calibrationData, int length);
}
