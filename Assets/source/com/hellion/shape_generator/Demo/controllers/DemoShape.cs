using com.hellion.shapes.interfaces;
using com.hellion.shapes.utils;
using UnityEngine;

namespace com.hellion.shapes.demo.controller
{
    [ExecuteInEditMode]
    public class DemoShape : MonoBehaviour
    {
        [SerializeField] private float x_radius = 1, y_radius = 1;
        [SerializeField] private Vector3 posOffset = Vector3.zero;
        [SerializeField] private int resolution = 10;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Transform cursor = null;
        [SerializeField] private GameObject moveObject = null;

        [SerializeField] private float moveSpeed = 1;
        [SerializeField] private float rotationSpeed = 1;

        private bool isMoving = false;

        private IShape shape;
        private float moveTime = 0;
        private ShapeGeneratorKeyBinding demoController;

        private void Awake()
        {
            shape = new BoomerangEllipseShape(x_radius, y_radius);
            demoController = new ShapeGeneratorKeyBinding();
        }

        private void OnEnable()
        {
            if (demoController != null)
            {
                demoController.Enable();
            }
        }

        private void OnDisable()
        {
            if (demoController != null)
            {
                demoController.Disable();
            }
        }

        private void Update()
        {
            if (lineRenderer == null || x_radius <= 0 || y_radius <= 0)
            {
                if (lineRenderer != null)
                {
                    lineRenderer.positionCount = 0;
                }
                return;
            }

            shape = new BoomerangEllipseShape(x_radius, y_radius);
            DrawShape();

            if (demoController != null && demoController.ShapeDemoMap.Click.WasPressedThisFrame() && !isMoving)
            {
                StartMove();
            }

            if (demoController != null && demoController.ShapeDemoMap.Left.IsPressed() && !isMoving)
            {
                MoveLeft();
            }

            if (demoController != null && demoController.ShapeDemoMap.Right.IsPressed() && !isMoving)
            {
                MoveRight();
            }

            if (isMoving)
            {
                MoveObject();
            }
        }

        private void MoveLeft()
        {
            if (cursor != null)
            {
                cursor.Rotate(Vector3.forward, rotationSpeed);
            }
        }

        private void MoveRight()
        {
            if (cursor != null)
            {
                cursor.Rotate(Vector3.forward, -rotationSpeed);
            }
        }

        private void StartMove()
        {
            moveTime = 0;
            isMoving = !isMoving;
        }

        private Transform GetCursor()
        {
            return this.cursor ? this.cursor : transform;
        }

        private void MoveObject()
        {
            moveTime += Time.deltaTime * moveSpeed;
            if (moveTime >= 1)
            {
                moveTime = 0;
                isMoving = false;
            }

            moveObject.transform.eulerAngles = GetCursor().transform.eulerAngles + Vector3.forward * 360f * moveTime * rotationSpeed;
            moveObject.transform.position = GetPoint(moveTime);
        }

        private void DrawShape()
        {
            lineRenderer.positionCount = (int)(resolution * 0.75f);
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            float step = 1f / resolution;
            for (int i = 0; i < resolution + 1; i++)
            {
                Vector3 pos = GetPoint(i * step);
                if (i < lineRenderer.positionCount)
                {
                    lineRenderer.SetPosition(i, pos);
                }
            }
        }

        private Vector3 GetPoint(float t)
        {
            Vector3 point = shape.GetPoint(t);
            return cursor.TransformPoint(GetCursor().position + posOffset + point);
        }
    }
}
