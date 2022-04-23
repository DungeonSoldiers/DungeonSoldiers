using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class automatedDialog : MonoBehaviour
{
    // Vari�vel com o di�logo
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    // Vari�vel com o canvas que indica a miss�o
    [SerializeField] private GameObject CanvasMissao;
    // Vari�vel com a caixa de di�logo
    [SerializeField] private GameObject dialoguePanel;
    // Vari�vel com o texto da caixa de di�logo
    [SerializeField] private TMP_Text dialogueText;
    // Vari�vel com a nova miss�o ap�s o di�logo
    public string missaoAposFala;
    // Vari�vel l�gica que indica se o jogador j� falou com o "NPC" ou n�o
    private bool didDialogueStart = false;
    // Vari�vel com o �ndice do di�logo
    private int lineIndex;
    // Vari�vel com o tempo de escrita
    private float typingTime = 0.025f;
    // Vari�vel que controla a velocidade do jogador
    private PlayerMovement movimentacao;
    // Vari�vel com o "fade in"
    public Image fadeIn;
    // Vari�vel com o zoneManager
    public zoneManager zManager;

    // A fun��o � chamada antes da atualiza��o do primeiro frame
    private void Start()
    {
        // Obt�m o "script" relacionado � movimenta��o do jogador
        movimentacao = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // A fun��o � chamada a cada frame
    void Update()
    {
        // Verifica se o "fade in" j� acabou e se o di�logo n�o come�ou
        if (fadeIn.color.a == 0 && !didDialogueStart)
            // Caso verdadeiro, o di�logo ir� come�ar
            StartDialogue();

        // Avan�a o di�logo se o jogador clicar com o bot�o esquerdo do rato
        if (Input.GetButtonDown("Fire1") && didDialogueStart && Time.timeScale == 1 && dialogueText.text == dialogueLines[lineIndex])
            NextDialogueLine();
    }

    // Fun��o para a pr�xima fala do "NPC"
    private void NextDialogueLine()
    {
        // Atualiza o �ndice
        lineIndex++;

        // Verifica se ainda falta di�logo
        if (lineIndex < dialogueLines.Length)
            // Caso falte, este ir� proseguir para a pr�xima fala
            StartCoroutine(ShowLine());
        // Caso contr�rio, o di�logo ir� encerrar
        else
        {
            // Aplica velocidade ao jogador para que ele possa mover de novo
            movimentacao.speed = 8;
            // Desativa a caixa de di�logo
            dialoguePanel.SetActive(false);
            // Ativa o canvas que indica a miss�o
            CanvasMissao.SetActive(true);
            // Atualiza a miss�o
            CanvasMissao.GetComponentInChildren<TextMeshProUGUI>().text = missaoAposFala;
            // Destroi a borda entre o inicio da masmorra e a primeira zona
            zManager.ClearBorder(1);
            // Destrui o "NPC"
            Destroy(gameObject);
        }
    }

    // Fun��o para demonstrar uma frase
    private IEnumerator ShowLine()
    {
        // Retira o texto da caixa de di�logo
        dialogueText.text = string.Empty;

        // Utiliza um ciclo para escrever a frase letra a letra
        foreach (char ch in dialogueLines[lineIndex])
        {
            // Adiciona uma letra
            dialogueText.text += ch;
            // Espera x segundos indicado na vari�vel "typingTime"
            yield return new WaitForSeconds(typingTime);
        }
    }

    // Fun��o para come�ar o di�logo
    public void StartDialogue()
    {
        // Atualiza a vari�vel l�gica
        didDialogueStart = true;
        // Ativa o pain�l de di�logo
        dialoguePanel.SetActive(true);
        // Atualiza o �ndice
        lineIndex = 0;
        // Faz aparecer o di�logo na caixa
        StartCoroutine(ShowLine());
    }
}