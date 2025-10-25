 
namespace _Project._Scripts.Cores.Stats 
{
    public interface IStatListener
    { 
        void OnTotalValueChanged(StatTypeSO statTypeSo, float newTotalValue);
    }
}