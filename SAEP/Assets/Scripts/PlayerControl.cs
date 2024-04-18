using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Animator playerAnim;
    private Rigidbody playerRb;
    private AudioSource playerAudio;
    public ParticleSystem explosionSmoke;
    public ParticleSystem dirtRun;
    public float jumpForce = 10f;
    public float gravityModifier;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public bool isOnGround = true;
    public bool gameOver = false;
    public bool secondJump;
    public float secondJumpForce = 300f;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        //player jumps when space is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtRun.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            secondJump = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && secondJump && !gameOver)
        {
            playerRb.AddForce(Vector3.up * secondJumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtRun.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            secondJump = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtRun.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");

            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionSmoke.Play();
            dirtRun.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
