using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0, 180f)] private float _rotationSmoothness;    // Коэффициент плавности поворота
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody rb;                       // Rigidbody
    private Vector3 movement = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float hor, ver;
    private Animator anim;
    bool isAttack = false;
    bool isJump = false;
    bool isLoss = false;

    private float timer = 1f;
    private float runStart = 0;
    private float myVelocity = 0;

    private int curArm = 0;
    private SelectArm selectArm;
    private PlaySounds playSounds;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        selectArm = GetComponent<SelectArm>();
        playSounds = GetComponent<PlaySounds>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (selectArm != null) selectArm.SetArmDamage(new int[] { 5, 10, 20 });
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0f) timer -= Time.deltaTime;
        else
        {
            timer = 0.25f;
            if (isLoss)
            {
                anim.SetBool("IsDead", true);
                return;
            }
            
            if (isAttack)
            {
                anim.SetBool("IsWalk", false);                
                Attack();
            }
            else if (isJump)
            {
                OnJump();
            }
            else
            {
                if (myVelocity > 0.2f)
                {
                    if (!isAttack)
                    {
                        anim.SetBool("IsWalk", true);
                        //print($"myVelocity = {myVelocity}");
                        if (myVelocity > 20f)
                        {
                            //anim.SetBool("IsWalk", false);
                            anim.SetBool("IsRun", true);
                        }
                        else
                        {
                            anim.SetBool("IsRun", false);
                        }
                    }
                }
                else
                {
                    anim.SetBool("IsWalk", false);
                }
            }

            myVelocity = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameObject.GetComponent<SelectArm>().NextArm();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            isAttack = true;
            
            curArm = selectArm.ArmIndex;
            playSounds.PlayKick(curArm);

            Attack();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        hor = UnityEngine.Input.GetAxis("Horizontal");
        ver = UnityEngine.Input.GetAxis("Vertical");
        Move(ver);
        Turn(hor);
    }

    public void PlayerLoss()
    {
        isLoss = true;
    }

    private void Attack()
    {
        anim.SetBool("IsRun", false);
        anim.SetBool("IsWalk", false);
        anim.SetBool("IsJump", false);
        anim.SetBool("IsAttack", true);
            
        Invoke("EndAttack", 0.6f);
    }

    private void EndAttack()
    {
        anim.SetBool("IsAttack", false);
        isAttack = false;
    }

    private void Move(float input)
    {
        if (isAttack) return;
        if (Mathf.Abs(input) > 0.99f) runStart += Time.deltaTime;
        else runStart = 0;
        //if (runStart < 3f) input = (input > 0.95f) ? 0.9f : input;
        float mult = 1f;
        if (runStart >= 2f) mult = 2.5f;
        movement = transform.forward * ver;
        rb.AddForce(movement * mult * moveSpeed);
        myVelocity += movement.magnitude * mult;
    }

    private void Turn(float input)
    {
        if (isAttack) return;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, input * _rotationSmoothness, 0));
        if (ver == 0) myVelocity += Mathf.Abs(input);
    }
    public void OnJump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            if (!isAttack) anim.SetBool("IsJump", true);
            Invoke("EndJump", 0.25f);
        }
    }
    private void EndJump()
    {
        isJump = false;
        anim.SetBool("IsJump", false);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.1f, ~0, QueryTriggerInteraction.Ignore);
    }

}
