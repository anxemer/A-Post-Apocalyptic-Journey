using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Canvas canva;
    [SerializeField] private UIInventoryItem item;

    private void Awake()
    {
        canva = transform.root.GetComponent<Canvas>();
        item = GetComponentInChildren<UIInventoryItem>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 positon;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canva.transform,
            Input.mousePosition,canva.worldCamera,
            out positon);
        transform.position = canva.transform.TransformPoint(positon);
    }
    public void SetData(Sprite sprite, int quantity)
    {
        item.SetData(sprite, quantity);
    }
    public void Toggle(bool toggle)
    {
        Debug.Log($"Item toggle{toggle}" );
        gameObject.SetActive(toggle);
    }
}
