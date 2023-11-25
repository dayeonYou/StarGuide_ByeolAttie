using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Find_Click : MonoBehaviour, IPointerClickHandler
{
    ObjectSelector ObjectSelector;
    private bool canClick = true;

    private void Start()
    {
        ObjectSelector = FindObjectOfType<ObjectSelector>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (canClick && ObjectSelector.isSelecting)
        {
            ObjectSelector.ClickCup();
            // Ŭ�� �� ���� �ð� ���� Ŭ�� �Ұ����ϰ� ����
            StartCoroutine(DisableClickForSeconds(3f));
        }

    }
    private IEnumerator DisableClickForSeconds(float seconds)
    {
        canClick = false;

        // ���� �ð� ���� ���
        yield return new WaitForSeconds(seconds);

        canClick = true;
    }
}