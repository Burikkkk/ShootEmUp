using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private LevelGraphics bossLevel;
    private LevelGraphics nextLevel;

    private AudioClip nextAudioClip;

    public void SetLevel(LevelGraphics bossLevel, LevelGraphics nextLevel, AudioClip nextAudioClip)
    {
        this.bossLevel = bossLevel;
        this.nextLevel = nextLevel;
        this.nextAudioClip = nextAudioClip;
    }

    private void OnDestroy()
    {
        if (bossLevel != null)
        {
            bossLevel.StartFading(false);
        }

        if (nextLevel != null)
        {
            if (nextAudioClip != null)
            {
                MusicSource.instance.audioSource.clip = nextAudioClip;
                MusicSource.instance.audioSource.Play();
            }
            nextLevel.SetAllRenderers(true);
            nextLevel.StartFading(true);
        }
    }
}
