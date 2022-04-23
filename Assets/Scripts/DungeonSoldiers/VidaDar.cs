using UnityEngine;

public class VidaDar : MonoBehaviour
{
    // Variável com a vida a dar ao jogador
    public int vidaParaDar;
    
    // Função de detecção de colisão entre um objeto
    void OnTriggerEnter2D(Collider2D other) 
    {
        // Verifica se o objeto que colidiu foi o jogador e se o jogador não têm a vida no máximo
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<VidaPlayer>().vidaAtual != other.gameObject.GetComponent<VidaPlayer>().vidaMaxima)
        {
            // Caso tenha sido, o jogador irá ganhar vida
            other.gameObject.GetComponent<VidaPlayer>().ReceberVida(vidaParaDar);
            // Destrói o objeto de dar vida
            Destroy(gameObject);
        }
    }
}
