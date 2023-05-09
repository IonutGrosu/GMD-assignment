using UnityEngine;
using UnityEngine.EventSystems;

public class Selectable : MonoBehaviour
{
    public static Selectable Instance;
    
    [SerializeField] private new string tag = "Selectable";
    private Camera _camera;
    private Transform _selection;
    private Transform _highlight;
    private RaycastHit _raycastHit;

    private void Awake()
    {
        Instance = this;
        _camera = Camera.main;
    }

    public GameObject GetSelection()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out var hit);
        if (hit.transform.CompareTag(tag))
        {
            print(hit.transform.name);
        }

        return hit.transform.gameObject;
    }

    void Update()
    {
        if (_highlight != null)
        {
            _highlight.gameObject.GetComponent<Outline>().enabled = false;
            _highlight = null;
        }
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out _raycastHit))
        {
            _highlight = _raycastHit.transform;
            if (_highlight.CompareTag("Selectable") && _highlight != _selection)
            {
                if (_highlight.gameObject.GetComponent<Outline>() != null)
                {
                    _highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = _highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    _highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                    _highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                }
            }
            else
            {
                _highlight = null;
            }
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (_highlight)
            {
                if (_selection != null)
                {
                    _selection.gameObject.GetComponent<Outline>().enabled = false;
                }
                _selection = _raycastHit.transform;
                _selection.gameObject.GetComponent<Outline>().enabled = true;
                _highlight = null;
            }
            else
            {
                if (_selection)
                {
                    _selection.gameObject.GetComponent<Outline>().enabled = false;
                    _selection = null;
                }
            }
        }
    }
}