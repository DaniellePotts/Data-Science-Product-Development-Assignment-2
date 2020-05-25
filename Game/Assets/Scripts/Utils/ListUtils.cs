using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListUtils : MonoBehaviour
{
    [SerializeField] private Dictionary<Button, Text> ListButtons;
    [SerializeField] private Player Player;
    [SerializeField] private List<Button> Buttons;
    [SerializeField] private List<Text> ButtonText;

    private void OnEnable()
    {
        ListButtons = new Dictionary<Button, Text>();

        if (Buttons.Count == ButtonText.Count)
        {
            for(var i=0;i< Buttons.Count; i++)
            {
                ListButtons.Add(Buttons[i], ButtonText[i]);
            }
        }
    }
    public void DisplayButtons()
    {
        foreach (KeyValuePair<Button, Text> entry in ListButtons)
        {
            var foundRegion = Player.PlayerData.Stores.Find(s => s.Region.Equals(entry.Value.text));

            if (foundRegion != null)
            {
                entry.Key.interactable = false;
            }
        }
    }

}
