using UnityEngine;

public class FloatingMine : MonoBehaviour
{
    public float explosionRadius = 5f; // Radio de la explosi�n
    public float damageAmount = 50f;   // Cantidad de da�o al jugador
    public float explosionDelay = 0.5f; // Tiempo antes de la explosi�n
    public GameObject explosionEffect; // Prefab de efectos de explosi�n
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
        // Evitar que explote m�s de una vez
        hasExploded = true;

        // Infligir da�o al jugador
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount); // Aseg�rate de que el PlayerHealth tenga el m�todo TakeDamage
        }

        // Crear efectos de explosi�n
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Destruir la mina despu�s de un retraso
        Destroy(gameObject, explosionDelay); // Se destruye la mina despu�s de la explosi�n
    }
}
