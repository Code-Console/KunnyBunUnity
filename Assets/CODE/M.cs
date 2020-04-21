using UnityEngine;
using System.Collections;



public static class M
{
	public static bool MUSIC = true;
	public static bool SOUND = true;
	public static bool NOTIFY = true;
	public static bool IS_ADS = true;
	public static bool IS_REWARD = false;
	public static int UNLOCKLEVEL = 1;
	public static int COINS = 1;
	public static int DIMONDS = 1;
	public static int BGNO = 0;
	public static int LEVEL = 1;
	public static int REWARDDATE = 0;
	public static int REWARDNO = 0;
    

    public static  int[] VALUESPINER = { 5, 200, 7, 150, 10, 250, 7, 100 };
	public static string[] ANIMSTR = { "Bath", "Eat", "Jump", "Sleep", "Walk" };
	public static int next2Dimand = 0;
	public static int next2Coin = 0;
	public static int itemAudio = 0;

	public static int dogNo = 0;
	public static int dogAnim = 0;
	public static int spinerAngle = 1000;
	public static float incAngle = 0;
	public static int randomAngle = 10;
	public static int direction = 0;
	


	public static int AppScreen;
	public  const int APP_SPLASH = 0;
	public  const int APP_SINGUP = 1;
	public  const int APP_LOGIN = 2;
	public  const int APP_HOME = 3;
	public  const int APP_SPINNER = 4;
	public  const int APP_DOGPLAY = 5;
	public  const int APP_SETTING = 6;
	public const int APP_SHOP = 7;

	
	//50270001

	//ca-app-pub-7665074309496944/1640960059 InterTiatial ANDROID
	//ca-app-pub-3395412980708319/9064332115 InterTiatial IPHONE

	public static void Save (){
		PlayerPrefs.SetInt("a", MUSIC ? 1 :0);
		PlayerPrefs.SetInt("b", SOUND ? 1 : 0);
		PlayerPrefs.SetInt("c", NOTIFY ? 1 : 0);
		PlayerPrefs.SetInt("d", IS_ADS ? 1 :0);
		PlayerPrefs.SetInt("e", UNLOCKLEVEL);
		PlayerPrefs.SetInt("f", COINS);

		PlayerPrefs.SetInt("g", DIMONDS);
		PlayerPrefs.SetInt("h", BGNO);
		PlayerPrefs.SetInt("i", LEVEL);
		PlayerPrefs.SetInt("j", REWARDDATE);
		PlayerPrefs.SetInt("k", REWARDNO);

		SHOWLOG(REWARDDATE + " ~Save~  " + REWARDNO);
	}

	public static void Open ()
	{
		MUSIC = PlayerPrefs.GetInt("a", 1) == 1;
		SOUND = PlayerPrefs.GetInt("b", 1) == 1;
		NOTIFY = PlayerPrefs.GetInt("c", 1) == 1;
		IS_ADS = PlayerPrefs.GetInt("d", 1) == 1;
        UNLOCKLEVEL = PlayerPrefs.GetInt ("f", UNLOCKLEVEL);
		COINS = PlayerPrefs.GetInt ("g", COINS);

		DIMONDS = PlayerPrefs.GetInt("g", DIMONDS);
		BGNO = PlayerPrefs.GetInt("h", BGNO);
		LEVEL = PlayerPrefs.GetInt("i", LEVEL);
		REWARDDATE = PlayerPrefs.GetInt("j", REWARDDATE);
		REWARDNO = PlayerPrefs.GetInt("k", REWARDNO);


		//LEVEL = 1;
		SHOWLOG(REWARDDATE + " ~Open~  " + REWARDNO);


	}

    public static void SHOWLOG(string str)
    {
		Debug.Log(str);

	}
}
