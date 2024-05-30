using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    [SerializeField] private Vector3 healthMoveSpeed = new Vector3(0, 75, 0);
    private TextMeshProUGUI m_TextMeshPro;
    private RectTransform textTransform;
    private float fadeExitTime = 0.5f;
    private float timeElapsed = 0f;
    private Color startColor;
    // Start is called before the first frame update
    void Start()
    {
        textTransform = GetComponent<RectTransform>();
        m_TextMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = m_TextMeshPro.color;
    }

    // Update is called once per frame
    void Update()
    {
        textTransform.position += healthMoveSpeed * Time.deltaTime;
        timeElapsed += Time.deltaTime;
        if(timeElapsed < fadeExitTime)
        {
            float newAlpha = startColor.a *(1 - timeElapsed / fadeExitTime);
            m_TextMeshPro.color = new Color(startColor.r, startColor.g, startColor.b,newAlpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
