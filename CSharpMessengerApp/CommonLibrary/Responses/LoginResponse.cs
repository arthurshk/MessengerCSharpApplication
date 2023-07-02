using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommonLibrary.Responses;

public class LoginResponse: IDataContent
{
    public bool Success { get; set; }
    public int MessagesCount { get;set; }

    public Data ToData()
    {
        return new Data
        {
            Type = DataType.LoginResponse,
            Content = JsonSerializer.Serialize(this)
        };
    }
}
