namespace ThesisCatalog.Core.Entities;

[Flags]
public enum ComponentType
{
    None = 0,
    Cpu = 1,
    Gpu = 2,
    Memory = 4,
    Storage = 8,
    Psu = 16
}