using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Inputer
{
    public enum tOK {pressed, hold, released } ;
    [Space(5)]
    public UnityEvent OnKeyEvent;
    [Space(5)]
    public KeyCode key;
    [Space(5)]
    public tOK typeOfKey;
}

public class InputsManager : MonoBehaviour
{
    public List<Inputer> inputs;
    
    private void Update()
    {
        foreach (Inputer input in inputs)
        {
            if(input.typeOfKey == Inputer.tOK.pressed)
            {
                if (Input.GetKeyDown(input.key))
                {
                    input.OnKeyEvent?.Invoke();
                }
            }

            if (input.typeOfKey == Inputer.tOK.hold)
            {
                if (Input.GetKey(input.key))
                {
                    input.OnKeyEvent?.Invoke();
                }
            }

            if (input.typeOfKey == Inputer.tOK.released)
            {
                if (Input.GetKeyUp(input.key))
                {
                    input.OnKeyEvent?.Invoke();
                }
            }
        }
    }


}
