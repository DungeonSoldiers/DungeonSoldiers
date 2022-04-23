using System.Collections;
using UnityEngine;
using TMPro;

public class finishQuest : MonoBehaviour
{
    // Vari�vel com o di�logo
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    // Vari�vel com a c�mara
    public GameObject Camera;
    // Vari�vel com o canvas que indica a miss�o
    public GameObject MissaoCanvas;
    // Vari�vel com a caixa de di�logo
    public GameObject chooseDialog;
    // Vari�vel com as op��es
    public GameObject chooseOptions;
    // Vari�vel com o �ndice do di�logo
    private int lineIndex = 0;
    // Vari�vel com o tempo de escrita
    private float typingTime = 0.025f;

    // Fun��o para demonstrar uma frase
    private IEnumerator ShowLine()
    {
        // Retira o texto da caixa de di�logo
        chooseDialog.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;

        // Utiliza um ciclo para escrever a frase letra a letra
        foreach (char ch in dialogueLines[lineIndex])
        {
            // Adiciona uma letra
            chooseDialog.GetComponentInChildren<TextMeshProUGUI>().text += ch;
            // Espera x segundos indicado na vari�vel "typingTime"
            yield return new WaitForSeconds(typingTime);
        }
    }

    // A fun��o � chamada a cada frame
    void Update()
    {
        // Verifica se a ultima frase foi conclu�da
        if (chooseDialog.GetComponentInChildren<TextMeshProUGUI>().text == dialogueLines[1])
        {
            // Caso seja verdade, as op��es iram aparecer
            chooseOptions.SetActive(true);
            // O "script" ser� desativado
            Destroy(this);
        }

        // Avan�a o di�logo se o jogador clicar com o bot�o esquerdo do rato
        if (Input.GetButtonDown("Fire1") && Time.timeScale == 1 && lineIndex == 0 && chooseDialog.GetComponentInChildren<TextMeshProUGUI>().text == dialogueLines[0])
        {
            // Atualiza o �ndice
            lineIndex++;
            // Prosegue para a pr�xima fala
            StartCoroutine(ShowLine());
        }
    }

    // Fun��o para come�ar o di�logo
    private void StartDialogue()
    {
        // Ativa o pain�l de di�logo
        chooseDialog.SetActive(true);
        // Faz aparecer o di�logo na caixa
        StartCoroutine(ShowLine());
    }

    // Deteta se algum objeto entrou em colis�o
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Deteta se o objeto que colidiu foi o jogador
        if (col.gameObject.tag.Equals("Player"))
        {
            // Para o jogador
            col.gameObject.GetComponent<PlayerMovement>().speed = 0;
            // Destroi o "CameraController"
            Destroy(Camera.GetComponent<CameraController>());
            // Destr�i o BoxCollider2D
            Destroy(GetComponent<BoxCollider2D>());
            // Destr�i o canvas que indica a miss�o
            Destroy(MissaoCanvas);
            // Move a c�mara
            LeanTween.moveX(Camera, 135, 1).setEase(LeanTweenType.easeOutSine).setDelay(0.2f).setOnComplete(StartDialogue);
        }
    }
}
