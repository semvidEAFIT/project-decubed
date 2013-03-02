using UnityEngine;
using System.Collections;

/// <summary>
/// Move option selector is an option component to
/// show an enabled next position in level to move.
/// </summary>
public class MoveOptionSelector : MonoBehaviour,IClickable {

    private CubeControllerInput listener;

    public CubeControllerInput Listener
    {
        set { listener = value; }
    }

    public void NotifyClick()
    {
        listener.NotifySelection(this);
    }
}
