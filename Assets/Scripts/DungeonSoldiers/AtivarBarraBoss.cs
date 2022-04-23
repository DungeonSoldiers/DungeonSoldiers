using UnityEngine;

public class AtivarBarraBoss : MonoBehaviour
{
    // Variável para guardar a barra de vida do BOSS
    public GameObject barradeVidaBOSS;
    
    // Deteta colisões de outros objetos
    void OnTriggerEnter2D(Collider2D other) 
    {
        // Verifica se o objeto é o jogador
        if (other.gameObject.CompareTag("Player"))
            // Se for, a barra de vida do Boss será ativada
            barradeVidaBOSS.SetActive(true);
    }
}
