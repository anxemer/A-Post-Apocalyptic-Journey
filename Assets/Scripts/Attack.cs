using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float knockBackForce = 100f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            Vector3 positionParent = gameObject.GetComponentInParent<Transform>().position;
            Vector2 direction = (positionParent - collision.gameObject.transform.position).normalized;
            Vector2 knockBack = direction * knockBackForce;
            damageable.Hit(damage, knockBack);

        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Damageable damageable = GetComponent<Damageable>();
    //    if (damageable != null)
    //    {

    //        Vector2 directionKnocbackX = transform.parent.localScale.x > 0 ? knockBack : new Vector2(-knockBack.x, knockBack.y);
    //        Vector2 directionKnockbackY = transform.parent.localScale.y > 0 ? knockBack : new Vector2(knockBack.x, -knockBack.y);
    //        damageable.Hit(damage, directionKnocbackX);
    //    }
    //}
}
