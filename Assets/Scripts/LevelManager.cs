using Doss;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public TextMeshProUGUI walletBalanceText;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }
    private void OnDestroy()
    {
    }

    private void Start()
    {
        FetchWalletBalance();
    }
    private void FetchWalletBalance()
    {
        WalletFunctions.GetDossBalance((result, amount) =>
        {
            if (result)
            {
                print("Success");
                walletBalanceText.text = "Doss Token : "  + amount.ToString();
            }
            else
            {
                walletBalanceText.text = "NaN";
            }
        }, GameData.address);
    }

    public void GetTournaments()
    {
        TournamentFactory.GetLiveTournaments((result, response) =>
        {
            Debug.Log(response.Count);
        });
    }
    public void Play()
    {
        Time.timeScale = 1;
        
        SceneManager.LoadScene(2);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
