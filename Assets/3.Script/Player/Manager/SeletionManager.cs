using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeletionManager : MonoBehaviour
{
    [SerializeField] private string selectable = "Selectable";
    private float distance = 5f;
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

        hitObject();
    }
    
    private void hitObject()
    {
        //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray ray = new Ray(transform.position + new Vector3(0, 1.2f, 0), transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            // Debug.DrawRay(transform.position + new Vector3(0, 1.2f, 0), transform.forward * 10f, Color.red);
            var selection = hit.transform;
            if (selection.CompareTag(selectable))
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
