using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolver : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _targetMesh;
    [SerializeField] private Material _dissolveMat, _previousMat;
    [SerializeField] private bool _shouldDissolve, _dissolving;
    private const string _dissolve = "_DissolveAmount";

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDissolve(0.1f);
        }
    }

    private void StartDissolve(float speed)
    {
        if (!_dissolving)
        {
            _shouldDissolve = !_shouldDissolve;
            StartCoroutine(Dissolve(_shouldDissolve, speed));
        }
    }

    private IEnumerator Dissolve(bool dissolve, float speed)
    {
        if (dissolve)
        {
            _previousMat = _targetMesh.sharedMaterial;
        }

        _targetMesh.sharedMaterial = _dissolveMat;
        yield return new WaitForSeconds(0.1f);

        float amount = 0;
        float target = 1;

        if (!dissolve) 
        {
            amount = 1;
            target = 0;
        }

        while (amount != target)
        {
            amount = Mathf.MoveTowards(amount, target, speed * Time.deltaTime);
            _dissolveMat.SetFloat(_dissolve, amount);
            _dissolving = true;
            yield return null;
        }

        if (!dissolve) 
        {
            _targetMesh.sharedMaterial = _previousMat;
        }

        //Debug.Log(gameObject.name + " dissolve : " + dissolve);
        _dissolving = false;
    }
}
