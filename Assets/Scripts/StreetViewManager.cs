using UnityEngine;

public class StreetViewManager : MonoBehaviour
{
    [System.Serializable]
    public class StreetViewLocation
    {
        public string name;
        public Texture2D texture;
        public GameObject navigationUI;
        public GameObject infographicUI; // Reference to the infographic panel for this location
    }

    [Header("Configuration")]
    public Renderer sphereRenderer;
    public StreetViewLocation[] locations;

    [Header("State")]
    public int currentLocationIndex = 0;

    void Start()
    {
        InitializeButtonLabels();
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && audioSource.isPlaying)
        {
            UpdateMusicButtonsText("🔊 Pause Suara Lapangan");
        }
        else
        {
            UpdateMusicButtonsText("🔊 Putar Suara Lapangan");
        }
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

        if (sphereRenderer != null && sphereRenderer.material != null)
        {
            sphereRenderer.material.mainTexture = targetLoc.texture;
        }
        else
        {
            Debug.LogError("Sphere Renderer or Material is not assigned.");
        }

        for (int i = 0; i < locations.Length; i++)
        {
            if (locations[i].navigationUI != null)
            {
                locations[i].navigationUI.SetActive(i == index);
            }
            if (locations[i].infographicUI != null)
            {
                locations[i].infographicUI.SetActive(false); // Hide all infographics during transition
            }
        }

        InitializeButtonLabels();
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && audioSource.isPlaying)
        {
            UpdateMusicButtonsText("🔊 Pause Suara Lapangan");
        }
        else
        {
            UpdateMusicButtonsText("🔊 Putar Suara Lapangan");
        }

        Debug.Log("Transitioned to: " + targetLoc.name);
    }

    public void InitializeButtonLabels()
    {
        GameObject canvas = GameObject.Find("CanvasParent/Canvas");
        if (canvas != null)
        {
            SetButtonLabel(canvas, "UI_Lapangan1/Btn_ToL2", "Ke Spot 2 GOR Lama");
            SetButtonLabel(canvas, "UI_Lapangan1/Btn_Info", "Info Lapangan Basket");

            SetButtonLabel(canvas, "UI_Lapangan2/Btn_ToL1", "Ke Spot 1 GOR Lama");
            SetButtonLabel(canvas, "UI_Lapangan2/Btn_ToL3", "Ke Spot 3 GOR Lama");
            SetButtonLabel(canvas, "UI_Lapangan2/Btn_Info", "Filosofi & Sejarah");

            SetButtonLabel(canvas, "UI_Lapangan3/Btn_ToL2", "Ke Spot 2 GOR Lama");
            SetButtonLabel(canvas, "UI_Lapangan3/Btn_Info", "Fakta Menarik");
        }
    }

    private void SetButtonLabel(GameObject canvas, string buttonPath, string newLabel)
    {
        Transform btnTrans = canvas.transform.Find(buttonPath);
        if (btnTrans != null)
        {
            TMPro.TextMeshProUGUI[] tmps = btnTrans.GetComponentsInChildren<TMPro.TextMeshProUGUI>(true);
            foreach (var tmp in tmps)
            {
                if (tmp.gameObject.name.Contains("Text (TMP)"))
                {
                    tmp.text = newLabel;
                }
            }
        }
    }

    public void ToggleMusic()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
                UpdateMusicButtonsText("🔊 Putar Suara Lapangan");
            }
            else
            {
                audioSource.Play();
                UpdateMusicButtonsText("🔊 Pause Suara Lapangan");
            }
        }
    }

    public void UpdateMusicButtonsText(string newLabel)
    {
        GameObject canvas = GameObject.Find("CanvasParent/Canvas");
        if (canvas != null)
        {
            SetButtonLabel(canvas, "UI_Lapangan1/Btn_MusicToggle", newLabel);
            SetButtonLabel(canvas, "UI_Lapangan2/Btn_MusicToggle", newLabel);
            SetButtonLabel(canvas, "UI_Lapangan3/Btn_MusicToggle", newLabel);
        }
    }

    public void ShowInfographic()
    {
        int index = currentLocationIndex;
        if (locations == null || index < 0 || index >= locations.Length) return;

        StreetViewLocation currentLoc = locations[index];
        if (currentLoc.infographicUI != null)
        {
            if (currentLoc.navigationUI != null)
            {
                currentLoc.navigationUI.SetActive(false); // Hide main buttons
            }
            currentLoc.infographicUI.SetActive(true); // Show info panel
            Debug.Log("Showing infographic for: " + currentLoc.name);
        }
    }

    public void HideInfographic()
    {
        int index = currentLocationIndex;
        if (locations == null || index < 0 || index >= locations.Length) return;

        StreetViewLocation currentLoc = locations[index];
        if (currentLoc.infographicUI != null)
        {
            currentLoc.infographicUI.SetActive(false); // Hide info panel
            if (currentLoc.navigationUI != null)
            {
                currentLoc.navigationUI.SetActive(true); // Bring back main buttons
            }
            Debug.Log("Hidden infographic for: " + currentLoc.name);
        }
    }

    public void ShowCustomInfographic(GameObject customPanel)
    {
        int index = currentLocationIndex;
        if (locations == null || index < 0 || index >= locations.Length) return;

        StreetViewLocation currentLoc = locations[index];
        if (currentLoc.navigationUI != null)
        {
            currentLoc.navigationUI.SetActive(false); // Hide main buttons
        }
        customPanel.SetActive(true);
        Debug.Log("Showing custom infographic: " + customPanel.name);
    }

    public void HideCustomInfographic(GameObject customPanel)
    {
        int index = currentLocationIndex;
        if (locations == null || index < 0 || index >= locations.Length) return;

        customPanel.SetActive(false);
        StreetViewLocation currentLoc = locations[index];
        if (currentLoc.navigationUI != null)
        {
            currentLoc.navigationUI.SetActive(true); // Bring back main buttons
        }
        Debug.Log("Hidden custom infographic: " + customPanel.name);
    }
}
