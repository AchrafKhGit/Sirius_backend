using System.Runtime.Serialization;

namespace sirius.Enumerations;

public enum ExpenceType
{
    [EnumMember(Value = "Achat")]
    Achat = 1,
    [EnumMember(Value = "Deplacement")]
    Deplacement = 2
}