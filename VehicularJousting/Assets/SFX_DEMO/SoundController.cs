using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    private AudioSource _myAS;
    public AudioClip clip;

    private List<AudioClip> _myClips;
    private Dictionary<SFX, AudioClip> _enumToClip;

    private List<SoundEffect> _mySFX;

    private AudioClip activeSong;
    private AudioClip songOnDeck;

    void Start()
    {
        _myClips = new List<AudioClip>();
        _enumToClip = new Dictionary<SFX, AudioClip>();

        Populate();

        _myAS = GetComponent<AudioSource>();
        _mySFX = new List<SoundEffect>();
    }

    void Populate()
    {
        _myClips.Add(Resources.Load<AudioClip>("SFX/Jump"));
        _enumToClip.Add(SFX.jump, _myClips[0]);

        _myClips.Add(Resources.Load<AudioClip>("SFX/Walk"));
        _enumToClip.Add(SFX.walk, _myClips[1]);
    }

    public void PlaySFX (SFX whatToPlay)
    {
        AudioClip clipToPlay;
        _enumToClip.TryGetValue(whatToPlay, out clipToPlay);
        _mySFX.Add(new SoundEffect(clipToPlay));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _myAS.PlayOneShot(clip);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _mySFX.Add(new SoundEffect(clip));
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            foreach(SoundEffect sE in _mySFX)
            {
                sE.PauseSFX(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (SoundEffect sE in _mySFX)
            {
                sE.PauseSFX(false);
            }
        }

        if (_mySFX.Count > 0)
        {
            for (int i = _mySFX.Count - 1; i >= 0; i--)
            {
                _mySFX[i].UpdateTimer();
                if (_mySFX[i].readyForCleanup == true)
                {
                    Destroy(_mySFX[i].soundGO);
                    _mySFX.Remove(_mySFX[i]);
                }
            }
        }

        //foreach (SoundEffect sE in _mySFX)
        //{
        //    sE.UpdateTimer();
        //    if (sE.readyForCleanup == true)
        //    {
        //        Destroy(sE.soundGO);
        //        _mySFX.Remove(sE);
        //    }
        //}
    }
}

public enum SFX { jump, walk, step, shoot, getShot, collect, whatever}

public class SoundEffect
{
    public AudioClip clip;
    public float clipDuration;

    private float internalTimer;

    public GameObject soundGO;

    public bool readyForCleanup = false;

    private bool _p;
    public bool isPaused
    {
        get
        {
            return _p;
        }
        set
        {
            if (value != _p)
            {
                _p = value;
                if (_p == true)
                    soundGO.GetComponent<AudioSource>().Play();
                else
                    soundGO.GetComponent<AudioSource>().Pause();
            }
        }
    }

    public SoundEffect (AudioClip _clip)
    {
        internalTimer = 0;
        clip = _clip;
        clipDuration = clip.length;
        CreateAndPlay();
    }

    public void UpdateTimer()
    {
        if (!isPaused)
            internalTimer += Time.deltaTime;

        if (internalTimer >= clipDuration)
        {
            readyForCleanup = true;
        }
    }

    private void CreateAndPlay()
    {
        soundGO = new GameObject();
        soundGO.AddComponent<AudioSource>();
        soundGO.GetComponent<AudioSource>().clip = clip;
        soundGO.GetComponent<AudioSource>().Play();
    }

    public void PauseSFX(bool trueForPauseFalseForUnpause)
    {
        isPaused = trueForPauseFalseForUnpause;
    }
}
