
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Reference")]
    public Transform xrOrigin;
    public Transform playerCamera;

    [Header("Panels Reference")]
    public GameObject homepanel;
    public GameObject hudPanel;
    public GameObject victoryPanel;

    [Header("Panel Position")]
    public float panelDistance = 1.5f;
    public float panelHeight = 1.4f;

    [Header("Buttons Reference")]
    public Button playButton;

    [Header("Movement Reference")]
    public Behaviour movementProvider;
    public ToySpawner ToySpawner;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        homepanel.SetActive(true);
        hudPanel.SetActive(false);
        victoryPanel.SetActive(false);

        homepanel.transform.SetParent(xrOrigin, true);
        victoryPanel.transform.SetParent(xrOrigin, true);

        PlacePanelInFront(homepanel.transform);

        movementProvider.enabled = false;

        playButton.onClick.AddListener(playBtn);
    }

    private void LateUpdate()
    {

        if (homepanel.activeSelf)
        {
            PlacePanelInFront(homepanel.transform);
        }
    }

    private void PlacePanelInFront(Transform panel)
    {
        Vector3 forward = playerCamera.forward;

        
        forward.y = 0f;

        if (forward.sqrMagnitude < 0.001f)
            return;

        forward.Normalize();

        
        panel.position =
            xrOrigin.position +
            forward * panelDistance +
            Vector3.up * panelHeight;

        
        panel.rotation =
            Quaternion.LookRotation(-forward, Vector3.up);
    }

    private void playBtn()
    {
        homepanel.SetActive(false);
        hudPanel.SetActive(true);

        movementProvider.enabled = true;

        ToySpawner.SpawnToy();
    }

    public void ShowVictory()
    {
        movementProvider.enabled = false;

        victoryPanel.SetActive(true);

        
        PlacePanelInFront(victoryPanel.transform);
    }
}
