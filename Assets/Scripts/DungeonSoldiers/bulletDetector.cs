using UnityEngine;

public class bulletDetector : MonoBehaviour
{
    // Ambas as fun��es s�o utilizadas para detetar colis�es
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto que colidiu � uma bala
        if (collision.gameObject.CompareTag("Bullet"))
            // Se for, a bala ser� destru�da
            Destroy(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que colidiu � uma bala
        if (collision.gameObject.CompareTag("Bullet"))
            // Se for, a bala ser� destru�da
            Destroy(collision.gameObject);
    }
}