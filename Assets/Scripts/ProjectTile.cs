using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private Vector2 knockBack = new Vector2 (0, 0);
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float timeRemove = 3f;
    private Rigidbody2D rb;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        animator.SetFloat(AnimationStrings.MoveX, Mathf.Max(rb.velocity.x, -1));
        animator.SetFloat(AnimationStrings.MoveY, Mathf.Max(rb.velocity.y, -1));
        Move();
        Destroy(gameObject, timeRemove);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        bool gotHit = damageable.Hit(damage, knockBack);
        
            Destroy(gameObject);

        
    }
    private void Move()
    {
        rb.velocity = transform.up * moveSpeed;
        UpdateDirection();

    }
    private void UpdateDirection()
    {
        Vector3 currenLocScale = transform.localScale;
        if (transform.localScale.x > 0)
        {
            //facing right
            if (rb.velocity.x < 0)
            {
                animator.SetFloat(AnimationStrings.MoveX, Mathf.Max(rb.velocity.x, 1));
            }

        }
        else
        {
            if (rb.velocity.x > 0)
            {
                //facing left
                animator.SetFloat(AnimationStrings.MoveX, Mathf.Min(rb.velocity.x, -1));

            }
        }
        if (transform.localScale.y > 0)
        {
            //facing up
            if (rb.velocity.y < 0)
            {
                animator.SetFloat(AnimationStrings.MoveY, Mathf.Max(rb.velocity.y, 1));

            }
        }
        else
        {
            if (rb.velocity.y < 0)
            {
                animator.SetFloat(AnimationStrings.MoveY, Mathf.Min(rb.velocity.y, -1));

            }
        }
    }
}
