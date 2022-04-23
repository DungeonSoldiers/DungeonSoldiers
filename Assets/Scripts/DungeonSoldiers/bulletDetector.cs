using UnityEngine;

public class bulletDetector : MonoBehaviour
{
    // Ambas as funções são utilizadas para detetar colisões
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto que colidiu é uma bala
        if (collision.gameObject.CompareTag("Bullet"))
            // Se for, a bala será destruída
            Destroy(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que colidiu é uma bala
        if (collision.gameObject.CompareTag("Bullet"))
            // Se for, a bala será destruída
            Destroy(collision.gameObject);
    }
}