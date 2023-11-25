using UnityEngine;
using UnityEngine.EventSystems;

public class Target_Click : MonoBehaviour, IPointerClickHandler
{

    private void Start()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Target_GazeInteraction.Touch) {
            Destroy(gameObject);
            TargetCount.count++;
        }
    }
}
