using Google;
using UnityEngine;
using TMPro;

public class GoogleSignInController : MonoBehaviour
{
    public FirebaseAuthController firebaseAuthController;
    public TMP_Text feedbackText;

    private GoogleSignInConfiguration configuration;

    void Awake()
    {
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = "tartessos-36f23.apps.googleusercontent.com",
            RequestIdToken = true,
            RequestEmail = true
        };
    }

    public void OnGoogleSignInButton()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }

    private void OnAuthenticationFinished(System.Threading.Tasks.Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            feedbackText.text = "<color=red> Error en Google Sign-In </color>";
        }else if (task.IsCanceled)
        {
            feedbackText.text = "<color=red> Cancelado </color>";
        }
        else
        {
            string idToken = task.Result.IdToken;
            string accessToken = task.Result.AuthCode;
            feedbackText.text = "Verificando con Firebase...";

            firebaseAuthController.SignInWithGoogle(idToken, accessToken);
        }
    }
}
