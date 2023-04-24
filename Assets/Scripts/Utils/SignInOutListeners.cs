using UnityEngine;

public class SignInOutListeners : MonoBehaviour
{
    // Called from onClick
    public void SignIn()
    {
        GoogleSignInService google = FindObjectOfType<GoogleSignInService>();
        
        if (google == null)
        {
            Debug.LogError("SignInOut - Couldn't invoke sign in function because GoogleSignInService was not found");
            return;
        }

        google.SignInWithGoogle();
    }

    // Called from onClick
    public void SignOut()
    {
        FirebaseAuthorization firebaseAuthorization = FindObjectOfType<FirebaseAuthorization>();
        if (firebaseAuthorization == null)
        {
            Debug.LogError("SignInOut - Couldn't invoke sign out function because FirebaseAuthorization was not found");
            return;
        }

        firebaseAuthorization.SignOutAuthenticatedUser();
    }
}
