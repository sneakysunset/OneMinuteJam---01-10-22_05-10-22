using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class BackgroundMusic : MonoBehaviour
{
    private EventInstance backgroundMusic;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        backgroundMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Game/Music");
        backgroundMusic.start();
    }
}
