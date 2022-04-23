using UnityEngine;

public class PortaScript : MonoBehaviour
{
    // Variável com o componente "Animator"
    public Animator anim;

    // Função para detetar colisões
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* Verifica se o objeto que colidiu é o jogador 
         * Se for, este irá executar o código abaixo */
        if (collision.gameObject.CompareTag("Player"))
        {
            // Para o jogador
            collision.gameObject.GetComponent<PlayerMovement>().speed = 0;
            // Teleporta o jogador para o jogo principal com uma transição de "fade out"
            anim.SetTrigger("startGame");
        }
    }
}