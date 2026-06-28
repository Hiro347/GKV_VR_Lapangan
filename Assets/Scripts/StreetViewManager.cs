using UnityEngine;

public class StreetViewManager : MonoBehaviour
{
    [System.Serializable]
    public class StreetViewLocation
    {
        public string name;
        public Texture2D texture;
        public GameObject navigationUI;
    }

    [Header("Configuration")]
    public Renderer sphereRenderer;
    public StreetViewLocation[] locations;

    [Header("State")]
    public int currentLocationIndex = 0;

    void Start()
    {
        // Initialize at first location
        GoToLocation(0);
    }

    public void GoToLocation(int index)
    {
        if (locations == null || locations.Length == 0)
        {
            Debug.LogWarning("No locations defined in StreetViewManager.");
            return;
        }

        if (index < 0 || index >= locations.Length)
        {
            Debug.LogWarning("Invalid location index: " + index);
            return;
        }

        currentLocationIndex = index;
        StreetViewLocation targetLoc = locations[index];

        // Swap texture on Sphere material
        if (sphereRenderer != null && sphereRenderer.material != null)
        {
            sphereRenderer.material.mainTexture = targetLoc.texture;
        }
        else
        {
            Debug.LogError("Sphere Renderer or Material is not assigned.");
        }

        // Enable correct UI group, disable others
        for (int i = 0; i < locations.Length; i++)
        {
            if (locations[i].navigationUI != null)
            {
                locations[i].navigationUI.SetActive(i == index);
            }
        }

        Debug.Log("Transitioned to: " + targetLoc.name);
    }
}
