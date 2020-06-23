using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class Interactions : MonoBehaviour
{
    Material material;
    Color randColor;
    
    void Start()
    {
        material = this.GetComponent<MeshRenderer>().material;
    }

    public Color RandomiseColor() {
        Color c = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        return c;
    }

    private void OnMouseUpAsButton() {
        if (!EventSystem.current.IsPointerOverGameObject())
            {
            material.color = RandomiseColor();
        }
    }
    
    [SerializeField] float distance = 10f;
    Vector3 objPosition;
    Vector3 mousePosition;
    [SerializeField] float speed = 1f;
    Vector3 startPos;

    private void OnMouseDrag() {
        if (!EventSystem.current.IsPointerOverGameObject()) {
            mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance); 
            objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            StartCoroutine("move");
        }
    }


    IEnumerator move() {
        startPos = transform.position;

        float step = speed / (transform.position - objPosition).magnitude;
        float progress = 0;

        while ((transform.position - objPosition).sqrMagnitude > 0.01f) {
            progress += step * Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, objPosition, progress);
            yield return null;
        }

        transform.position = objPosition;
    }
}

