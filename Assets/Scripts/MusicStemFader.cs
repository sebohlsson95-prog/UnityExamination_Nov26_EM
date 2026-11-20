using UnityEngine;

public class MusicStemFader : MonoBehaviour
{
    [SerializeField] private AudioSource baseTrack;
    [SerializeField] private AudioSource extraStem;
    [SerializeField] private float fadeDuration = 3f;

    private float targetVolume = 0f;

    private void Start()
    {
        double startTime = AudioSettings.dspTime + 0.1;
        baseTrack.volume = 1f;
        extraStem.volume = 0f;

        baseTrack.PlayScheduled(startTime);
        extraStem.PlayScheduled(startTime);
    }

    private void Update()
    {
        extraStem.volume = Mathf.MoveTowards(extraStem.volume, targetVolume, Time.deltaTime / fadeDuration);
    }

    public void FadeInStem() => targetVolume = 1f;
    public void FadeOutStem() => targetVolume = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) FadeInStem();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) FadeOutStem();
    }
}