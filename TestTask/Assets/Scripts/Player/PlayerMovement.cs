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

    private float timer = 1f;
    private float myVelocity = 0;

    private int curArm = 0;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<SelectArm>().SetArmDamage(new int[] { 5, 10, 20 });
    }

    // Update is called once per frame
    void Update()
    {
        //hor = UnityEngine.Input.GetAxis("Horizontal");
        //ver = UnityEngine.Input.GetAxis("Vertical");
        if (timer > 0f) timer -= Time.deltaTime;
        else
        {
            timer = 0.25f;
            //anim.SetFloat("WalkSpeed", rb.linearVelocity.magnitude * 1000);
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
                    if (!isAttack) anim.SetBool("IsWalk", true);
                }
                else
                {
                    anim.SetBool("IsWalk", false);
                }
            }

            /*if (myVelocity < 0.2f)
            {
                anim.SetBool("IsWalk", false);
                //anim.SetBool("IsStop", false);
                anim.SetBool("IsRun", false);
            }
            else if (myVelocity < 10f)
            {
                anim.SetBool("IsWalk", true);
                anim.SetBool("IsRun", false);
            }
            else
            {
                anim.SetBool("IsRun", true);
                //anim.SetBool("IsStop", true);
                anim.SetBool("IsWalk", false);
            }*/
            //anim.SetFloat("WalkSpeed", myVelocity);
            //print($"myVelocity = {myVelocity}");
            myVelocity = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            curArm++; curArm %= 3;
            gameObject.GetComponent<SelectArm>().SelectCurrentArm(curArm);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            isAttack = true;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        hor = UnityEngine.Input.GetAxis("Horizontal");
        ver = UnityEngine.Input.GetAxis("Vertical");
        //rb.AddForce(movement, ForceMode.Impulse);
        Move(ver);
        Turn(hor);
    }

    private void Attack()
    {
        anim.SetBool("IsWalk", false);
        anim.SetBool("IsJump", false);
        anim.SetBool("IsAttack", true);
            
        Invoke("EndAttack", 0.5f);
    }

    private void EndAttack()
    {
        anim.SetBool("IsAttack", false);
        isAttack = false;
    }

    private void Move(float input)
    {
        if (isAttack) return;
        //if (input > 0.99f) runStart += Time.deltaTime;
        //else runStart = 0;
        //if (runStart < 3f) input = (input > 0.95f) ? 0.9f : input;
        float mult = 1f;
        //if (runStart >= 3f) mult = 2f;
        //transform.Translate(Vector3.forward * input * moveSpeed * mult * Time.fixedDeltaTime);//Можно добавить Time.DeltaTime
        movement = transform.forward * ver; // + transform.right * hor * 0.1f;
        //movement = Vector3.forward * ver + Vector3.right * hor * 0.1f;
        rb.AddForce(movement * mult * moveSpeed);
        //myVelocity += rb.linearVelocity.magnitude * 1000;
        myVelocity += movement.magnitude * 2;
        //print($"speed = {rb.linearVelocity.magnitude * 1000}");
        //anim.SetFloat("WalkSpeed", rb.linearVelocity.magnitude * 1000);
        //rb.AddForce(transform.forward * input * moveSpeed * mult * Time.fixedDeltaTime);
        //anim.SetFloat("speed", Mathf.Abs(input));
    }

    private void Turn(float input)
    {
        if (isAttack) return;
        // Плавный поворот в сторону движения
        //if (movement != Vector3.zero)
        //{
        //    Quaternion targetRotation = Quaternion.LookRotation(movement);
        //    //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSmoothness * Time.deltaTime);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSmoothness);
        //}

        //rb.MoveRotation(rb.rotation * Quaternion.Euler(0, movement.y * _rotationSmoothness * Time.fixedDeltaTime, 0));
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, input * _rotationSmoothness, 0));
        if (ver == 0) myVelocity += Mathf.Abs(input);
        //transform.Rotate(0, input * _rotationSmoothness * Time.deltaTime, 0);
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
