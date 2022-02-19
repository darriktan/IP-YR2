using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//importing directives for authentication
using Firebase;
using Firebase.Auth; //Using Firebase Auth service
using Firebase.Extensions; //For threading purposes
using Firebase.Database; //Firebase real time databa
using TMPro; //TextMesh Pro
using UnityEngine.UI; //UI handling
using UnityEngine.SceneManagement; //scene loading
using System.Threading.Tasks;

using System.Text.RegularExpressions;

public class AuthManager : MonoBehaviour
{
    //Firebase references
    public FirebaseAuth auth;
    public DatabaseReference dbReference;
    //retrieve user text
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField usernameInput;

    //setup buttons and UI
    public GameObject signupBtn;
    public GameObject loginBtn;
    public GameObject forgetPasswordBtn;
    public GameObject logoutBtn;


    public TextMeshProUGUI errorMsgContent;

    void Awake()
    {
        Initalizefirebase();
        //CreateNewSimplePlayer("uuidxxx", "displaynaemtest", "usernametest", "email test");
    }

    //handle Firebase in the start
    void Initalizefirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public async void SignUpNewUser()
    {
        string email = emailInput.text.Trim();
        string password = passwordInput.text.Trim();

        if (ValidateEmail(email) && ValidatePassword(password))
        {
            FirebaseUser newPlayer = await SignUpNewUserOnly(email, password);

            if (newPlayer != null)
            {
                string username = usernameInput.text;

                await CreateNewSimplePlayer(newPlayer.UserId, username, username, newPlayer.Email);
                await UpdatePlayerDisplayName(username);//update user's display name in authenticate service
                SceneManager.LoadScene(1);
            }
        }
        else
        {
            errorMsgContent.text = "Error in Signing Up. Invalid email or password";
            errorMsgContent.gameObject.SetActive(true);
        }

    }

