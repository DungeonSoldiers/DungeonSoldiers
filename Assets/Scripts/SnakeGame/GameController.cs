using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //variável para armazenar o valor inserido no teclado.
    public enum Direction 
    {
        LEFT, RIGHT, UP, DOWN //Possiveis valores
    }
    //variável para definir para onde vou mover
    public Direction moveDirection;
    //Tempo entre um passo e outro
    public float delayStep;
    //Quantidade de movimento a cada passo
    public float step;
    //variável para ter acesso à cabeça, variável em tranform pois somente será necessario alterar a posição
    public Transform Head;
    //variável para o rabo da cobra
    public List<Transform> Tail;
    //variável para guardar a ultima posição que se encontra
    private Vector3 lasPos;
    //variável para o prefab da comida 
    public Transform food;
    //variável para o prefab do rabo
    public GameObject tailPrefab;
    //numero de colunas
    public int col = 29;
    //numero de linhas
    public int row = 15;
    //variável para os pontos
    public Text txtScore;
    //v ariavel para guardar o recorde
    // Variável para o texto de "Hi-Score"
    public Text txtHiScore;
    // Variável para o "Score"
    private int score;
    // Variável para o "Hi-Score"
    private int HiScore; 
    //variável para habilitar o painel de game over
    public GameObject panelGameOver; 
    // Variável para habilitar o Painel de titulo
    public GameObject panelTitle;


    //A função start é chamada antes do primeiro frame
    void Start()
    {
        StartCoroutine("moveSnake");
        SetFood();
        HiScore = PlayerPrefs.GetInt("HiScore");
        txtScore.text = "HiScore: " + HiScore.ToString();
        Time.timeScale = 0; // para o jogo começar pausado
    }
    //A função Update é chamada a cada frame
    void Update()
    //Região do código para o movimento
    #region Movement 
    {
        if (Input.GetKeyDown(KeyCode.W)) //Teste logico para saber a entrada com o valor W para movimento para CIMA
        {
            moveDirection = Direction.UP;
        }
        if (Input.GetKeyDown(KeyCode.A)) //Teste logico para saber a entrada com o valor A para movimento para ESQUERDA
        {
            moveDirection = Direction.LEFT;
        }
        if (Input.GetKeyDown(KeyCode.S)) //Teste logico para saber a entrada com o valor S para movimento para BAIXO
        {
            moveDirection = Direction.DOWN;
        }
        if (Input.GetKeyDown(KeyCode.D)) //Teste logico para saber a entrada com o valor D para movimento para DIREITA
        {
            moveDirection = Direction.RIGHT;
        }
        #endregion  //Região do código para o movimento


    }
    IEnumerator moveSnake()
    {
        yield return new WaitForSeconds(delayStep); //Esperar o tempo a determinar para executar
        Vector3 nexPos = Vector3.zero; //Vetor para definir para onde irei me mover e começa em zero
        switch (moveDirection) //realizará varios testes condicionais quando estamos a procura de um valor unico
        {
            //valores unicos
            case Direction.DOWN:
                nexPos = Vector3.down;
                Head.rotation = Quaternion.Euler(0, 0, 90); //ângulos para a rotação da cabeça em sentido a direção que a cobra segue.
                break;

            case Direction.LEFT:
                nexPos = Vector3.left;
                Head.rotation = Quaternion.Euler(0, 0, 0); //ângulos para a rotação da cabeça em sentido a direção que a cobra segue.
                break;

            case Direction.RIGHT:
                nexPos = Vector3.right;
                Head.rotation = Quaternion.Euler(0, 0, 180); //ângulos para a rotação da cabeça em sentido a direção que a cobra segue.
                break;

            case Direction.UP:
                nexPos = Vector3.up;
                Head.rotation = Quaternion.Euler(0, 0, -90); //ângulos para a rotação da cabeça em sentido a direção que a cobra segue.
                break;
        }
        nexPos *= step; //pega o valor e multiplica pelo indicado a cada tecla
        lasPos = Head.position;
        Head.position += nexPos; //irá pegar a posição que tem e somar pela seguinte

        foreach(Transform t in Tail)
        {
            Vector3 temp = t.position; //substitui as posições do objeto da frente
            t.position = lasPos;
            lasPos = temp;
            t.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

        StartCoroutine("moveSnake");
    }

    public void Eat()
    {
        // Posição da última parte da cobra
        Vector3 tailPosition = Head.position;

        if(Tail.Count > 0)
        {
            /* Teste para o programa conferir se existe algum rabo junto à cabeça
               Caso este seja falso, é inserido o ultimo elemento da lista de corpos */
            tailPosition = Tail[Tail.Count - 1].position; 
        }

        GameObject temp = Instantiate(tailPrefab, tailPosition, transform.localRotation);
        // Ao entrar em contacto com a cobra, será adicionado um prefab do rabo ligado ao corpo da cobra
        Tail.Add(temp.transform);
        score += 1;
        txtScore.text = "Score: " + score.ToString();
        SetFood();
    }

    void SetFood()
    {
        int x = Random.Range((col - 1) / 2 * - 1, (col - 1) / 2); // Determina o valor mínimo e máximo
        int y = Random.Range((row - 1) / 2 * -1, (row - 1) / 2); // Determina o valor mínimo e máximo
        food.position = new Vector2(x * step, y * step);
    }

    public void gameOver()
    {
        panelGameOver.SetActive(true); // Ao perder será aberto a tela de game over
        Time.timeScale = 0;
        if (score > HiScore) // Função que irá conferir se o score do jogo atual é maior que o do jogo anterior
        {
            // Caso seja, esta funcão irá guardar um novo recorde
            PlayerPrefs.SetInt("HiScore", score);
            HiScore = score;
            txtHiScore.text = "New Hi-Score: " + score.ToString();
        }
    }

    public void Jogar()
    {
        // Todas as seguintes funções resetam os valores que tem o jogo, menos o recorde
        Head.position = Vector3.zero;
        moveDirection = Direction.LEFT; // Posição de reinício do jogo

        foreach (Transform t in Tail)
        {
            Destroy(t.gameObject); // Destroi os rabos existentes no jogo
        }

        Tail.Clear(); // Zera os valores da cobra
        Head.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        SetFood();
        score = 0;
        txtScore.text = "Score: 0"; 
        txtHiScore.text = "Hi-Score: " + HiScore.ToString(); // Guarda o novo score após o reinício do jogo
        panelGameOver.SetActive(false);
        panelTitle.SetActive(false);
        Time.timeScale = 1; 
    }
}