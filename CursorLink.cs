using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CursorLink : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    public TextMeshProUGUI linkText;
    public Color32 normalColor = new Color32(59, 77, 138, 255);
    public Color32 hoverColor = new Color32(0, 224, 224, 255);

    void Start()
    {
        linkText.color = normalColor;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        linkText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        linkText.color = normalColor;    
    }
}
