using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Soap : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    Vector2 startPos;
    public Transform soapTrans;
    int count = 0;
    //int count2 = 0;

    int count3 = 0;
    int count4 = 0;

    void Update()
    {
        if (soapTrans.GetChild(1).GetComponent<Animator>().GetBool("isOpen"))
        {
            if (count3 % 3 == 0)
            {
                soapTrans.GetChild(0).GetChild(count4).gameObject.SetActive(false);
                count4++;
                count4 %= soapTrans.GetChild(0).childCount;
            }
            count3++;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        startPos = this.transform.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position += (Vector3)eventData.delta;
        
        //if(count2 % 3 == 0)
        //{
            soapTrans.GetChild(0).GetChild(count).gameObject.SetActive(true);
            count++;
            count %= soapTrans.GetChild(0).childCount;
        //}
        //count2++;
    }
    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");
        this.transform.position = startPos;
    }


}
