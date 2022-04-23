using UnityEngine;

public class swordColliderScript : MonoBehaviour
{
    // Vari�vel com o efeito sonoro de antigir com a espada
    public AudioClip swingHit;

    // Fun��o para detetar colis�es
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* Verifica se o script est� ativado e se o objeto atingido � um inimigo 
         * Caso este seja o caso, o inimigo perder� vida */
        if (enabled && collision.gameObject.CompareTag("Enemy") && !collision.isTrigger)
        {
            // Retira vida ao inimigo
            collision.GetComponent<VidaNPC>().ReceberDano(25);
            // Inicia o a�dio para indicar que o inimigo foi antigido pela espada
            GetComponentInParent<Espada>().PlayAudio(swingHit);
        }
    }
}