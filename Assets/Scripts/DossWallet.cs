using UnityEngine;
using Doss;
using System.Collections.Generic;
using System.Numerics;

public class DossWallet : MonoBehaviour
{
    [SerializeField] private GameObject webviewButton;

    void Start()
    {
        WalletLoader.InitWallet(webviewButton, WalletGenerated);
    }

    public void WalletGenerated(bool success, string address, BigInteger balance)
    {
        Debug.Log("Wallet : " + WalletSettings.current.developmentMode);
        Debug.Log("success " + success);
        Debug.Log("address " + address);
        Debug.Log("balance " + balance.ToString());
        Debug.Log(StakingLoader.IsClientInitialized());
        Debug.Log("Wallet seting " + WalletSettings.current.polygonChainId);
       
        GameData.address = address;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
