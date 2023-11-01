using UnityEngine;
using Fungus;
using UnityStandardAssets.Characters.FirstPerson;

[CommandInfo("Your Category", "Modify Walk Speed", "Modify the walk speed property in FirstPersonController")]
public class ModifyWalkSpeed : Command
{
    [Tooltip("The FirstPersonController script to modify")]
    [SerializeField] private FirstPersonController firstPersonController;

    [Tooltip("The new walk speed value")]
    [SerializeField] private float newWalkSpeed;
    [SerializeField] private float newRunSpeed;

    public override void OnEnter()
    {
        if (firstPersonController != null)
        {
            firstPersonController.WalkSpeed = newWalkSpeed;
            firstPersonController.RunSpeed = newRunSpeed;
        }
        Continue();
    }

    public override string GetSummary()
    {
        if (firstPersonController == null)
        {
            return "Error: FirstPersonController not set";
        }
        return "Modify Walk Speed to " + newWalkSpeed;
    }
}
