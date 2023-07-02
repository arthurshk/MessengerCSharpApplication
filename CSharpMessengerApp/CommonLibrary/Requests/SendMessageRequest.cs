using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommonLibrary.Requests;

public class SendMessageRequest: IDataContent
{
    public Message Message { get; set; }
        
    public Data ToData()
    {
        return new Data
        {
            Type = DataType.SendMessageRequest,
            Content = JsonSerializer.Serialize(this)
        };
    }
}
