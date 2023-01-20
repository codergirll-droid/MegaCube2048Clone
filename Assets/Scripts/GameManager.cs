using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<int> boxNumbers;
    public List<Material> boxMaterials;

    public GameObject box;
    public GameObject boxPrefab;

    public float touchAmountRatio;
    public int forceRatio = 1000;

    public Transform boxSpawnPoint;

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        boxNumbers = new List<int> { 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048 };
        BoxSpawner();
        box = GameObject.FindGameObjectWithTag("box");
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

                float newXPos = box.transform.position.x + touchAmount;
                if (newXPos < 2.5f && newXPos > -2.5f)
                {
                    box.transform.position += new Vector3(touchAmount, 0, 0);

                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                box.GetComponent<Rigidbody>().AddForce(Vector3.forward * forceRatio);
                Invoke(nameof(BoxSpawner), 0.2f);
            }




        }
    }

    void BoxSpawner()
    {
        int randomPerc = Random.Range(0, 101);
        box = Instantiate(boxPrefab, boxSpawnPoint.position, Quaternion.identity);
        int randomNum;

        if (randomPerc > 80)
        {
            randomNum = Random.Range(8, 11);
        }
        else if(randomPerc > 60)
        {
            randomNum = Random.Range(4, 8);
        }
        else
        {
            randomNum = Random.Range(0, 4);
        }

        
        box.GetComponent<Box>().boxIndx = randomNum;
        box.GetComponent<Box>().boxNumber = boxNumbers[randomNum];
        box.GetComponent<Renderer>().material = boxMaterials[randomNum];
    }

    public void BoxUpdater(GameObject boxObj)
    {
        Box b = boxObj.GetComponent<Box>();
        b.boxIndx += 1;
        b.boxNumber = boxNumbers[b.boxIndx];
        boxObj.GetComponent<Renderer>().material = boxMaterials[b.boxIndx];
        
    }

    public void BoxDestroyer(GameObject boxObj)
    {
        Destroy(boxObj);
    }

}
