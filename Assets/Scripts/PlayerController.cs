using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float aimSpeed = 10f;
    private Vector2 moveInput;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isAttack;
    private Animator animator;
    public int currentWeaponNo = 0;
    //private bool isFacingRignt = true;

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
    
    public float AttackCoolDown { get {
            return animator.GetFloat(AnimationStrings.AttackCoolDown);
        } private set { 
            animator.SetFloat(AnimationStrings.AttackCoolDown, Mathf.Max(value,0));
        } }

    

    public bool IsAlive { get { 
            return animator.GetBool(AnimationStrings.isAlive);
            
        }}

    public float CurrentSpeed { get {
            if (IsMoving)
            {
                if (isAttack)
                {
                    return aimSpeed;
                }
                else
                {
                    return moveSpeed;
                }
            }
            else
            {
                return 0;
            }
        }  }

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

        if(AttackCoolDown > 0)
        {
            AttackCoolDown -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeWeapon();
        }

    }
    private void FixedUpdate()
    {
        if (IsAlive)
        {
            animator.SetFloat(AnimationStrings.MoveX, moveInput.x);
            animator.SetFloat(AnimationStrings.MoveY, moveInput.y);
            if (moveInput.x != 0) moveInput.y = 0;

            rb.velocity = new Vector2(CurrentSpeed * moveInput.x * Time.fixedDeltaTime, moveInput.y * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        


    }
    public void ChangeWeapon()
    {
        if(currentWeaponNo == 0)
        {
            currentWeaponNo += 1;
            animator.SetLayerWeight(currentWeaponNo - 1, 0);
            animator.SetLayerWeight(currentWeaponNo, 1);
        }
        else
        {
            currentWeaponNo -= 1;
            animator.SetLayerWeight(currentWeaponNo + 1, 0);
            animator.SetLayerWeight(currentWeaponNo, 1);
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        //if (context.started)
        //{
        //    IsMoving = true;
        //}
        //if(context.canceled)
        //{
        //    IsMoving = false;
        //}
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
            animator.SetTrigger(AnimationStrings.IsAttack);
    }
    public void OnCrossAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(currentWeaponNo == 0)
            {
                animator.SetTrigger(AnimationStrings.CrossbowAttack);
                isAttack = true;

            }
            else if(currentWeaponNo == 1)
            {
                animator.SetTrigger(AnimationStrings.GunAttack);
                isAttack = true;

            }

        }
        else if (context.canceled)
        {
            isAttack = false;

        }
    }
    //public void FlipFacing()
    //{
    //    if(moveInput.x < 0 && isFacingRignt)
    //    {
    //        transform.localScale = new Vector3(-1, 1, 1);
    //        isFacingRignt= false;
    //    }else if(moveInput.x > 0 && !isFacingRignt)
    //    {
    //        transform.localScale = new Vector3(1, 1, 1);
    //        isFacingRignt = true;
    //    }
    //}
    public void OnHit(int damage,Vector2 knockBcak)
    {
        rb.velocity = new Vector2(knockBcak.x , knockBcak.y + rb.velocity.y);
    }
}
