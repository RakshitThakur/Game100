using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Numerics;

namespace Doss
{
    public class WalletInit : MonoBehaviour
    {
        [SerializeField] private GameObject toUnlock;
        [SerializeField] private GameObject toCreateNew;
        [SerializeField] private Text password;


        Action onHidePasswordBox;
        bool walletPresent;
        Action<bool, string, BigInteger> OnWalletCreated;

        void Start()
        {
            gameObject.SetActive(false);
            OnWalletCreated = WalletLoader.OnWalletLoaded;
        }

        public void ShowPasswordBox(bool isWallet, Action onHideWalletBox)
        {
            gameObject.SetActive(true);
            walletPresent = isWallet;
            toUnlock.SetActive(isWallet);
            toCreateNew.SetActive(!isWallet);
            this.onHidePasswordBox = onHideWalletBox;
        }

        public void OnSubmit()
        {
            var passHash = Helper.GetHashSha256(password.text);
            if (walletPresent)
            {
                WalletManager.ImportWalletFromServer((success) =>
                {
                    if (success)
                    {
                        HidePasswordBox();
                        WalletFunctions.GetDossBalance((success, balance) =>
                        {
                            if (success)
                            {
                                OnWalletCreated?.Invoke(true, WalletManager.address, balance);
                            }
                            else
                            {
                                OnWalletCreated?.Invoke(false, WalletManager.address, 0);
                            }
                        }, WalletManager.address);
                    }
                    else
                    {
                        OnWalletCreated?.Invoke(false, "", 0);
                    }
                }, passHash);
            }
            else
            {
                WalletManager.CreateWallet((success) =>
                {
                    if (success)
                    {
                        HidePasswordBox();
                        WalletFunctions.GetDossBalance((success, balance) =>
                        {
                            if (success)
                            {
                                OnWalletCreated?.Invoke(true, WalletManager.address, balance);
                            }
                            else
                            {
                                OnWalletCreated?.Invoke(false, WalletManager.address, 0);
                            }
                        }, WalletManager.address);
                    }
                    else
                    {
                        OnWalletCreated?.Invoke(false, "", 0);
                    }
                }, passHash);
            }
        }

        public void HidePasswordBox()
        {
            onHidePasswordBox?.Invoke();
            gameObject.SetActive(false);
        }

    }
}
