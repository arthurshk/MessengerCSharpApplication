using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommonLibrary.Responses;

public class ErrorResponse: IDataContent
{
    public string Error { get; set; }

    public Data ToData()
    {
        return new Data
        {
            Type = DataType.ErrorResponse,
            Content = JsonSerializer.Serialize(this)
        };
    }

}
