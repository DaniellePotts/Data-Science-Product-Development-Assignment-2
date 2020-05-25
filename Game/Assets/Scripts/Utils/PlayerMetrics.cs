using UnityEngine;
using TMPro;

public class PlayerMetrics : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI PlayerNameLabel;

    void Start()
    {
        SetLabels();
        player.onMetricUpdate += Player_onMetricUpdate;
    }

    private void Player_onMetricUpdate(double Money, int StoreCount)
    {
        SetLabels();
    }


    void SetLabels()
    {
        PlayerNameLabel.text = player.PlayerData.Name;
    }

    private void OnDestroy()
    {
        player.onMetricUpdate -= Player_onMetricUpdate;
    }
}
