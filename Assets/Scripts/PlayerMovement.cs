using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // MOVIMIENTO HORIZONTAL DEL PERSONAJE

    public GameController gameController;

    public float speed = 1.5f;  // Velocidad máxima del personaje al caminar

    private ParticleSystem dustParticle;

    //COMPONENTES EMPTYOBJECT PLAYER
    private Rigidbody2D playerrigidbody2D;
    private SpriteRenderer spriteRenderer;  // Referencia al componente SpriteRenderer del personaje

    private GameObject hidder;
    // SALTO DEL JUGADOR

    public float health = 4;
    [SerializeField] public float jumpForce; // Fuerza aplicada al saltar
    [SerializeField] private bool isJumping;
    [SerializeField] public float jumpMultiplier;
    [SerializeField] public float jumpTime;
    [SerializeField] float jumpTimer;
    [SerializeField] GameObject torch;
    [SerializeField] GameObject[] torchWaypoints;
    [SerializeField] float torchSpeed;
    [SerializeField] GameObject healthUI;
    private Image healthUIImage;
    private float iFrames = 0;
    private float imageColor;
    private int jumpCount = 0; // Contador de saltos realizados
    private bool isDead = false;
    Light2D globalLight;

    [SerializeField] float fallMultiplier;
    Vector2 vecGravity;

    public float coyoteTime = 0.2f; // Tiempo de coyote     (El tiempo máximo que te deja hacer un salto despues de salir del suelo)
    private float coyoteTimer = 0f; // Temporizador del coyote time   (Empieza cuando el personaje deja de estar en el suelo)

    private Animator animator;
    Animator hidderAnimator;
    AudioManager audioManager;
    [SerializeField] AudioSource audioSource;


    private void Awake()
    {
        globalLight = GetComponent<Light2D>();
        animator = GetComponent<Animator>();
        dustParticle = GetComponent<ParticleSystem>();

        healthUI = GameObject.FindGameObjectWithTag("HealthUI");
        healthUIImage = healthUI.GetComponent<Image>();
        
        // Obtener la referencia al Rigidbody2D del objeto al que está adjunto el script
        playerrigidbody2D = GetComponent<Rigidbody2D>();

        // Obtener la referencia al componente SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        hidder = GameObject.FindGameObjectWithTag("scenehidder");

        hidderAnimator = hidder.GetComponent<Animator>();

        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
    }



    // // // // // // // // // // // // // // // // // // // // // // //
    void FixedUpdate()
    {

        // Obtener la entrada del jugador en el eje horizontal
        if (!isDead)
        {
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
        
    }

    private void Update()
    {
        if (!isDead)
        {
            if (iFrames > 0)
            {
                iFrames -= Time.deltaTime;
            }

            if (imageColor < 255)
            {
                imageColor += Time.deltaTime;
                healthUIImage.color = new Color(255, imageColor, 255);
            }

            if (health == 0 && !isDead)
            {
                StartCoroutine(DeathCoroutine());
            }

            if (!spriteRenderer.flipX)
            {
                torch.transform.position = Vector3.MoveTowards(torch.transform.position, torchWaypoints[0].transform.position, Time.deltaTime * torchSpeed);

            }
            else
            {
                torch.transform.position = Vector3.MoveTowards(torch.transform.position, torchWaypoints[1].transform.position, Time.deltaTime * torchSpeed);
            }
            if (Input.GetAxis("Horizontal") != 0.0f && !dustParticle.isPlaying && CheckGround.isGrounded)
            {
                audioSource.Play();
                dustParticle.Play();
            }
            else if (Input.GetAxis("Horizontal") == 0.0f && dustParticle.isPlaying || !CheckGround.isGrounded)
            {
                audioSource.Stop();
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
                audioManager.PlaySFX(audioManager.jump);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (iFrames <= 0 && collision.gameObject.CompareTag("Enemy") || iFrames <= 0 && collision.gameObject.CompareTag("EnemyBullet"))
        {
            health = health - 1;
            iFrames = 1.5f;
            healthUIImage.color = new Color(255, 0, 255);
            imageColor = 0;
            UpdateHealthUI();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (iFrames <= 0 && collision.gameObject.CompareTag("Enemy") || iFrames <= 0 && collision.gameObject.CompareTag("EnemyBullet"))
        {
            health = health - 1;
            iFrames = 1.5f;
            healthUIImage.color = new Color(255, 0, 255);
            UpdateHealthUI();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (iFrames <= 0 && collision.gameObject.CompareTag("Enemy") || iFrames <= 0 && collision.gameObject.CompareTag("EnemyBullet"))
        {
            health = health - 1;
            iFrames = 1.5f;
            healthUIImage.color = new Color(255, 0, 255);
            imageColor = 0;
            UpdateHealthUI();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (iFrames <= 0 && collision.gameObject.CompareTag("Enemy") || iFrames <= 0 && collision.gameObject.CompareTag("EnemyBullet"))
        {
            health = health - 1;
            iFrames = 1.5f;
            healthUIImage.color = new Color(255, 0, 255);
            UpdateHealthUI();
        }
    }



    private void UpdateHealthUI()
    {
        healthUIImage.fillAmount = health / 4;
    }

    private IEnumerator DeathCoroutine()
    {
        Light2D light = GameObject.FindGameObjectWithTag("GlobalLight").GetComponent<Light2D>();
        dustParticle.Stop();
        isDead = true; // Marcar al jugador como muerto para evitar ejecutar mltiples veces la rutina
        animator.SetBool("die", true);
        // Gradualmente oscurecer la luz global
        float duration = 2f; // Duracin de la transicin
        float elapsedTime = 0f;
        Color initialColor = globalLight.color;
        Color initialColor2 = light.color;

        yield return new WaitForSeconds(0.5f);
        animator.SetBool("muriendo", true);
        animator.SetBool("die", false);
        while (elapsedTime < duration)
        {
            light.color = Color.Lerp(initialColor2, Color.black, elapsedTime / duration);
            globalLight.color = Color.Lerp(initialColor, Color.black, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Mostrar mensaje "YOU LOSE"
        Debug.Log("YOU LOSE");
        hidderAnimator.Play("sceneHidderIn");
        // Esperar un breve momento antes de recargar la escena
        yield return new WaitForSeconds(3f); // Puedes ajustar el tiempo sen tus necesidades
        
        // Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        animator.SetBool("muriendo", false);
        yield return new WaitForSeconds(2f);
        hidderAnimator.Play("sceneHidderOut");

    }
}