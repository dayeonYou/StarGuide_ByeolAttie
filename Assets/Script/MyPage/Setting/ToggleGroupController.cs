using UnityEngine;
using UnityEngine.UI;

public class ToggleGroupController : MonoBehaviour
{
    public Toggle toggle1;
    public Toggle toggle2;
    private const string toggle1Key = "Toggle1";
    private const string toggle2Key = "Toggle2";

    private void Start()
    {
        LoadToggles(); // 저장된 설정 불러오기
    }

    public void SaveToggles()
    {
        PlayerPrefs.SetInt(toggle1Key, toggle1.isOn ? 1 : 0);
        PlayerPrefs.SetInt(toggle2Key, toggle2.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LoadToggles()
    {
        int toggle1Value = PlayerPrefs.GetInt(toggle1Key, 0);
        int toggle2Value = PlayerPrefs.GetInt(toggle2Key, 0);
        toggle1.isOn = toggle1Value == 1;
        toggle2.isOn = toggle2Value == 1;
    }
}
