using TMPro;
using UnityEngine;

public class DateDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI DayMonthYear;
    [SerializeField] private Player Player;
    [SerializeField] private TextMeshProUGUI CurrentSeason;

    public int Seconds;

    private int Rounds = 1;

    void Start()
    {
        UpdateDateText(Player.PlayerData.CurrentDate);
        UpdateSeason();
    }

    void OnEnable()
    {
        InvokeRepeating("UpdateDate", 0, Seconds);

    }

    void UpdateDate()
    {
        if (Rounds > 1)
        {
            Player.PlayerData.CurrentDate = DateUtils.AddDay(Player.PlayerData.CurrentDate, 3);
            UpdateDateText(Player.PlayerData.CurrentDate);
        }

        Rounds++;
    }

        void UpdateDateText(string date)
    {
        DayMonthYear.text = DateUtils.DateToText(date);

    }

    void UpdateSeason()
    {
       CurrentSeason.text = DateUtils.DetermineSeason( Player.PlayerData.CurrentDate);
    }

}
