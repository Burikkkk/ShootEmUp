using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class NewScriptText : MonoBehaviour
{
    public Score score;
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = score.DisplayScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
