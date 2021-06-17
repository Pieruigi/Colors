using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zom.Pie;

public class TestLevelMaker : MonoBehaviour
{
    [SerializeField]
    int numOfRows, numOfColumns;

    [SerializeField]
    int numOfColors;

    [SerializeField]
    float loadFactor;

    [SerializeField]
    bool horizontalSymmetry, verticalSymmetry;

    // Start is called before the first frame update
    void Start()
    {
        //LevelMaker lm = new LevelMaker(numOfRows, numOfColumns, numOfColors, loadFactor, horizontalSymmetry, verticalSymmetry);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
