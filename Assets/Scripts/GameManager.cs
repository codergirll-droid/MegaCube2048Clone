using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<int> boxNumbers;
    public List<Material> boxMaterials;
    public List<Color> particleColors;

    public GameObject box;
    public GameObject boxPrefab;

    public float touchAmountRatio;
    public int forceRatio = 1000;

    public Transform boxSpawnPoint;

    public int totalPoints = 0;
    public int highScore = 0;

    public TMP_Text totalPointsTxt;
    public TMP_Text highScoreTxt;

    bool canSpawn = true;

    public GameObject particles;
    public GameObject failPanel;

    public int boxCount = 0;
    bool canPlay = true;



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

        totalPointsTxt.text = "Score " + 0;
        highScoreTxt.text = "High score: " + SaveSystem.Instance.GetPoints().ToString();
    }

    private void Update()
    {
        Controls();
        CheckGameState();
    }

    void CheckGameState()
    {
        if(boxCount >= 30)
        {
            Fail();
        }
    }

    public void Fail()
    {
        failPanel.SetActive(true);
        canPlay = false;
    }

    public void RetryBtn()
    {
        SceneManager.LoadScene(0);
    }

    void Controls()
    {
        if (canPlay)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPos = new Vector2();
                float touchAmount = 0;
                if (touch.phase == TouchPhase.Began)
                {
                    touchPos = touch.deltaPosition;
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    if ((touchPos.x - touch.deltaPosition.x) > 0)
                    {
                        touchAmount = -touchAmountRatio;

                    }
                    else if ((touchPos.x - touch.deltaPosition.x) < 0)
                    {
                        touchAmount = touchAmountRatio;
                    }
                    else
                    {
                        touchAmount = 0;
                    }

                    if (box != null)
                    {
                        float newXPos = box.transform.position.x + touchAmount;
                        if (newXPos < 2.5f && newXPos > -2.5f)
                        {
                            box.transform.position += new Vector3(touchAmount, 0, 0);

                        }
                    }
                    else
                    {
                        Fail();
                    }


                }

                if (touch.phase == TouchPhase.Ended && canSpawn)
                {
                    if(box != null)
                    {
                        Destroy(box.GetComponent<boxInHand>());
                        box.GetComponent<Rigidbody>().AddForce(Vector3.forward * forceRatio);
                        Invoke(nameof(BoxSpawner), 0.2f);
                        canSpawn = false;
                        Invoke(nameof(CoolDown), 0.3f);
                        boxCount++;
                    }
                    else
                    {
                        Fail();
                    }

                }




            }
        }
        
    }

    #region BOX STUFF
    
    void BoxSpawner()
    {
        int randomPerc = Random.Range(0, 67);
        box = Instantiate(boxPrefab, boxSpawnPoint.position, boxSpawnPoint.rotation);
        int randomNum;

        /*
        if (randomPerc > 90)
        {
            randomNum = Random.Range(0, 3);
        }
        else if(randomPerc > 50)
        {
            randomNum = Random.Range(3, 6);
        }
        else if(randomPerc > 10)
        {
            randomNum = Random.Range(6, 9);
        }
        else
        {
            randomNum = Random.Range(9, 11);
        }
        */

        if (randomPerc > 65)
        {
            randomNum = 10;
        }
        else if (randomPerc > 63)
        {
            randomNum = 9;
        }
        else if (randomPerc > 60)
        {
            randomNum = 8;
        }
        else if (randomPerc > 56)
        {
            randomNum = 7;
        }
        else if (randomPerc > 51)
        {
            randomNum = 6;
        }
        else if (randomPerc > 45)
        {
            randomNum = 5;
        }
        else if (randomPerc > 38)
        {
            randomNum = 4;
        }
        else if (randomPerc > 30)
        {
            randomNum = 3;
        }
        else if (randomPerc > 21)
        {
            randomNum = 2;
        }
        else if (randomPerc > 11)
        {
            randomNum = 1;
        }
        else
        {
            randomNum = 0;
        }


        box.GetComponent<Box>().boxIndx = randomNum;
        box.GetComponent<Box>().boxNumber = boxNumbers[randomNum];
        box.GetComponent<Renderer>().material = boxMaterials[randomNum];
        box.AddComponent<boxInHand>();
        
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
    #endregion

    #region POINTS AND SAVE

    public void AddPoints()
    {
        totalPoints += 1;
    }

    #endregion


    private void OnDisable()
    {
        SaveSystem.Instance.SaveGame();
    }

    private void OnApplicationQuit()
    {
        SaveSystem.Instance.SaveGame();
    }

    void CoolDown()
    {
        canSpawn = true;
    }

    public void PopParticles(Box b, Vector3 pos)
    {
        GameObject x = Instantiate(particles, pos, Quaternion.identity);
        x.GetComponent<ParticleSystem>().startColor = particleColors[b.boxIndx];

        Destroy(x, 1f);
    }

}
