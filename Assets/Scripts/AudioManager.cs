using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------------Audio Source-------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-------------Audio Clip-------------")]
    public bool Dungeon;
    public bool Boss;
    public AudioClip background1;
    public AudioClip background2;
    public AudioClip bossMusic;
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
    public AudioClip handSpawn;
    public AudioClip handMove;
    public AudioClip enemyMageStartAttack;
    public AudioClip enemyProjectileHit;
    public AudioClip enemyMageShoot;

    private void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");
        if (!Dungeon)
        {
            musicSource.clip = background1;
        }
        else
        {
            musicSource.clip = background2;
        }

        if(Boss)
            musicSource.clip = bossMusic;

        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
