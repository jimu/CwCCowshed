using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const float introX = 12.75f;
    const float introSpeed = 1f;
    const float initialSpeed = 10f;

    Rigidbody playerRb;
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 15;
    //[SerializeField] float gravityModifier = 1f;

    Terrain terrain;

    Animator animator;

    bool isOnGround = true;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        //Physics.gravity = gravityModifier;
        animator = GetComponent<Animator>();
        terrain = GameObject.FindObjectOfType<Terrain>();

        speed = introSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            animator.SetTrigger("Jump_trig");
        }
        if (Input.GetKeyDown(KeyCode.X))
            animator.SetBool("Death_b", true);

        //if (Input.GetKeyDown(KeyCode.R))
        //    animator.SetBool

        GetComponent<Animator>().speed = 3f;
        //Vector3 offset = new Vector3(Input.GetAxis("Horizontal"), 0f, 0) * Time.deltaTime * speed;
        Vector3 offset = new Vector3(1f, 0f, 0) * Time.deltaTime * speed;
        Vector3 pos = transform.position + offset;
        float height = terrain.SampleHeight(pos);
        if (height > pos.y)
            pos.y = height;
        //playerRb.AddForce(dir * moveForce, ForceMode.Force);

        if (speed < initialSpeed)
        {
            pos = introMovement(pos);
        }

        transform.position = pos ;

    }

    Vector3 introMovement(Vector3 pos)
    {
        if (pos.x < introX)
        {
            pos.z = playerIntroZCoord(pos.x);
            speed = playerIntroSpeed(pos.x);
        }
        else
        {
            pos.z = playerIntroZCoord(introX);
            speed = initialSpeed;
        }
        return pos;
    }


    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }

    private float playerIntroZCoord(float x)
    {
        // walking coordinates are (0, 2.51) to (12.75, 5.42)
        // z = mx+b
        // 2.51 = m0 + b          b = 2.51
        // 5.42 = m(12.75) + b    5.42 = m(12.75) + 2.51      (5.42 - 2.51)/12.75 = m
        const float slope = (5.42f - 2.51f) / 12.75f;
        return 2.51f + x * slope;
    }
    private float playerIntroSpeed(float x)
    {
        // (x=0, speed=5) to (x=12.75, speed=15)
        // z = mx+b
        // 2.51 = m0 + b          b = 2.51
        // 5.42 = m(12.75) + b    5.42 = m(12.75) + 2.51      (5.42 - 2.51)/12.75 = m
        const float slope = (initialSpeed - introSpeed) / introX;

        return introSpeed + x * slope;
    }

}
