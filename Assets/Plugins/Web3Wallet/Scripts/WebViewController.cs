using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

namespace Doss
{
    public class WebViewController : MonoBehaviour
    {
        #region Constants
        private const string TOKEN_FILE_NAME = "token.data";
        #endregion

        #region Properties
        private static string _tokenfilePath => System.IO.Path.Combine(Application.persistentDataPath, TOKEN_FILE_NAME);
        #endregion

        private GameObject avatar;

        Action<bool, string, BigInteger> OnWalletCreated;

        [SerializeField] private WebView webView;
        [SerializeField] private GameObject loadingLabel = null;
        [SerializeField] private Button displayButton = null;
        [SerializeField] private Button closeButton = null;
        [SerializeField] private WalletInit walletInit = null;

        private void Start()
        {
            displayButton.onClick.AddListener(DisplayWebView);
            closeButton.onClick.AddListener(HideWebView);
            OnWalletCreated = WalletLoader.OnWalletLoaded;
        }

        // Display WebView or create it if not initialized yet
        private void DisplayWebView()
        {
#if UNITY_EDITOR || !(UNITY_ANDROID || UNITY_IOS)
            Doss.WalletAppRepository.PostLocalAuthTokenVerification((resp, error) =>
            {
                if (error == "")
                {
                    OnAvatarCreated(resp.accessToken, resp.refreshToken, resp.walletExists);
                }
                else
                {
                    Debug.Log("Failed to sign in locally err: " + error);
                }
                displayButton.gameObject.SetActive(false);
            }, new Doss.LocalAuthTokenVerification
            {
                clientId = WalletSettings.current.clientId,
                projectId = WalletSettings.current.projectId,
                walletAddress = WalletSettings.current.walletAddress,
            });
#else
            if (webView == null)
            {
                webView = FindObjectOfType<WebView>();
            }

            if (webView.Loaded)
            {
                webView.SetVisible(true);
            }
            else
            {
                webView.OnAvatarCreated = OnAvatarCreated;
                webView.CreateWebView();
            }

            closeButton.gameObject.SetActive(true);
            displayButton.gameObject.SetActive(false);
#endif
        }

        private void HideWebView()
        {
            webView.SetVisible(false);
            closeButton.gameObject.SetActive(false);
            displayButton.gameObject.SetActive(true);
        }

        // WebView callback for retrieving avatar url
        private void OnAvatarCreated(string authToken, string refreshToken, bool isWallet)
        {
            webView.SetVisible(false);
            displayButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(false);

            Constants.AccessToken = authToken;

            var tokens = new Tokens { AccessToken = authToken, RefreshToken = refreshToken };
            Helper.SaveObjectInFile<Tokens>(tokens, _tokenfilePath);

            StakingLoader.InitClient((success, message) =>
            {
                if (success)
                {
                    walletInit.ShowPasswordBox(isWallet, onHideWalletBox);
                }
                else
                {
                    OnWalletCreated?.Invoke(false, "", 0);
                }
            });
        }

        private void onHideWalletBox()
        {
            displayButton.gameObject.SetActive(true);
            closeButton.gameObject.SetActive(false);
        }

        private void Destroy()
        {
            displayButton.onClick.RemoveListener(DisplayWebView);
            closeButton.onClick.RemoveListener(HideWebView);
        }
    }
}
