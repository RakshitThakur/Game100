using System.Numerics;
using TMPro;
using UnityEngine;

public class LeaderBoardListItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI usernameText;
    [SerializeField] private TextMeshProUGUI scoreText;
    public void Setup(string walletAddress, BigInteger score)
    {
        usernameText.text = walletAddress;
        scoreText.text = score.ToString();
    }
}
