using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
public class Menu : MonoBehaviour
{
    public Transform trnsDog;
    public Transform bgCanvas;
    public Transform trnsShower;
    public Transform trnsHeart;
    public Transform trnsSnoz;
    public Transform lightDirection;
    Animator tapAnimator;
    public Sprite[] bgSprite;
    float timeShower = 0.0f;
    private RectTransform rect_spiner;
    bool isHome = true;
    public AudioClip[] clip;
    AudioSource audioBackground = null;
    AudioSource audioItem = null;
    AudioSource audioButtom = null;
    AudioSource audioSpineer = null;
    Ads ads;
    private void Awake()
    {
        M.Open();
        ads = transform.GetComponent<Ads>();
        trnsDog.gameObject.SetActive(false);
        rect_spiner = transform.Find("Spinner").Find("Panel").Find("spin").GetComponent<RectTransform>();
        setSound();
        tapAnimator = transform.Find("DogPlay").Find("shower").Find("tap").GetComponent<Animator>();
        audioBackground = gameObject.AddComponent<AudioSource>();
        audioBackground.clip = clip[6];
        audioBackground.loop = true;
        audioBackground.volume = .81f;

        audioItem = gameObject.AddComponent<AudioSource>();
        audioItem.clip = clip[0];
        //audioItem.loop = true;
        M.itemAudio = -1;

        audioButtom = gameObject.AddComponent<AudioSource>();
        audioButtom.clip = clip[7];
        audioButtom.loop = false;

        audioSpineer = gameObject.AddComponent<AudioSource>();
        audioSpineer.clip = clip[8];
        audioSpineer.loop = true;
        

    }
    // Start is called before the first frame update
    void Start()
    {   
        transform.Find("Splash").GetComponent<Animator>().SetInteger("state", 1);
        UpdateValues();
        //Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser;
    }

