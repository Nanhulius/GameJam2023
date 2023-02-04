using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    [SerializeField] string sceneName = "menu";
        // Start is called before the first frame update
    void Start()
    {
        
    }

    public string getSceneName() {
        return sceneName;
    }
}
