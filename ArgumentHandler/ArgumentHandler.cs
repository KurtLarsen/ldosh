using System.Collections.Generic;
using System.Linq;

namespace ArgumentHandler{
public class ArgumentHandler{
    private readonly List<Argument> _arguments=new List<Argument>();

    public List<Argument> Arguments => _arguments;

    public void AddArgument(Argument argument){
        _arguments.Add(argument);
    }

    public void Input(params string[] strings){
        var index = 0;
        while (index < strings.Length){
            if (!strings[index].StartsWith(@"-")){
                throw new ArgumentIdExpected(strings[index]);
            }

            Argument argument;
            if (strings[index].StartsWith(@"--")){
                var longId = strings[index].Substring(2);
                argument = _arguments.Find(x => x.GetLongId.Equals(longId));
            }
            else{
                var shortId = strings[index].Substring(1);
                argument = _arguments.Find(x => x.GetShortId.Equals(shortId));
            }

            if (argument == null) throw new UnknownArgumentIdException(strings[index]);

            

            // Check if argument already is assigned
            if (_arguments.Exists(x => x.RawId!=null))
                throw new ArgumentGivenMoreThanOnceException(argument);

            argument.RawId = strings[index];

            var valueIndex = 1;
            while (valueIndex <= argument.ValueCountMax && index + valueIndex < strings.Length){
                argument.Values.Add(strings[index+valueIndex++]);
            }

            if (argument.Values.Count < argument.ValueCountMin) throw new MissingValuesException( argument);

            if(argument.IsStop) return;
            
            index += 1 + argument.Values.Count;
        }

        foreach (var argument in _arguments.Where(argument => argument.IsRequired && argument.RawId == null)){
            throw new MissingRequiredArgumentException(argument);
        }

    }

    public bool Contains(params string[] shortIds){
        foreach (var shortId in shortIds){
            if (_arguments.Find(x => x.GetShortId.Equals(shortId) && x.RawId!=null) != null) return true;
        }

        return false;
    }
}
}