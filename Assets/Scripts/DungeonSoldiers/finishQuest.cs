using System.Collections;
using UnityEngine;
using TMPro;

public class finishQuest : MonoBehaviour
{
    // Variável com o diálogo
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    // Variável com a câmara
    public GameObject Camera;
    // Variável com o canvas que indica a missão
    public GameObject MissaoCanvas;
    // Variável com a caixa de diálogo
    public GameObject chooseDialog;
    // Variável com as opções
    public GameObject chooseOptions;
    // Variável com o índice do diálogo
    private int lineIndex = 0;
    // Variável com o tempo de escrita
    private float typingTime = 0.025f;

    // Função para demonstrar uma frase
    private IEnumerator ShowLine()
    {
        // Retira o texto da caixa de diálogo
        chooseDialog.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;

        // Utiliza um ciclo para escrever a frase letra a letra
        foreach (char ch in dialogueLines[lineIndex])
        {
            // Adiciona uma letra
            chooseDialog.GetComponentInChildren<TextMeshProUGUI>().text += ch;
            // Espera x segundos indicado na variável "typingTime"
            yield return new WaitForSeconds(typingTime);
        }
    }

    // A função é chamada a cada frame
    void Update()
    {
        // Verifica se a ultima frase foi concluída
        if (chooseDialog.GetComponentInChildren<TextMeshProUGUI>().text == dialogueLines[1])
        {
            // Caso seja verdade, as opções iram aparecer
            chooseOptions.SetActive(true);
            // O "script" será desativado
            Destroy(this);
        }

        // Avança o diálogo se o jogador clicar com o botão esquerdo do rato
        if (Input.GetButtonDown("Fire1") && Time.timeScale == 1 && lineIndex == 0 && chooseDialog.GetComponentInChildren<TextMeshProUGUI>().text == dialogueLines[0])
        {
            // Atualiza o índice
            lineIndex++;
            // Prosegue para a próxima fala
            StartCoroutine(ShowLine());
        }
    }

    // Função para começar o diálogo
    private void StartDialogue()
    {
        // Ativa o painél de diálogo
        chooseDialog.SetActive(true);
        // Faz aparecer o diálogo na caixa
        StartCoroutine(ShowLine());
    }

    // Deteta se algum objeto entrou em colisão
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Deteta se o objeto que colidiu foi o jogador
        if (col.gameObject.tag.Equals("Player"))
        {
            // Para o jogador
            col.gameObject.GetComponent<PlayerMovement>().speed = 0;
            // Destroi o "CameraController"
            Destroy(Camera.GetComponent<CameraController>());
            // Destrói o BoxCollider2D
            Destroy(GetComponent<BoxCollider2D>());
            // Destrói o canvas que indica a missão
            Destroy(MissaoCanvas);
            // Move a câmara
            LeanTween.moveX(Camera, 135, 1).setEase(LeanTweenType.easeOutSine).setDelay(0.2f).setOnComplete(StartDialogue);
        }
    }
}
