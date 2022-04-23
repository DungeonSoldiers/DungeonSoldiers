using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{
    // Vari�vel com o componente "Animator"
    public Animator anim;
    // Vari�vel com o bot�o de escolha da pistola
    public GameObject gunButton;
    // Vari�vel com o bot�o de escolha da espada
    public GameObject swordButton;
    // Vari�vel com o menu de escolha do minijogo
    public GameObject chooseGameCanvas;
    // Vari�vel com o bot�o de escolha do minijogo Pacman
    public GameObject pacButton;
    // Vari�vel com o bot�o de escolha do minijogo Snake
    public GameObject snakeButton;
    // Vari�vel com o bot�o de escolha do minijogo Tetris
    public GameObject tetrisButton;
    // Vari�vel com o bot�o de voltar para o bar
    public GameObject returnButton;
    // Vari�vel com o menu principal
    public GameObject menuPrincipal;
    // Vari�vel com o menu de controlos
    public GameObject menuControlos;
    // Vari�vel com o menu de cr�ditos
    public GameObject menuCreditos;
    // Vari�vel com o menu de pausa
    public GameObject menuPausa;
    // Vari�vel com o menu de escolha da arma
    public GameObject weaponChoose;
    // Vari�vel com o menu final da masmorra
    public GameObject finalChoice;
    // Vari�vel com o canvas da mensagem final
    public GameObject lastMsg;

    // A fun��o � chamada a cada frame
    void Update()
    {
        /* Verifica se o utilizador pressionou a tecla de "Escape"
         * Tamb�m ir� verificar se existe o menu pausa na cena */
        if (Input.GetKeyDown(KeyCode.Escape) && menuPausa != null)
        {
            // Caso ambos sejam verdade, o c�digo ir� verificar se � para pausar o jogo ou n�o
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

    // Fun��o que ser� executada ap�s a mensagem
    private void afterMsg()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    // Vari�vel para mostrar uma mensagem de agradecimento
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

    // Fun��o para abrir o menu de cr�ditos
    public void openCredits()
    {
        menuPrincipal.SetActive(false);
        menuCreditos.SetActive(true);
    }

    // Fun��o para fechar o menu de cr�ditos
    public void closeCredits()
    {
        menuPrincipal.SetActive(true);
        menuCreditos.SetActive(false);
    }

    // Fun��o para escolher o bar
    public void chooseBar()
    {
        // Grava a op��o
        PlayerPrefs.SetString("finalChoice", "Bar");
        // Destr�i o menu
        Destroy(finalChoice);
        // Faz o "fade out"
        PlayFadeOut();
    }

    // Fun��o para escolher os jogos
    public void chooseGames()
    {
        // Grava a op��o
        PlayerPrefs.SetString("finalChoice", "Games");
        // Destr�i o menu
        Destroy(finalChoice);
        // Faz o "fade out"
        PlayFadeOut();
    }

    // Fun��o para o "Fade Out"
    public void fadeFunction()
    {
        if (SceneManager.GetActiveScene().name == "MenuPrincipal")
            // Se o "fade out" acontecer no menu principal, o bar ser� carregado
            SceneManager.LoadScene("Bar");
        else if (SceneManager.GetActiveScene().name == "Bar")
            // Se o "fade out" acontecer no bar, o menu de escolha da arma ir� aparecer
            weaponChoose.SetActive(true);
        else if (SceneManager.GetActiveScene().name == "DungeonSoldiers")
            // Se o "fade out" acontecer na masmorra, ir� fazer a a��o que o jogador pediu
            if (PlayerPrefs.GetString("finalChoice") == "Bar")
                // Se o jogador escolheu voltar, o bar ser� recarregado
                SceneManager.LoadScene("Final");
            else
                // Caso contr�rio, o menu de escolha do minijogo ser� aberto
                chooseGameCanvas.SetActive(true);
        else if (SceneManager.GetActiveScene().name == "ChooseGame")
            // Se o jogador escolher voltar no menu dos minijogos, o bar ser� recarregado
            SceneManager.LoadScene("Final");
        else if (SceneManager.GetActiveScene().name == "Final")
            // Caso o "fade out" aconte�a no final, uma mensagem de agradecimento ir� aparecer
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

    // Fun��o para voltar ao menu principal
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
        /* Verifica se o jogador est� no menu principal ou no menu de pausa
         * Esta fun��o simplesmente ir� desativar o menu correto */
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
        /* Verifica se o jogador est� no menu principal ou no menu de pausa
         * Esta fun��o simplesmente ir� ativar o menu correto */
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

    // Fun��o para fazer o "Fade Out"
    public void PlayFadeOut()
    {
        anim.SetTrigger("startGame");
    }

    // Fun��o para carregar o jogo principal
    private void teleport()
    {
        SceneManager.LoadScene("DungeonSoldiers");
    }

    // Fun��es em rela��o ao menu de escolha do minijogo
    // Fun��o para fazer o "fade out" de um bot�o
    private IEnumerator buttonDissapear(GameObject button)
    {
        // Utiliza um ciclo para verificar se o bot�o est� vis�vel ou invis�vel
        while (button.GetComponent<Image>().color.a > 0)
        {
            // Obt�m todos os componentes "Image" do bot�o
            for (int i = 0; i < button.GetComponentsInChildren<Image>().Length; i++)
                // Aplica os novos valores a cada um deles
                button.GetComponentsInChildren<Image>()[i].color -= new Color(0, 0, 0, 0.05f);

            // Espera 0.05 segundos
            yield return new WaitForSeconds(0.05f);
        }

        // Quando o ciclo tiver terminado, o bot�o ser� removido da cena
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
        // Desativa ambos os bot�es
        Destroy(pacButton.GetComponent<Button>());
        Destroy(snakeButton.GetComponent<Button>());
        Destroy(tetrisButton.GetComponent<Button>());
        if (returnButton != null) Destroy(returnButton);
        // Faz o "fade out" nas outras op��es
        StartCoroutine(buttonDissapear(snakeButton));
        StartCoroutine(buttonDissapear(tetrisButton));
        LeanTween.moveX(pacButton, 960, 1).setEase(LeanTweenType.easeOutSine).setDelay(1).setOnComplete(quitPac);
    }

    public void selectSnake()
    {
        // Desativa ambos os bot�es
        Destroy(pacButton.GetComponent<Button>());
        Destroy(snakeButton.GetComponent<Button>());
        Destroy(tetrisButton.GetComponent<Button>());
        if (returnButton != null) Destroy(returnButton);
        // Faz o "fade out" nas outras op��es
        StartCoroutine(buttonDissapear(pacButton));
        StartCoroutine(buttonDissapear(tetrisButton));
        StartCoroutine(waitOneSec());
    }

    public void selectTetris()
    {
        // Desativa ambos os bot�es
        Destroy(pacButton.GetComponent<Button>());
        Destroy(snakeButton.GetComponent<Button>());
        Destroy(tetrisButton.GetComponent<Button>());
        if (returnButton != null) Destroy(returnButton);
        // Faz o "fade out" nas outras op��es
        StartCoroutine(buttonDissapear(snakeButton));
        StartCoroutine(buttonDissapear(pacButton));
        LeanTween.moveX(tetrisButton, 960, 1).setEase(LeanTweenType.easeOutSine).setDelay(1).setOnComplete(quitTetris);
    }

    public void selectQuit()
    {
        // Desativa ambos os bot�es
        Destroy(pacButton.GetComponent<Button>());
        Destroy(snakeButton.GetComponent<Button>());
        Destroy(tetrisButton.GetComponent<Button>());
        Destroy(returnButton.GetComponent<Button>());
        // Faz o "fade out"
        PlayFadeOut();
    }

    // Fun��es em rela��o ao menu de escolha da arma
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
        // Desativa ambos os bot�es
        Destroy(gunButton.GetComponent<Button>());
        Destroy(swordButton.GetComponent<Button>());
        // Define a pistola como a arma escolhida
        PlayerPrefs.SetString("Weapon", "Gun");
        LeanTween.moveX(swordButton, -150, 1).setEase(LeanTweenType.easeInSine).setOnComplete(tweenGunMiddle);
    }

    public void selectSword()
    {
        // Desativa ambos os bot�es
        Destroy(gunButton.GetComponent<Button>());
        Destroy(swordButton.GetComponent<Button>());
        // Define a espada como a arma escolhida
        PlayerPrefs.SetString("Weapon", "Sword");
        LeanTween.moveX(gunButton, 2070, 1).setEase(LeanTweenType.easeInSine).setOnComplete(tweenSwordMiddle);
    }
}