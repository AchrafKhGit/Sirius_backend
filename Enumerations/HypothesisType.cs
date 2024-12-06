using System.Runtime.Serialization;

namespace sirius.Enumerations;

public enum HypothesisType
{
    [EnumMember(Value = "Project")]
    Project = 1,

    [EnumMember(Value = "Lot")]
    Lot = 2,

    [EnumMember(Value = "Activity")]
    Activity = 3,

    [EnumMember(Value = "Task")]
    Task = 4
}