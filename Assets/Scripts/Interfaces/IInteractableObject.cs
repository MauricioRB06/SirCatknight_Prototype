namespace Interfaces
{
    public interface IInteractableObject
    {
        float HoldDuration { get; }
        bool HoldInteract { get; }
        bool MultipleUse { get; }
        bool IsInteractable { get; }

        void OnInteractable();
    }
}
