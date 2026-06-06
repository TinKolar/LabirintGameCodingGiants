using UnityEngine;

public class MusicScript : MonoBehaviour
{

    AudioSource source;
    double pauseClipTime = 0f;
    public AudioClip[] clips;
    int actualClip = 0;

    private void Awake()
    {
        source=GetComponent<AudioSource>();
    }
    void Start()
    {
        if (clips.Length > 0)
        {
            source.clip = clips[0];
            source.Play();
        }

    }
    public void OnPauseGame()
    {
        pauseClipTime=source.time;
        source.Pause();
    }

    public void OnResumeGame()
    {
        source.PlayScheduled(pauseClipTime);
        pauseClipTime =0;
    }

    void Update()
    {
        if (source.time >= clips[actualClip].length)
        {
            actualClip++;
            if (actualClip > clips.Length - 1)
            {
                actualClip = 0;
            }
            source.clip = clips[actualClip];
            source.Play();
        }
    }
    public void PitchThis(float pitch)
    {
        source.pitch = pitch;
    }
}
