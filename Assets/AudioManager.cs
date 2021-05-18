using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum ELanguage { English, Spanish, Polish };
    private ELanguage language;
    private int intT;

    void Start()
    {
        language = (ELanguage)1;
        Debug.Log(language);
        language = (ELanguage)2;
        Debug.Log(language);

        intT = (int)language;
    }

    void Update()
    {
        LangTest();
    }

    private void LangTest()
    {
        
        intT = (int)language;
        intT += 1;

        if (intT >= 3) 
            intT = 0;
        language = (ELanguage)intT;

        Debug.Log(language);
    }
}
