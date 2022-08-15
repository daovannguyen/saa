using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sound
{
    public SoundName name;
    public AudioClip clip;
    [HideInInspector]
    public AudioSource source;
}
public class SoundManager : Singleton<SoundManager>
{
    public Sound[] sounds;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
        }
    }
    public Sound GetSoundByName(SoundName name)
    {
        foreach (var i in sounds)
        {
            if (i.name == name)
            {
                return i;
            }
        }
        return null;
    }
    public void PlaySound(SoundName name, bool loop)
    {
        Sound s = GetSoundByName(name);
        if(s == null)
        {
            Debug.Log($"Sound {name} not found");
            return;
        }
        s.source.loop = loop;
        s.source.Play();
    }
    private void OnEnable()
    {
        EventManager.TurnOnAudio += PlaySound;
    }
    private void OnDisable()
    {
        EventManager.TurnOnAudio -= PlaySound;
    }
}
