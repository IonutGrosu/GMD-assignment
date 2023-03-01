using UnityEngine;

public class Selectable : MonoBehaviour
{
    [SerializeField] private new string tag = "Selectable";
    
    private Transform _selection;
    private void Update()
    {
        if (_selection != null)
        {
            _selection.gameObject.GetComponent<Outline>().enabled = false;
            _selection = null;
        }
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

     
        if (Physics.Raycast(ray, out var hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(tag))
            {
                var selectionOutline = selection.gameObject.GetComponent<Outline>();

                if (selectionOutline != null)
                {
                    selectionOutline.gameObject.GetComponent<Outline>().enabled = true;

                    if (Input.GetMouseButtonDown(0))
                    {
                        Destroy(selection.gameObject);
                        _selection = null;
                    }
                }
                else
                {
                    var outline = selection.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    selection.gameObject.GetComponent<Outline>().OutlineColor = Color.white;
                    selection.gameObject.GetComponent<Outline>().OutlineWidth = 25.0f;
                }

                _selection = selection;
            }
            else
            {
                _selection = null;
            }
        }
    }
}