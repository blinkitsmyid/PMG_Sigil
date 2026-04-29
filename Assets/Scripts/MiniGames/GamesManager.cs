using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamesManager : MonoBehaviour
{
    public GameObject BowlsPanel;
    public GameObject ItemsPanel;
    public GameObject CandlesPanel;
    public GameObject SmokePanel;
    public static GamesManager Instance;
    public bool IsGameActive { get; private set; }
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        BowlsPanel.SetActive(false);  
    }
    // стоп
    
    public void OpenBowlsGame()
    {
        BowlsPanel.SetActive(true); 
        IsGameActive = true;
    }
    public void OpenItemsGame()
    {
        ItemsPanel.SetActive(true);  
        IsGameActive = true;
    }
    public void OpenCandlesGame()
    {
        CandlesPanel.SetActive(true);
        IsGameActive = true;
    }
    public void OpenSmokeGame()
    {
        SmokePanel.SetActive(true);   
        IsGameActive = true;
    }
    public void CloseBowlsGame()
    {
        BowlsPanel.SetActive(false);   
        IsGameActive = false;
        Debug.Log("Game Closed");
        PlayerController.Instance.SetMovementEnabled(true);
    }
    public void CloseItemsGame()
    {
        ItemsPanel.SetActive(false);   
        IsGameActive = false;
        Debug.Log("Game Closed");
        PlayerController.Instance.SetMovementEnabled(true);
    }
    public void CloseCandlesGame()
    {
        CandlesPanel.SetActive(false);  
        IsGameActive = false;
        Debug.Log("Game Closed");
        PlayerController.Instance.SetMovementEnabled(true);
    }
    public void CloseSmokeGame()
    {
        SmokePanel.SetActive(false);  
        IsGameActive = false;
        Debug.Log("Game Closed");
        PlayerController.Instance.SetMovementEnabled(true);
    }
    public void EsorcismBowlGame()
    {
        PanelManager.Instance.CloseBook();
        OpenBowlsGame();
        PlayerController.Instance.SetMovementEnabled(false); 
    }
    public void EsorcismItemsGame()
    {
        PanelManager.Instance.CloseBook();
        OpenItemsGame();
        PlayerController.Instance.SetMovementEnabled(false); 
    }
    public void EsorcismCandlesGame()
    {
        PanelManager.Instance.CloseBook();
        OpenCandlesGame();
        PlayerController.Instance.SetMovementEnabled(false); 
    }
    public void EsorcismSmokeGame()
    {
        PanelManager.Instance.CloseBook();
        OpenSmokeGame();
        PlayerController.Instance.SetMovementEnabled(false); 
    }
}
