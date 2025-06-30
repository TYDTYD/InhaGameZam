using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//사운드 타입 정의
public enum SoundType
{
    //main 화면 사운드
    MainBackgroundMusic,
    MainButtonClick,
    MainGameStart,

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
public class SoundManager : Singleton<SoundManager>
{
    //public static SoundManager Instance { get; private set; }

    //사운드 매핑
    [SerializeField] private List<SoundEntry> soundTable;

    private Dictionary<SoundType, AudioClip> clipMap;
    private Queue<AudioClip> playQueue = new Queue<AudioClip>();

    private AudioSource audioSource;
    private AudioSource bgmSource;


    protected override void OnAwakeWork()
    {
        DontDestroyOnLoad(gameObject);

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

        // 씬 전환 이벤트 구독
        SceneManager.sceneLoaded += OnSceneLoaded;
        // 첫 씬(Startup)도 바로 재생
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);

    }

    protected override void OnDestroyedWork()
    {
        // 반드시 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬이 바뀔 때마다 호출
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 이전 BGM 멈춤
        if (bgmSource.isPlaying) bgmSource.Stop();

        // 재생할 타입 결정
        SoundType typeToPlay;
        if (scene.name == "Main") typeToPlay = SoundType.MainBackgroundMusic;
        else if (scene.name == "Stage") typeToPlay = SoundType.BackgroundMusic;
        else return; // 그 외 씬은 BGM 없음

        // 클립이 있으면 재생
        if (clipMap.TryGetValue(typeToPlay, out var clip) && clip != null)
        {
            bgmSource.clip = clip;
            bgmSource.Play();
        }
    }

    public void PlaySound(SoundType type, float volume = 0.3f)
    {
        if (type == SoundType.BackgroundMusic || type == SoundType.MainBackgroundMusic)
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