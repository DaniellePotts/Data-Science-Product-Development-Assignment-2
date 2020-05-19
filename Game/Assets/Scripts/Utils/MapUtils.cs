using System.Collections.Generic;
using UnityEngine;

public class MapUtils : MonoBehaviour
{
    [SerializeField] private Player Player;
    [SerializeField]private List<Region> Regions;

    [SerializeField] private GameObject RegionMarker;

    [SerializeField] private GameObject ObtainedRegion;
    [SerializeField] private GameObject UnobtainedRegion;

    [SerializeField] private bool Trigger;

    List<GameObject> Markers = new List<GameObject>();

    void Awake()
    {
        if (Trigger)
        {
            for (var i = 0; i < Player.PlayerData.Stores.Count; i++)
            {
                var regionExists = Regions.Find(x => x.Name.Equals(Player.PlayerData.Stores[i].Region));

                if (regionExists)
                {
                    var newMarker = Instantiate(RegionMarker);
                    newMarker.transform.parent = regionExists.transform;
                    newMarker.transform.position = regionExists.transform.position;
                    newMarker.SetActive(true);
                }
            }
        }
        else
        {
            Debug.Log("displaying all regions");
            DisplayAllRegions();
        }
    }

    public void DisplayAllRegions()
    {
        DestroyMarkers();

        for (var i = 0; i < Regions.Count; i++)
        {
            var hasRegion = Player.PlayerData.Stores.Find(s => s.Region.Equals(Regions[i].Name));

            var newMarker = Instantiate(UnobtainedRegion);

            if (hasRegion != null)
            {
                newMarker = Instantiate(ObtainedRegion);
            }
            
            newMarker.transform.parent = Regions[i].transform;
            newMarker.transform.localPosition = Vector3.zero;
            newMarker.transform.localScale = new Vector3(1f, 1f, 1f);
            newMarker.SetActive(true);

            Markers.Add(newMarker);
        }
    }

    private void DestroyMarkers()
    {
        Markers.ForEach((marker) =>
        {
            Destroy(marker);
        });

        Markers.Clear();
    }
}
