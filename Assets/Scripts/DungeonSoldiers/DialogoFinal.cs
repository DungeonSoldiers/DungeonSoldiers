using System.Collections;
using UnityEngine;
using TMPro;

public class DialogoFinal : MonoBehaviour
{
    // Variável que verifica se o jogador está no raio do "NPC"
    private bool isPlayerInRange;
    // Variável com o diálogo
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    // Variável com o canvas que indica a missão
    [SerializeField] private GameObject CanvasMissao;
    // Variável com a caixa de diálogo
    [SerializeField] private GameObject dialoguePanel;
    // Variável com o texto da caixa de diálogo
    [SerializeField] private TMP_Text dialogueText;
    // Variável lógica que indica se o jogador já falou com o "NPC" ou não
    private bool didDialogueStart;
    // Variável com o índice do diálogo
    private int lineIndex;
    // Variável com o tempo de escrita
    private float typingTime = 0.025f;
    // Variável com a notificação acima do "NPC"
    public GameObject Notification;
    // Variável que controla a velocidade do jogador
    private PlayerMovement movimentacao;
    // Variável com o "Fade Out"
    public Animator fadeOut;

    // Deteta se algum objeto entrou em colisão com o "NPC"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se esse objeto é o jogador e se este já falou com o "NPC"
        if (collision.gameObject.CompareTag("Player") && (didDialogueStart == false))
        {
            // Associa o "script" de movimentação à variável
            movimentacao = collision.gameObject.GetComponent<PlayerMovement>();
            // Atualiza a variável lógica
            isPlayerInRange = true;
            // Ativa a notificação
            Notification.SetActive(true);
        }
    }

    // Deteta se algum objeto parou de estar em colisão com o "NPC"
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Verifica se esse objeto é o jogador e se este já falou com o "NPC"
        if (collision.gameObject.CompareTag("Player") && (didDialogueStart == false))
        {
            // Desassocia o "script" de movimentação
            movimentacao = null;
            // Atualiza a variável lógica
            isPlayerInRange = false;
            // Desativa a notificação
            Notification.SetActive(false);
        }
    }

    // A função é chamada a cada frame
    void Update() 
    {
        // Verifica se o jogador está no raio do "NPC" e se este clicou
        if (isPlayerInRange && Input.GetButtonDown("Fire1") && Time.timeScale == 1)
            // Caso o diálogo não tenha começado, este irá ser começado
            if (!didDialogueStart)
                StartDialogue();
            // Caso contrário, o diálogo será continuado
            else if (dialogueText.text == dialogueLines[lineIndex])
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
            // Desativa o painél de diálogo
            dialoguePanel.SetActive(false);
            // Começa o "fade out"
            fadeOut.SetTrigger("startGame");
        }
    }

    // Função para demonstrar uma frase
    private IEnumerator ShowLine()
    {
        // Retira o texto da caixa de diálogo
        dialogueText.text = string.Empty;

        // Utiliza um ciclo para escrever a frase letra a letra
        foreach(char ch in dialogueLines[lineIndex])
        {
            // Adiciona uma letra
            dialogueText.text += ch;
            // Espera x segundos indicado na variável "typingTime"
            yield return new WaitForSeconds(typingTime);
        }
    }

    // Função para começar o diálogo
    void StartDialogue() 
    {
        // Destrói o canvas que indica a missão
        Destroy(CanvasMissao);
        // Para o jogador
        movimentacao.speed = 0;
        // Destrói a notificação
        Destroy(Notification);
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