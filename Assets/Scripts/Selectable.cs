using UnityEngine;
using UnityEngine.EventSystems;

public class Selectable : MonoBehaviour
{
    public static Selectable instance;
    
    [SerializeField] private new string tag = "Selectable";
    private Camera camera;
    private Transform selection;
    private Transform highlight;
    private RaycastHit raycastHit;

    private void Awake()
    {
        instance = this;
        camera = Camera.main;
    }

    public GameObject GetSelection()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out var hit);
        if (hit.transform.CompareTag(tag))
        {
            print(hit.transform.name);
        }

        return hit.transform.gameObject;
    }

    void Update()
    {
        // Highlight
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Selectable") && highlight != selection)
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                }
            }
            else
            {
                highlight = null;
            }
        }

        // Selection
        if (Input.GetMouseButtonDown(0))
        {
            if (highlight)
            {
                if (selection != null)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                }
                selection = raycastHit.transform;
                selection.gameObject.GetComponent<Outline>().enabled = true;
                highlight = null;
            }
            else
            {
                if (selection)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                    selection = null;
                }
            }
        }
    }
}