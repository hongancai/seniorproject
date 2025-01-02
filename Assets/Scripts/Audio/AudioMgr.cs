using UnityEngine;

public class AudioMgr : MonoBehaviour
{
    public AudioData audioData;
    private AudioSource _bgmAudioSource;
    private AudioSource _sfxAudioSource;

    private void Awake()
    {
        if (GameDB.Audio == null)
        {
            DontDestroyOnLoad(gameObject);
            Initialize();
            GameDB.Audio = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log($"Audio Manager Started ... {GameDB.Config.bgmAudioVolume:F2}");
        _bgmAudioSource.volume = GameDB.Config.bgmAudioVolume;

        if (audioData != null)
        {
            Debug.Log($"Audio Data was detected ... Enable AutoPlay");
            _bgmAudioSource.clip = audioData.bgmClip;
            _bgmAudioSource.Play();
        }
    }

    private void Update()
    {
    }

    private void Initialize()
    {
        // create BGM
        GameObject bgmGameObject = new GameObject("BGM")
        {
            transform =
            {
                parent = transform
            }
        };
        _bgmAudioSource = bgmGameObject.GetComponent<AudioSource>();
        if (_bgmAudioSource == null)
        {
            _bgmAudioSource = bgmGameObject.AddComponent<AudioSource>();
        }

        _bgmAudioSource.loop = true;

        // create SFX
        GameObject sfxGameObject = new GameObject("SFX")
        {
            transform =
            {
                parent = transform
            }
        };
        _sfxAudioSource = sfxGameObject.GetComponent<AudioSource>();
        if (_sfxAudioSource == null)
        {
            _sfxAudioSource = sfxGameObject.AddComponent<AudioSource>();
        }

        _sfxAudioSource.loop = false;
    }

    #region Play BGM or SFX

    public void PlayBgm(AudioClip bgm = null)
    {
        _bgmAudioSource.Stop();
        if (bgm != null)
        {
            audioData.bgmClip = bgm;
            _bgmAudioSource.clip = audioData.bgmClip;
        }

        _bgmAudioSource.Play(0);
    }

    public void PlaySfx(AudioClip clip)
    {
        _sfxAudioSource.PlayOneShot(clip);
    }

    #endregion

    #region Set Volume

    public float GetBgmVolume()
    {
        GameDB.Config.bgmAudioVolume = _bgmAudioSource.volume;
        return _bgmAudioSource.volume;
    }

    public void SetBgmVolume(float volume)
    {
        GameDB.Config.bgmAudioVolume = volume;
        _bgmAudioSource.volume = GameDB.Config.bgmAudioVolume;
    }

    public float GetSfxVolume()
    {
        GameDB.Config.sfxAudioVolume = _sfxAudioSource.volume;
        return _sfxAudioSource.volume;
    }

    public void SetSfxVolume(float volume)
    {
        GameDB.Config.sfxAudioVolume = volume;
        _sfxAudioSource.volume = GameDB.Config.sfxAudioVolume;
    }

    #endregion

    #region Play Sound

    public void PlayPress()
    {
        _sfxAudioSource.PlayOneShot(audioData.pressClip);
    }

    public void PlaySubmit()
    {
        _sfxAudioSource.PlayOneShot(audioData.submitClip);
    }

    public void PlayCancel()
    {
        _sfxAudioSource.PlayOneShot(audioData.cancelClip);
    }

    #endregion
}