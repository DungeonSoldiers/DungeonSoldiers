using UnityEngine;

public class swordColliderScript : MonoBehaviour
{
    // Variável com o efeito sonoro de antigir com a espada
    public AudioClip swingHit;

    // Função para detetar colisões
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* Verifica se o script está ativado e se o objeto atingido é um inimigo 
         * Caso este seja o caso, o inimigo perderá vida */
        if (enabled && collision.gameObject.CompareTag("Enemy") && !collision.isTrigger)
        {
            // Retira vida ao inimigo
            collision.GetComponent<VidaNPC>().ReceberDano(25);
            // Inicia o aúdio para indicar que o inimigo foi antigido pela espada
            GetComponentInParent<Espada>().PlayAudio(swingHit);
        }
    }
}