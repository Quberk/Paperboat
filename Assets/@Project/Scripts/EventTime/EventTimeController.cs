using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class EventTimeController : MonoBehaviour
{
    [System.Serializable]
    public class DailyReward
    {
        public string tag;
        public DateTime startTime;
        public DateTime goalTime;
        public Text dailyRewardWaitingTimeLeft;
        public GameObject dailyRewardActiveBtn;
    }

    #region Singleton

    public static EventTimeController Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion


    [Header("Event Time Reward")]
    [SerializeField] private List<DailyReward> dailyRewards;

    void Start()
    {
        foreach (DailyReward dailyReward in dailyRewards)
        {
            //Membuat Player Prefs sesuai dengan tag Event Time yang ingin dibuat, jika belum dibuat PlayerPrefnya.
            if (!PlayerPrefs.HasKey(dailyReward.tag + "startTime"))
            {
                PlayerPrefs.SetString((dailyReward.tag + "startTime"), DateTime.Now.ToString());
                PlayerPrefs.SetString((dailyReward.tag + "finishTime"), DateTime.Now.ToString());
            }

        }

    }

    private void Update()
    {
        foreach (DailyReward dailyReward in dailyRewards)
        {
            //Memasukkan nilai StartTime berdasarkan dari waktu yang telah disimpan
            dailyReward.startTime = Convert.ToDateTime(PlayerPrefs.GetString((dailyReward.tag + "startTime")));
            dailyReward.goalTime = Convert.ToDateTime(PlayerPrefs.GetString((dailyReward.tag + "finishTime")));

            TimeSpan currentTime = dailyReward.goalTime - DateTime.Now;

            //Jika hitung mundur waktunya lebih dari 23 jam maka akan mengaktifkan Button untuk mengambil Reward dan menonaktifkan Timer
            if (currentTime.TotalSeconds <= 1)
            {
                dailyReward.dailyRewardActiveBtn.SetActive(true);
                dailyReward.dailyRewardWaitingTimeLeft.gameObject.SetActive(false);
            }

            //Jika hitung mundur waktunya kurang dari 23 jam maka akan menonaktifkan Button untuk mengambil Reward dan mengaktifkan Timer
            else
            {
                dailyReward.dailyRewardActiveBtn.SetActive(false);
                dailyReward.dailyRewardWaitingTimeLeft.gameObject.SetActive(true);

                CountingTheTimeLeft(dailyReward);
            }

        }

    }

    private void CountingTheTimeLeft(DailyReward dailyReward)
    {
        TimeSpan currentTime = dailyReward.goalTime - DateTime.Now;

        string hours = currentTime.Hours.ToString();
        string minutes = currentTime.Minutes.ToString();
        string seconds = currentTime.Seconds.ToString();

        dailyReward.dailyRewardWaitingTimeLeft.text = hours + ":" + minutes + ":" + seconds;
    }

    public void ResetEventTimer(string tag)
    {
        PlayerPrefs.SetString(tag + "startTime", DateTime.Now.ToString());
        PlayerPrefs.SetString(tag + "finishTime", DateTime.Now.AddHours(24).ToString());
    }
}
