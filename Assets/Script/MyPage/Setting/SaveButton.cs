using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    public ToggleGroupController toggleGroupController;

    private void Start()
    {
        Toggle toggle = GetComponentInChildren<Toggle>(); // 자식 오브젝트에서 Toggle 컴포넌트를 찾기
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(SaveToggles);
        }
        else
        {
            Debug.LogWarning("SaveButton: Toggle component not found in children.");
        }
    }

    private void SaveToggles(bool value)
    {
        toggleGroupController.SaveToggles();
    }
}
