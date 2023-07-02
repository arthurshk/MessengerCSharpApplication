using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.BaseViewModels;
using CommonLibrary;

namespace DesktopClient.ViewModels;

public class ClientViewModel: NotifyPropertyChangedBase
{
    public ClientViewModel(Client model)
    {
        Model = model;
    }

    public Client Model { get; set; }

    public string Login
    {
        get => Model.Login;
        set
        {
            Model.Login = value;
            OnPropertyChanged(nameof(Login));
        }
    }

    public string Password
    {
        get => Model.Password;
        set
        {
            Model.Password = value;
            OnPropertyChanged(nameof(Password));
        }
    }
}
