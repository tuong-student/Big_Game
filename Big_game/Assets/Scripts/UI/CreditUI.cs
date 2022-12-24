using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreditUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject MainPanel;

    private void Start()
    {
        TurnOffCredits();
    }
    private IEnumerator Turnoff()
    {
        yield return new WaitForSeconds(15f);
        gameObject.SetActive(false);
        MainPanel.SetActive(true);
    }
    private void TurnOffCredits()
    {
        StartCoroutine(Turnoff());

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            MainPanel.SetActive(true);

        }
    }
}
