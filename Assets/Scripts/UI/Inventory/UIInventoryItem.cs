using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour, IPointerClickHandler,
        IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Image borderImage;
    [SerializeField] private TMP_Text quantityText;
    public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;
    private bool empty = true;

    private void Awake()
    {
        ResetData();
        Desert();
    }

    public void ResetData()
    {
        borderImage.enabled = false;

        this.itemImage.gameObject.SetActive(false);
        empty = true;
    }

    public void Desert()
    {
        borderImage.enabled = false;
    }
    public void SetData(Sprite sprite, int quantity)
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = sprite;
        quantityText.text = quantity + "";
        empty = false;
    }
    public void Select()
    {
        borderImage.enabled = true;
    }
    public void Deselect()
    {
        borderImage.enabled = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (empty)
            return;
        OnItemBeginDrag?.Invoke(this);
    }
}
