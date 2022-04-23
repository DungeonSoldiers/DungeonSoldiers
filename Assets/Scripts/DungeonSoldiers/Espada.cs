using System.Collections;
using UnityEngine;

public class Espada : MonoBehaviour
{
    // Variavel l�gica com o estado da espada (Equipada ou guardada)
    private bool swordEquipped = false;
    // Vari�vel do "Player"
    public Transform Player;
    // Variavel com o "SpriteRenderer" da espada
    private SpriteRenderer srSword;
    // Vari�vel para guardar o eixo y do �ngulo do jogador
    private float angleRecovery;
    // Vari�vel l�gica para detetar se a espada est� em uso ou n�o
    private bool ableToSwing = true;
    // Vari�vel com o componente "Audio Source"
    private AudioSource _audioSource;
    // Vari�vel com o efeito sonoro de atacar com a espada
    public AudioClip swingSound;
    // Vari�vel com o efeito "Slash"
    public GameObject Slash;
    // Vari�vel com a espada para baixo
    public GameObject swingedSword;

    // A fun��o � chamada antes da atualiza��o do primeiro frame
    private void Start()
    {
        // Obt�m o componente SpriteRenderer
        srSword = GetComponent<SpriteRenderer>();
        // Obt�m o componente "Audio Source"
        _audioSource = GetComponent<AudioSource>();
    }

    // Fun��o para iniciar um novo a�dio
    public void PlayAudio(AudioClip ac)
    {
        // Para o a�dio a dar
        _audioSource.Stop();
        // Muda a propriedade "Audio Clip" para o novo efeito sonoro
        _audioSource.clip = ac;
        // Inicia o a�dio
        _audioSource.Play();
    }

    // A fun��o � chamada a cada frame
    private void Update()
    {
        // Fun��o l�gica de detec��o de input da tecla T
        if (Input.GetKeyDown(KeyCode.T))
            // Caso a tecla T seja pressionada, o jogador ir� equipar ou guardar a espada
            ToggleSword();

        /* Fun��o l�gica de detec��o de input do bot�o esquerdo do rato
         * Caso seja detetado o input, ser� verificado se a espada est� equipada e se � poss�vel atacar */
        if (Input.GetMouseButton(0) && swordEquipped && ableToSwing)
            // Se for poss�vel atacar, a fun��o ser� realizada
            StartCoroutine(Swing());
    }

    // Fun��o chamada quando o jogador quer atacar com a espada
    private IEnumerator Swing()
    {
        // Atualiza a vari�vel para indicar que a espada est� a ser utilizada
        ableToSwing = false;
        // Inicia o a�dio de ataque
        PlayAudio(swingSound);
        // Ativa as colis�es da espada
        GetComponentInChildren<BoxCollider2D>().enabled = true;
        // Para o jogador
        GetComponentInParent<PlayerMovement>().speed = 0;
        // Muda o "sprite" para demonstrar o efeito da espada a atacar
        srSword.enabled = false;
        swingedSword.SetActive(true);
        Slash.SetActive(true);
        // Espera 0.3 segundos
        yield return new WaitForSeconds(0.3f);
        // Desativa as colis�es da espada
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        // Faz com que o jogador volte a andar
        GetComponentInParent<PlayerMovement>().speed = 8;
        // Muda o "sprite" para demonstrar a espada no estado normal
        srSword.enabled = true;
        swingedSword.SetActive(false);
        Slash.SetActive(false);
        // Espera 1 segundo
        yield return new WaitForSeconds(1);
        // Atualiza a vari�vel para indicar que a espada pode voltar a ser utilizada
        ableToSwing = true;
    }

    // Fun��o para equipar ou guardar a espada
    private void ToggleSword()
    {
        // Muda a variavel l�gica
        swordEquipped = !swordEquipped;

        // Armazena o eixo y do �ngulo do jogador
        angleRecovery = Player.eulerAngles.y;

        // Verifica se o jogador est� virado para a esquerda
        if (Player.eulerAngles.y != 0)
            // Se estiver, este ser� mudado para a direita
            Player.eulerAngles = new Vector3(0f, 0f, 0f);

        /* Deteta se � para equipar a espada
         * Se for, est� ser� equipada */
        if (swordEquipped)
        {
            // Muda a propriedade "sortingOrder" para esta aparecer acima do "sprite" do jogador
            srSword.sortingOrder = 9;
            // Muda a posi��o da espada
            srSword.transform.position = Player.position + new Vector3(-0.4f, 0.3f, 0f);
            // Muda o �ngulo da espada
            srSword.transform.eulerAngles = new Vector3(0f, 0f, 45f);
        }
        else // Caso contr�rio, esta ser� guardada
        {
            // Muda a propriedade "sortingOrder" para esta ser coberta pelo "sprite" do jogador
            srSword.sortingOrder = 4;
            // Muda a posi��o da espada
            srSword.transform.position = Player.position + new Vector3(-0.27f, 0.18f, 0f);
            // Muda o �ngulo da espada
            srSword.transform.eulerAngles = new Vector3(0f, 0f, -155.03f);
        }

        // Aplica o �ngulo guardado na vari�vel "angleRecovery" no jogador
        Player.eulerAngles = new Vector3(0f, angleRecovery, 0f);
    }
}