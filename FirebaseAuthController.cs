using Firebase.Auth;
using UnityEngine;
using TMPro;

public class FirebaseAuthController : MonoBehaviour
{
    private FirebaseAuth auth;
    public TMP_Text feedbackText;

    void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void SignInWithGoogle(string googleIdToken, string googleAccessToken)
    {
        Credential credential = GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);

        auth.SignInAndRetrieveDataWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                feedbackText.text = "<color=red> Login cancelado </color>";
                return;
            }
            if (task.IsFaulted)
            {
                feedbackText.text = "<color=red> Error: " + task.Exception + "</color>";
                return;
            }

            AuthResult result = task.Result;
            feedbackText.text = "<color=green> Bienvenido " + result.User.DisplayName + "</color>";
            Debug.Log("Usuario autenticado: " + result.User.Email);
        });
    }

     public void SignOut()
        {
            auth.SignOut();
            feedbackText.text = "Sesión cerrada";
        }
}