    // Update is called once per frame
    void Update()
    {
        if (M.incAngle > 0)
        {
            M.spinerAngle++;
            if (M.spinerAngle > 50)
            {
                if (M.incAngle > 1 && M.spinerAngle % 20 == 0)
                {
                    M.incAngle--;
                    if(M.incAngle >= 4 && M.incAngle <= 5)
                    {
                        audioSpineer.loop = false;
                    }
                    if(M.SOUND)
                    audioSpineer.Play();
                }

                if (M.incAngle <= 1)
                {
                    M.incAngle = 0;
                    M.randomAngle = (int)rect_spiner.eulerAngles.z;
                    transform.Find("Spinner").Find("Panel").Find("Panelcong").gameObject.SetActive(true);
                    int val= ((int)(M.randomAngle / 45)) % 8;
                    Transform result = transform.Find("Spinner").Find("Panel").Find("Panelcong").Find("congImage");
                    M.SHOWLOG(val+ " result = " + result.name);
                    result.GetChild(1).GetComponent<Text>().text = M.VALUESPINER[val]+(val % 2 == 0 ? " DIMANDS" : " COINS");
                    result.GetChild(3).gameObject.SetActive(val % 2 != 0);
                    result.GetChild(4).gameObject.SetActive(val % 2 == 0);
                    if (val % 2 == 0)
                        M.DIMONDS += M.VALUESPINER[val];
                    else
                        M.COINS += M.VALUESPINER[val];
                    UpdateValues();

                }
            }
            rect_spiner.Rotate(new Vector3(0, 0, -M.incAngle));
        }
    }
    public void OnDogClick(string type) {
        Transform tDogs = trnsDog.Find("Dogs").transform;
        switch (type) {
            case "dogpre":
                M.dogNo++;
                M.dogNo %= tDogs.childCount;
                for (int i = 0; i < tDogs.childCount; i++)
                {
                    tDogs.GetChild(i).gameObject.SetActive(i == M.dogNo);
                }
                break;
            case "dognext":
                M.dogNo--;
                if(M.dogNo < 0)
                    M.dogNo = tDogs.childCount-1;
                for (int i = 0; i < tDogs.childCount; i++)
                {
                    tDogs.GetChild(i).gameObject.SetActive(i == M.dogNo);
                }
                break;
            case "amin":
                M.dogAnim++;
                M.dogAnim %= 5;
                break;
        }
        buttonClick();
    }
    public void OnClick(string type)    {
        switch (type)
        {
            case "SplashEnter":
                Firebase.Auth.FirebaseUser user = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser;
                if (user == null)
                    setScreen("SingUp", "Splash", 2);
                else
                {
                    if(M.REWARDDATE != System.DateTime.Now.Day)
                    {
                        M.REWARDDATE = System.DateTime.Now.Day;
                        isHome = true;
                        setShopScreen("multi");
                        setScreen("shop", "Splash", 2);
                    }
                    else
                    {
                        setScreen("Home", "Splash", 2);
                    }
                    

                }
                break;
            case "SingUpStart":
                setScreen("Home", "SingUp", 2);
                break;
            case "SingUpLogin":
                setScreen("Login", "SingUp", 2);
                break;
            case "Login_forget":
                transform.Find("Login").Find("Forget").gameObject.SetActive(true);
                break;
            case "Login_close":
                transform.Find("Login").Find("Forget").gameObject.SetActive(false);
                break;
            case "Login_Login":
                setScreen("Home", "Login", 2);
                break;
            case "Login_SIGNUP":
                setScreen("SingUp", "Login", 0);
                break;
            case "Home_Setting":
                setSound();
                transform.Find("Setting").GetComponent<Animator>().SetBool("isOpen", true);
                break;
            case "Home_select":
                setScreen("DogPlay", "Home", 2);
                break;
            case "Setting_sound":
                M.SOUND = !M.SOUND;
                setSound();
                break;
            case "Setting_notification":
                M.NOTIFY = !M.NOTIFY;
                setSound();
                break;
            case "Setting_language":
                break;
            case "Setting_SignOut":
                break;
            case "Setting_LEGAL":
                Application.OpenURL("https://hututusoftwares.com/privacy.php");
                break;
            case "Setting_How2play":
                Application.OpenURL("http://unity3d.com/");
                break;
            case "Setting_MORE":
                Application.OpenURL("https://play.google.com/store/apps/developer?id=Onedaygame24");
                break;
            case "Setting_close":
                transform.Find("Setting").GetComponent<Animator>().SetBool("isOpen", false);
                break;
            case "Spinner_spin":
                if (M.incAngle == 0)
                {
                    int val = Random.Range(0, 180)+45;
                    M.SHOWLOG("val = "+ val);
                    rect_spiner.Rotate(new Vector3(0, 0, -val));

                    M.spinerAngle = 0;
                    M.incAngle = 10 + Random.Range(0, 360) * .03f;
                    M.randomAngle = Random.Range(0, 360);
                    audioSpineer.loop = true;
                    if (M.SOUND)
                        audioSpineer.Play();
                }
                break;
            case "Spinner_collect":
                setScreen("DogPlay", "Spinner", 0);
                break;
            case "Home_rt_coins":
                isHome = true;
                setShopScreen("coin");
                setScreen("shop", "Home", 2);
                break;
            case "Home_rt_dimond":
                isHome = true;
                setShopScreen("dimond");
                setScreen("shop", "Home", 2);
                break;
            case "Home_rt_multi":
                //isHome = true;
                //setShopScreen("multi");
                //setScreen("shop", "Home", 2);
                break;
            case "DogPlay_rt_coins":
                setPlay();
                isHome = false;
                setShopScreen("coin");
                setScreen("shop", "Home", 2);
                break;
            case "DogPlay_rt_dimond":
                setPlay();
                isHome = false;
                setShopScreen("dimond");
                setScreen("shop", "Home", 2);
                break;
            case "DogPlay_rt_multi":
                //isHome = false;
                //setShopScreen("multi");
                //setScreen("shop", "Home", 2);
                break;
        }
        buttonClick();
        M.SHOWLOG(""+ (type == "Splash" )+" "+( type == "SingUp") + " " + (type == "Login"));
        
        
    }

