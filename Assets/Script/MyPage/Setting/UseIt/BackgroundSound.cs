using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    public AudioClip backgroundMusicClip;

    private AudioSource audioSource;

    public void btnClick()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusicClip;

        // 저장된 배경음 크기 로드
        float volume = PlayerPrefs.GetFloat("SliderValueB", 1f);
        Debug.Log("backgroundMusic Value: "+volume);
        // 배경음 크기 설정
        audioSource.volume = volume;
        // 배경음 재생
        audioSource.Play();
    }
}

