using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SprintAnimationScript : MonoBehaviour
{
    // Variável para armazenar o "SpriteRenderer"
    public SpriteRenderer spriteRenderer { get; private set; }
    // Variável para armazenar os "Sprite"s da animação
    public Sprite[] sprites;
    // Variável para armazenar o tempo da animação
    public float animationTime;
    // Variável com o índice do "sprite" a ser utilizado
    public int animationFrame { get; private set; }

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

        /* Verifica se a animação já acabou
         * Caso já tenha acabado, o indíce será recomeçado */
        if (animationFrame >= sprites.Length)
            animationFrame = 0;

        // Atualiza o "sprite"
        if (animationFrame >= 0 && animationFrame < sprites.Length)
            spriteRenderer.sprite = sprites[animationFrame];
    }
}
