using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Buttonfill : MonoBehaviour
{
    Image img;
    bool isFill = false;
    void Start() {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {
        if (isFill){
            img.fillAmount += .04f;
            if (img.fillAmount >= 1) {
                img.fillAmount = 1;
                isFill = false;
            }
        }
    }
    public void set() {
        isFill = true;
    }
    public bool get() {
        return isFill;
    }
    public void Coroutine(){
        isFill = false;
        
        img.fillAmount -= .1f;
        if (img.fillAmount < 0)
            img.fillAmount = 0;
    }
}
