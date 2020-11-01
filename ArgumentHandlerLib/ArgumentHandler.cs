using System;
using System.Collections.Generic;
using System.Linq;
using ArgumentHandlerLib.exceptions;

namespace ArgumentHandlerLib{
public class ArgumentHandler{
    private readonly List<Argument> _arguments = new List<Argument>();
    private Exception _exceptionThrown;


    public ArgumentHandler(params Argument[] arguments){
        foreach (var argument in arguments) _arguments.Add(argument);
    }


    // public List<Argument> Arguments => _arguments;

    public Exception ExceptionThrown => _exceptionThrown;
    
    public bool SetInputParam(params string[] strings){
        try{
            var index = 0;
            while (index < strings.Length){
                if (!strings[index].StartsWith(@"-")) throw new ArgumentIdExpectedException(strings[index]);

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
                if (_arguments.Exists(x =>x.GetShortId==argument.GetShortId &&  x.RawId != null))
                    throw new ArgumentGivenMoreThanOnceException(argument);

                argument.RawId = strings[index];

                var valueIndex = 1;
                while (valueIndex <= argument.ValueCountMax && index + valueIndex < strings.Length)
                    argument.Values.Add(strings[index + valueIndex++]);

                if (argument.Values.Count < argument.ValueCountMin) throw new MissingValuesException(argument);

                if (argument.IsIgnoreOthers) return true;

                index += 1 + argument.Values.Count;
            }

            foreach (var argument in _arguments.Where(argument => argument.IsRequired && argument.RawId == null))
                throw new MissingRequiredArgumentException(argument);
        }
        catch (Exception e){
            if (!(e is ArgumentGivenMoreThanOnceException) && !(e is ArgumentIdExpectedException) &&
                !(e is MissingRequiredArgumentException) && !(e is MissingValuesException) &&
                !(e is UnknownArgumentIdException)) throw;
            _exceptionThrown = e;
            return false;
        }

        return true;
    }

    public bool Contains(params string[] shortIds){
        foreach (var shortId in shortIds)
            if (_arguments.Find(x => x.GetShortId.Equals(shortId) && x.RawId != null) != null)
                return true;

        return false;
    }

    public Argument GetArgument(string shortId){
        return _arguments.Find(x => x.GetShortId.Equals(shortId) && x.RawId != null);
    }
}
}