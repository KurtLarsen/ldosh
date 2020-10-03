using System.Collections.Generic;

namespace ArgumentHandler{
public class Argument{
    private readonly string _shortId;
    private bool _required = true;
    private string _longId;
    private readonly List<string> _values = new List<string>();
    private string _rawId;
    private bool _stop; // default is false
    private int _valueCountMin;
    private int _valueCountMax;

    public Argument(string shortId){
        _shortId = shortId;
    }

    public string GetLongId => _longId;

    public string GetShortId => _shortId;


    public List<string> Values => _values;

    public bool IsRequired => _required;

    public bool IsStop => _stop;

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


    public Argument ValueCount(int valueCount){
        _valueCountMin = valueCount;
        _valueCountMax = valueCount;
        return this;
    }

    // ReSharper disable once UnusedMember.Global
    public Argument ValueCount(int min, int max){
        _valueCountMin = min;
        _valueCountMax = max;
        return this;
    }

    public Argument LongId(string longId){
        _longId = longId;
        return this;
    }


    public Argument Stop(){
        _stop = true;
        return this;
    }

    public string RequiredValueCountAsString(){
        if (_valueCountMin == _valueCountMax){
            return _valueCountMin == 1 ? $"{_valueCountMin} value" : $"{_valueCountMin} values";
        }

        return _valueCountMax - _valueCountMin == 1 ? $"{_valueCountMin} or {_valueCountMax} values" : $"{_valueCountMin} to {_valueCountMax} values";
    }

    public string ValuesAsString(){
        return string.Join(" ", _values);
    }
}
}