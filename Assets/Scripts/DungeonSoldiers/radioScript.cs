using UnityEngine;

public class radioScript : MonoBehaviour
{
    // Vari�vel com as m�sicas
    public AudioClip[] clips;
    // Vari�vel com o componente "AudioSource"
    private AudioSource audioSource;

    // A fun��o � chamada antes da atualiza��o do primeiro frame
    void Start()
    {
        // Caso n�o exista alguma m�sica na vari�vel, o "script" ir� se destruir
        if (clips.Length == 0)
            Destroy(this);

        // Obt�m o componente
        audioSource = GetComponent<AudioSource>();
    }

    // A fun��o � chamada a cada frame
    void Update()
    {
        // Verifica se a m�sica parou
        if (!audioSource.isPlaying)
        {
            // Se parou, iremos buscar uma m�sica aleat�ria
            audioSource.clip = GetRandomClip();
            // Toca a nova m�sica
            audioSource.Play();
        }
    }

    // Fun��o para buscar uma m�sica aleat�ria
    private AudioClip GetRandomClip()
    {
        // Retorna uma m�sica aleat�ria obtendo um �ndice aleat�rio
        return clips[Random.Range(0, clips.Length)];
    }
}
