using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChanger : MonoBehaviour
{
    public enum BGMType { Normal, Matsuri, GameOver}

    public AudioClip NormalBGM;
    public AudioClip MtsuriBGM;
    public AudioClip GameOverBGM;
    private AudioSource _audioSource;
    private GameManager _gm;
    private BGMType nowBGM = default;

    // Start is called before the first frame update
    void Start()
    {
        _gm = FindAnyObjectByType<GameManager>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _gm.SoundLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if(_audioSource.volume != _gm.SoundLevel)
        {
            Debug.Log($"[{name}.PlayBGM] Volume Changed! GmVolume={_gm.SoundLevel}, SourceVolume={_audioSource.volume}");
            _audioSource.volume = _gm.SoundLevel;
        }
        if(_gm.IsOnBGM && !_audioSource.isPlaying)
        {
            PlayBGM(nowBGM);
        }
    }
    public void PlayBGM(BGMType bGMType)
    {
        Debug.Log($"[{name}.PlayBGM] NowBGM={nowBGM}, NewBGM={bGMType}, GmVolume={_gm.SoundLevel}, SourceVolume={_audioSource.volume}");
        nowBGM = bGMType;

        if (_gm.IsOnBGM)
        {
            _audioSource.loop = true;

            switch (bGMType)
            {
                case BGMType.Normal:
                    _audioSource.PlayOneShot(NormalBGM, _gm.SoundLevel);
                    break;

                case BGMType.Matsuri:
                    _audioSource.PlayOneShot(MtsuriBGM, _gm.SoundLevel);
                    break;

                case BGMType.GameOver:
                    _audioSource.PlayOneShot(GameOverBGM, _gm.SoundLevel);
                    break;
            }
        }
    }
    public void StopBGM()
    {
        if (_audioSource.isPlaying) 
        {
            _audioSource.Stop();
        }
    }
}
