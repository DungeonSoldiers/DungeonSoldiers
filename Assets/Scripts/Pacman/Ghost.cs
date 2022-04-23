using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Ghost : MonoBehaviour
{
    public Movement movement { get; private set; } //variavel para armazenar o movimento
    public GhostHome home { get; private set; } //variavel para armazenar a Home
    public GhostScatter scatter { get; private set; } // variavel para armazenar o GhostScatter
    public GhostChase chase { get; private set; } //variavel para armazenar o GhostChase
    public GhostFrightened frightened { get; private set; }
    public GhostBehavior initialBehavior; //variavel que armazena o InitialBehavior
    public Transform target; //variavel que armazena o Target (inimigo)
    public int points = 200; //variavel que indica a quantidade de pontos que um Ghost da

    private void Awake() //funcao usada para ir buscar os 
    {
        //direciona as variaveis criadas para os componentes existentes (serve para os componentes abaixo)
        movement = GetComponent<Movement>();
        home = GetComponent<GhostHome>(); 
        scatter = GetComponent<GhostScatter>(); 
        chase = GetComponent<GhostChase>(); 
        frightened = GetComponent<GhostFrightened>(); 
    }

    //funcao iniciada automaticamente assim que a funcao "Awake" inicia
    private void Start()
    {
        ResetState(); //chamar a funcao "ResetState"
    }
    //funcao para resetar os Ghosts
    public void ResetState()
    {
        gameObject.SetActive(true); //ativa os Ghosts
        movement.ResetState(); //reinicia o movement

        frightened.Disable(); //desativar o frightened
        chase.Disable(); //desativar a perseguicao do ghost
        scatter.Enable(); //ativar o Scatter

        if (home != initialBehavior)
        { //desativar Home assim que  algum ghost sai da Home
            home.Disable(); //desativar a home
        }

        if (initialBehavior != null)
        { //se não tiver nenhum InitialBehavior
            initialBehavior.Enable(); //ativar o InitialBehavior
        }
    }

    public void SetPosition(Vector3 position) //setar a posicao
    {
        // manter a posicao z igual
        position.z = transform.position.z;
        transform.position = position;
    }

    //funcao usada pra determinar a colisao se foi o pacman que tocou num dos ghosts
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //se layer do objeto for igual a "Pacman"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (frightened.enabled)
            { //se for ativado
                FindObjectOfType<GameManager>().GhostEaten(this); //ativar a funcao "GhostEaten"
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten(); //se nao, ativa a funcao "PacmanEaten"
            }
        }
    }

}