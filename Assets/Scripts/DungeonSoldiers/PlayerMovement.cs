// C�digo do movimento do Player
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Vari�vel com o "RigidBody2D" do jogador
    private Rigidbody2D playerRb;
    // Vari�vel com a dire��o em que o jogador quer ir
    private Vector2 moveVector;
    // Vari�vel com a dire��o do "sprite"
    private bool facingRight = true;
    // Vari�vel que define a velocidade do "Player"
    public int speed = 8;
    // Vari�vel com o script de anima��es do jogador parado
    private AnimacaoScript idleAnim;
    // Vari�vel com o script de anima��es de andar
    private SprintAnimationScript runAnim;

    // A fun��o � chamada antes da atualiza��o do primeiro frame
    private void Start()
    {
        // Obt�m o "RigidBody2D" do jogador
        playerRb = GetComponent<Rigidbody2D>();
        // Obt�m o c�digo para a anima��o do personagem parado
        idleAnim = GetComponent<AnimacaoScript>();
        // Obt�m o c�digo para a anima��o do personagem a correr
        runAnim = GetComponent<SprintAnimationScript>();
    }

    // A fun��o � chamada a cada frame
    private void Update()
    {
        /* Verifica se o jogo est� parado
         * Caso esteja, a fun��o ser� avan�ada */
        if (Time.timeScale == 0) return;

        // L� o "input" horizontal e vertical
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");

        // Fun��o para habilitar a anima��o de corrida
        // e desabilitar a anima��o do "player" parado
        if ((moveVector.x * speed != 0 || moveVector.y * speed != 0) && runAnim.enabled == false)
        {
            idleAnim.enabled = false;
            runAnim.enabled = true;
        }
        // Fun��o para desabilitar a anima��o de corrida
        // e habilitar a anima��o do "player" parado
        else if (moveVector.x * speed == 0 && moveVector.y * speed == 0 && idleAnim.enabled == false)
        {
            idleAnim.enabled = true;
            runAnim.enabled = false;
        }

        /* Verifica se o "player" est� na dire��o correta
         * de acordo com a posi��o do cursor (direita ou esquerda) */
        Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

        if (dir.x > 0 && !facingRight || dir.x < 0 && facingRight)
            Flip();
    }

    // Muda a dire��o do personagem
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    // A fun��o � chamada a cada frame
    private void FixedUpdate()
    {
        // Aplica a velocidade ao movimento do jogador
        Vector2 _velocity = moveVector.normalized * speed;
        // Aplica a movimenta��o
        playerRb.velocity = _velocity;
    }
}