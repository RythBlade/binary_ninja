﻿using System;

public enum PointerType
{
    eAddress32
    , eAddress64
    , eOffset32
    , eOffset64
}

public class MyTestPointer<T>
{

}

public class BnPointer : Attribute
{
    public BnPointer(PointerType type)
    {
        MyTestPointer<float> testing = null;
        MyTestPointer<MyTestPointer<float>> anotherTest = null;
    }

    public BnPointer(PointerType type, int test)
    {
    }
}
