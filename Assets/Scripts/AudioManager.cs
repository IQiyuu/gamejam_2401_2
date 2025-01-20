using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Clips")]
    public AudioClip walk;
    public AudioClip jump;
    public AudioClip land;
    public AudioClip roll;
    public AudioClip rollCollision;
    public AudioClip death;

    [SerializeField] AudioSource source;
    [SerializeField] AudioSource sourceDeath;

    public void PlayWalk() {
        StopAudio();
        source.clip = walk;
        source.loop = true;
        source.Play();
    }
    public void PlayLand() {
        StopAudio();
        source.clip = land;
        source.loop = true;
        source.Play();
    }
    public void PlayRoll() {
        StopAudio();
        source.clip = roll;
        source.loop = true;
        source.Play();
    }
    public void PlayJump() {
        StopAudio();
        source.clip = jump;
        source.loop = true;
        source.Play();
    }
    public void PlayRollCollision() {
        StopAudio();
        source.clip = rollCollision;
        source.loop = true;
        source.Play(); 
    }

    public void PlayRollCollisionOS() {
        StopAudio();
        source.clip = rollCollision;
        source.loop = false;
        source.Play(); 
    }

    public void PlayLandOS() {
        StopAudio();
        source.clip = land;
        source.loop = false;
        source.Play();
    }

    public void PlayJumpOS() {
        StopAudio();
        source.clip = jump;
        source.loop = false;
        source.Play();
    }

    public void PlayDeathOS() {
        sourceDeath.loop = false;
        sourceDeath.Play();

    }

    public bool isPlayingWalk() {
        return (source.isPlaying && source.clip == walk);
    }
    public bool isPlayingJump() {
        return (source.isPlaying && source.clip == jump);
    }
    public bool isPlayingRoll() {
        return (source.isPlaying && source.clip == roll);
    }
    public bool isPlayingRollCollision() {
        return (source.isPlaying && source.clip == rollCollision);
    }
    public bool isPlayingLand() {
        return (source.isPlaying && source.clip == land);
    }


    public void StopAudio() {
        if (source.isPlaying)
            source.Stop();
    }

    public void StopWalk() {
        if (isPlayingWalk())
            source.Stop();
    }
    public void StopJump() {
        if (isPlayingJump())
            source.Stop();
    }
    public void StopRoll() {
        if (isPlayingRoll())
            source.Stop();
    }
    public void StopLand() {
        if (isPlayingLand())
            source.Stop();
    }
    public void StopRollCollision() {
        if (isPlayingRollCollision())
            source.Stop();
    }
}
