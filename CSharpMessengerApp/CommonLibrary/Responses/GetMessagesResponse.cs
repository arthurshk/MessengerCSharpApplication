using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommonLibrary.Responses;

public class GetMessagesResponse: IDataContent
{
    public List<Message> Messages { get; set; }

    
    public Data ToData()
    {
        return new Data
        {
            Type = DataType.GetMessageResponse,
            Content = JsonSerializer.Serialize(this)
        };
    }
}
