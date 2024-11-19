using UnityEngine;

public class FloatingMine : MonoBehaviour
{
    public float explosionRadius = 5f; // Radio de la explosión
    public float damageAmount = 50f;   // Cantidad de daño al jugador
    public float explosionDelay = 0.5f; // Tiempo antes de la explosión
    public GameObject explosionEffect; // Prefab de efectos de explosión
    public LayerMask playerLayer; // Capa del jugador para detectar el contacto

    private bool hasExploded = false;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si la mina entra en contacto con el jugador
        if (other.CompareTag("Player") && !hasExploded)
        {
            Explode(other.gameObject);
        }
    }

    private void Explode(GameObject player)
    {
        // Evitar que explote más de una vez
        hasExploded = true;

        // Infligir daño al jugador
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount); // Asegúrate de que el PlayerHealth tenga el método TakeDamage
        }

        // Crear efectos de explosión
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Destruir la mina después de un retraso
        Destroy(gameObject, explosionDelay); // Se destruye la mina después de la explosión
    }
}
