using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInveotoryDescription : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemTile;
    [SerializeField] private TMP_Text itemDescription;

    private void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        this.itemImage.gameObject.SetActive(false);
        this.itemTile.text = string.Empty;
        this.itemDescription.text = string.Empty;
    }
    public void SetDescription(Sprite sprite,string textTile,string textDescription)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemTile.text = textTile;
        this.itemDescription.text = textDescription;
        this.itemImage.sprite = sprite;
    }
}
