using UnityEngine;
using UnityEngine.UI;

public class SetupGame : MonoBehaviour
{
    [SerializeField] private Player Player;
    [SerializeField] private InputField NameSetter;
    [SerializeField] private Dropdown RegionSetter;
    
    public void SetupPlayer()
    {
        Player.PlayerData.Name = NameSetter.text;

        var m_DropdownValue = RegionSetter.value;

        Player.PlayerData.Stores.Add(new Store()
        {
            Region = RegionSetter.options[m_DropdownValue].text
        });

        Player.PlayerData.StoreCount = 1;
        Player.PlayerData.Money = 1000;
    }
}
