using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // MOVIMIENTO HORIZONTAL DEL PERSONAJE

    public GameController gameController;

    public float speed = 1.5f;  // Velocidad máxima del personaje al caminar

    private ParticleSystem dustParticle;

    //COMPONENTES EMPTYOBJECT PLAYER
    private Rigidbody2D playerrigidbody2D;
    private SpriteRenderer spriteRenderer;  // Referencia al componente SpriteRenderer del personaje

    // SALTO DEL JUGADOR


    [SerializeField] public float jumpForce; // Fuerza aplicada al saltar
    [SerializeField] private bool isJumping;
    [SerializeField] public float jumpMultiplier;
    [SerializeField] public float jumpTime;
    [SerializeField] float jumpTimer;
    private int jumpCount = 0; // Contador de saltos realizados

    [SerializeField] float fallMultiplier;
    Vector2 vecGravity;

    public float coyoteTime = 0.2f; // Tiempo de coyote     (El tiempo máximo que te deja hacer un salto despues de salir del suelo)
    private float coyoteTimer = 0f; // Temporizador del coyote time   (Empieza cuando el personaje deja de estar en el suelo)

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        dustParticle = GetComponent<ParticleSystem>();
        
        // Obtener la referencia al Rigidbody2D del objeto al que está adjunto el script
        playerrigidbody2D = GetComponent<Rigidbody2D>();

        // Obtener la referencia al componente SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
    }



    // // // // // // // // // // // // // // // // // // // // // // //
    void FixedUpdate()
    {

        // Obtener la entrada del jugador en el eje horizontal
        float moveHorizontal = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * speed * moveHorizontal * Time.deltaTime);

        

        // Girar el sprite según la dirección del movimiento
        if (moveHorizontal > 0)
        {
            spriteRenderer.flipX = false;  // No voltear el sprite
        }
        else if (moveHorizontal < 0)
        {
            spriteRenderer.flipX = true;  // Voltear el sprite
        }
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0.0f && !dustParticle.isPlaying && CheckGround.isGrounded)
        {
            dustParticle.Play();
        }
        else if (Input.GetAxis("Horizontal") == 0.0f && dustParticle.isPlaying || !CheckGround.isGrounded)
        {
            dustParticle.Stop();
        }

        if (Input.GetAxis("Horizontal") != 0.0000f)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }

        animator.SetFloat("jump", playerrigidbody2D.velocity.y);

        //SALTO PERSONAJE
        if (jumpCount < 1 && Input.GetButtonDown("Jump") && (CheckGround.isGrounded || coyoteTimer < coyoteTime))
        {
            playerrigidbody2D.velocity = new Vector2(playerrigidbody2D.velocity.x, jumpForce);
            jumpCount++;
            isJumping = true;
            jumpTimer = 0;
        }

        if (playerrigidbody2D.velocity.y > 0 && isJumping)
        {
            jumpTimer += Time.deltaTime;
            if (jumpTimer > jumpTime)
            {
                isJumping = false;
            }
            float t = jumpTimer / jumpTime;
            float currentJumpM = jumpMultiplier;

            if (t > 0.5f)
            {
                currentJumpM = jumpMultiplier * (1 - t);
            }
            playerrigidbody2D.velocity += vecGravity * currentJumpM * Time.deltaTime;
        }

        if (playerrigidbody2D.velocity.y < 0)
        {
            playerrigidbody2D.velocity += vecGravity * fallMultiplier * Time.deltaTime;
        }

        

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        if (CheckGround.isGrounded)
        {
            coyoteTimer = 0f; // Reinicia el temporizador del coyote time si está en el suelo
            jumpCount = 0; // Reinicia el contador de saltos si está en el suelo
            animator.SetBool("grounded", true);
        }
        else
        {
            coyoteTimer += Time.deltaTime; // Incrementa el temporizador del coyote time si no está en el suelo
            animator.SetBool("grounded", false);
        }

        

    }
}