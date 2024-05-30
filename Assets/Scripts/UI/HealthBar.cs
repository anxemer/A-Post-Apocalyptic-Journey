using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healBar;
    [SerializeField] private TMP_Text healthBarText;
    private Damageable playerDamageable;
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.Log("No player in scene");

        }
        playerDamageable = player.GetComponent<Damageable>();
    }
    private void OnEnable()
    {
        playerDamageable.playerHealthChanged.AddListener(onPlayerHealthChanged);
    }
    private void OnDisable()
    {
        playerDamageable.playerHealthChanged.RemoveListener(onPlayerHealthChanged);

    }
    // Start is called before the first frame update
    void Start()
    {
        healBar.value = caculateSliderPercontage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text = "HP " + playerDamageable.Health + "/" + playerDamageable.MaxHealth;
    }

    private float caculateSliderPercontage(float health, float maxHealth)
    {
        return health / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onPlayerHealthChanged(float currenHeal,float maxHeal)
    {
        healBar.value = caculateSliderPercontage(currenHeal,maxHeal);
        healthBarText.text =  "HP " + currenHeal + "/" + maxHeal;

    }
}
