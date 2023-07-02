using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommonLibrary.Requests;

public class LoginRequest : IDataContent
{
    public Client Me { get; set; }

    public Data ToData()
    {
        return new Data
        {
            Type = DataType.LoginRequest,
            Content = JsonSerializer.Serialize(this)
        };
    }
}

