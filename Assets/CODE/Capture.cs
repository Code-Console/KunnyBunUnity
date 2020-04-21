using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class Capture : MonoBehaviour
{
    Texture2D ss = null;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CaptureScreenshot()
    {
        set();
        StartCoroutine(TakeSSAndShare());
    }

    private IEnumerator TakeSSAndShare()
    {
        yield return new WaitForEndOfFrame();
        ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

//#if UNITY_ANDROID || UNITY_IOS
        
        
//        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
//        File.WriteAllBytes(filePath, ss.EncodeToPNG());

//        Destroy(ss);

//        new NativeShare().AddFile(filePath).SetSubject("Subject goes here").SetText("Hello world!").Share();
//#else

//#endif
        image.transform.GetChild(0).GetComponent<Image>().sprite = Sprite.Create(ss, new Rect(0.0f, 0.0f, ss.width, ss.height), new Vector2(0.5f, 0.5f), 100.0f);
        image.gameObject.SetActive(true);
        Transform dogpplay = transform.Find("DogPlay");
        for (int i = 0; i < dogpplay.childCount; i++)
        {
            dogpplay.GetChild(i).gameObject.SetActive(true);
        }
    }


    void set() {
        Transform dogpplay = transform.Find("DogPlay");
        for(int i = 1; i < dogpplay.childCount; i++)
        {
            dogpplay.GetChild(i).gameObject.SetActive(false);
        }

    }
    public void CaptureShare()
    {
        //set();
        StartCoroutine(Share());
    }
    public IEnumerator Share()
    {
        yield return new WaitForEndOfFrame();
#if UNITY_ANDROID || UNITY_IOS
        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());
        Destroy(ss);
        new NativeShare().AddFile(filePath).SetSubject("Kenny pet").SetText("Pet!").Share();
#else

#endif
        delete();
    }
    public void delete()
    {
        Destroy(ss);
        Transform dogpplay = transform.Find("DogPlay");
        for (int i = 0; i < dogpplay.childCount - 3; i++)
        {
            dogpplay.GetChild(i).gameObject.SetActive(true);
        }
        image.gameObject.SetActive(false);
    }

}
