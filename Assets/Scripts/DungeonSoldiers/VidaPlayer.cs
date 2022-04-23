using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VidaPlayer : MonoBehaviour
{
    // Variável para o "Slider" da barra de vida do jogador
    public Slider barraDeVidaJogador;
    // Variável com a vida máxima do jogador
    public int vidaMaxima;
    // Variável com a vida atual do jogador
    public int vidaAtual;

    // A função "Start" é chamada antes da atualização do primeiro frame
    void Start()
    {
        // Função para maximizar a vida do jogador
        vidaAtual = vidaMaxima;
        // Define o valor máximo da barra de vida do jogador
        barraDeVidaJogador.maxValue = vidaMaxima;
        // Atualiza a barra de vida com a vida atual do jogador
        barraDeVidaJogador.value = vidaAtual; 
    }

    // Função pra dar dano ao jogador
    public void ReceberDano(int danoParaReceber)
    {
        // Tira uma porção de vida ao jogador
        vidaAtual -= danoParaReceber;
        // Atualiza a barra de vida com a vida atual do jogador
        barraDeVidaJogador.value = vidaAtual;

        // Verifica se o jogador morreu
        if (vidaAtual <= 0)
            // Caso tenha, o jogador será reiniciado
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Função pra dar vida ao player quando toca no "prefab" vida
    public void ReceberVida(int vidaParaDar) 
    {
        // Verifica se a vida nova excede o valor máximo
        if (vidaAtual + vidaParaDar > vidaMaxima)
        {
            // Se exceder, o valor será diminuido para chegar à vida máxima
            vidaParaDar = vidaMaxima - vidaAtual;
        }

        // Dá uma porção de vida ao jogador
        vidaAtual += vidaParaDar;
        // Atualiza a barra de vida com a vida atual do jogador
        barraDeVidaJogador.value = vidaAtual;
    }
}
