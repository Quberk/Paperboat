using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Facebook.Unity;

public class FacebookAndFirebaseAuthController : MonoBehaviour
{
    FirebaseAuth auth;

    // Start function from Unity's MonoBehavior
    void Start()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(initCallback, onHideUnity);
        }
        else
        {
            // Already initialized
            FB.ActivateApp();
        }
    }

    private void initCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Something went wrong to Initialize the Facebook SDK");
        }
    }

    private void onHideUnity(bool isGameScreenShown)
    {
        if (!isGameScreenShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    public void loginBtnForFB()
    {
        // Permission option list
        var permissons = new List<string>() { "public_profile", "email" };

        FB.LogInWithReadPermissions(permissons, authStatusCallback);
    }

    private void authStatusCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            string accessToken = AccessToken.CurrentAccessToken.ToString();
            // current access token's User ID : aToken.UserId
            loginviaFirebaseFacebook(accessToken);

        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }

    private void loginviaFirebaseFacebook(string accessToken)
    {
        auth = FirebaseAuth.DefaultInstance;
        Credential credential = FacebookAuthProvider.GetCredential(accessToken);

        auth.SignInWithCredentialAsync(credential).ContinueWith(task => {

            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }
}
