using Assets.Scripts;
using Assets.Scripts.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<float, float> playerHealthChanged;
    public UnityEvent death;
    private Animator animator;
    [SerializeField] private int _health = 100;
    [SerializeField] private int _maxHealth = 100;
    //[SerializeField] private Vector2 knockBack = Vector2.zero;
    [SerializeField] private float invicintableTime = 0.25f;
    private float timeSinceHit = 0;
    public int Health { get { 
            return _health;
        }  set { 
            _health = value;
            playerHealthChanged?.Invoke(Health, _maxHealth);

            if (_health <= 0)
            {
                IsAlive = false;
            }
        } }
    [SerializeField] private bool _isAlive = true;
    public bool IsAlive { get { 
         return _isAlive;
        } private set{
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
        } }

    public int MaxHealth { get {
            return _maxHealth;
        } private set {
            _maxHealth = value;
        } }

    private bool IsInvicintable = false;


    private void Awake()
    {
        animator = GetComponent<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeSinceHit > invicintableTime)
        {
            IsInvicintable = false;
            timeSinceHit = 0;
        }
        timeSinceHit += Time.deltaTime;
    }
    public bool Hit(int damage,Vector2 knockBack)
    {
        if (IsAlive && !IsInvicintable)
        {
            Health -= damage;
            IsInvicintable = true;
            damageableHit?.Invoke(damage, knockBack);
            CharacterEvent.characterTookDamage(gameObject, damage);
            return true;
        }
        return false;
    }
    public void HealthRestore(int healRestore)
    {
        int maxHealth = Mathf.Max(MaxHealth - Health, 0);
        int currentHeal = Mathf.Min(maxHealth, healRestore);
        Health += currentHeal;
        CharacterEvent.characterHealed(gameObject, currentHeal);
    }
    public void OnDeath()
    {
        if (!IsAlive)
        {
            CharacterEvent.EnemyDeath();
        }
    }
}
