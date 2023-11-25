using UnityEngine;
using UnityEngine.UI;

public class SliderManagerE : MonoBehaviour
{
    public Slider slider;

    private float savedSliderValue;

    private void Start()
    {
        // 이전에 저장된 슬라이더 값이 있는지 확인합니다.
        if (PlayerPrefs.HasKey("SliderValueE"))
        {
            // 저장된 값이 있을 경우 해당 값을 로드합니다.
            savedSliderValue = PlayerPrefs.GetFloat("SliderValueE");
        }
        else
        {
            // 저장된 값이 없을 경우 기본값을 설정합니다.
            savedSliderValue = 0f;
        }

        // 슬라이더 값을 설정합니다.
        slider.value = savedSliderValue;
    }

    public void SaveSliderValue()
    {
        // 슬라이더 값을 저장합니다.
        savedSliderValue = slider.value;
        PlayerPrefs.SetFloat("SliderValueE", savedSliderValue);
    }
}