    public void OnPlayClick(string type)
    {
        switch (type)
        {
            case "setting":
                transform.Find("Setting").GetComponent<Animator>().SetBool("isOpen", true);
                buttonClick();
                setSound();
                break;
            case "music":
                M.MUSIC = !M.MUSIC;
                setSound();
                break;
            case "spiner":
                transform.Find("Spinner").Find("Panel").Find("Panelcong").gameObject.SetActive(false);
                setScreen("Spinner", "DogPlay", 2);
                break;
            case "heart":
                setPlay();
                trnsHeart.gameObject.SetActive(true);
                levelFill("leftbutttom", "heart");
                itemSoundPlay(1);
                break;
            case "Food":
                setPlay();
                trnsDog.Find("bowl").gameObject.SetActive(true);
                levelFill("leftbutttom", "Food");
                setDogAnim(1);
                itemSoundPlay(0);
                break;
            case "Bath":
                if (trnsShower.gameObject.activeInHierarchy == false)
                {
                    setPlay();
                    trnsShower.gameObject.SetActive(true);
                    transform.Find("DogPlay").Find("shower").gameObject.SetActive(true);
                    itemSoundPlay(-1);
                }
                break;
            case "tapshawer":
                tapAnimator.SetBool("isOpen", !tapAnimator.GetBool("isOpen"));
                trnsShower.Find("PS").gameObject.SetActive(tapAnimator.GetBool("isOpen"));
                if (tapAnimator.GetBool("isOpen"))
                {
                    levelFill("leftbutttom", "Bath");
                    itemSoundPlay(3);
                }
                else
                {
                    itemSoundPlay(-1);
                }
                
                break;
            case "sound":
                M.SOUND = !M.SOUND;
                setSound();
                break;
            case "walk":
                setPlay();
                setDogAnim(4);
                levelFill("rightBottom", "walk");
                itemSoundPlay(5);
                break;
            case "sleep":
                setPlay();
                if(transform.Find("DogPlay").Find("Light").gameObject.activeInHierarchy== false)
                {
                    itemSoundPlay(-1);
                    transform.Find("DogPlay").Find("Light").gameObject.SetActive(true);
                }
                
                break;
            case "snoring":
                setPlay();
                trnsSnoz.gameObject.SetActive(true);
                levelFill("rightBottom", "snoring");
                itemSoundPlay(4);
                break;
            case "left":
                setPlay();
                setDogAnim(4);
                M.direction = 1;
                itemSoundPlay(9);
                //M.BGNO--;
                //setBG();
                break;
            case "down":
                setPlay();
                //M.BGNO++;
                //setBG();
                setDogAnim(4);
                M.direction = 3;
                itemSoundPlay(9);
                break;
            case "right":
                setPlay();
                setDogAnim(4);
                M.direction = 2;
                itemSoundPlay(9);
                //M.BGNO++;
                //setBG();
                break;
            case "up":
                setPlay();
                //M.BGNO--;
                //setBG();
                //M.direction = 0;
                setDogAnim(2);
                itemSoundPlay(9);
                break;
            case "seleBackground":
                transform.Find("Background").GetComponent<Animator>().SetBool("isOpen", true);
                break;
            case "back_close":
                transform.Find("Background").GetComponent<Animator>().SetBool("isOpen", false);
                break;
            case "light":
                transform.Find("DogPlay").Find("Light").gameObject.SetActive(false);
                trnsDog.Find("back").gameObject.SetActive(true);
                lightDirection.gameObject.SetActive(false);
                setPlay();
                setDogAnim(3);
                levelFill("rightBottom", "sleep");
                setScreen("Sleep", "DogPlay", 2);
                itemSoundPlay(4);
                break;
            case "sleepover":
                transform.Find("DogPlay").Find("Light").gameObject.SetActive(true);
                trnsDog.Find("back").gameObject.SetActive(false);
                lightDirection.gameObject.SetActive(true);
                setPlay();
                levelFill("rightBottom", "sleep");
                setScreen("DogPlay", "Sleep", 0);
                transform.Find("DogPlay").Find("Light").gameObject.SetActive(true);
                itemSoundPlay(-1);
                break;
            case "Video":
                M.IS_REWARD = true;
                ads.showReward(0, 0);
                //sleepVideoRewordAds();
                break;
            case "Double":
                transform.Find("DogPlay").Find("LvlCom").gameObject.SetActive(false);
                ads.showReward(M.next2Dimand, M.next2Coin);
                break;
            case "Reward":
                transform.Find("DogPlay").Find("LvlCom").gameObject.SetActive(false);
                ads.showInterstitial();
                break;
        }
    }
    public void sleepVideoRewordAds() {
        transform.Find("DogPlay").Find("Light").gameObject.SetActive(true);
        trnsDog.Find("back").gameObject.SetActive(false);
        lightDirection.gameObject.SetActive(true);
        itemSoundPlay(-1);
        setPlay();
        levelFill("rightBottom", "sleep");
        setScreen("DogPlay", "Sleep", 0);
    }
    public void OnShopClick(string type) {
        switch (type)
        {
            case "shop_back":
                if (isHome) {
                    setScreen("Home", "shop", 0);
                }
                else
                    setScreen("DogPlay", "shop", 0);
                break;
            case "shop_topCoins":
                setShopScreen("coin"); 
                break;
            case "shop_topDImond":
                setShopScreen("dimond"); 
                break;
            case "shop_tabCoin":
                setShopScreen("coin");
                break;
            case "shop_tabDimond":
                setShopScreen("dimond");
                break;
            case "shop_CoinBronze0":
                ads.showReward(0, 300);
                break;
            case "shop_CoinSilver1":
                ads.showReward(0, 300);
                break;
            case "shop_CoinGold2":
                ads.showReward(0, 300);
                break;
            case "shop_CoinBronze3":
                ads.showReward(0, 300);
                break;
            case "shop_CoinGold4":
                ads.showReward(0, 300);
                break;
            case "shop_CoinSilver5":
                ads.showReward(0, 300);
                break;
            case "shop_CoinBronze6":
                ads.showReward(0, 300);
                break;
            case "shop_CoinShare7":
                ads.showReward(0, 300);
                break;


            case "shop_DimondBadge0":
                ads.showReward(30,0);
                break;
            case "shop_DimondLavender1":
                ads.showReward(30, 0);
                break;
            case "shop_DimondGold2":
                ads.showReward(30, 0);
                break;
            case "shop_DimondRuby3":
                ads.showReward(30, 0);
                break;
            case "shop_DimondRUby4":
                ads.showReward(30, 0);
                break;
            case "shop_DimondBadge5":
                ads.showReward(30, 0);
                break;
            case "shop_DimondRUby6":
                ads.showReward(30, 0);
                break;
            case "shop_DimondShare6":
                ads.showReward(30, 0);
                break;


            case "Multipler_Collect":
                M.REWARDNO++;
                M.REWARDDATE = System.DateTime.Now.Day;
                M.DIMONDS += M.REWARDNO * 2;
                int val = (M.REWARDNO / 7);
                if(val == 6)
                {
                    M.DIMONDS += M.REWARDNO * 10;
                }
                if (isHome) {
                    setScreen("Home", "shop", 0);
                }
                else
                    setScreen("DogPlay", "shop", 0);
                UpdateValues();
                break;
        }
        buttonClick();
    }

