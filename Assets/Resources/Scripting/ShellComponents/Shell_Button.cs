using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class Shell_Button : MonoBehaviour
{
    [Tooltip("When your button is pressed, this JTrigger will be activated.\n\nMake sure to configure your JTrigger's event mode as On Demand.")]
    public JTrigger onButtonPress;
    public bool ShowTriggerToAffectAndInvert() { return !impulseButton || timedButton; }
    [Space]
    [ConditionalField(true, "ShowTriggerToAffectAndInvert")]
    [Tooltip("[This setting can be ignored if you've marked the impulse button checkbox as true]\n\nThis is the trigger that your button will modify when pressed.\n\nFor non-impulse buttons, this MUST be assigned, and it should be unique.")]
    public string triggerToAffect = "button_id";
    [ConditionalField(true, "ShowTriggerToAffectAndInvert")]
    [Tooltip("Whether or not to invert the button output. If this is true, button triggers default to 1, and turn to 0 when pressed. When this is false, it's the opposite.\n\nImpulse buttons cannot affect triggers, so this setting can be ignored if impulseButton is true.")]
    public bool invertTriggerOutput = false;
    [Space]
    [Tooltip("If this checkbox is marked, your button will have a timer ring that counts down when pressed. Once the timer runs out, a new event in the form of On Button Timer End is activated, and the button will reset.")]
    public bool timedButton = false;
    [ConditionalField("timedButton", false, false)]
    [Tooltip("By default, buttons will stay pressed in until the level reloads. If you mark this checkbox as true, you can hit a button an infinite amount of times to trigger On Button Press.\n\nThis setting cannot be changed if timedButton is active, since by default timedButtons are treated as impulse buttons.")]
    public bool impulseButton = false;
    [ConditionalField("timedButton", false, false)]
    [Tooltip("This is just a reskin of the standard button, making it blue. This is good if you need to emphasize the importance of a button.\n\nThis setting cannot be changed if timedButton is active, since timed buttons do not have a special button equivalent to work with")]
    public bool specialButton = false;
    [Space]
    [ConditionalField("timedButton", false, true)]
    [MinValue(0.001f)]
    public float buttonTime = 5f;
    [ConditionalField("timedButton", false, true)]
    [Tooltip("This is the associated JTrigger that will be activated when this button's timer runs out.\n\nMake sure to configure your JTrigger's event mode as On Demand.")]
    public JTrigger onButtonTimerEnd;


    public void OnDrawGizmos()
    {
        Matrix4x4 m = Gizmos.matrix;

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);

        if(specialButton && !timedButton)
            Gizmos.color = new Color(0f, 0.3f, 1f, 0.6f);
        else
            Gizmos.color = new Color(1f, 0.3f, 0f, 0.6f);

        Gizmos.DrawCube(Vector3.up * 0.3f, new Vector3(2.3f,0.6f,2.3f));

        if (specialButton && !timedButton)
            Gizmos.color = new Color(0f, 0.8f, 1f, 0.6f);
        else
            Gizmos.color = new Color(1f, 0.8f, 0f, 0.6f);

        Gizmos.DrawCube(Vector3.up * 0.75f, new Vector3(1.5f,1f,1.5f));

        Gizmos.matrix = m;
    }

    public void CancelTimer() { }
    public void CancelTimerWithoutEvent() { }
    public void SetButtonState(bool state) { }
}
