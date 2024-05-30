using Assets.Scripts;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 100f;
    [SerializeField] private List<Transform> wayPoints;
    [SerializeField] private float wayPointReachedDistance = 0.25f;
    [SerializeField] private Rigidbody2D rb;
    private Animator animator;

    private DetectionZone detectionObj;
    private int wayNumber;
    private Transform nextWayPoint;
    public bool _isMoving;
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }
    public bool _hasTarget;
    public bool HasTarget { get { 
            return _hasTarget;
        } private set {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        } }
    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);

        }
    }
    public float AttackCoolDown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.AttackCoolDown);
        }
        private set
        {
            animator.SetFloat(AnimationStrings.AttackCoolDown, Mathf.Max(value, 0));
        }
    }
    private void Start()
    {
        nextWayPoint = wayPoints[wayNumber];


    }
    // Start is called before the first frame update
    void Awake()
    {
        detectionObj = GetComponent<DetectionZone>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        HasTarget = detectionObj.detectedCollider.Count > 0;
        if (AttackCoolDown > 0)
        {
            AttackCoolDown -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {

        if (IsAlive)
        {
            animator.SetFloat(AnimationStrings.MoveX, Mathf.Max(rb.velocity.x, -1));
            animator.SetFloat(AnimationStrings.MoveY, Mathf.Max(rb.velocity.y, -1));
            Move();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
            
        



    }

    public void Move()
    {
        if (detectionObj.detectedCollider.Count > 0)
        {
            Vector2 direction = (detectionObj.detectedCollider[0].transform.position - transform.position).normalized;
            rb.velocity =  (direction * moveSpeed * Time.fixedDeltaTime);
            
        }
        else
        {
            Vector2 directionToNextPoint = (nextWayPoint.position - transform.position).normalized;
            float distance = Vector2.Distance(nextWayPoint.position, transform.position);
            rb.velocity = directionToNextPoint * moveSpeed * Time.fixedDeltaTime;
            IsMoving = rb.velocity != Vector2.zero;
            UpdateDirection();
            if (distance <= wayPointReachedDistance)
            {
                wayNumber++;
                if (wayNumber >= wayPoints.Count)
                {
                    wayNumber = 0;
                }
                nextWayPoint = wayPoints[wayNumber];
            }
        }
    }

    private void UpdateDirection()
    {
        Vector3 currenLocScale = transform.localScale;
        if(transform.localScale.x > 0)
        {
            //facing right
            if(rb.velocity.x < 0)
            {
                animator.SetFloat(AnimationStrings.MoveX, Mathf.Max(rb.velocity.x, -1));
            }

        }
        else
        {
            if(rb.velocity.x > 0)
            {
                //facing left
                animator.SetFloat(AnimationStrings.MoveX, Mathf.Min(rb.velocity.x, 1));

            }
        }
        if (transform.localScale.y > 0)
        {
            //facing up
            if(rb.velocity.y < 0)
            {
                animator.SetFloat(AnimationStrings.MoveY, Mathf.Max(rb.velocity.y, -1));

            }
        }
        else
        {
            if(rb.velocity.y < 0)
            {
                animator.SetFloat(AnimationStrings.MoveY, Mathf.Min(rb.velocity.y, 1));

            }
        }
    }
    public void OnHit(int damage, Vector2 knockBcak)
    {
        rb.AddForce(knockBcak);
    }
}
