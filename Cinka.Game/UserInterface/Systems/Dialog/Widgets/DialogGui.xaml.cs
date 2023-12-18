using System.Collections.Generic;
using Cinka.Game.Dialog.Data;
using Robust.Client.AutoGenerated;
using Robust.Client.Graphics;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;
using Robust.Shared.Localization;
using Robust.Shared.Maths;
using Robust.Shared.Utility;

namespace Cinka.Game.UserInterface.Systems.Dialog.Widgets;

[GenerateTypedNameReferences]
public sealed partial class DialogGui : UIWidget
{
    private readonly DialogUIController _dialogUiController;
    private FormattedMessage _currentMessage = new();

    public List<DialogButton> Buttons = new List<DialogButton>();
    
    public DialogGui()
    {
        RobustXamlLoader.Load(this);
        _dialogUiController = UserInterfaceManager.GetUIController<DialogUIController>();
        _dialogUiController.RegisterDialog(this);
        
        Text.SetMessage(_currentMessage,defaultColor: Color.Black);
    }

    public void SetEmote(Texture? texture)
    {
        PersRect.Texture = texture;
        PersRect.Visible = texture != null;
    }

    public void AppendLetter(char let)
    {
        _currentMessage.AddText(let.ToString());
        Text.SetMessage(_currentMessage,defaultColor:Color.Black);
    }

    public FormattedMessage AppendLabel(string? text = null)
    {
        if (text != null)
            _currentMessage.AddMessage(FormattedMessage.FromMarkup(Loc.GetString(text)));
        
        return _currentMessage;
    }

    public void AddButton(DialogButton button)
    {
        var btn = new Button();
        btn.Text = button.Name;
        btn.OnPressed += _ => button.DialogAction.Act();
        btn.Margin = new Thickness(0, 0, 36, 0);
        btn.MinWidth = 162;
        ButtonContainer.AddChild(btn);
        
        Buttons.Add(button);
    }

    public void ClearText()
    {
        _currentMessage.Clear();
        ClearButtons();
    }

    public void ClearButtons()
    {
        ButtonContainer.DisposeAllChildren();
        Buttons.Clear();
    }

    public bool IsEmpty()
    {
        return _currentMessage.IsEmpty;
    }
}