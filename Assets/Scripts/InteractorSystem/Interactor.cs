using Assets.Scripts.InteractorSystem;
using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionRadius = 2f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private InventorySO hasAnswer;
    private readonly Collider2D[] _collider = new Collider2D[3];
    [SerializeField] private int _numFoubd;
    [SerializeField] private GameObject questionPanel; // Thêm biến này để tham chiếu đến panel câu hỏi

    private IInteractable currentInteractable;

    // Update is called once per frame
    void Update()
    {
        _numFoubd = Physics2D.OverlapCircleNonAlloc(_interactionPoint.position, _interactionRadius,_collider,_interactableMask);
        if( _numFoubd > 0)
        {
            currentInteractable = _collider[0].GetComponent<IInteractable>();
            if( currentInteractable != null && Input.GetKeyDown(KeyCode.E))
            {
                if (hasAnswer.HasAnswer)
                {
                    if(hasAnswer.AnswerCorrect)
                    {
                        currentInteractable.Interact(this);
                    }
                    else
                    {
                        currentInteractable.DestroyChest(this);
                    }
                }
                else
                {
                    questionPanel.SetActive(true);
                }
            }
        }
    }

    public void HandleCorrectAnswer()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact(this);
            currentInteractable.DestroyChest(this);
        }
    }

    public void HandleWrongAnswer()
    {
        if (currentInteractable != null)
        {
            currentInteractable.DestroyChest(this);
        }
    }
}
