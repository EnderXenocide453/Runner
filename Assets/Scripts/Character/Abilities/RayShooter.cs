using LevelObjects;
using System.Collections;
using UnityEngine;

namespace Character.Abilities
{
    [RequireComponent(typeof(LineRenderer))]
    public class RayShooter : Shooter
    {
        [SerializeField] private float _rayDistance = 10;
        [SerializeField] private float _rayRadius = 0.5f;
        [SerializeField] private LayerMask _rayMask;
        [SerializeField] private float _fadeDuration = 1;
        [SerializeField] private ShootHitBehaviour[] _hitBehaviours;
        private LineRenderer _lineRenderer;
        private Coroutine _drawCoroutine;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public override void Shoot()
        {
            Vector3 position = _shootPoint.position + _shootPoint.forward * _rayDistance;

            if (Physics.SphereCast(_shootPoint.position, _rayRadius, _shootPoint.forward, out RaycastHit hit, _rayDistance, _rayMask)) {
                position = hit.point;

                foreach (var behaviour in _hitBehaviours)
                    behaviour?.OnHit(hit.collider);
            }

            if (_drawCoroutine != null)
                StopCoroutine(_drawCoroutine);

            _drawCoroutine = StartCoroutine(DrawRay(_shootPoint.position, position));
        }

        public IEnumerator DrawRay(Vector3 start, Vector3 end)
        {
            float currTime = _fadeDuration;
            _lineRenderer.enabled = true;
            _lineRenderer.SetPositions(new Vector3[] { start, end });

            while (currTime > 0) {
                yield return null;

                currTime -= Time.deltaTime;

                var color = _lineRenderer.startColor;
                color.a = currTime / _fadeDuration;
                _lineRenderer.startColor = color;

                color = _lineRenderer.endColor;
                color.a = currTime / _fadeDuration;
                _lineRenderer.endColor = color;
            }

            _lineRenderer.enabled = false;
        }
    }
}