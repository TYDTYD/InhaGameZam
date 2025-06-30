using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//사운드 타입 정의
public enum SoundType
{
    //main 화면 사운드





    //stage 사운드
    Shoot,
    Missile,
    Dash,
    Jump,
    GetHit,
    BackgroundMusic,

    // …추가 가능
}

//인스펙터에서 사운드 타입 넣기 위한 구조체
[System.Serializable]
public struct SoundEntry
{
    public SoundType type;
    public AudioClip clip;
}

//SoundManager 싱글톤
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    //사운드 매핑
    [SerializeField] private List<SoundEntry> soundTable;

    private Dictionary<SoundType, AudioClip> clipMap;
    private Queue<AudioClip> playQueue = new Queue<AudioClip>();

    private AudioSource audioSource;
    private AudioSource bgmSource;


    void Awake()
    {
        // 싱글톤 세팅
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // AudioSource 컴포넌트 세팅
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0;

        // 배경음악용 AudioSource 설정
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.playOnAwake = false; 
        bgmSource.loop = true;
        bgmSource.spatialBlend = 0;
        bgmSource.volume = 0.1f; // 배경음악 볼륨 설정 (필요시 조정)

        // Dictionary로 매핑 초기화
        clipMap = new Dictionary<SoundType, AudioClip>();
        foreach (var entry in soundTable)
            clipMap[entry.type] = entry.clip;

        if (clipMap.TryGetValue(SoundType.BackgroundMusic, out var bgmClip) && bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.Play();
        }

    }
    public void PlaySound(SoundType type, float volume = 0.3f)
    {
        if (type == SoundType.BackgroundMusic)
            return; // 배경음악은 별도로 처리

        if (clipMap.TryGetValue(type, out var clip) && clip != null)
        {
            playQueue.Enqueue(clip);

            var next = playQueue.Dequeue();
            audioSource.PlayOneShot(next, volume);
        }
        else
        {
            Debug.LogWarning($"SoundManager: '{type}' 클립이 없습니다.");
        }
    }
}