    public async Task<FirebaseUser> SignUpNewUserOnly(string email, string password)
    {
        Debug.Log("Signup");

        FirebaseUser newPlayer = null;
        await auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                if (task.Exception != null)
                {
                    string errorMsg = this.HandleSignUpError(task);
                    errorMsgContent.text = errorMsg;
                    Debug.Log("Error in signing up " + errorMsg);
                    errorMsgContent.gameObject.SetActive(true);
                }

                //Debug.LogError("Sorry, an error has occured while creating your new account, ERROR: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                errorMsgContent.gameObject.SetActive(false);
                newPlayer = task.Result;

                Debug.LogFormat("New Player Details {0} {1}", newPlayer.UserId, newPlayer.Email);
            }
        });
        return newPlayer;
    }

    public async Task CreateNewSimplePlayer(string uuid, string displayName,
        string userName, string email)
    {
        SimpleGamePlayer newPlayer = new SimpleGamePlayer(displayName, userName, email);
        Debug.LogFormat("Player details : {0}", newPlayer.PrintPlayer());

        //root/players/$uuid
        await dbReference.Child("players/" + uuid).SetRawJsonValueAsync(newPlayer.SimpleGamePlayerToJson());

        //Update auth player with new display name -> tagging along the username input field
        //UpdatePlayerDisplayName(displayName);
    }

    public async Task UpdatePlayerDisplayName(string displayName)
    {
        if (auth.CurrentUser != null)
        {
            UserProfile profile = new UserProfile
            {
                DisplayName = displayName
            };
            await auth.CurrentUser.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was cancelled");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }
                Debug.Log("User profile updated successfully");
                Debug.LogFormat("Checking current user display name from auth {0}", GetCurrentUserDisplayName());
            });
        }
    }

    //simple function to retrieve the auth object's display name in authmanager.cs
    public string GetCurrentUserDisplayName()
    {
        //CurrentUser is a built in function of FirebaseAuth FirebaseUser
        return auth.CurrentUser.DisplayName;
    }

    public void LoginUser()
    {
        Debug.Log("Login");
        string email = emailInput.text.Trim();
        string password = passwordInput.text.Trim();

        if (ValidateEmail(email) && ValidatePassword(password))
        {
            auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    string errorMsg = this.HandleSigninError(task);
                    errorMsgContent.text = errorMsg;
                    Debug.Log("Error in signing in " + errorMsg);
                    errorMsgContent.gameObject.SetActive(true);
                    //Debug.LogError("Sorry, an error has occured while logging into your account, ERROR: " + task.Exception);
                }
                else if (task.IsCompleted)
                {
                    errorMsgContent.gameObject.SetActive(false);
                    FirebaseUser currentPlayer = task.Result;
                    Debug.LogFormat("Welcome to First Responder {0} {1}", currentPlayer.UserId, currentPlayer.Email);
                    SceneManager.LoadScene(1);
                }
            });
        }
        else
        {
            errorMsgContent.text = "Error in Signing In. Invalid email or password";
            errorMsgContent.gameObject.SetActive(true);
        }
    }

    public void LogoutUser()
    {
        if (auth.CurrentUser != null)
        {
            Debug.LogFormat("Auth User {0} {1}", auth.CurrentUser.UserId, auth.CurrentUser.Email);

            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            auth.SignOut();
            if (currentSceneIndex != 0)
            {
                SceneManager.LoadScene(0);
            }
        }
        Debug.Log("Logout");
    }

    public void ForgetPassword()
    {
        string email = emailInput.text.Trim();

        auth.SendPasswordResetEmailAsync(email).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Sorry, an error has occured while sending a password reset, ERROR: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                Debug.LogFormat("Forget password email sent successfully...");
            }
        });
        Debug.Log("ForgetPass");
    }

    public bool ValidateEmail(string email)
    {
        bool isValid = false;

        const string pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
        const RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

        if (email != "" && Regex.IsMatch(email, pattern, options))
        {
            isValid = true;
        }

        return isValid;
    }

    public bool ValidatePassword(string password)
    {
        bool isValid = false;

        if (password != "" && password.Length >= 6)
        {
            isValid = true;
        }
        return isValid;
    }

    /*public bool ValidateUsername(string username)
    {
        bool isValid = false;

        if (username != "" && username.Length >= 3)
        {
            isValid = true;
        }
        return isValid;
    }*/

    public string HandleSignUpError(Task<FirebaseUser> task)
    {
        string errorMsg = "";

        if (task.Exception != null)
        {
            FirebaseException firebaseEx = task.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            errorMsg = "Sign up fail\n";
            switch (errorCode)
            {
                case AuthError.EmailAlreadyInUse:
                    errorMsg += "Email already in use.";
                    break;
                case AuthError.WeakPassword:
                    errorMsg += "Password is weak. Use at least 6 characters.";
                    break;
                case AuthError.MissingPassword:
                    errorMsg += "Password is missing.";
                    break;
                case AuthError.InvalidEmail:
                    errorMsg += "Invalid email used.";
                    break;
                default:
                    errorMsg += "Issue in authentication: " + errorCode;
                    break;
            }
            Debug.Log("Error message" + errorMsg);
        }
        return errorMsg;
    }

    public string HandleSigninError(Task<FirebaseUser> task)
    {
        string errorMsg = "";

        if (task.Exception != null)
        {
            FirebaseException firebaseEx = task.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            errorMsg = "Sign in fail\n";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    errorMsg += "Email is missing.";
                    break;
                case AuthError.WrongPassword:
                    errorMsg += "Wrong password.";
                    break;
                case AuthError.MissingPassword:
                    errorMsg += "Password is missing.";
                    break;
                case AuthError.InvalidEmail:
                    errorMsg += "Invalid email used.";
                    break;
                case AuthError.UserNotFound:
                    errorMsg += "User not found.";
                    break;
                default:
                    errorMsg += "Issue in authentication: " + errorCode;
                    break;
            }
            Debug.Log("Error message" + errorMsg);
        }
        return errorMsg;
    }
}
