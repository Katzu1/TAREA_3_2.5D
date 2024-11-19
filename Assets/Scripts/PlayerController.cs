using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 10f;  // Velocidad hacia adelante
    public float lateralSpeed = 5f;   // Velocidad lateral
    public float jumpForce = 5f;      // Fuerza del salto
    public LayerMask groundLayer;     // Capa del suelo para detectar si está tocando el suelo

    [Header("Slide Settings")]
    public float slideDuration = 1f;      // Duración del deslizamiento
    public float slideSpeedMultiplier = 1.5f; // Aumento de velocidad durante el deslizamiento
    public Vector3 slideScale = new Vector3(1, 0.5f, 1); // Escala del jugador mientras desliza

    [Header("Direction Settings")]
    public Vector3 moveDirection = Vector3.forward; // Dirección de movimiento asignable

    private Rigidbody rb;
    private bool isGrounded;
    private bool isSliding;
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // Evitar rotaciones
        originalScale = transform.localScale; // Guardar la escala original
        moveDirection = moveDirection.normalized; // Normalizar la dirección para evitar valores extraños
    }

    void Update()
    {
        if (!isSliding)
        {
            MoveForward();
            MoveLateral();
        }

        CheckGroundStatus();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isSliding)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded && !isSliding)
        {
            StartCoroutine(Slide());
        }
    }

    void MoveForward()
    {
        // Movimiento constante en la dirección configurada
        Vector3 forwardVelocity = moveDirection * forwardSpeed;
        rb.velocity = new Vector3(forwardVelocity.x, rb.velocity.y, forwardVelocity.z);
    }

    void MoveLateral()
    {
        // Movimiento lateral controlado por teclas A/D o flechas izquierda/derecha
        float lateralInput = Input.GetAxis("Horizontal");
        Vector3 lateralDirection = Vector3.Cross(Vector3.up, moveDirection).normalized;
        Vector3 lateralMovement = lateralDirection * lateralInput * lateralSpeed;

        // Combina el movimiento lateral con el movimiento hacia adelante
        rb.velocity = new Vector3(lateralMovement.x + forwardSpeed * moveDirection.x, rb.velocity.y, lateralMovement.z + forwardSpeed * moveDirection.z);
    }

    void Jump()
    {
        // Agrega fuerza hacia arriba para el salto
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void CheckGroundStatus()
    {
        // Detecta si está tocando el suelo usando un raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
    }

    IEnumerator Slide()
    {
        isSliding = true;

        // Cambiar la escala para simular deslizamiento
        transform.localScale = slideScale;

        // Aumentar la velocidad mientras desliza
        float originalSpeed = forwardSpeed;
        forwardSpeed *= slideSpeedMultiplier;

        yield return new WaitForSeconds(slideDuration);

        // Restaurar la escala y la velocidad originales
        transform.localScale = originalScale;
        forwardSpeed = originalSpeed;

        isSliding = false;
    }
}
