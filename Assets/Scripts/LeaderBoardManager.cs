using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardManager : MonoBehaviour
{
    [SerializeField] private GameObject leaderListItemPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private Button button;
    private Doss.Tournament.Status status;
    private void OnEnable()
    {
        //Get leaderboard
        GameData.currentTournament.GetTournamentLeaderBoard((success, walletAddress, scores) =>
        {
            if (success)
            {
                for (int i = 0; i < walletAddress.Count; i++)
                {
                    GameObject listItem = Instantiate(leaderListItemPrefab, container);
                    listItem.GetComponent<LeaderBoardListItem>().Setup(walletAddress[i], scores[i]);
                }
            }
        });

        GameData.currentTournament.GetTournamentStatus((success, result) =>
        {
            if (success)
            {
                status = result;
                if (status == Doss.Tournament.Status.RUNNING)
                {
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(OnClickEvent);
                }
                else
                {
                    button.gameObject.SetActive(false);
                }
            }
        });
    }

    public void OnClickEvent()
    {
        GameData.currentTournament.RegisterAndStake((success) =>
        {
            if (success)
            {
                print("Registered");

                //LOAD YOUR GAME SCENE
                Debug.Log("Load Your Game Scene Here", this.gameObject);
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            }
        });
    }
}
