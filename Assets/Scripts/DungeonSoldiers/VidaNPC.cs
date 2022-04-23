using UnityEngine;
using UnityEngine.UI;

public class VidaNPC : MonoBehaviour
{
    // Variável para a barra de vida do NPC (Boss)
    public Slider barraNPC;
    // Variável com a vida máxima do inimigo
    public int vidaMaxima;
    // Variável com a vida atual do inimigo
    public int vidaAtual;
    // Variável com o "prefab" vida
    public GameObject Vida;

    // A função é chamada antes da atualizaçãoo do primeiro frame
    void Start()
    {
        // Função para maximizar a vida do NPC
        vidaAtual = vidaMaxima;
        
        // Verifica se possui uma barra de vida
        if (barraNPC != null)
        {
            // Define o valor máximo da barra de vida do NPC
            barraNPC.maxValue = vidaMaxima;
            // Atualiza a barra de vida com a vida atual do NPC
            barraNPC.value = vidaAtual;
        }
    }

    // Função para dar dano ao inimigo
    public void ReceberDano(int danoParaReceber) 
    {
        // Caso a vida seja 0 ou menor, o inimigo irá morrer
        if (vidaAtual - danoParaReceber <= 0)
        {
            // Destrói a barra de vida caso esta exista
            if (barraNPC != null)
                Destroy(barraNPC.gameObject);

            // Insere um coração na posição do inimigo
            Instantiate(Vida, transform.position, Quaternion.identity);

            // Desativa todos os "BoxCollider2D's"
            for (int i = 0; i < GetComponents<BoxCollider2D>().Length; i++)
                Destroy(GetComponents<BoxCollider2D>()[i]);

            // Desativa o RigidBody2D
            Destroy(GetComponent<Rigidbody2D>());

            // Desativa as ações do inimigo
            Destroy(GetComponent<EnemyAI>());
            Destroy(GetComponent<AnimacaoScript>());
            Destroy(GetComponent<AttackAnimation>());
            Destroy(GetComponent<SprintAnimationScript>());

            // Faz a animação de morte
            GetComponent<MorteAnimacao>().enabled = true;

            // Destroi o "script" de vida
            Destroy(this);
        }
        // Caso contrário, o inimigo irá perder vida
        else
        {
            // Tira uma porção da vida
            vidaAtual -= danoParaReceber;

            // Atualiza a barra de vida caso esta exista
            if (barraNPC != null)
                barraNPC.value = vidaAtual;
        }
    }
}
 
