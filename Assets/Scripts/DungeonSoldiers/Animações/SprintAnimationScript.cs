using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SprintAnimationScript : MonoBehaviour
{
    // Vari�vel para armazenar o "SpriteRenderer"
    public SpriteRenderer spriteRenderer { get; private set; }
    // Vari�vel para armazenar os "Sprite"s da anima��o
    public Sprite[] sprites;
    // Vari�vel para armazenar o tempo da anima��o
    public float animationTime;
    // Vari�vel com o �ndice do "sprite" a ser utilizado
    public int animationFrame { get; private set; }

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

        /* Verifica se a anima��o j� acabou
         * Caso j� tenha acabado, o ind�ce ser� recome�ado */
        if (animationFrame >= sprites.Length)
            animationFrame = 0;

        // Atualiza o "sprite"
        if (animationFrame >= 0 && animationFrame < sprites.Length)
            spriteRenderer.sprite = sprites[animationFrame];
    }
}
