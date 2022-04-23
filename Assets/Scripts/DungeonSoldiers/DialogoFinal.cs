using System.Collections;
using UnityEngine;
using TMPro;

public class DialogoFinal : MonoBehaviour
{
    // Vari�vel que verifica se o jogador est� no raio do "NPC"
    private bool isPlayerInRange;
    // Vari�vel com o di�logo
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    // Vari�vel com o canvas que indica a miss�o
    [SerializeField] private GameObject CanvasMissao;
    // Vari�vel com a caixa de di�logo
    [SerializeField] private GameObject dialoguePanel;
    // Vari�vel com o texto da caixa de di�logo
    [SerializeField] private TMP_Text dialogueText;
    // Vari�vel l�gica que indica se o jogador j� falou com o "NPC" ou n�o
    private bool didDialogueStart;
    // Vari�vel com o �ndice do di�logo
    private int lineIndex;
    // Vari�vel com o tempo de escrita
    private float typingTime = 0.025f;
    // Vari�vel com a notifica��o acima do "NPC"
    public GameObject Notification;
    // Vari�vel que controla a velocidade do jogador
    private PlayerMovement movimentacao;
    // Vari�vel com o "Fade Out"
    public Animator fadeOut;

    // Deteta se algum objeto entrou em colis�o com o "NPC"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se esse objeto � o jogador e se este j� falou com o "NPC"
        if (collision.gameObject.CompareTag("Player") && (didDialogueStart == false))
        {
            // Associa o "script" de movimenta��o � vari�vel
            movimentacao = collision.gameObject.GetComponent<PlayerMovement>();
            // Atualiza a vari�vel l�gica
            isPlayerInRange = true;
            // Ativa a notifica��o
            Notification.SetActive(true);
        }
    }

    // Deteta se algum objeto parou de estar em colis�o com o "NPC"
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Verifica se esse objeto � o jogador e se este j� falou com o "NPC"
        if (collision.gameObject.CompareTag("Player") && (didDialogueStart == false))
        {
            // Desassocia o "script" de movimenta��o
            movimentacao = null;
            // Atualiza a vari�vel l�gica
            isPlayerInRange = false;
            // Desativa a notifica��o
            Notification.SetActive(false);
        }
    }

    // A fun��o � chamada a cada frame
    void Update() 
    {
        // Verifica se o jogador est� no raio do "NPC" e se este clicou
        if (isPlayerInRange && Input.GetButtonDown("Fire1") && Time.timeScale == 1)
            // Caso o di�logo n�o tenha come�ado, este ir� ser come�ado
            if (!didDialogueStart)
                StartDialogue();
            // Caso contr�rio, o di�logo ser� continuado
            else if (dialogueText.text == dialogueLines[lineIndex])
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
            // Desativa o pain�l de di�logo
            dialoguePanel.SetActive(false);
            // Come�a o "fade out"
            fadeOut.SetTrigger("startGame");
        }
    }

    // Fun��o para demonstrar uma frase
    private IEnumerator ShowLine()
    {
        // Retira o texto da caixa de di�logo
        dialogueText.text = string.Empty;

        // Utiliza um ciclo para escrever a frase letra a letra
        foreach(char ch in dialogueLines[lineIndex])
        {
            // Adiciona uma letra
            dialogueText.text += ch;
            // Espera x segundos indicado na vari�vel "typingTime"
            yield return new WaitForSeconds(typingTime);
        }
    }

    // Fun��o para come�ar o di�logo
    void StartDialogue() 
    {
        // Destr�i o canvas que indica a miss�o
        Destroy(CanvasMissao);
        // Para o jogador
        movimentacao.speed = 0;
        // Destr�i a notifica��o
        Destroy(Notification);
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