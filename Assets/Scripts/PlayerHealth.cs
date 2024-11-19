using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f; // Salud inicial del jugador

    public void TakeDamage(float damage)
    {
        health -= damage; // Restar la cantidad de da�o
        Debug.Log("Player Health: " + health);

        // Verifica si la salud llega a 0 y realiza alguna acci�n (ejemplo: muerte)
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // L�gica de muerte del jugador (ejemplo: reiniciar nivel, mostrar mensaje, etc.)
        Debug.Log("Player Died!");
    }
}
