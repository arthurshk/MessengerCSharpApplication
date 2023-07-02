using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.BaseViewModels;
using CommonLibrary;

namespace DesktopClient.ViewModels;

public class MessageViewModel : NotifyPropertyChangedBase
{
    public MessageViewModel(Message model)
    {
        Model = model;
    }

    public Message Model { get; set; }

    public string Text
    {
        get => Model.Text;
        set
        {
            Model.Text = value;
            OnPropertyChanged(nameof(Text));
        }
    }

    public ClientViewModel From
    {
        get => new ClientViewModel(Model.From);
        set
        {
            Model.From = value.Model;
            OnPropertyChanged(nameof(From));
        }
    }

    public DateTime CreatedAt => Model.CreatedAt;

    public string To
    {
        get => Model.To.Login;
        set
        {
            Model.To.Login = value;
            OnPropertyChanged(nameof(To));
        }
    }
}
