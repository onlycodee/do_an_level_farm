// Copyright (C) 2015 ricimi - All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement.
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms.

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// This class is responsible for loading the next scene in a transition (the core of
// this work is performed in the Transition class, though).
public class SceneTransition : MonoBehaviour 
{
    public string scene = "<Insert scene name>";
    public float duration = 1.0f;
    public Color color = Color.black;

    public void PerformTransition()
    {
        //Transition.LoadLevel(scene, duration, color);
        StartCoroutine(COLoadScene());
        //DontDestroyOnLoad(gameObject);
        //Debug.LogError("Load scene done");
    }
    IEnumerator COLoadScene()
    {
        DontDestroyOnLoad(gameObject);
        yield return ScreenFader.Instance.FadeIn();
        yield return SceneManager.LoadSceneAsync(scene);
        yield return new WaitForSeconds(.5f);
        yield return ScreenFader.Instance.FadeOut();
        Destroy(gameObject);
    }
}