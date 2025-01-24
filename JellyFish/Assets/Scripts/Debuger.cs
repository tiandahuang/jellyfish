using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debuger : MonoBehaviour
{
    public Text testdubug;
    // Start is called before the first frame update
    void Start()
    {
        testdubug.text = "test\n";
    }

    // Update is called once per frame
    void Update()
    {
        testdubug.text = testdubug.text + "test\n";

        
    }
}
