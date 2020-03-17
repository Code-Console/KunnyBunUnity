using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Transform trnsDog;
    public Transform bgCanvas;
    int dogNo = 0;
    int dogAnim = 0;
    string[] animStr = {"Bath", "Eat", "Jump", "Sleep", "Walk"};
    public Sprite[] bgSprite;
    private void Awake()
    {
        bgCanvas.transform.Find("three").GetComponent<Image>().sprite = bgSprite[2];
        trnsDog.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        //for (int i=0;i< trnsDog.childCount; i++)
        //{
        //    trnsDog.GetChild(i).GetComponent<Animator>().SetInteger("state", dogAnim);
        //}
        transform.Find("Splash").GetComponent<Animator>().SetInteger("state", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDogClick(string type) {
        Transform tDogs = trnsDog.Find("Dogs").transform;
        switch (type) {
            case "dogpre":
                dogNo++;
                dogNo %= tDogs.childCount;
                for (int i = 0; i < tDogs.childCount; i++)
                {
                    tDogs.GetChild(i).gameObject.SetActive(i == dogNo);
                }
                break;
            case "dognext":
                dogNo--;
                if(dogNo < 0)
                    dogNo = tDogs.childCount-1;
                for (int i = 0; i < tDogs.childCount; i++)
                {
                    tDogs.GetChild(i).gameObject.SetActive(i == dogNo);
                }
                break;
            case "amin":
                dogAnim++;
                dogAnim %= 5;
                for (int i = 0; i < tDogs.childCount; i++)
                {
                    tDogs.GetChild(i).GetComponent<Animator>().SetInteger("state", dogAnim);
                }
                break;
        }
    }
    public void OnClick(string type)
    {
        switch (type)
        {
            case "SplashEnter":
                setScreen("SingUp", "Splash", 2);
                break;
            case "SingUpStart":
                setScreen("Home", "SingUp", 2);
                break;
            case "SingUpLogin":
                setScreen("Login", "SingUp", 2);
                break;
            case "Login_Login":
                setScreen("Home", "Login", 2);
                break;
            case "Login_SIGNUP":
                setScreen("SingUp", "Login", 0);
                break;
            case "Home_Setting":
                transform.Find("Setting").GetComponent<Animator>().SetBool("isOpen", true);
                break;
            case "Home_select":
                
                break;
           

            case "Setting_sound":
                break;
            case "Setting_notification":
                break;
            case "Setting_language":
                break;
            case "Setting_LEGAL":
                break;
            case "Setting_How2play":
                break;
            case "Setting_MORE":
                break;
            case "Setting_close":
                transform.Find("Setting").GetComponent<Animator>().SetBool("isOpen", false);
                break;
        }
        Debug.Log(""+ (type == "Splash" )+" "+( type == "SingUp") + " " + (type == "Login"));
        
    }

    void setScreen(string open,string close,int state) {
        transform.Find(open).GetComponent<Animator>().SetInteger("state", 1);
        transform.Find(close).GetComponent<Animator>().SetInteger("state", state);
        trnsDog.gameObject.SetActive(!(open == "Splash" || open == "SingUp" || open == "Login"));
    }

}
