using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GameStartButtonController : MonoBehaviour
{
    [Header("사운드 세팅")]
    public AudioSource bgmSource;       // BGM_Player의 AudioSource
    public AudioSource sfxSource;       // 효과음용 AudioSource (Play On Awake 끔)
    public AudioClip clickClip;         // 똥도로롱 클립

    [Header("씬 세팅")]
    public string nextSceneName;        // 빌드 세팅에 등록된 다음 씬 이름

    private Button btn;

    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnStartClicked);
    }

    void OnStartClicked()
    {
        // 클릭하자마자 버튼 비활성화해서 중복 클릭 방지
        btn.interactable = false;
        StartCoroutine(PlaySfxAndLoad());
    }

    IEnumerator PlaySfxAndLoad()
    {
        // 1) BGM 정지
        if (bgmSource.isPlaying)
            bgmSource.Stop();

        // 2) SFX 재생
        sfxSource.PlayOneShot(clickClip);

        // 3) SFX 길이만큼 대기
        yield return new WaitForSeconds(clickClip.length);

        // 4) 씬 전환
        SceneManager.LoadScene("Stage");
    }
}