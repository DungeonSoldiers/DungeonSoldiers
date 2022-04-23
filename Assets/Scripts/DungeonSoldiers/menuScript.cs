using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{
    // Variável com o componente "Animator"
    public Animator anim;
    // Variável com o botão de escolha da pistola
    public GameObject gunButton;
    // Variável com o botão de escolha da espada
    public GameObject swordButton;
    // Variável com o menu de escolha do minijogo
    public GameObject chooseGameCanvas;
    // Variável com o botão de escolha do minijogo Pacman
    public GameObject pacButton;
    // Variável com o botão de escolha do minijogo Snake
    public GameObject snakeButton;
    // Variável com o botão de escolha do minijogo Tetris
    public GameObject tetrisButton;
    // Variável com o botão de voltar para o bar
    public GameObject returnButton;
    // Variável com o menu principal
    public GameObject menuPrincipal;
    // Variável com o menu de controlos
    public GameObject menuControlos;
    // Variável com o menu de créditos
    public GameObject menuCreditos;
    // Variável com o menu de pausa
    public GameObject menuPausa;
    // Variável com o menu de escolha da arma
    public GameObject weaponChoose;
    // Variável com o menu final da masmorra
    public GameObject finalChoice;
    // Variável com o canvas da mensagem final
    public GameObject lastMsg;

    // A função é chamada a cada frame
    void Update()
    {
        /* Verifica se o utilizador pressionou a tecla de "Escape"
         * Também irá verificar se existe o menu pausa na cena */
        if (Input.GetKeyDown(KeyCode.Escape) && menuPausa != null)
        {
            // Caso ambos sejam verdade, o código irá verificar se é para pausar o jogo ou não
            if (Time.timeScale == 1)
            {
                // Ativa o menu pausa
                menuPausa.SetActive(true);
                // Pausa o jogo
                Time.timeScale = 0;
            }
            else
            {
                // Desativa o menu pausa utilizando um loop
                foreach (Transform ui in transform)
                    ui.gameObject.SetActive(false);

                // Continua o jogo
                Time.timeScale = 1;
            }
        }
    }

    // Função que será executada após a mensagem
    private void afterMsg()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    // Variável para mostrar uma mensagem de agradecimento
    public void showFinalMsg()
    {
        // Ativa o canvas da mensagem
        lastMsg.SetActive(true);
        // Utiliza "tween" para ir para o centro da tela
        LeanTween.moveY(lastMsg.GetComponentInChildren<Text>().gameObject, 540, 1).setEase(LeanTweenType.easeInSine).setDelay(1);
        // Utiliza "tween" para ir para baixo
        LeanTween.moveY(lastMsg.GetComponentInChildren<Text>().gameObject, -60, 1).setEase(LeanTweenType.easeInSine).setDelay(4).setOnComplete(afterMsg);
    }

    public void VoltarMenuJogos()
    {
        // Continua o jogo
        Time.timeScale = 1;
        // Carrega a cena para escolher o jogo
        SceneManager.LoadScene("ChooseGame");
    }

    // Função para abrir o menu de créditos
    public void openCredits()
    {
        menuPrincipal.SetActive(false);
        menuCreditos.SetActive(true);
    }

    // Função para fechar o menu de créditos
    public void closeCredits()
    {
        menuPrincipal.SetActive(true);
        menuCreditos.SetActive(false);
    }

    // Função para escolher o bar
    public void chooseBar()
    {
        // Grava a opção
        PlayerPrefs.SetString("finalChoice", "Bar");
        // Destrói o menu
        Destroy(finalChoice);
        // Faz o "fade out"
        PlayFadeOut();
    }

    // Função para escolher os jogos
    public void chooseGames()
    {
        // Grava a opção
        PlayerPrefs.SetString("finalChoice", "Games");
        // Destrói o menu
        Destroy(finalChoice);
        // Faz o "fade out"
        PlayFadeOut();
    }

    // Função para o "Fade Out"
    public void fadeFunction()
    {
        if (SceneManager.GetActiveScene().name == "MenuPrincipal")
            // Se o "fade out" acontecer no menu principal, o bar será carregado
            SceneManager.LoadScene("Bar");
        else if (SceneManager.GetActiveScene().name == "Bar")
            // Se o "fade out" acontecer no bar, o menu de escolha da arma irá aparecer
            weaponChoose.SetActive(true);
        else if (SceneManager.GetActiveScene().name == "DungeonSoldiers")
            // Se o "fade out" acontecer na masmorra, irá fazer a ação que o jogador pediu
            if (PlayerPrefs.GetString("finalChoice") == "Bar")
                // Se o jogador escolheu voltar, o bar será recarregado
                SceneManager.LoadScene("Final");
            else
                // Caso contrário, o menu de escolha do minijogo será aberto
                chooseGameCanvas.SetActive(true);
        else if (SceneManager.GetActiveScene().name == "ChooseGame")
            // Se o jogador escolher voltar no menu dos minijogos, o bar será recarregado
            SceneManager.LoadScene("Final");
        else if (SceneManager.GetActiveScene().name == "Final")
            // Caso o "fade out" aconteça no final, uma mensagem de agradecimento irá aparecer
            showFinalMsg();
    }

    // Retira o menu pausa
    public void Retornar()
    {
        // Desativa o menu pausa
        menuPausa.SetActive(false);
        // Continua o jogo
        Time.timeScale = 1;
    }

    // Função para voltar ao menu principal
    public void voltarMenuPrincipal()
    {
        // Continua o jogo
        Time.timeScale = 1;
        // Muda para a cena do menu principal
        SceneManager.LoadScene("MenuPrincipal");
    }

    // Abre o menu de controlos
    public void abrirMenuControlos()
    {
        /* Verifica se o jogador está no menu principal ou no menu de pausa
         * Esta função simplesmente irá desativar o menu correto */
        if (menuPrincipal != null)
            menuPrincipal.SetActive(false);
        else
            menuPausa.SetActive(false);

        // Ativa o menu controlos
        menuControlos.SetActive(true);
    }

    // Fecha o menu de controlos
    public void fecharMenuControlos()
    {
        /* Verifica se o jogador está no menu principal ou no menu de pausa
         * Esta função simplesmente irá ativar o menu correto */
        if (menuPrincipal != null)
            menuPrincipal.SetActive(true);
        else
            menuPausa.SetActive(true);

        // Desativa o menu de controlos
        menuControlos.SetActive(false);
    }

    // Direcionar o jogador para o Youtube do Jogo
    public void openYoutube()
    {
        Application.OpenURL("https://www.youtube.com/channel/UCbA_fhDCa_dJqQyt-XPT3fw");
    }

    // Direcionar o jogador para o Twitter do Jogo
    public void openTwitter()
    {
        Application.OpenURL("https://twitter.com/dungeonsoldiers");
    }

    // Direcionar o jogador para o Instagram do Jogo
    public void openInstagram()
    {
        Application.OpenURL("https://www.instagram.com/dungeonsoldiers/");
    }

    // Função para fazer o "Fade Out"
    public void PlayFadeOut()
    {
        anim.SetTrigger("startGame");
    }

    // Função para carregar o jogo principal
    private void teleport()
    {
        SceneManager.LoadScene("DungeonSoldiers");
    }

    // Funções em relação ao menu de escolha do minijogo
    // Função para fazer o "fade out" de um botão
    private IEnumerator buttonDissapear(GameObject button)
    {
        // Utiliza um ciclo para verificar se o botão está visível ou invisível
        while (button.GetComponent<Image>().color.a > 0)
        {
            // Obtêm todos os componentes "Image" do botão
            for (int i = 0; i < button.GetComponentsInChildren<Image>().Length; i++)
                // Aplica os novos valores a cada um deles
                button.GetComponentsInChildren<Image>()[i].color -= new Color(0, 0, 0, 0.05f);

            // Espera 0.05 segundos
            yield return new WaitForSeconds(0.05f);
        }

        // Quando o ciclo tiver terminado, o botão será removido da cena
        Destroy(button);
    }

    private IEnumerator waitOneSec()
    {
        yield return new WaitForSeconds(1);
        quitSnake();
    }

    private void Pacman()
    {
        SceneManager.LoadScene("Pacman");
    }

    private void Snake()
    {
        SceneManager.LoadScene("SnakeGame");
    }

    private void Tetris()
    {
        SceneManager.LoadScene("Tetris");
    }

    private void quitPac()
    {
        LeanTween.moveY(GetComponentInChildren<Text>().gameObject, 1240, 1).setEase(LeanTweenType.easeInSine);
        LeanTween.moveY(pacButton, -160, 1).setEase(LeanTweenType.easeInSine).setOnComplete(Pacman);
    }

    private void quitSnake()
    {
        LeanTween.moveY(GetComponentInChildren<Text>().gameObject, 1240, 1).setEase(LeanTweenType.easeInSine);
        LeanTween.moveY(snakeButton, -160, 1).setEase(LeanTweenType.easeInSine).setOnComplete(Snake);
    }

    private void quitTetris()
    {
        LeanTween.moveY(GetComponentInChildren<Text>().gameObject, 1240, 1).setEase(LeanTweenType.easeInSine);
        LeanTween.moveY(tetrisButton, -160, 1).setEase(LeanTweenType.easeInSine).setOnComplete(Tetris);
    }

    public void selectPac()
    {
        // Desativa ambos os botões
        Destroy(pacButton.GetComponent<Button>());
        Destroy(snakeButton.GetComponent<Button>());
        Destroy(tetrisButton.GetComponent<Button>());
        if (returnButton != null) Destroy(returnButton);
        // Faz o "fade out" nas outras opções
        StartCoroutine(buttonDissapear(snakeButton));
        StartCoroutine(buttonDissapear(tetrisButton));
        LeanTween.moveX(pacButton, 960, 1).setEase(LeanTweenType.easeOutSine).setDelay(1).setOnComplete(quitPac);
    }

    public void selectSnake()
    {
        // Desativa ambos os botões
        Destroy(pacButton.GetComponent<Button>());
        Destroy(snakeButton.GetComponent<Button>());
        Destroy(tetrisButton.GetComponent<Button>());
        if (returnButton != null) Destroy(returnButton);
        // Faz o "fade out" nas outras opções
        StartCoroutine(buttonDissapear(pacButton));
        StartCoroutine(buttonDissapear(tetrisButton));
        StartCoroutine(waitOneSec());
    }

    public void selectTetris()
    {
        // Desativa ambos os botões
        Destroy(pacButton.GetComponent<Button>());
        Destroy(snakeButton.GetComponent<Button>());
        Destroy(tetrisButton.GetComponent<Button>());
        if (returnButton != null) Destroy(returnButton);
        // Faz o "fade out" nas outras opções
        StartCoroutine(buttonDissapear(snakeButton));
        StartCoroutine(buttonDissapear(pacButton));
        LeanTween.moveX(tetrisButton, 960, 1).setEase(LeanTweenType.easeOutSine).setDelay(1).setOnComplete(quitTetris);
    }

    public void selectQuit()
    {
        // Desativa ambos os botões
        Destroy(pacButton.GetComponent<Button>());
        Destroy(snakeButton.GetComponent<Button>());
        Destroy(tetrisButton.GetComponent<Button>());
        Destroy(returnButton.GetComponent<Button>());
        // Faz o "fade out"
        PlayFadeOut();
    }

    // Funções em relação ao menu de escolha da arma
    private void quitWithGun()
    {
        LeanTween.moveY(GetComponentInChildren<Text>().gameObject, 1240, 1).setEase(LeanTweenType.easeInSine);
        LeanTween.moveY(gunButton, -160, 1).setEase(LeanTweenType.easeInSine).setOnComplete(teleport);
    }

    private void quitWithSword()
    {
        LeanTween.moveY(GetComponentInChildren<Text>().gameObject, 1240, 1).setEase(LeanTweenType.easeInSine);
        LeanTween.moveY(swordButton, -160, 1).setEase(LeanTweenType.easeInSine).setOnComplete(teleport);
    }

    private void tweenGunMiddle()
    {
        LeanTween.moveX(gunButton, 960, 1).setEase(LeanTweenType.easeOutSine).setOnComplete(quitWithGun);
    }

    private void tweenSwordMiddle()
    {
        LeanTween.moveX(swordButton, 960, 1).setEase(LeanTweenType.easeOutSine).setOnComplete(quitWithSword);
    }

    public void selectGun()
    {
        // Desativa ambos os botões
        Destroy(gunButton.GetComponent<Button>());
        Destroy(swordButton.GetComponent<Button>());
        // Define a pistola como a arma escolhida
        PlayerPrefs.SetString("Weapon", "Gun");
        LeanTween.moveX(swordButton, -150, 1).setEase(LeanTweenType.easeInSine).setOnComplete(tweenGunMiddle);
    }

    public void selectSword()
    {
        // Desativa ambos os botões
        Destroy(gunButton.GetComponent<Button>());
        Destroy(swordButton.GetComponent<Button>());
        // Define a espada como a arma escolhida
        PlayerPrefs.SetString("Weapon", "Sword");
        LeanTween.moveX(gunButton, 2070, 1).setEase(LeanTweenType.easeInSine).setOnComplete(tweenSwordMiddle);
    }
}