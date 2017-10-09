using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ParticleManager : MonoBehaviour
{
    

    ParticleSystem ps;
    [SerializeField]
    AudioClip windBlowSFX;
    int particlesPerS;


    // Use this for initialization
    void Start()
    {
        particlesPerS = 10;
        ps = GetComponent<ParticleSystem>();
    }

    //particle effect that starts the scene swap
    void ParticleIncreaseEffect()
    {
        var em = ps.emission;
        em.rateOverTime = new ParticleSystem.MinMaxCurve(particlesPerS);
    }

    public void StartNewGame()
    {
        ParticleIncreaseEffect();
        StartCoroutine(startNewGame());
    }

    public void ContinueGameInProgress()
    {
        ParticleIncreaseEffect();
        StartCoroutine(continueGameInProgress());

    }

    //each yield adds 100 more particles to the game so that we get the overlap effect
    IEnumerator startNewGame()
    {
        float speed = GameObject.Find("Global Managers").GetComponent<FadeManager>().BeginFade(1);
        yield return new WaitForSeconds(1);
        particlesPerS += 100;
        ParticleIncreaseEffect();
        yield return new WaitForSeconds(1);
        particlesPerS += 100;
        ParticleIncreaseEffect();
        yield return new WaitForSeconds(1);
        particlesPerS += 100;
        ParticleIncreaseEffect();
        yield return new WaitForSeconds(1);
        particlesPerS += 100;
        ParticleIncreaseEffect();
        yield return new WaitForSeconds(1);
        particlesPerS += 100;
        ParticleIncreaseEffect();
        yield return new WaitForSeconds(1);
        //then change scene after you wait
        SceneManager.LoadScene("bedroom", LoadSceneMode.Single);

    }

    IEnumerator continueGameInProgress()
    {
        yield return new WaitForSeconds(5);
        //then change scene after you wait

        //to make this I need to make a saving system and somehow load the necesary scene id
    }

}
