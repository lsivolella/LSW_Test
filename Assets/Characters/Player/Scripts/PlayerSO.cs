using UnityEngine;

/// <summary>
/// Player configuration Scriptable Object
/// </summary>
[CreateAssetMenu(menuName = "Player Configuration")]
public class PlayerSO : ScriptableObject
{
    [SerializeField] float moveSpeed = 10f;

    public float MoveSpeed { get { return moveSpeed; } }
}
