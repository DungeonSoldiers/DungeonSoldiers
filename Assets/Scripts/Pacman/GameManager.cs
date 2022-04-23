using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts; 
    public Pacman pacman; 
    public Transform pellets; 

    public Text gameOverText; // declara��o usada para o texto GameOver
    public Text scoreText; // declara��o da variavel para o testo de score
    public Text livesText; // declara��o da variavel para o texto de vidas

    public int ghostMultiplier { get; private set; } = 1; //
    public int score { get; private set; } //
    public int lives { get; private set; } //

    private void Start() // funcao que come�a um novo jogo assim que o programa � executado
    {
        NewGame(); //chamar a fun��o "NewGame()"
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKey)
        {
            NewGame();
        }
    }

    private void NewGame() //funcao usada para come�ar um novo jogo
    {
        SetScore(0); //resetar o score
        SetLives(3); //resetar o numero de vidas para a quantidade inicial
        NewRound(); //come�ar nova ronda
    }

    private void NewRound() //funcao usada para come�ar nova ronda
    {
        gameOverText.enabled = false; //desativar o texto de GameOver

        foreach (Transform pellet in pellets)
        {
            pellet.gameObject.SetActive(true); //ativar todas as pellets existente na ronda
        }

        ResetState(); //chamar funcao "ResetState()"
    }

    private void ResetState() //funcao usada para resetar o estado do jogo
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].ResetState(); //resetar o estado para todos os Ghosts existente no jogo
        }
        SetScore(0); //setar o score para 0
        pacman.ResetState(); //resetar a posicao do Pac-Man
    }

    private void GameOver() //funcao usada 
    {
        gameOverText.enabled = true; //ativar o texto do GameOver

        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(false); //desativar todos os Ghosts existentes
        }

        pacman.gameObject.SetActive(false); //desativar o Pac-Man
    }

    private void SetLives(int lives) //funcao usada para declarar a quantidade de vidas
    {
        this.lives = lives; //setar a vida ganha ou perdida ao total
        livesText.text = "x" + lives.ToString();
    }

    private void SetScore(int score) //funcao usada para setar o Score
    {
        this.score = score; //setar o score ganho ao total
        scoreText.text = score.ToString().PadLeft(2, '0');
    }

    public void PacmanEaten() //funcao usada para quando o PacMan morre
    {
        pacman.DeathSequence(); //comecar a anima��o do PacMan

        SetLives(lives - 1); //retirar uma vida a quantidade das vidas totais quando o PacMan morre

        if (lives > 0)
        { //se  o numero de vidas for menor
            Invoke(nameof(ResetState), 3f); //invokar a funcao "ResetState" resetando tudo e chamar a fun��o GameOver
        }
        else
        {
            GameOver(); //chamar funcao "GameOver"
        }
    }

    public void GhostEaten(Ghost ghost) //funcao usada para adicionar pontos e diminuir o numero de Ghost asssim que algum � comido
    {
        int points = ghost.points * ghostMultiplier; //
        SetScore(score + points); //setar o score total conforme a quantidade que cada Ghost da

        ghostMultiplier++;
    }

    public void PelletEaten(Pellet pellet) //funcao usada para adicionar pontos e diminuir a quantidade de Pellets existentes no jogo
    {
        pellet.gameObject.SetActive(false); //se a Pellet for comida pelo Pac-man

        SetScore(score + pellet.points); //somar a quantidade de pontos que cada Ghost da 

        if (!HasRemainingPellets()) //se ainda existir "pellets" no jogo , desativar o PacMan e come�ar uma nova ronda.
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f); //invocar a funcao "NewRound" depois de 3seg 
        }
    }

    public void PowerPelletEaten(PowerPellet pellet) //funcao usada para adicionar pontos e diminuir a quantidade de PowerPellets existentes no jogo
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke(nameof(ResetGhostMultiplier));
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }

}