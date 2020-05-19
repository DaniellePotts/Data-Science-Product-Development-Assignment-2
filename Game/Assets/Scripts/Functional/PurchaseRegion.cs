using UnityEngine;
using UnityEngine.UI;

public class PurchaseRegion : MonoBehaviour
{
    [SerializeField] private Player Player;
    [SerializeField] private Text PopupLabel;

    public void SetPopupLabel(string regionName)
    {
        PopupLabel.text = regionName;
    }

    public void Purchase(Text regionName)
    {
        Player.PlayerData.Stores.Add(new Store
        {
            Region = regionName.text
        });

        SetPopupLabel(regionName.text);
    }
}
