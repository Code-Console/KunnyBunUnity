using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using System.Text.RegularExpressions;
using Firebase.Extensions;
public class GameAuth : MonoBehaviour
{
    private Firebase.Auth.FirebaseAuth auth;
    Text singUpErrorTxt, singInErrorTxt;
    string error = "";
    Menu menu;
    private int Action = 0;
    // Start is called before the first frame update
    void Start()
    {
        Transform gametrans = transform;
        var email = "email@gmail.com";
        var password = "tester123";
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        menu = transform.GetComponent<Menu>();
        singUpErrorTxt = transform.Find("SingUp").Find("Error").GetComponent<Text>();
        singInErrorTxt = transform.Find("Login").Find("Error").GetComponent<Text>();
        if (auth.CurrentUser != null)
        {
            Debug.Log("User profile updated successfully." + auth.CurrentUser.DisplayName);
        }
           
         Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            //Debug.Log("User profile updated successfully." + user.DisplayName);
            //Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
            //{
            //    DisplayName = "Yogesh Bangar",
            //};
            //user.UpdateUserProfileAsync(profile).ContinueWith(task =>
            //{
            //    if (task.IsCanceled)
            //    {
            //        Debug.LogError("UpdateUserProfileAsync was canceled.");
            //        return;
            //    }
            //    if (task.IsFaulted)
            //    {
            //        Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
            //        return;
            //    }

            //    Debug.Log("User profile updated successfully.");
            //});
        }
    }
    public void SignIn()
    {
        Debug.Log("SignIn ");
        Transform tranSignIn = transform.Find("Login").Find("Panel");
        string email = tranSignIn.Find("Email").GetComponent<InputField>().text;
        string password = tranSignIn.Find("Password").GetComponent<InputField>().text;
        string Error = "";
        if (!TestEmail.IsEmail(email))
        {
            Error = "Use correct Email";
        }
        else if (password.Length < 6)
        {
            Error = "Incorrect password";
        }
        else
        {
            transform.Find("Loading").gameObject.SetActive(true);
            auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                    error = "Email or Password incrrect.";
                    Action = 4;
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    error = "Email or Password incrrect.";
                    Action = 4;
                    return;
                }
                Action = 5;
                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",newUser.DisplayName, newUser.UserId);
            });
        }
        singInErrorTxt.text = "" + Error;
    }
    public void SignUp() {
        Debug.Log("SignUp ");
        Transform tranSignup = transform.Find("SingUp").Find("Panel");
        string username = tranSignup.Find("User").GetComponent<InputField>().text;
        string Email = tranSignup.Find("Email").GetComponent<InputField>().text;
        string Password = tranSignup.Find("Password").GetComponent<InputField>().text;
        string confirm = tranSignup.Find("confirm").GetComponent<InputField>().text;
        string Error = "";

        Debug.Log("[username = " + username+ "][Email = " + Email + "]");
        Debug.Log("[Password = " + Password + "][confirm = " + confirm + "]");
        if (username.Length == 0)
        {
            Error = "Please enter username.";
        }
        else if (Password.Length < 6)
        {
            Error = "use 6 characters or more for password";
        }
        else if (!Password.Equals(confirm))
        {
            Error = "Those password didn't match.";
        }
        else if (!TestEmail.IsEmail(Email))
        {
            Error = "Use correct Email";
        }
        else
        {
            transform.Find("Loading").gameObject.SetActive(true);
            auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            auth.CreateUserWithEmailAndPasswordAsync(Email, Password).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    transform.Find("SingUp").Find("Error").GetComponent<Text>().text = "Registration was canceled";
                    error = "Registration was canceled";
                    Action = 2;
                    return;
                }
                if (task.IsFaulted)
                {
                    foreach (FirebaseException exception in task.Exception.Flatten().InnerExceptions)
                    {
                        string authErrorCode = "";
                        Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                        if (firebaseEx != null)
                        {
                            authErrorCode = string.Format("AuthError.{0}: ",((Firebase.Auth.AuthError)firebaseEx.ErrorCode).ToString());
                        }
                        Debug.Log(authErrorCode + exception.ToString());

                        if(Firebase.Auth.AuthError.EmailAlreadyInUse == (Firebase.Auth.AuthError)firebaseEx.ErrorCode)
                        {
                            error = "Email Already In Use";
                            Action = 2;
                        }
                        error = "Email Already In Use";
                        Action = 2;
                    }
                    return;
                }
                // Firebase user has been created.
                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})",newUser.DisplayName, newUser.UserId);
                Firebase.Auth.FirebaseUser user = auth.CurrentUser;
                if (user != null)
                {
                    Debug.Log("User profile updated successfully." + user.DisplayName);
                    Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
                    {
                        DisplayName = username,
                    };
                    user.UpdateUserProfileAsync(profile).ContinueWith(task1 =>
                    {
                        if (task1.IsCanceled)
                        {
                            Debug.LogError("UpdateUserProfileAsync was canceled.");
                            error = "Registration was canceled";
                            Action = 2;
                            return;
                        }
                        if (task1.IsFaulted)
                        {
                            Debug.LogError("UpdateUserProfileAsync encountered an error: " + task1.Exception);
                            error = "Registration was canceled";
                            Action = 2;
                            return;
                        }

                        Debug.Log("User profile updated successfully.");
                        Action = 1;


                    });
                }

            });
        }
        singUpErrorTxt.text = ""+Error;
        Debug.Log("[username == " + username + "][Email = " + Email + "]"+ Error);
    }
    public void SignOut()
    {
        menu.setScreen("Login", "DogPlay", 0);
        transform.Find("Home").GetComponent<Animator>().SetInteger("state", 0);
        transform.Find("Setting").GetComponent<Animator>().SetBool("isOpen", false);
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SignOut();
    }
        // Send a password reset email to the current email address.
    public void SendPasswordResetEmail()
    {
        string email = transform.Find("Login").Find("Forget").Find("base").Find("Email").GetComponent<InputField>().text;
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        string Error = "";
        if (!TestEmail.IsEmail(email))
        {
            Error = "Use correct Email";
        }
        else
        {
            transform.Find("Loading").gameObject.SetActive(true);
            auth.SendPasswordResetEmailAsync(email).ContinueWithOnMainThread((authTask) =>
            {
                if (authTask.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was canceled.");
                    error = "Email encountered an error";
                    Action = 3;
                    return;
                }
                if (authTask.IsFaulted)
                {
                    //Debug.LogError("UpdateUserProfileAsync encountered an error: " + authTask.Exception);
                    error = "Email encountered an error";
                    Action = 3;
                    return;
                }

                Debug.Log("User profile updated successfully.");
                error = "Check mail for reset your password";
                Action = 4;
            });
        }
        transform.Find("Login").Find("Forget").Find("base").Find("Error").GetComponent<Text>().text = Error;
    }
    void Update()
    {
        if (Action != 0)
        {
            if (Action == 1)
            {
                Action = 0;
                menu.OnClick("SingUpStart");
            }
            if (Action == 2)
            {
                Action = 0;
                singUpErrorTxt.text = error;
            }
            if (Action == 3)
            {
                Action = 0;
                transform.Find("Login").Find("Forget").Find("base").Find("Error").GetComponent<Text>().text = error;
            }
            if (Action == 4)
            {
                Action = 0;
                singInErrorTxt.text = error;
                transform.Find("Login").Find("Forget").gameObject.SetActive(false);
            }
            if (Action == 5)
            {
                Action = 0;
                menu.OnClick("Login_Login");
            }
            transform.Find("Loading").gameObject.SetActive(false);
        }
    }
}
public static class TestEmail
{
    public const string MatchEmailPattern =
        @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
        + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
          + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
        + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
    public static bool IsEmail(string email)
    {
        if (email != null) return Regex.IsMatch(email, MatchEmailPattern);
        else return false;
    }
}
