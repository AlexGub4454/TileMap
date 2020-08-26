using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] int countOfLifes = 3;

    private void Awake()
    {
        if (FindObjectsOfType<SceneController>().Length > 1) {

            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void TakeLife()
    {
        if (countOfLifes > 1)
        {
            StartCoroutine(SceneReloader());
        }
        else
        {
            Destroy(gameObject);
            SceneManager.LoadScene(0);
        }
    }
    IEnumerator SceneReloader()
    {
        yield return new WaitForSeconds(1f);
        countOfLifes--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
