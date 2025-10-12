//using NUnit.Framework;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float movementSpeed = 10f;
    private float rotationSpeed = 5f;
    private Vector3 target;
    private bool isMove = false;
    private bool isAttack = false;
    private bool isDead = false;

    private List<Vector3> points = new List<Vector3>();
    private int curIndex = 0;
    private bool isPatrouille = false;
    private Rigidbody rb;
    private float stoppingDistance = 0.2f;

    private Animator anim;
    CapsuleCollider capsule;
    private PlaySounds playSounds;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        capsule = GetComponent<CapsuleCollider>();
        playSounds = GetComponent<PlaySounds>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            // ѕровер€ем, достигли ли мы текущей точки
            Vector2 pos = new Vector2(transform.position.x, transform.position.z);
            Vector2 tg = new Vector2(target.x, target.z);
            //if (Vector3.Distance(transform.position, target) < stoppingDistance)
            if (Vector3.Distance(pos, tg) < stoppingDistance)
            {
                NextPoint();
            }
            else
            {
                // ѕоворачиваем в сторону следующей точки
                if (isAttack == false) LookAtWaypoint();

                // ѕеремещаем врага к текущей точке
                MoveTowardsWaypoint();
            }
            //anim.SetFloat("Speed", 1f);
        }
        //else anim.SetBool("IsWalk", false);
    }

    public void EnemyDead()
    {
        anim.SetBool("IsWalk", false);
        anim.SetBool("IsAttack", false);
        anim.SetBool("IsDead", true);
        if (false == isDead)
        {
            isDead = true;
            playSounds.PlayClip(0);
        }
    }

    public void Attack(Transform targetAttack, int damage, int numArm)
    {
        //print($"targetAttack name={targetAttack.name}  tag={targetAttack.tag}");
        Vector3 direction = targetAttack.position - transform.position;
        direction.y = 0;
        if (targetAttack.CompareTag("Player"))
        {
            //print($"dir={direction}({direction.magnitude})  rot={Quaternion.LookRotation(direction)}");
            if (direction.magnitude < 1.3f)
            {
                if (false == isAttack) playSounds.PlayKick(numArm);
                isAttack = true;
                transform.rotation = Quaternion.LookRotation(direction);
                anim.SetBool("IsWalk", false);
                anim.SetBool("IsAttack", true);
                Invoke("EndAttack", 0.5f);
                
            }
            else
            {
                rb.MovePosition(transform.position + direction.normalized * movementSpeed * 0.05f);
                if (false == isMove) transform.rotation = Quaternion.LookRotation(direction);
                anim.SetBool("IsWalk", true);
            }
        }
        if (targetAttack.CompareTag("Temple"))
        {
            //print($"Temple dist={direction.magnitude} dir={direction}");
            if (direction.magnitude < 2.9f)
            {
                if (false == isAttack) playSounds.PlayKick(numArm);
                isAttack = true;
                transform.rotation = Quaternion.LookRotation(direction);
                anim.SetBool("IsAttack", true);
                Invoke("EndAttack", 0.5f);
                
            }
            else
            {
                rb.MovePosition(transform.position + direction.normalized * movementSpeed * 0.1f);
                if (false == isMove) transform.rotation = Quaternion.LookRotation(direction);
                anim.SetBool("IsWalk", true);
            }
            /*BoxCollider box = targetAttack.GetComponent<BoxCollider>();
            
            if (box != null)
            {
                target = targetAttack.transform.position;
                Vector3 dir;
                float distance;
                Physics.ComputePenetration(capsule, transform.position, transform.rotation, box, box.transform.position, box.transform.rotation, out dir, out distance);
                print($"enPos={transform.position} temlePos={box.transform.position} dir={dir} dist={distance}");
                if (distance < 1f)
                {
                    isAttack = true;
                    transform.rotation = Quaternion.LookRotation(direction);
                    anim.SetBool("IsAttack", true);
                    Invoke("EndAttack", 0.5f);
                }
                else
                {
                    rb.MovePosition(transform.position + dir.normalized * movementSpeed * Time.deltaTime);
                }
            }*/
        }
    }

    public void EndAttack()
    {
        anim.SetBool("IsAttack", false);
        anim.SetBool("IsWalk", isMove);
        isAttack = false;
    }

    private void LookAtWaypoint()
    {
        // ѕоворачиваем врага в сторону следующей точки
        Vector3 dir = target - transform.position;dir.y = 0f;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, rotationSpeed * Time.deltaTime);
    }

    private void MoveTowardsWaypoint()
    {
        // ѕеремещаем врага к текущей точке
        Vector3 dir = target - transform.position;dir.y = 0f;
        rb.MovePosition(transform.position + dir.normalized * movementSpeed * Time.deltaTime);
    }

    private void NextPoint()
    {
        if (curIndex < points.Count)
        {
            target = points[curIndex];
            curIndex++;
            if (isPatrouille) curIndex %= points.Count;
        }
        else
        {
            isMove = false;
            //anim.SetFloat("Speed", 0);
            anim.SetBool("IsWalk", false);
        }
    }

    public void SetPath(List<Vector3> path, bool isPatrouille = false)
    {
        points.Clear();
        points.AddRange(path);
        curIndex = 0;
        target = points[curIndex];
        this.isPatrouille = isPatrouille;
        isMove = true;
        //anim.SetFloat("Speed", 1f);
        anim.SetBool("IsWalk", true);
    }
}
