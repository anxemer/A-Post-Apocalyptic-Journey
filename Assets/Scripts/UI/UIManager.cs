using Assets.Scripts.Event;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject damageableText;
    [SerializeField] private GameObject healthedText;
    [SerializeField] private Canvas canvas;
    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }
    private void OnEnable()
    {
        CharacterEvent.characterTookDamage += characterTookDamage;
        CharacterEvent.characterHealed += characterHealthed;
    }
    private void OnDisable()
    {
        CharacterEvent.characterTookDamage -= characterTookDamage;
        CharacterEvent.characterHealed -= characterHealthed;
    }
    public void characterTookDamage(GameObject character,int damegeRecived)
    {
        Vector2 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tMP_Text = Instantiate(damageableText, spawnPosition, Quaternion.identity,canvas.transform).GetComponent<TMP_Text>();
        tMP_Text.text = damegeRecived.ToString();
    }
    public void characterHealthed(GameObject character,int healthRestored)
    {
        Vector2 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tMP_Text = Instantiate(healthedText, spawnPosition, Quaternion.identity, canvas.transform).GetComponent<TMP_Text>();
        tMP_Text.text = healthRestored.ToString();
    }

}
