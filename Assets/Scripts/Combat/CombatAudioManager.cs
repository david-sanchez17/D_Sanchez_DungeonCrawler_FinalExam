using UnityEngine;

public class CombatAudioManager : MonoBehaviour
{
    public static CombatAudioManager Instance;

    [Header("Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource enemySfxSource;
    [SerializeField] private AudioSource playerSfxSource;

    [Header("Music")]
    [SerializeField] private AudioClip battleMusic;

    [Header("Player sfx")]
    [SerializeField] private AudioClip playerAttackClip;
    [SerializeField] private AudioClip playerGuardClip;
    [SerializeField] private AudioClip playerHitClip;
    [SerializeField] private AudioClip playerDeathClip;
    [SerializeField] private AudioClip playerVictoryClip;

    [SerializeField] private AudioClip enemyAttackClip;
    [SerializeField] private AudioClip enemyHitClip;
    [SerializeField] private AudioClip enemyDeathClip;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    private void Start()
    {
        PlayBattleMusic();
    }

   public void PlayBattleMusic()
    {
        if (battleMusic == null)
        {
            return;
        }
        //Can be manually set to loop but i feel its better to make sure its looped automatically
        musicSource.clip = battleMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopBattleMusic()
    {
        musicSource.Stop();
    }

    private void PlayPlayerClip(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }
        playerSfxSource.PlayOneShot(clip);
    }

    public void PlayPlayerAttack()
    {
        PlayPlayerClip(playerAttackClip);
    }

    public void PlayPlayerGuard()
    {
        PlayPlayerClip(playerGuardClip);
    }

    public void PlayPlayerHit()
    {
        PlayPlayerClip(playerHitClip);
    }

    public void PlayPlayerDeath()
    {
        PlayPlayerClip(playerDeathClip);
    }

    public void PlayPlayerVictory()
    {
        PlayPlayerClip(playerVictoryClip);
    }

    private void PlayEnemyClip (AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }
        enemySfxSource.PlayOneShot(clip);
    }
    public void PlayEnemyAttack()
    {
        PlayEnemyClip(enemyAttackClip);
    }

    public void PlayEnemyHit()
    {
        PlayEnemyClip(enemyHitClip);
    }

    public void PlayEnemyDeath()
    {
        PlayEnemyClip(enemyDeathClip);
    }

    void Update()
    {
        
    }
}
