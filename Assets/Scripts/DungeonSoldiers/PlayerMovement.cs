// Código do movimento do Player
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variável com o "RigidBody2D" do jogador
    private Rigidbody2D playerRb;
    // Variável com a direção em que o jogador quer ir
    private Vector2 moveVector;
    // Variável com a direção do "sprite"
    private bool facingRight = true;
    // Variável que define a velocidade do "Player"
    public int speed = 8;
    // Variável com o script de animações do jogador parado
    private AnimacaoScript idleAnim;
    // Variável com o script de animações de andar
    private SprintAnimationScript runAnim;

    // A função é chamada antes da atualização do primeiro frame
    private void Start()
    {
        // Obtêm o "RigidBody2D" do jogador
        playerRb = GetComponent<Rigidbody2D>();
        // Obtêm o código para a animação do personagem parado
        idleAnim = GetComponent<AnimacaoScript>();
        // Obtêm o código para a animação do personagem a correr
        runAnim = GetComponent<SprintAnimationScript>();
    }

    // A função é chamada a cada frame
    private void Update()
    {
        /* Verifica se o jogo está parado
         * Caso esteja, a função será avançada */
        if (Time.timeScale == 0) return;

        // Lê o "input" horizontal e vertical
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");

        // Função para habilitar a animação de corrida
        // e desabilitar a animação do "player" parado
        if ((moveVector.x * speed != 0 || moveVector.y * speed != 0) && runAnim.enabled == false)
        {
            idleAnim.enabled = false;
            runAnim.enabled = true;
        }
        // Função para desabilitar a animação de corrida
        // e habilitar a animação do "player" parado
        else if (moveVector.x * speed == 0 && moveVector.y * speed == 0 && idleAnim.enabled == false)
        {
            idleAnim.enabled = true;
            runAnim.enabled = false;
        }

        /* Verifica se o "player" está na direção correta
         * de acordo com a posição do cursor (direita ou esquerda) */
        Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

        if (dir.x > 0 && !facingRight || dir.x < 0 && facingRight)
            Flip();
    }

    // Muda a direção do personagem
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    // A função é chamada a cada frame
    private void FixedUpdate()
    {
        // Aplica a velocidade ao movimento do jogador
        Vector2 _velocity = moveVector.normalized * speed;
        // Aplica a movimentação
        playerRb.velocity = _velocity;
    }
}