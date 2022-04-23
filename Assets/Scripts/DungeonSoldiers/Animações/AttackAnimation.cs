using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AttackAnimation : MonoBehaviour
{
    // Vari�vel para armazenar o "SpriteRenderer"
    public SpriteRenderer spriteRenderer { get; private set; }
    // Vari�vel para armazenar os "Sprite"s da anima��o
    public Sprite[] sprites;
    // Vari�vel para armazenar o tempo da anima��o
    public float animationTime;
    // Vari�vel com o �ndice do "sprite" a ser utilizado
    public int animationFrame;
    // Vari�vel com o dano que o inimigo far� ao jogador
    public int Damage;
    // Vari�vel com o sprite que ir� atacar o jogador
    public int attackFrame;

    // Esta fun��o � chamada quando o jogo come�a
    private void Awake()
    {
        // Obt�m o "SpriteRenderer"
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // A fun��o � chamada antes da atualiza��o do primeiro frame
    private void Start()
    {
        // Chama a funcao "Advance" a cada x tempo definido em "animationTime"
        InvokeRepeating(nameof(Advance), animationTime, animationTime);
    }

    // Fun��o da anima��o
    private void Advance()
    {
        // Caso o "script" ou o "SpriteRenderer" esteja desativado, a anima��o n�o ser� atualizada
        if (!spriteRenderer.enabled || !enabled)
            return;

        // Atualiza o �ndice
        animationFrame++;

        // Verifica se o jogador est� no raio de ataque do inimigo quando este atacar
        if (animationFrame == attackFrame && GetComponent<EnemyAI>().playerInRange)
            // Caso esteja, o jogador ir� perder vida
            GameObject.FindGameObjectWithTag("Player").GetComponent<VidaPlayer>().ReceberDano(Damage);

        // Verifica se a anima��o j� acabou ou n�o
        if (animationFrame >= 0 && animationFrame < sprites.Length)
            // Caso n�o tenha acabado, o "sprite" ser� atualizado
            spriteRenderer.sprite = sprites[animationFrame];
        else
            // Caso contr�rio, o "script" ir� desativar-se
            enabled = false;
    }
}
