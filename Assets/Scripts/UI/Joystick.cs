using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private float joystickHandleMaxDistance;
    [SerializeField]
    private Image joystickContainer;
    [SerializeField]
    private Image joystickHandle;

    private bool usingJoystick;
    private Vector3 direction;
    public Vector2 pointerPosition;

    [SerializeField]
    private PlayerMovement scriptMovement;

    private void Start()
    {
        scriptMovement = GameManager.scriptPlayer.gameObject.GetComponentInChildren<PlayerMovement>();
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        pointerPosition = eventData.position;


        /*
        Vector2 handlePosition = Vector2.zero;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickContainer.rectTransform, eventData.position, eventData.pressEventCamera, out handlePosition))
        {
            handlePosition.x = (handlePosition.x / joystickContainer.rectTransform.sizeDelta.x);
            handlePosition.y = (handlePosition.y / joystickContainer.rectTransform.sizeDelta.y);

            float x = Mathf.Clamp(handlePosition.x, -1, 1);
            float y = Mathf.Clamp(handlePosition.y, -1, 1);
            direction = new Vector3(x, 0, y).normalized;

            joystickHandle.rectTransform.anchoredPosition = new Vector3(direction.x * joystickHandleMaxDistance, direction.z * joystickHandleMaxDistance);

            scriptMovement.JoystickMove(direction);
        }
        */
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
        usingJoystick = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ResetHandle();
        scriptMovement.StopMove();
        usingJoystick = false;

    }

    private void ResetHandle()
    {
        direction = Vector3.zero;
        joystickHandle.rectTransform.anchoredPosition = Vector3.zero;
       

    }

    private void JoystickUse()
    {
        if (usingJoystick)
        {
            direction = Calculations.GetDirectionToTarget(joystickContainer.rectTransform.anchoredPosition, pointerPosition);
            
            scriptMovement.JoystickMove(direction);
        }

    }

    private void Update()
    {
        JoystickUse();
    }
}

