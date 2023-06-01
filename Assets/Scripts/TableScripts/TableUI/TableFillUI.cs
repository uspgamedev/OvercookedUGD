using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableFillUI : MonoBehaviour
{
    [SerializeField] private Image _fillImage;

    private void Awake()
    {
        SetImageActive(false);
    }

    public void SetImageActive(bool value)
    {
        _fillImage.gameObject.SetActive(value);
    }

    public void SetRelativeFill(float relativeFillAmount, float maximumAmount)
    {
        _fillImage.fillAmount = relativeFillAmount / maximumAmount;
    }
    public void SetFill(float fillAmount)
    {
        _fillImage.fillAmount = fillAmount;
    }
}
