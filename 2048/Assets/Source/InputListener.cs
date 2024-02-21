using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INputListener : MonoBehaviour
{
    [SerializeField] FieldSpawner _fieldSpawner;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _fieldSpawner.OnInput(Vector2.up);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _fieldSpawner.OnInput(Vector2.left);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _fieldSpawner.OnInput(Vector2.down);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _fieldSpawner.OnInput(Vector2.right);
        }
    }
}
