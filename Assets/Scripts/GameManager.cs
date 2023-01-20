using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<int> boxNumbers;
    public List<Material> boxMaterials;

    public GameObject box;
    public float touchAmountRatio;

    private void Start()
    {
        boxNumbers = new List<int> { 2, 4, 8, 16, 32, 64, 128, 512, 1024, 2048 };

    }

    private void Update()
    {
        Controls();
    }

    void Controls()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = new Vector2();
            float touchAmount = 0;
            if(touch.phase == TouchPhase.Began)
            {
                touchPos = touch.deltaPosition;
            }

            if(touch.phase == TouchPhase.Moved)
            {
                if((touchPos.x - touch.deltaPosition.x) > 0)
                {
                    touchAmount = -touchAmountRatio;

                }else if((touchPos.x - touch.deltaPosition.x) < 0)
                {
                    touchAmount = touchAmountRatio;
                }
                else
                {
                    touchAmount = 0;
                }

                box.transform.position += new Vector3(touchAmount, 0, 0);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                box.GetComponent<Rigidbody>().AddForce(Vector3.forward * 1000);
            }




        }
    }

    void BoxSpawner()
    {

    }

    public void BoxUpdater()
    {

    }

    public void BoxDestroyer()
    {

    }

}