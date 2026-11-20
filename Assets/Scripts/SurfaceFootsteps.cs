using UnityEngine;
using UnityEngine.Audio;

public class SurfaceFootsteps : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioResource grassAudio;
    [SerializeField] private AudioResource stoneAudio;
    [SerializeField] private float stepInterval = 0.35f;

    private AudioResource currentAudio;
    private float stepTimer;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Mathf.Abs(rb.linearVelocity.x) > 0.2f && currentAudio != null)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                audioSource.resource = currentAudio;
                audioSource.pitch = Random.Range(0.9f, 1.1f); // Also knocks out the 3p random pitch task!
                audioSource.Play();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Grass"))
            currentAudio = grassAudio;
        else if (collision.collider.CompareTag("Stone"))
            currentAudio = stoneAudio;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        currentAudio = null;
    }
}