using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommonLibrary.Responses;

public class SendMessageResponse: IDataContent
{
    public bool Success { get; set; }

    public Data ToData()
    {
        return new Data
        {
            Type = DataType.SendMessageResponse,
            Content = JsonSerializer.Serialize(this)
        };
    }
}
