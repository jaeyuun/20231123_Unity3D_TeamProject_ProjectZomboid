using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeletionManager : MonoBehaviour
{
    [SerializeField] private string selectable = "Selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defalultMateral;

    private Transform _seletion;

    private void Update()
    {
        if(_seletion != null)
        {
            var slectionRender = _seletion.GetComponent<Renderer>();
            slectionRender.material = defalultMateral;
            _seletion = null;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if(selection.CompareTag(selectable))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterial;
                    Debug.Log("hit");
                }
                _seletion = selection;
            }
            
            
        }
    }
    

}
