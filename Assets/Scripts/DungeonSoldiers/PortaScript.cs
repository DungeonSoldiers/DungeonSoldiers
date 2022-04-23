using UnityEngine;

public class PortaScript : MonoBehaviour
{
    // Vari�vel com o componente "Animator"
    public Animator anim;

    // Fun��o para detetar colis�es
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* Verifica se o objeto que colidiu � o jogador 
         * Se for, este ir� executar o c�digo abaixo */
        if (collision.gameObject.CompareTag("Player"))
        {
            // Para o jogador
            collision.gameObject.GetComponent<PlayerMovement>().speed = 0;
            // Teleporta o jogador para o jogo principal com uma transi��o de "fade out"
            anim.SetTrigger("startGame");
        }
    }
}