using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TournamentListItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tournamentNameText;
    [SerializeField] private TextMeshProUGUI playerCountText;
    [SerializeField] private TextMeshProUGUI stakingAmountText;
    [SerializeField] private TextMeshProUGUI prizePoolText;
    [SerializeField] private TextMeshProUGUI endsOnText;
    [SerializeField] private Button startTournamentButton;

    private Doss.Tournament.TournamentDetail data;
    private Doss.Tournament tourney;

    public void Setup(string address)
    {
        tourney = new Doss.Tournament(address);
        tourney.FetchTournamentDetails((success, result) =>
        {
            if (success)
            {
                data = result;
                tournamentNameText.text = data.name;
                playerCountText.text = "Players Joined : " + data.numPlayersStaked.ToString();
                stakingAmountText.text = "Entry Fee : " + data.stakingAmount.ToString();
                prizePoolText.text = "Prize Pool : " + data.prizePool;

                switch (data.status)
                {
                    case Doss.Tournament.Status.INACTIVE:
                        endsOnText.text = "Starts On: " + UnixTimeStampToDateTime((double)data.startTime).ToString("g");
                        break;

                    case Doss.Tournament.Status.RUNNING:
                        endsOnText.text = "Ends On: " + UnixTimeStampToDateTime((double)data.endTime).ToString("g");
                        startTournamentButton.onClick.RemoveAllListeners();
                        startTournamentButton.onClick.AddListener(OnClickEvent);
                        break;

                    case Doss.Tournament.Status.ENDED:
                        startTournamentButton.onClick.RemoveAllListeners();
                        startTournamentButton.onClick.AddListener(OnClickEvent);
                        endsOnText.text = "Prizes will be distributed soon";
                        break;

                    case Doss.Tournament.Status.SETTLED:
                        endsOnText.text = "Prizes have been distributed";
                        break;
                }
            }
        });
    }
    public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
    {
        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dtDateTime;
    }

    public void OnClickEvent()
    {
        GameData.currentTournament = tourney;
        TournamentManager.OpenLeaderBoard();
    }
}