    public void selBackGround(int val) {
        M.BGNO = val;
        setBG();
        transform.Find("Background").GetComponent<Animator>().SetBool("isOpen", false);
    }

    public void setScreen(string open,string close,int state) {
        transform.Find(open).GetComponent<Animator>().SetInteger("state", 1);
        transform.Find(close).GetComponent<Animator>().SetInteger("state", state);
        trnsDog.gameObject.SetActive(!(open == "Splash" || open == "SingUp" || open == "Login"));
        bgCanvas.transform.Find("NEW").gameObject.SetActive(false);
        switch (open) {
            case "Splash":
                M.AppScreen = M.APP_SPLASH;
                break;
            case "SingUp":
                M.AppScreen = M.APP_SINGUP;
                break;
            case "Login":
                M.AppScreen = M.APP_LOGIN;
                break;
            case "Home":
                M.AppScreen = M.APP_HOME;
                setBG();
                break;
            case "Spinner":
                M.AppScreen = M.APP_SPINNER;
                break;
            case "DogPlay":
                M.AppScreen = M.APP_DOGPLAY;
                break;
            case "Setting":
                M.AppScreen = M.APP_SETTING;
                break;
            case "shop":
                M.AppScreen = M.APP_SHOP;
                break;
        }
        if(M.AppScreen == M.APP_DOGPLAY && M.MUSIC){
            audioBackground.Play();
        }else{
            audioBackground.Pause();
            itemSoundPlay(-1);
            setDogAnim(0);
        }
        UpdateValues();
    }
    void setSound() {
        transform.Find("DogPlay").Find("lefttop").GetChild(1).GetChild(1).gameObject.SetActive(!M.MUSIC);
        transform.Find("DogPlay").Find("rightBottom").GetChild(0).GetChild(1).gameObject.SetActive(!M.SOUND);
        transform.Find("Setting").Find("base").Find("container").Find("Panel").GetChild(0).GetChild(1).gameObject.SetActive(!M.SOUND);
        transform.Find("Setting").Find("base").Find("container").Find("Panel").GetChild(1).GetChild(1).gameObject.SetActive(!M.NOTIFY);
        transform.Find("Setting").Find("base").Find("container").Find("Panel").GetChild(3).GetChild(1).gameObject.SetActive(!M.MUSIC);
        Firebase.Auth.FirebaseUser user = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser;
        M.SHOWLOG(user + " ~~~user~");
        if (user != null)
            transform.Find("Setting").Find("base").Find("container").Find("GameObject").Find("User").GetComponent<InputField>().text = ""+ user.DisplayName;
        UpdateValues();
        if (M.AppScreen == M.APP_DOGPLAY && M.MUSIC && audioBackground != null){
            audioBackground.Play();
        }
        else {
            if(audioBackground != null)
                audioBackground.Pause();
        }
        if (M.AppScreen == M.APP_DOGPLAY && M.SOUND && audioItem != null && M.itemAudio > -1) {
            
                audioItem.Play();
        }
        else {
            if (audioItem != null)
                audioItem.Pause();
        }
        

    }
    public void UpdateValues() {
        transform.Find("DogPlay").Find("rightTop").Find("Coins").GetChild(1).GetComponent<Text>().text = M.COINS+"";
        transform.Find("DogPlay").Find("rightTop").Find("DImonds").GetChild(1).GetComponent<Text>().text = M.DIMONDS + "";

        transform.Find("Home").Find("rightTop").Find("Coins").GetChild(1).GetComponent<Text>().text = M.COINS + "";
        transform.Find("Home").Find("rightTop").Find("DImonds").GetChild(1).GetComponent<Text>().text = M.DIMONDS + "";

        transform.Find("shop").Find("Panel").Find("Coins").GetChild(0).GetComponent<Text>().text = M.COINS + "";
        transform.Find("shop").Find("Panel").Find("Dimonds").GetChild(0).GetComponent<Text>().text = M.DIMONDS + "";

        transform.Find("DogPlay").Find("Star").Find("Image").Find("Text").GetComponent<Text>().text = "" + M.LEVEL;
        M.Save();
    }
    void setBG() {
        if (M.BGNO < 0)
            M.BGNO = bgSprite.Length; 
        if (M.BGNO > bgSprite.Length)
            M.BGNO = 0;
        bgCanvas.transform.Find("NEW").gameObject.SetActive(M.BGNO > 0);
        transform.Find("DogPlay").Find("Back").gameObject.SetActive(M.BGNO == 0);
        if (M.BGNO > 0)
        {
            bgCanvas.transform.Find("NEW").GetComponent<Image>().sprite = bgSprite[M.BGNO - 1];
        }
    }
    void setShopScreen(string type) {
        Transform tshop = transform.Find("shop");

        tshop.GetChild(1).gameObject.SetActive(false);
        tshop.GetChild(1).GetChild(2).gameObject.SetActive(false);
        tshop.GetChild(1).GetChild(3).gameObject.SetActive(false);
        tshop.GetChild(2).gameObject.SetActive(false);
        tshop.GetChild(0).GetChild(0).gameObject.SetActive(true);
        if (type == "multi") {
            tshop.GetChild(0).GetChild(0).gameObject.SetActive(false);
            tshop.GetChild(2).gameObject.SetActive(true);
            Transform tMulti = tshop.Find("Multipler").Find("Dimond");
            int val = (M.REWARDNO / 7);
            for (int i = 0; i < 7; i++)
            {
                tMulti.Find("day" + (i + 1)).Find("Text").GetComponent<Text>().text = "DAY "+(i+1+ val*7);
                if(M.REWARDNO % 7 >= i)
                {
                    if(i<6)
                        tMulti.Find("day" + (i + 1)).Find("right").gameObject.SetActive(true);
                    else
                        tMulti.Find("traser").Find("right").gameObject.SetActive(true);
                }
            }
        }
        if (type == "coin"){
            tshop.GetChild(1).gameObject.SetActive(true);
            tshop.GetChild(1).GetChild(2).gameObject.SetActive(true);
            tshop.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(175, 160, 128, 255);
            tshop.GetChild(1).GetChild(1).GetComponent<Image>().color = new Color32(255, 243, 193, 255);
        }
        if (type == "dimond"){
            tshop.GetChild(1).gameObject.SetActive(true);
            tshop.GetChild(1).GetChild(3).gameObject.SetActive(true);
            tshop.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(255, 243, 193, 255);
            tshop.GetChild(1).GetChild(1).GetComponent<Image>().color = new Color32(175, 160, 128, 255);
        }
    }
    void setPlay() {
        setDogAnim(0);
        trnsShower.gameObject.SetActive(false);
        trnsShower.GetChild(0).gameObject.SetActive(false);
        trnsHeart.gameObject.SetActive(false);
        trnsSnoz.gameObject.SetActive(false);
        transform.Find("DogPlay").Find("shower").gameObject.SetActive(false);
        trnsShower.Find("PS").gameObject.SetActive(false);
        trnsDog.Find("bowl").gameObject.SetActive(false);
        trnsDog.Find("back").gameObject.SetActive(false);
        transform.Find("DogPlay").Find("Light").gameObject.SetActive(false);

        transform.Find("DogPlay").Find("rightBottom").Find("walk").Find("fill").GetComponent<Buttonfill>().Coroutine();
        transform.Find("DogPlay").Find("rightBottom").Find("sleep").Find("fill").GetComponent<Buttonfill>().Coroutine();
        transform.Find("DogPlay").Find("rightBottom").Find("snoring").Find("fill").GetComponent<Buttonfill>().Coroutine();

        transform.Find("DogPlay").Find("leftbutttom").Find("heart").Find("fill").GetComponent<Buttonfill>().Coroutine();
        transform.Find("DogPlay").Find("leftbutttom").Find("Food").Find("fill").GetComponent<Buttonfill>().Coroutine();
        transform.Find("DogPlay").Find("leftbutttom").Find("Bath").Find("fill").GetComponent<Buttonfill>().Coroutine();

    }
    void setDogAnim(int type)
    {
        M.dogAnim = type;
        Transform tDogs = trnsDog.Find("Dogs");
        for (int i = 0; i < tDogs.childCount; i++){
            tDogs.GetChild(i).GetComponent<Animator>().SetInteger("state", M.dogAnim);
        }
        M.direction = 0;
    }
    void levelFill(string penal,string type) {
        Buttonfill buttonfill = transform.Find("DogPlay").Find(penal).Find(type).Find("fill").GetComponent<Buttonfill>();
        if (!buttonfill.get()){
            buttonfill.set();
            M.SHOWLOG(""+ transform.Find("DogPlay").Find("Star").Find("base").Find("fill").GetComponent<Image>().fillAmount);



            transform.Find("DogPlay").Find("Star").Find("base").Find("fill").GetComponent<Image>().fillAmount += .1f;// +1/M.LEVEL;

            M.SHOWLOG(".01f+1/M.LEVEL = " + (.01f + 1 / M.LEVEL));


            if (transform.Find("DogPlay").Find("Star").Find("base").Find("fill").GetComponent<Image>().fillAmount >= 1)
            {
                M.LEVEL++;
                transform.Find("DogPlay").Find("Star").Find("base").Find("fill").GetComponent<Image>().fillAmount = 0;
                transform.Find("DogPlay").Find("Star").Find("Image").Find("Text").GetComponent<Text>().text = "" + M.LEVEL; 
                Transform tans = transform.Find("DogPlay").Find("LvlCom").Find("Image");
                transform.Find("DogPlay").Find("LvlCom").gameObject.SetActive(true);
                tans.Find("lvl").Find("Text").GetComponent<Text>().text = ""+ M.LEVEL;
                tans.Find("coin").Find("Text").GetComponent<Text>().text = "" + M.LEVEL*50;
                tans.Find("dimond").Find("Text").GetComponent<Text>().text = "" + M.LEVEL;

                M.next2Coin = M.LEVEL * 50;
                M.next2Dimand = M.LEVEL * 2;

                M.COINS += M.next2Coin;
                M.DIMONDS += M.next2Dimand;
                
                UpdateValues();

            }

        }
    }
    void itemSoundPlay(int i)
    {
        M.SHOWLOG(i+" <= M.itemAudio => " + M.itemAudio);
        M.itemAudio = i;
        if (M.SOUND && M.itemAudio > -1)
        {
            audioItem.clip = clip[i];
            audioItem.Play();
        }
        else{
            audioItem.Pause();
        }
    }
    void buttonClick() {
        if (M.SOUND){
            audioButtom.Play();
        }
    }
}
