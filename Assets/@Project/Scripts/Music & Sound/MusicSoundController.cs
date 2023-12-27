using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicSoundController : MonoBehaviour
{
    [Header("Setting Panel")]
    [SerializeField] private GameObject musicOnBtn;
    [SerializeField] private GameObject musicOffBtn;
    [SerializeField] private GameObject sfxOnBtn;
    [SerializeField] private GameObject sfxOffBtn;

    [Header("Pause Panel")]
    [SerializeField] private GameObject musicOnBtn1;
    [SerializeField] private GameObject musicOffBtn1;
    [SerializeField] private GameObject sfxOnBtn1;
    [SerializeField] private GameObject sfxOffBtn1;

    private AudioSource[] beginningAudioSource;
    private float[] initialVolume; 
    private AudioSource[] allAudioSource;

    [SerializeField]private AudioSource musicAudio;
    private float initialMusicVolume;

    private bool musicStatus = true;
    private bool soundFxStatus = true;

    // Start is called before the first frame update
    void Start()
    {
        beginningAudioSource = FindObjectsOfType<AudioSource>().Where(f => f.gameObject != musicAudio.gameObject).ToArray();
        initialVolume = new float[beginningAudioSource.Length];

        for (int i = 0; i < beginningAudioSource.Length; i++) {
            initialVolume[i] = beginningAudioSource[i].volume;
        }

        initialMusicVolume = musicAudio.volume;

        #region Initializing the PlayerPrefs
        //Jika belum ada Music Status dan SFX status maka buat dlu PlayerPrefs nya
        if (!PlayerPrefs.HasKey("MusicStatus"))
            PlayerPrefs.SetInt("MusicStatus", 1); //Jika 1 maka On jika 0 maka Off

        if (!PlayerPrefs.HasKey("SfxStatus"))
            PlayerPrefs.SetInt("SfxStatus", 1);
        #endregion

        musicOffBtn.SetActive(false);
        musicOffBtn1.SetActive(false);
        musicOnBtn.SetActive(false);
        musicOnBtn1.SetActive(false);
        sfxOffBtn.SetActive(false);
        sfxOffBtn1.SetActive(false);
        sfxOnBtn.SetActive(false);
        sfxOnBtn1.SetActive(false);

        #region MusicStatus Check
        if (PlayerPrefs.GetInt("MusicStatus") == 0)
        {
            musicAudio.enabled = false;
            musicStatus = false;

            musicOffBtn.SetActive(true);
            musicOffBtn1.SetActive(true);
        }   

        else if (PlayerPrefs.GetInt("MusicStatus") == 1)
        {
            musicAudio.volume = initialMusicVolume;
            musicStatus = true;

            musicOnBtn.SetActive(true);
            musicOnBtn1.SetActive(true);
        }

        #endregion

        #region Sfx Status Check
        if (PlayerPrefs.GetInt("SfxStatus") == 0)
        {
            foreach (AudioSource initialAudio in beginningAudioSource)
            {
                initialAudio.volume = 0f;
            }

            soundFxStatus = false;

            sfxOffBtn.SetActive(true);
            sfxOffBtn1.SetActive(true);
        }

        else if (PlayerPrefs.GetInt("SfxStatus") == 1)
        {
            for (int i = 0; i < beginningAudioSource.Length; i++)
            {
                beginningAudioSource[i].volume = initialVolume[i];
            }

            soundFxStatus = true;

            sfxOnBtn.SetActive(true);
            sfxOnBtn1.SetActive(true);
        }

        #endregion


    }

    // Update is called once per frame
    void Update()
    {
        allAudioSource = FindObjectsOfType<AudioSource>();

        //Jika SFX tidak aktif
        if (soundFxStatus == false)
        {
            foreach (AudioSource myAudio in allAudioSource)
            {
                for (int i = 0; i < beginningAudioSource.Length; i++)
                {
                    if (myAudio == beginningAudioSource[i])
                    {
                        myAudio.enabled = true;
                        break;
                    }

                    if (myAudio == musicAudio)
                        break;

                    myAudio.enabled = false;
                }
            }
        }
    }

    public void SetTheMusicStatus(bool status)
    {
        musicStatus = status;

        musicOffBtn.SetActive(false);
        musicOffBtn1.SetActive(false);
        musicOnBtn.SetActive(false);
        musicOnBtn1.SetActive(false);

        if (musicStatus == true)
        {
            musicAudio.enabled = true;
            musicAudio.volume = initialMusicVolume;
            PlayerPrefs.SetInt("MusicStatus", 1);
 
            musicOnBtn.SetActive(true);
            musicOnBtn1.SetActive(true);

            return;
        }

        musicAudio.enabled = false;
        PlayerPrefs.SetInt("MusicStatus", 0);

        musicOffBtn.SetActive(true);
        musicOffBtn1.SetActive(true);
    }

    public void SetTheSfxStatus(bool status)
    {
        soundFxStatus = status;

        sfxOffBtn.SetActive(false);
        sfxOffBtn1.SetActive(false);
        sfxOnBtn.SetActive(false);
        sfxOnBtn1.SetActive(false);

        if (soundFxStatus == true)
        {
            for (int i = 0; i < beginningAudioSource.Length; i++)
            {
                if (beginningAudioSource[i] != null)
                    beginningAudioSource[i].volume = initialVolume[i];
            }

            PlayerPrefs.SetInt("SfxStatus", 1);

            sfxOnBtn.SetActive(true);
            sfxOnBtn1.SetActive(true);

            return;
        }

        for (int i = 0; i < beginningAudioSource.Length; i++)
        {
            if (beginningAudioSource[i] != null) 
                beginningAudioSource[i].volume = 0f;
        }

        PlayerPrefs.SetInt("SfxStatus", 0);

        sfxOffBtn.SetActive(true);
        sfxOffBtn1.SetActive(true);
    }

    public bool GetTheMusicStatus()
    {
        return musicStatus;
    }

    public bool GetTheSfxStatus()
    {
        return soundFxStatus;
    }
}
