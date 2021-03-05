using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PawnController : MonoBehaviour
{
    [SerializeField] private Vector2 ZeroPosition, ZeroScale, UnoPosition, UnoScale;

    private Vector2 actualPosition = Vector2.zero, actualScale = Vector2.zero;

    private float percentage = 1;

    [SerializeField] private UIManager uiMan;

    public void UpdateDimension(System.Single perc)
    {
        actualPosition.x = Mathf.Lerp(ZeroPosition.x, UnoPosition.x, perc);
        actualPosition.y = Mathf.Lerp(ZeroPosition.y, UnoPosition.y, perc);
        actualScale.x = Mathf.Lerp(ZeroScale.x, UnoScale.x, perc);
        actualScale.y = Mathf.Lerp(ZeroScale.y, UnoScale.y, perc);

        transform.position = new Vector3(actualPosition.x, actualPosition.y, transform.position.z);
        transform.localScale = new Vector3(actualScale.x, actualScale.y, transform.localScale.z);
    }

    public void ModifyPercentage(float delta = 0)
    {
        percentage = Mathf.Clamp(percentage += delta, 0f, 1f);
        UpdateDimension(percentage);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.name);
        if (collision.collider.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Hole"))
        {
            uiMan.AddPoint();
        }
    }
}
