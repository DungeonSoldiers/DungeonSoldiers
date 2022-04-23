using UnityEngine;

public class DarDano : MonoBehaviour
{
    // Variável com o dano a dar ao inimigo
    public int danoParaReceber;

    /* Função para detetar se a bala colidiu com
     * algum objeto na hierarquia */
    void OnTriggerEnter2D(Collider2D other) 
    {
        // Verifica se a bala colidiu com um inimigo vivo
        if (other.gameObject.CompareTag("Enemy") && !other.gameObject.GetComponent<MorteAnimacao>().enabled && !other.isTrigger)
        {
            // Caso tenha, o inimigo irá perder vida
            other.gameObject.GetComponent<VidaNPC>().ReceberDano(danoParaReceber);
            // Destrói o projétil
            Destroy(gameObject);
        }
    }
}
