using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationCode : MonoBehaviour
{
    public int anim_no = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetInteger("state", anim_no);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
