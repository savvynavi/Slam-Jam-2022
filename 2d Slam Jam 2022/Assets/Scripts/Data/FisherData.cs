using UnityEngine;

[CreateAssetMenu(menuName = "Data/Fisherman Type Data", order = 1)]
public class FisherData : ScriptableObject
{
    public enum AttackType
    {
        Easy,
        Normal,
        Hard
    }

    [SerializeField] private string name;
    [SerializeField] private float maxEnergy;
    [SerializeField] private AttackType fisherType;


    public string Name => name;
    public float MaxEnergy => maxEnergy;
    public AttackType FisherFisherType => fisherType;
}
