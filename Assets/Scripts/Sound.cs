using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound : MonoBehaviour
{
    public AudioClip clip;
    public string clipName;
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    public bool loop;
    [HideInInspector]
    public AudioSource source;
}
