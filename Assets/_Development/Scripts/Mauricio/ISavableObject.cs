
namespace _Development.Scripts.Mauricio
{
    public interface ISavableObject
    {
        void Save(int saveSlot);
        void Load(int saveSlot);
    }
}
