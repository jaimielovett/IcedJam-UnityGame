using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {

    [SerializeField] private AudioClip _gameOverClip;
    [SerializeField] private AudioClip _levelCompleteClip;
    [SerializeField] private AudioClip[] _paintSplatClips;
    [SerializeField] private AudioClip[] _explosionClips;
    [SerializeField] private AudioClip[] _musicClips;

    [SerializeField] private AudioClip _buttonHoverClip;
    [SerializeField] private AudioClip _buttonClickClip;

    [SerializeField] private AudioSource _audioSourceGameOver;
    [SerializeField] private AudioSource _audioSourceLevelComplete;
    [SerializeField] private AudioSource _audioSourcePaintSplat;
    [SerializeField] private AudioSource _audioSourceExplosion;
    [SerializeField] private AudioSource _audioSourceBackgroundMusic;
    [SerializeField] private AudioSource _audioSourceButtonSounds;

    [SerializeField] private Slider _musicVolume;
    [SerializeField] private Slider _soundFXVolume;

    private int _indexCurrentSong;

    public static AudioController Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayButtonHoverClip()
    {
        _audioSourceButtonSounds.pitch = 1;
        _audioSourceButtonSounds.PlayOneShot(_buttonHoverClip);
    }

    public void PlayButtonClickClip()
    {
        _audioSourceButtonSounds.pitch = 1;
        _audioSourceButtonSounds.PlayOneShot(_buttonClickClip);
    }

    public void PlayGameOverClip()
    {
        _audioSourceGameOver.pitch = 1;
        _audioSourceGameOver.PlayOneShot(_gameOverClip);
    }

    public void PlayLevelCompleteClip()
    {
        _audioSourceLevelComplete.pitch = 1;
        _audioSourceLevelComplete.PlayOneShot(_levelCompleteClip);
    }

    public void PlayPaintSplatClip()
    {
        _audioSourcePaintSplat.pitch = Random.Range(2f, 3f);
        _audioSourcePaintSplat.PlayOneShot(_paintSplatClips[Random.Range(0, _paintSplatClips.Length)]);
    }

    public void PlayExplosionClip()
    {
        _audioSourceExplosion.pitch = Random.Range(2f, 3f);
        _audioSourceExplosion.PlayOneShot(_explosionClips[Random.Range(0, _explosionClips.Length)]);
    }

    public void PlayImpactClip()
    {
        PlayPaintSplatClip();
        PlayExplosionClip();
    }

    public void PlayMusicClip()
    {
        int index = Random.Range(0, _musicClips.Length);
        if (index == _indexCurrentSong)
        {
            PlayMusicClip();
        }
        else
        {
            _indexCurrentSong = index;
            _audioSourceBackgroundMusic.PlayOneShot(_musicClips[_indexCurrentSong]);
        }
    }

    public void StopMusicClip()
    {
        _audioSourceBackgroundMusic.Stop();
    }

    void Update()
    {
        if (!_audioSourceBackgroundMusic.isPlaying && GameController.Instance.State != GameState.GAME_OVER)
        {
            PlayMusicClip();
        }

        if (GameController.Instance.GlobalMuteOn)
        {
            _audioSourceBackgroundMusic.volume = 0;
            _audioSourceGameOver.volume = 0;
            _audioSourceLevelComplete.volume = 0;
            _audioSourcePaintSplat.volume = 0;
            _audioSourceExplosion.volume = 0;
        }
        else
        {
            // Set the background music according to the slider. But divide it by 3 so that it is 1/3rd as loud as the sound FX.
            // Also the explosions are set to 1/10th the volume of the other sound FX.
            _audioSourceBackgroundMusic.volume = _musicVolume.value / 3;
            _audioSourceGameOver.volume = _soundFXVolume.value;
            _audioSourceLevelComplete.volume = _soundFXVolume.value;
            _audioSourcePaintSplat.volume = _soundFXVolume.value;
            _audioSourceExplosion.volume = _soundFXVolume.value / 10;
        }
    }
}
