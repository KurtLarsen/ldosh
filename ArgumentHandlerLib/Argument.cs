﻿using System.Collections.Generic;

namespace ArgumentHandlerLib{
public class Argument{
    private readonly string _shortId;
    private readonly List<string> _values = new List<string>();
    private bool _ignoreOthers; // default is false
    private string _longId;
    private string _rawId;
    private bool _required = true;
    private int _valueCountMax;
    private int _valueCountMin;

    public Argument(string shortId){
        _shortId = shortId;
    }

    public string GetLongId => _longId;

    public string GetShortId => _shortId;


    public List<string> Values => _values;

    public bool IsRequired => _required;

    public bool IsIgnoreOthers => _ignoreOthers;

    public string RawId{
        get => _rawId;
        set => _rawId = value;
    }

    public int ValueCountMin => _valueCountMin;

    public int ValueCountMax => _valueCountMax;

    public Argument Required(){
        _required = true;
        return this;
    }

    public Argument Optional(){
        _required = false;
        return this;
    }


    public Argument RequiredValueCount(int valueCount){
        _valueCountMin = valueCount;
        _valueCountMax = valueCount;
        return this;
    }

    // ReSharper disable once UnusedMember.Global
    public Argument RequiredValueCount(int min, int max){
        _valueCountMin = min;
        _valueCountMax = max;
        return this;
    }

    public Argument LongId(string longId){
        _longId = longId;
        return this;
    }


    public Argument IgnoreOthers(){
        _ignoreOthers = true;
        return this;
    }

    public string RequiredValueCountAsString(){
        if (_valueCountMin == _valueCountMax)
            return _valueCountMin == 1 ? $"{_valueCountMin} value" : $"{_valueCountMin} values";

        return _valueCountMax - _valueCountMin == 1
            ? $"{_valueCountMin} or {_valueCountMax} values"
            : $"{_valueCountMin} to {_valueCountMax} values";
    }

    public string GivenValuesAsString(){
        return string.Join(" ", _values);
    }
}
}