using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AttackAnimation : MonoBehaviour
{
    // Variável para armazenar o "SpriteRenderer"
    public SpriteRenderer spriteRenderer { get; private set; }
    // Variável para armazenar os "Sprite"s da animação
    public Sprite[] sprites;
    // Variável para armazenar o tempo da animação
    public float animationTime;
    // Variável com o índice do "sprite" a ser utilizado
    public int animationFrame;
    // Variável com o dano que o inimigo fará ao jogador
    public int Damage;
    // Variável com o sprite que irá atacar o jogador
    public int attackFrame;

    // Esta função é chamada quando o jogo começa
    private void Awake()
    {
        // Obtêm o "SpriteRenderer"
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // A função é chamada antes da atualização do primeiro frame
    private void Start()
    {
        // Chama a funcao "Advance" a cada x tempo definido em "animationTime"
        InvokeRepeating(nameof(Advance), animationTime, animationTime);
    }

    // Função da animação
    private void Advance()
    {
        // Caso o "script" ou o "SpriteRenderer" esteja desativado, a animação não será atualizada
        if (!spriteRenderer.enabled || !enabled)
            return;

        // Atualiza o índice
        animationFrame++;

        // Verifica se o jogador está no raio de ataque do inimigo quando este atacar
        if (animationFrame == attackFrame && GetComponent<EnemyAI>().playerInRange)
            // Caso esteja, o jogador irá perder vida
            GameObject.FindGameObjectWithTag("Player").GetComponent<VidaPlayer>().ReceberDano(Damage);

        // Verifica se a animação já acabou ou não
        if (animationFrame >= 0 && animationFrame < sprites.Length)
            // Caso não tenha acabado, o "sprite" será atualizado
            spriteRenderer.sprite = sprites[animationFrame];
        else
            // Caso contrário, o "script" irá desativar-se
            enabled = false;
    }
}
