using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f; // Salud inicial del jugador

    public void TakeDamage(float damage)
    {
        health -= damage; // Restar la cantidad de daño
        Debug.Log("Player Health: " + health);

        // Verifica si la salud llega a 0 y realiza alguna acción (ejemplo: muerte)
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Lógica de muerte del jugador (ejemplo: reiniciar nivel, mostrar mensaje, etc.)
        Debug.Log("Player Died!");
    }
}
