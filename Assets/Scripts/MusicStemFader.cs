using UnityEngine;

public class MusicStemFader : MonoBehaviour
{
    [SerializeField] private AudioSource baseTrack;
    [SerializeField] private AudioSource extraStem;
    [SerializeField] private float fadeDuration = 2f;

    private float targetVolume = 0f;

    private void Start()
    {
        // Start both at the exact same DSP time so they never drift out of beat!
        double startTime = AudioSettings.dspTime + 0.1;
        baseTrack.volume = 1f;
        extraStem.volume = 0f;

        baseTrack.PlayScheduled(startTime);
        extraStem.PlayScheduled(startTime);
    }

    private void Update()
    {
        // Smoothly fade the extra stem volume toward targetVolume
        extraStem.volume = Mathf.MoveTowards(extraStem.volume, targetVolume, Time.deltaTime / fadeDuration);
    }

    // Call this from a UI Button, UnityEvent, or Trigger!
    public void FadeInStem()
    {
        targetVolume = 1f;
    }

    public void FadeOutStem()
    {
        targetVolume = 0f;
    }

    // Optional: Automatically fade in when Player enters a trigger zone!
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FadeInStem();
        }
    }
}