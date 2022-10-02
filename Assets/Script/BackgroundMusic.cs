using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class BackgroundMusic : MonoBehaviour
{
    private EventInstance backgroundMusic;
    static public bool musicLoader;
    // Start is called before the first frame update
    void Awake()
    {
        if (!musicLoader)
        {
            backgroundMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Game/Music");
            backgroundMusic.start();
            musicLoader = true;
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}
