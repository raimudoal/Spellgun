using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // MOVIMIENTO HORIZONTAL DEL PERSONAJE


    public float speed = 1.5f;  // Velocidad máxima del personaje al caminar


    //COMPONENTES EMPTYOBJECT PLAYER
    private Rigidbody2D playerrigidbody2D;
    private SpriteRenderer spriteRenderer;  // Referencia al componente SpriteRenderer del personaje

    // SALTO DEL JUGADOR


    public float jumpForce = 4f; // Fuerza aplicada al saltar
    private int jumpCount = 0; // Contador de saltos realizados

    public float coyoteTime = 0.2f; // Tiempo de coyote     (El tiempo máximo que te deja hacer un salto despues de salir del suelo)
    private float coyoteTimer = 0f; // Temporizador del coyote time   (Empieza cuando el personaje deja de estar en el suelo)

    private void Awake()
    {
        // Obtener la referencia al Rigidbody2D del objeto al que está adjunto el script
        playerrigidbody2D = GetComponent<Rigidbody2D>();

        // Obtener la referencia al componente SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        //SALTO PERSONAJE
        if (CheckGround.isGrounded)
        {
            coyoteTimer = 0f; // Reinicia el temporizador del coyote time si está en el suelo
            jumpCount = 0; // Reinicia el contador de saltos si está en el suelo
        }
        else
        {
            coyoteTimer += Time.deltaTime; // Incrementa el temporizador del coyote time si no está en el suelo
        }

        if (jumpCount < 1 && Input.GetKeyDown(KeyCode.Space) && (CheckGround.isGrounded || coyoteTimer < coyoteTime))
        {
            playerrigidbody2D.velocity = new Vector2(playerrigidbody2D.velocity.x, jumpForce);
            jumpCount++;
        }

    }
}