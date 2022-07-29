using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TournamentManager : MonoBehaviour
{
    public static TournamentManager instance;

    [SerializeField] private GameObject tournamentListItemPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private GameObject leaderboardPage;

    private List<string> allTournamentAddresses = new List<string>();
    private List<TournamentListItem> item = new List<TournamentListItem>();
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void ClearList()
    {
        foreach(var item in item)
        {
            Destroy(item.gameObject);
        }
        item.Clear();
    }
    private void OnEnable()
    {
        ClearList();
        GetAllTournaments();
    }
    private void GetAllTournaments()
    {
        Doss.TournamentFactory.GetTournaments((success, result) =>
        {
            if (success)
            {
                foreach (var item in result)
                {
                    print(item);
                    allTournamentAddresses.Add(item);
                    GameObject listItem = Instantiate(tournamentListItemPrefab, container);
                    this.item.Add(listItem.GetComponent<TournamentListItem>());
                    listItem.GetComponent<TournamentListItem>().Setup(item);
                }
                Debug.Log($"{result.Count} total tournaments");
            }
        });
    }
    public static void OpenLeaderBoard()
    {
        instance.leaderboardPage.SetActive(true);
        instance.gameObject.SetActive(false);
    }
}
