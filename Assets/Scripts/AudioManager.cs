using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------------Audio Source-------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-------------Audio Clip-------------")]
    public AudioClip background;
    public AudioClip enemyDeath;
    public AudioClip enemyHurt;
    public AudioClip jump;
    public AudioClip mushroomJump;
    public AudioClip pepAttack;
    public AudioClip projectileHit;
    public AudioClip shoot;
    public AudioClip skeletonThrow;
    public AudioClip slimeFall;
    public AudioClip slimeGround;

    private void Start()
    {
        //musicSource.clip = background;
        //musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
