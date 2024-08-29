using Random = UnityEngine.Random;
using UnityEngine;
using System;

[Serializable]
public class FindFreePoss
{
    [SerializeField] private Vector2 _area;
    [SerializeField] private LayerMask _layerDetect;

    private Collider[] _colliders;

    public Vector3 FreePoss(float radius)
    {
        Vector3 pos = Vector3.zero;
        bool finded = false;

        while (!finded)
        {
            pos.x = Random.Range(_area.x, _area.y);
            pos.z = Random.Range(_area.x, _area.y);
            Physics.OverlapSphereNonAlloc(pos, radius, _colliders, _layerDetect);
            finded = _colliders == null || _colliders.Length < 1;
        }

        return pos;
    }
}