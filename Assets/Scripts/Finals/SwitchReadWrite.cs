using UnityEngine;
using DG.Tweening;
public class SwitchReadWrite : MonoBehaviour
{
    [SerializeField] RectTransform readDataPanel;
    [SerializeField] RectTransform writeDataPanel;

    private float slideDuration = 0.5f;
    private float slideOffset = 1920f; 

    public void OnNextButtonClick()
    {
        readDataPanel.DOAnchorPosX(-slideOffset, slideDuration).SetEase(Ease.InOutQuad);

        writeDataPanel.anchoredPosition = new Vector2(slideOffset, 0); 
        writeDataPanel.DOAnchorPosX(0, slideDuration).SetEase(Ease.InOutQuad);

    }

    public void OnBackButtonClick()
    {
        readDataPanel.DOAnchorPosX(0f , slideDuration).SetEase(Ease.InOutQuad);

        writeDataPanel.anchoredPosition = new Vector2(0, 0); 
        writeDataPanel.DOAnchorPosX(1920, slideDuration).SetEase(Ease.InOutQuad);

    }
}
