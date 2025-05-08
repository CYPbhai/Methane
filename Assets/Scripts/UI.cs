using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance { get; private set; }
    [SerializeField] GameObject hydrogenPanel;
    [SerializeField] GameObject carbonPanel;
    [SerializeField] GameObject bondPanel;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        hydrogenPanel.SetActive(false);
        carbonPanel.SetActive(false);
        bondPanel.SetActive(false);
    }

    public void ShowHydrogenPanel()
    {
        carbonPanel.SetActive(false);
        bondPanel.SetActive(false);
        hydrogenPanel.SetActive(true);
    }

    public void ShowCarbonPanel()
    {
        hydrogenPanel.SetActive(false);
        bondPanel.SetActive(false);
        carbonPanel.SetActive(true);
    }

    public void ShowBondPanel()
    {
        hydrogenPanel.SetActive(false);
        carbonPanel.SetActive(false);
        bondPanel.SetActive(true);
    }

    public void Hide()
    {
        hydrogenPanel.SetActive(false);
        carbonPanel.SetActive(false);
        bondPanel.SetActive(false);
    }
}
