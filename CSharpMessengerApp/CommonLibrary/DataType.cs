using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary;

public enum DataType
{
    ErrorResponse,
    LoginRequest, LoginResponse,
    GetMessageRequest, GetMessageResponse,
    SendMessageRequest, SendMessageResponse,
}
