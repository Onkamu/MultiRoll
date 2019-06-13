using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotator : MonoBehaviour
{
    public GameObject winScreen;
    public Text winner;

    void Update()
    {
        transform.Rotate (new Vector3 (0, 30, 0) * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        winScreen.SetActive(true);
        Time.timeScale = 0;
        winner.text = collision.rigidbody.name;
    }
}