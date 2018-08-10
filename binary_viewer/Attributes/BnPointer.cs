using System;

public enum PointerType
{
    eAddress32
    , eAddress64
    , eOffset32
    , eOffset64
}

public class BnPointer : Attribute
{
    public BnPointer(PointerType type)
    {
    }
}
