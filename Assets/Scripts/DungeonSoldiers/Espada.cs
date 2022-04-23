using System.Collections;
using UnityEngine;

public class Espada : MonoBehaviour
{
    // Variavel lógica com o estado da espada (Equipada ou guardada)
    private bool swordEquipped = false;
    // Variável do "Player"
    public Transform Player;
    // Variavel com o "SpriteRenderer" da espada
    private SpriteRenderer srSword;
    // Variável para guardar o eixo y do ângulo do jogador
    private float angleRecovery;
    // Variável lógica para detetar se a espada está em uso ou não
    private bool ableToSwing = true;
    // Variável com o componente "Audio Source"
    private AudioSource _audioSource;
    // Variável com o efeito sonoro de atacar com a espada
    public AudioClip swingSound;
    // Variável com o efeito "Slash"
    public GameObject Slash;
    // Variável com a espada para baixo
    public GameObject swingedSword;

    // A função é chamada antes da atualização do primeiro frame
    private void Start()
    {
        // Obtêm o componente SpriteRenderer
        srSword = GetComponent<SpriteRenderer>();
        // Obtêm o componente "Audio Source"
        _audioSource = GetComponent<AudioSource>();
    }

    // Função para iniciar um novo aúdio
    public void PlayAudio(AudioClip ac)
    {
        // Para o aúdio a dar
        _audioSource.Stop();
        // Muda a propriedade "Audio Clip" para o novo efeito sonoro
        _audioSource.clip = ac;
        // Inicia o aúdio
        _audioSource.Play();
    }

    // A função é chamada a cada frame
    private void Update()
    {
        // Função lógica de detecção de input da tecla T
        if (Input.GetKeyDown(KeyCode.T))
            // Caso a tecla T seja pressionada, o jogador irá equipar ou guardar a espada
            ToggleSword();

        /* Função lógica de detecção de input do botão esquerdo do rato
         * Caso seja detetado o input, será verificado se a espada está equipada e se é possível atacar */
        if (Input.GetMouseButton(0) && swordEquipped && ableToSwing)
            // Se for possível atacar, a função será realizada
            StartCoroutine(Swing());
    }

    // Função chamada quando o jogador quer atacar com a espada
    private IEnumerator Swing()
    {
        // Atualiza a variável para indicar que a espada está a ser utilizada
        ableToSwing = false;
        // Inicia o aúdio de ataque
        PlayAudio(swingSound);
        // Ativa as colisões da espada
        GetComponentInChildren<BoxCollider2D>().enabled = true;
        // Para o jogador
        GetComponentInParent<PlayerMovement>().speed = 0;
        // Muda o "sprite" para demonstrar o efeito da espada a atacar
        srSword.enabled = false;
        swingedSword.SetActive(true);
        Slash.SetActive(true);
        // Espera 0.3 segundos
        yield return new WaitForSeconds(0.3f);
        // Desativa as colisões da espada
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        // Faz com que o jogador volte a andar
        GetComponentInParent<PlayerMovement>().speed = 8;
        // Muda o "sprite" para demonstrar a espada no estado normal
        srSword.enabled = true;
        swingedSword.SetActive(false);
        Slash.SetActive(false);
        // Espera 1 segundo
        yield return new WaitForSeconds(1);
        // Atualiza a variável para indicar que a espada pode voltar a ser utilizada
        ableToSwing = true;
    }

    // Função para equipar ou guardar a espada
    private void ToggleSword()
    {
        // Muda a variavel lógica
        swordEquipped = !swordEquipped;

        // Armazena o eixo y do ângulo do jogador
        angleRecovery = Player.eulerAngles.y;

        // Verifica se o jogador está virado para a esquerda
        if (Player.eulerAngles.y != 0)
            // Se estiver, este será mudado para a direita
            Player.eulerAngles = new Vector3(0f, 0f, 0f);

        /* Deteta se é para equipar a espada
         * Se for, está será equipada */
        if (swordEquipped)
        {
            // Muda a propriedade "sortingOrder" para esta aparecer acima do "sprite" do jogador
            srSword.sortingOrder = 9;
            // Muda a posição da espada
            srSword.transform.position = Player.position + new Vector3(-0.4f, 0.3f, 0f);
            // Muda o ângulo da espada
            srSword.transform.eulerAngles = new Vector3(0f, 0f, 45f);
        }
        else // Caso contrário, esta será guardada
        {
            // Muda a propriedade "sortingOrder" para esta ser coberta pelo "sprite" do jogador
            srSword.sortingOrder = 4;
            // Muda a posição da espada
            srSword.transform.position = Player.position + new Vector3(-0.27f, 0.18f, 0f);
            // Muda o ângulo da espada
            srSword.transform.eulerAngles = new Vector3(0f, 0f, -155.03f);
        }

        // Aplica o ângulo guardado na variável "angleRecovery" no jogador
        Player.eulerAngles = new Vector3(0f, angleRecovery, 0f);
    }
}