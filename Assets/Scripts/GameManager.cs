using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<int> boxNumbers;
    public List<Material> boxMaterials;

    private void Start()
    {
        boxNumbers = new List<int> { 2, 4, 8, 16, 32, 64, 128, 512, 1024, 2048 };

    }

    void boxSpawner()
    {

    }

    public void boxUpdater()
    {

    }

    public void boxDestroyer()
    {

    }

}
