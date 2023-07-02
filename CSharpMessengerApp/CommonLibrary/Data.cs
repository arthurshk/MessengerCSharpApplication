using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommonLibrary;

public class Data
{

    public DataType Type { get; set; }
    public string Content { get; set; }

    public static Data Create(IDataContent model)
    {
        return model.ToData();
    }
}
