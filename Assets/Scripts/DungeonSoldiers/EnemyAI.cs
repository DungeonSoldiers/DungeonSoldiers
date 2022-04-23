using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Variável do jogador
    public Transform Player;
    // Variável da posição inicial
    private Vector3 homePosition;
    // Mostra a estrutura no Unity
    [SerializeField]
    // Variável com a velocidade do "NPC"
    public float speed;
    [SerializeField]
    // Variável com a distância máxima entre o jogador e o "NPC"
    private float maxRange;
    [SerializeField]
    // Variável com a distância mínima entre o jogador e o "NPC"
    private float minRange;
    // Variável com o componente "SpriteRenderer"
    SpriteRenderer sr;
    public bool playerInRange;

    // A função é chamada antes da atualização do primeiro frame
    void Start()
    {
        // Obtêm o componente "SpriteRenderer"
        sr = GetComponent<SpriteRenderer>();
        // Obtêm a posição inicial do "NPC"
        homePosition = transform.position;
    }

    // A função é chamada a cada frame
    void Update()
    {
        /* Verifica se o inimigo está a fazer a animação de ataque
         * Caso esteja, a função será avançada */
        if (GetComponent<AttackAnimation>().enabled)
            return;

        // Verifica se o "Player" está dentro do raio de alcance
        if (Vector3.Distance(Player.position, transform.position) <= maxRange && Vector3.Distance(Player.position, transform.position) >= minRange)
            // Caso esteja, este irá seguir o "Player"
            FollowPlayer();
        else if (Vector3.Distance(Player.position, transform.position) >= maxRange)
            // Caso contrário, este irá voltar para a sua posição inicial
            GoHome();
    }

    // Função para o "NPC" seguir o "Player"
    void FollowPlayer()
    {
        // Desativa as animações "idle"
        GetComponent<AnimacaoScript>().enabled = false;
        // Ativa as animações "Run"
        GetComponent<SprintAnimationScript>().enabled = true;

        // Faz o "NPC" mover-se para a posição do "Player"
        transform.position = Vector3.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);

        // Deteta se o "NPC" está a ir para a direita
        if (Player.position.x - transform.position.x > 0)
            // Muda a direção do "Sprite"
            sr.flipX = false;
        // Deteta se o "NPC" está a ir para a esquerda
        else if (Player.position.x - transform.position.x < 0)
            // Muda a direção do "Sprite"
            sr.flipX = true;
    }

    // Função para o "NPC" voltar para a posição inicial
    void GoHome()
    {
        // Faz o "NPC" mover-se para a posição inicial
        transform.position = Vector3.MoveTowards(transform.position, homePosition, speed * Time.deltaTime);

        // Deteta se o "NPC" está a ir para a direita
        if (homePosition.x - transform.position.x > 0)
            // Muda a direção do "Sprite"
            sr.flipX = false;
        // Deteta se o "NPC" está a ir para a esquerda
        else if (homePosition.x - transform.position.x < 0)
            // Muda a direção do "Sprite"
            sr.flipX = true;
        // Deteta se o inimigo está parado no "homePosition"
        else
        {
            // Ativa as animações "idle"
            GetComponent<AnimacaoScript>().enabled = true;
            // Desativa as animações "Run"
            GetComponent<SprintAnimationScript>().enabled = false;
        }
    }

    // Deteta se algum objeto entrou em colisão com o inimigo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se esse objeto é o jogador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Atualiza a variável
            playerInRange = true;

            // Verifica se o inimigo já está a atacar
            if (!GetComponent<AttackAnimation>().enabled)
            {
                // Caso não esteja, o inimigo irá realizar o ataque
                GetComponent<AttackAnimation>().animationFrame = -1;
                GetComponent<AttackAnimation>().enabled = true;
                GetComponent<SprintAnimationScript>().enabled = false;
            }
        }
    }

    // Deteta se algum objeto ainda se encontra em colisão com o inimigo
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Verifica se esse objeto é o jogador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Atualiza a variável
            playerInRange = true;

            // Verifica se o inimigo já está a atacar
            if (!GetComponent<AttackAnimation>().enabled)
            {
                // Caso não esteja, o inimigo irá realizar o ataque
                GetComponent<AttackAnimation>().animationFrame = -1;
                GetComponent<AttackAnimation>().enabled = true;
                GetComponent<SprintAnimationScript>().enabled = false;
            }
        }
    }

    // Deteta se algum objeto parou de estar em colisão com o inimigo
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Verifica se esse objeto é o jogador
        if (collision.gameObject.CompareTag("Player"))
            // Atualiza a variável
            playerInRange = false;
    }
}