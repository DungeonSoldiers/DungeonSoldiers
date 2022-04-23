using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class automatedDialog : MonoBehaviour
{
    // Variável com o diálogo
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    // Variável com o canvas que indica a missão
    [SerializeField] private GameObject CanvasMissao;
    // Variável com a caixa de diálogo
    [SerializeField] private GameObject dialoguePanel;
    // Variável com o texto da caixa de diálogo
    [SerializeField] private TMP_Text dialogueText;
    // Variável com a nova missão após o diálogo
    public string missaoAposFala;
    // Variável lógica que indica se o jogador já falou com o "NPC" ou não
    private bool didDialogueStart = false;
    // Variável com o índice do diálogo
    private int lineIndex;
    // Variável com o tempo de escrita
    private float typingTime = 0.025f;
    // Variável que controla a velocidade do jogador
    private PlayerMovement movimentacao;
    // Variável com o "fade in"
    public Image fadeIn;
    // Variável com o zoneManager
    public zoneManager zManager;

    // A função é chamada antes da atualização do primeiro frame
    private void Start()
    {
        // Obtêm o "script" relacionado à movimentação do jogador
        movimentacao = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // A função é chamada a cada frame
    void Update()
    {
        // Verifica se o "fade in" já acabou e se o diálogo não começou
        if (fadeIn.color.a == 0 && !didDialogueStart)
            // Caso verdadeiro, o diálogo irá começar
            StartDialogue();

        // Avança o diálogo se o jogador clicar com o botão esquerdo do rato
        if (Input.GetButtonDown("Fire1") && didDialogueStart && Time.timeScale == 1 && dialogueText.text == dialogueLines[lineIndex])
            NextDialogueLine();
    }

    // Função para a próxima fala do "NPC"
    private void NextDialogueLine()
    {
        // Atualiza o índice
        lineIndex++;

        // Verifica se ainda falta diálogo
        if (lineIndex < dialogueLines.Length)
            // Caso falte, este irá proseguir para a próxima fala
            StartCoroutine(ShowLine());
        // Caso contrário, o diálogo irá encerrar
        else
        {
            // Aplica velocidade ao jogador para que ele possa mover de novo
            movimentacao.speed = 8;
            // Desativa a caixa de diálogo
            dialoguePanel.SetActive(false);
            // Ativa o canvas que indica a missão
            CanvasMissao.SetActive(true);
            // Atualiza a missão
            CanvasMissao.GetComponentInChildren<TextMeshProUGUI>().text = missaoAposFala;
            // Destroi a borda entre o inicio da masmorra e a primeira zona
            zManager.ClearBorder(1);
            // Destrui o "NPC"
            Destroy(gameObject);
        }
    }

    // Função para demonstrar uma frase
    private IEnumerator ShowLine()
    {
        // Retira o texto da caixa de diálogo
        dialogueText.text = string.Empty;

        // Utiliza um ciclo para escrever a frase letra a letra
        foreach (char ch in dialogueLines[lineIndex])
        {
            // Adiciona uma letra
            dialogueText.text += ch;
            // Espera x segundos indicado na variável "typingTime"
            yield return new WaitForSeconds(typingTime);
        }
    }

    // Função para começar o diálogo
    public void StartDialogue()
    {
        // Atualiza a variável lógica
        didDialogueStart = true;
        // Ativa o painél de diálogo
        dialoguePanel.SetActive(true);
        // Atualiza o índice
        lineIndex = 0;
        // Faz aparecer o diálogo na caixa
        StartCoroutine(ShowLine());
    }
}