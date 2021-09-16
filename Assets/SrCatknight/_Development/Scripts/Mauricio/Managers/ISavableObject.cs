
namespace _Development.Scripts.Mauricio.Managers
{
    public interface ISavableObject
    {
        void Save(int saveSlot);
        void Load(int saveSlot);
    }
}
