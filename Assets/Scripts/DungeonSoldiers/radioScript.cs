using UnityEngine;

public class radioScript : MonoBehaviour
{
    // Variável com as músicas
    public AudioClip[] clips;
    // Variável com o componente "AudioSource"
    private AudioSource audioSource;

    // A função é chamada antes da atualização do primeiro frame
    void Start()
    {
        // Caso não exista alguma música na variável, o "script" irá se destruir
        if (clips.Length == 0)
            Destroy(this);

        // Obtêm o componente
        audioSource = GetComponent<AudioSource>();
    }

    // A função é chamada a cada frame
    void Update()
    {
        // Verifica se a música parou
        if (!audioSource.isPlaying)
        {
            // Se parou, iremos buscar uma música aleatória
            audioSource.clip = GetRandomClip();
            // Toca a nova música
            audioSource.Play();
        }
    }

    // Função para buscar uma música aleatória
    private AudioClip GetRandomClip()
    {
        // Retorna uma música aleatória obtendo um índice aleatório
        return clips[Random.Range(0, clips.Length)];
    }
}
