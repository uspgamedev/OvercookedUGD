using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("ItemSlot")]
    [SerializeField] Image Slot;
    [SerializeField] Image Item;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void ChangeImage(Sprite sprite)
    {
        Item.sprite = sprite;
        Item.gameObject.SetActive(true);
    }
}
