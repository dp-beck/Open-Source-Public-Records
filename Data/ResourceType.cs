using System.ComponentModel;

namespace Data;

public enum ResourceType
{
    [Description("Business Records")]
    BusinessRecords,
    
    [Description("Civil Courts")]
    CivilCourts,
    
    [Description("Upper Civil Courts Only")]
    UpperCivilCourtsOnly,
    
    [Description("Lower Civil Courts Only")]
    LoverCivilCourtsOnly,
    
    [Description("Criminal Courts")]
    CriminalCourts,
    
    [Description("Upper Criminal Courts Only")]
    UpperCriminaCourtsOnly,
    
    [Description("Lower Criminal Courts Only")]
    LoverCriminaCourtsOnly,

    Recorder,
    
    Ucc,
    
    Assessor,
}

public static class EnumExtensions
{
    public static string ToFriendlyString(this Enum value)
    {
        return "TO BUILD";
    }
}