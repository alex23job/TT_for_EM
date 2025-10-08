using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0, 180f)] private float _rotationSmoothness;    // Коэффициент плавности поворота
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody rb;                       // Rigidbody
    private Vector3 movement = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float hor, ver;
    private Animator anim;

    private float timer = 1f;
    private float myVelocity = 0;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
            if (myVelocity > 0.2f)
            {
                anim.SetBool("IsWalk", true);
            }
            else
            {
                anim.SetBool("IsWalk", false);
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
    private void Move(float input)
    {
        //if (input > 0.99f) runStart += Time.deltaTime;
        //else runStart = 0;
        //if (runStart < 3f) input = (input > 0.95f) ? 0.9f : input;
        float mult = 1f;
        //if (runStart >= 3f) mult = 2f;
        //transform.Translate(Vector3.forward * input * moveSpeed * mult * Time.fixedDeltaTime);//Можно добавить Time.DeltaTime
        //movement = transform.forward * ver + transform.right * hor;
        movement = Vector3.forward * ver + Vector3.right * hor;
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
        // Плавный поворот в сторону движения
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSmoothness * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSmoothness);
        }

        //rb.MoveRotation(rb.rotation * Quaternion.Euler(0, movement.y * _rotationSmoothness * Time.fixedDeltaTime, 0));
        //rb.MoveRotation(rb.rotation * Quaternion.Euler(0, input * _rotationSmoothness * Time.fixedDeltaTime, 0));
        //transform.Rotate(0, input * _rotationSmoothness * Time.deltaTime, 0);
    }
}
