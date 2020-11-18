using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class CrewMember : MonoBehaviour
{
    public string Name { get; set; }
    public float Health { get; set; }

    [SerializeField]
    private Profession _profession;

    private MoveController _moveController;

    public CrewMember()
    {
        
    }

    private void Awake()
    {
        _moveController = GetComponent<MoveController>();
    }

    private void Update()
    {
        Move();
    }
        
    private void Move()
    {
        _moveController.Move();
    }
}
