using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed = 8f; //declaracao da variavel da velocidade
    public float speedMultiplier = 1f;
    public Vector2 initialDirection; //declarar a variavel "initialDirection" no Vector2
    public LayerMask obstacleLayer;

    public new Rigidbody2D rigidbody { get; private set; }
    public Vector2 direction { get; private set; } //declarar
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }

    private void Awake() //assim que o PacMan mover
    {
        rigidbody = GetComponent<Rigidbody2D>(); //chamar o componente associado ao PacMan
        startingPosition = transform.position; //transformar a posicao inicial para a atual
    }

    private void Start() //funcao iniciada logo que o jogo começa
    {
        ResetState(); //chamar funcao "ResetState"
    }

    public void ResetState() //funcao usada para resetar o Pac-Man par
    {
        speedMultiplier = 1f; //reseta a velocidade para 1
        direction = initialDirection; //resetar o PacMan para a direcao inicial
        nextDirection = Vector2.zero; //setar a proxima direção para o Vetor2
        transform.position = startingPosition; //transformar a posicao atual para a inicial
        rigidbody.isKinematic = false;
        enabled = true;
    }

    private void Update()
    {
        // Try to move in the next direction while it's queued to make movements
        // more responsive
        if (nextDirection != Vector2.zero)
        { //se a direcao nao estiver no Vector2
            SetDirection(nextDirection); //setar a proxima direcao
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 direction, bool forced = false) //funcao usada para setar a nova direção
    {
        // Only set the direction if the tile in that direction is available
        // otherwise we set it as the next direction so it'll automatically be
        // set when it does become available
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            nextDirection = Vector2.zero;
        }
        else
        {
            nextDirection = direction; //setar a proxima direcao como a direcao principal
        }
    }

    public bool Occupied(Vector2 direction) //funcao para identificar se o espaco tiver ocupado ou nao
    {
        // If no collider is hit then there is no obstacle in that direction
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0f, direction, 1.5f, obstacleLayer);
        return hit.collider != null; //retornar o campo "hit.collider" como null
    }

}