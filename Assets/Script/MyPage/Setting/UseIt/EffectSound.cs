using UnityEngine;

public class EffectSound : MonoBehaviour
{
    public AudioClip backgroundMusicClip;

    private AudioSource audioSource;

    public void btnClick()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusicClip;

        // 저장된 배경음 크기 로드
        float volume = PlayerPrefs.GetFloat("SliderValueE", 1f);
        Debug.Log("effect sound Value: " + volume);
        // 배경음 크기 설정
        audioSource.volume = volume;
        // 배경음 재생
        audioSource.Play();
    }
}

