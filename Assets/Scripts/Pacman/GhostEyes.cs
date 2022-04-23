using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GhostEyes : MonoBehaviour
{
    //declarar as variaveis para as direcoes
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    //variavel para armazenar
    public SpriteRenderer spriteRenderer { get; private set; }

    //variavel para armazenar o movimento
    public Movement movement { get; private set; }

    private void Awake() //funcao usada para buscar os Componentes "SpriteRenderer" e o "Movement"
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); //buscar o Componente "SpriteRenderer"
        movement = GetComponentInParent<Movement>(); //buscar o Compponente "Movement"
    }

    private void Update() //funcao usada para determinar a nova posicao do olhos
    {
        if (movement.direction == Vector2.up)
        {
            spriteRenderer.sprite = up;
        }
        else if (movement.direction == Vector2.down)
        {
            spriteRenderer.sprite = down;
        }
        else if (movement.direction == Vector2.left)
        {
            spriteRenderer.sprite = left;
        }
        else if (movement.direction == Vector2.right)
        {
            spriteRenderer.sprite = right;
        }
    }

}