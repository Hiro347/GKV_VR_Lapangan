using UnityEngine;
using UnityEngine.UI;

public class InfographicController : MonoBehaviour
{
    [Header("UI Panels")]
    [Tooltip("Panel infografis yang ingin ditampilkan.")]
    public GameObject infographicPanel;

    [Tooltip("UI Navigasi utama (tombol-tombol lain) yang ingin disembunyikan saat infografis terbuka (Opsional).")]
    public GameObject navigationUI;

    [Header("Buttons")]
    [Tooltip("Tombol untuk membuka infografis (Opsional).")]
    public Button openButton;

    [Tooltip("Tombol untuk menutup infografis (Opsional).")]
    public Button closeButton;

    void Start()
    {
        // Pastikan panel infografis tertutup saat game dimulai
        if (infographicPanel != null)
        {
            infographicPanel.SetActive(false);
        }

        // Hubungkan event click secara otomatis jika tombol dipasang
        if (openButton != null)
        {
            openButton.onClick.AddListener(OpenInfographic);
        }

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseInfographic);
        }
    }

    /// <summary>
    /// Menampilkan panel infografis dan menyembunyikan UI navigasi.
    /// </summary>
    public void OpenInfographic()
    {
        if (infographicPanel != null)
        {
            infographicPanel.SetActive(true);
            
            if (navigationUI != null)
            {
                navigationUI.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("Infographic Panel belum dipasang di inspector!");
        }
    }

    /// <summary>
    /// Menyembunyikan panel infografis dan menampilkan kembali UI navigasi.
    /// </summary>
    public void CloseInfographic()
    {
        if (infographicPanel != null)
        {
            infographicPanel.SetActive(false);
            
            if (navigationUI != null)
            {
                navigationUI.SetActive(true);
            }
        }
    }
}
