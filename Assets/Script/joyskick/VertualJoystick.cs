using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VertualJoystick : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    [SerializeField]
    private RectTransform laver;
    private RectTransform rectTransform;

    [SerializeField, Range(10,150)]
    private float laverlange;

    public Vector2 inputDirecton;
    private bool isInput;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    public void OnBeginDrag(PointerEventData eventData)
    {
        var inputPos = eventData.position - rectTransform.anchoredPosition;
        var inputVector = inputPos.magnitude < laverlange ? inputPos : inputPos.normalized * laverlange;

        laver.anchoredPosition = inputVector;
        inputDirecton = inputVector / laverlange;
        isInput = true;
        //throw new System.NotImplementedException();
    }
    public void OnDrag(PointerEventData eventData)
    {
        var inputPos = eventData.position - rectTransform.anchoredPosition;
        var inputVector = inputPos.magnitude < laverlange ? inputPos : inputPos.normalized * laverlange;

        laver.anchoredPosition = inputVector;
        inputDirecton = inputVector / laverlange;
        //throw new System.NotImplementedException();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        laver.anchoredPosition = Vector2.zero;
        isInput = false;
        //throw new System.NotImplementedException();
    }
    private void InputControlVector()
    {
        Debug.Log(inputDirecton.x + "/" + inputDirecton.y);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInput == true)
        {
            InputControlVector();
        }
        
    }
}
