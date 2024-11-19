using UnityEngine;

public class SideCameraFollow : MonoBehaviour
{
    public Transform player;    // Referencia al jugador
    public Vector3 offset = new Vector3(5, 2, 0); // Posición lateral relativa al jugador

    void LateUpdate()
    {
        // Posiciona la cámara en relación al jugador
        if (player != null)
        {
            transform.position = player.position + offset;
            transform.LookAt(player.position); // Apunta al jugador
        }
    }
}
