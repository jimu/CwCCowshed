using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const float introX = 12.75f;
    const float introSpeed = 1f;
    const float initialSpeed = 10f;
    const float walkThreshold = 7f;
    const float deathDuration = 2f;

    float deathTime = 0f;

    Rigidbody playerRb;
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 30;

    //[SerializeField] float gravityModifier = 1f;

    [SerializeField] ParticleSystem explosionParticle;

    Terrain terrain;

    Animator animator;

    bool isOnGround = true;
    bool isDoubleJump = false;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        //Physics.gravity = gravityModifier;
        animator = GetComponent<Animator>();
        terrain = GameObject.FindObjectOfType<Terrain>();
        Physics.gravity = new Vector3(0, -18f, 0);
        animator.SetFloat("Speed_f", 0);
//        playerRb.useGravity = false;
        speed = introSpeed;
        deathTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {/*
        if (GameManager.instance.Running)
        {
            playerRb.isKinematic = false;
            playerRb.useGravity = true;
        */
        float currentSpeed = !GameManager.instance.Running ? 0f :
          deathTime == 0f ? speed : Mathf.Lerp(speed, 0f, (Time.time - deathTime) / deathDuration);
            // set speed_f to 1.0 for full speed

            animator.SetFloat("Speed_f", currentSpeed / walkThreshold);

            Vector3 offset = new Vector3(1f, 0f, 0) * Time.deltaTime * currentSpeed;

            Vector3 pos = transform.position;
            if (GameManager.instance.Running)
                pos += offset;

            if (speed < initialSpeed)
                pos = introMovement(pos);

            float height = terrain.SampleHeight(pos);
            if (height > pos.y)
                pos.y = height;

            transform.position = pos;
            if (!isOnGround && pos.y < height + 0.2)
            {
                isOnGround = true;
                isDoubleJump = false;
            }

            if (Input.GetKeyDown(KeyCode.Space) && !isDoubleJump)
            {
                Debug.Log("Jumping with force " + jumpForce + " Gravity is " + Physics.gravity);
                if (!isOnGround)
                    isDoubleJump = true;
                if (isOnGround)
                    playerRb.velocity = Vector3.zero;
                playerRb.AddForce(Vector3.up * jumpForce * (isDoubleJump ? 2 : 1), ForceMode.Impulse);
                isOnGround = false;
                animator.SetTrigger("Jump_trig");
            }
            /*
        }
        else
        {
            playerRb.isKinematic = false;
        }*/

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


    /*
    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I hit a " + other.name);

        if (other.CompareTag("Obstacle") && GameManager.instance.Running)
        {
            explosionParticle.Play();
            animator.SetInteger("DeathType_int", 2);
            animator.SetBool("Death_b", true);
            if (deathTime == 0f)
                deathTime = Time.time;
            Invoke("GameOver", deathDuration);
        }
    }

    // todo emoji pleading
    // todo emoji derp

    private void GameOver()
    {
        GameManager.instance.GameOver();
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
