using UnityEngine;

public class Interactable : MonoBehaviour {

    public float Radius = 3.0f;
    public Transform InteractionTransform;

    private bool _isFocus = false;
    private Transform _player;

    private bool _hasInteracted = false;

    public void OnFocused(Transform playerTransform)
    {
        _isFocus = true;
        _player = playerTransform;
        _hasInteracted = false;
    }

    public void OnDefocused()
    {
        _isFocus = false;
        _player = null;
        _hasInteracted = false;
    }

    public virtual void Interract()
    {
        Debug.Log("Interact with " + transform.name);
    }

    void Update()
    {
        if (_isFocus && !_hasInteracted)
        {
            float distance = Vector3.Distance(_player.position, InteractionTransform.position);

            if (distance <= Radius)
            {
                Interract();
                _hasInteracted = true;
            }
        }
    }

    /// <summary>
    /// Рисуем радиус внутри эдитора
    /// </summary>
    void OnDrawGizmosSelected()
    {
        if (InteractionTransform == null)
        {
            InteractionTransform = transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(InteractionTransform.position, Radius);
    }
}
