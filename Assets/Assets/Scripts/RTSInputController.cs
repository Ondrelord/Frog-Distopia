using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSInputController : MonoBehaviour
{
    private Vector2 selectionStartPos;
    [SerializeField] private GameObject selectionAreaBox = null;


    public List<ISelectable> selectables;

    // Start is called before the first frame update
    void Start()
    {
        selectables = new List<ISelectable>();
    }

    // Update is called once per frame
    void Update()
    {
        Selecting();
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePos = Utils.GetMouseWorldPosition();

            Collider2D hit = Physics2D.OverlapPoint(mousePos);
            if (hit != null)
            {
                IDamagable damagable = hit.GetComponent<IDamagable>();
                Attack(damagable);
            }
            else 
                Move(Utils.GetMouseWorldPosition());


        }
    }

    private void Move(Vector2 position)
    {
        foreach(ISelectable selected in selectables)
        {
            IMover mover = selected.GetGameObject().GetComponent<IMover>();
            if (mover != null)
            {
                mover.MoveTo(position);
                selected.SetState(UnitState.Moving);
            }
        }
    }

    private void Attack(IDamagable target)
    {
        foreach (ISelectable selected in selectables)
        {
            IAttacker attacker = selected.GetGameObject().GetComponent<IAttacker>();
            if (attacker != null)
            {
                attacker.Attack(target);
                selected.SetState(UnitState.Attacking);
            }
        }
    }

    private void Selecting()
    {
        // pressed
        if (Input.GetMouseButtonDown(0))
        {
            foreach (ISelectable selected in selectables)
                selected.HideHighlight();
            selectables.Clear();
            
            selectionStartPos = Utils.GetMouseWorldPosition();
            selectionAreaBox.SetActive(true);
        }

        // hold
        if (Input.GetMouseButton(0))
        {
            Vector2 selectionCurrPos = Utils.GetMouseWorldPosition();
            Vector2 lowerLeft = new Vector2(
                Mathf.Min(selectionCurrPos.x, selectionStartPos.x),
                Mathf.Min(selectionCurrPos.y, selectionStartPos.y)
                );

            Vector2 upperRight = new Vector2(
                Mathf.Max(selectionCurrPos.x, selectionStartPos.x),
                Mathf.Max(selectionCurrPos.y, selectionStartPos.y)
                );

            selectionAreaBox.transform.position = lowerLeft;
            selectionAreaBox.transform.localScale = upperRight - lowerLeft;

        }

        // released
        if (Input.GetMouseButtonUp(0))
        {
            Collider2D[] selectedColliders = Physics2D.OverlapAreaAll(selectionStartPos, Utils.GetMouseWorldPosition());

            foreach (Collider2D col in selectedColliders)
            {
                ISelectable selected = col.GetComponent<ISelectable>();

                if (selected != null)
                {
                    selectables.Add(selected);
                    selected.ShowHighlight();
                }
            }

            selectionAreaBox.SetActive(false);
        }
    }
}
