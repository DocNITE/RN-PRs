using System.Collections.Generic;
using Cinka.Game.Dialog.Data;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;
using Robust.Shared.Maths;

namespace Cinka.Game.UserInterface.Systems.Dialog.Widgets;

[GenerateTypedNameReferences]
public sealed partial class DialogGui : UIWidget
{
    public DialogUIController _dialogUiController;
    public Label? currentLabel;
    public bool IsEmpty => currentLabel == null;
    private List<IDialogAction>? _actions;
    
    public DialogGui()
    {
        RobustXamlLoader.Load(this);
        _dialogUiController = UserInterfaceManager.GetUIController<DialogUIController>();
        _dialogUiController.RegisterDialog(this);

        Skip.OnPressed += _ => SkipMessage();
        _dialogUiController.MessageEnded += OnMessageEnded;
    }
    

    private void OnMessageEnded(Game.Dialog.Data.Dialog dialog)
    {
        Continue.Visible = true;
        _actions = dialog.Actions;
        if (dialog.SkipCommand)
            SkipMessage();
        
    }

    private void Act()
    {
        if(ButtonContainer.ChildCount > 0)
            return;
        foreach (var action in _actions)
        {
            action.Act();
        }

        _actions = null;
    }

    public void AppendText(string text)
    {
        var label = AppendLabel();
        label.Text = text;
    }

    public void SkipMessage()
    {
        if(_dialogUiController.IsMessage)
            _dialogUiController.SpeedUpText();
        else
            Act();
    }
    
    public void AppendLetter(char let)
    {
        if (currentLabel == null)
            AppendLabel();
        
        currentLabel.Text += let;
    }
    
    public Label AppendLabel(string? text = null)
    {
        var label = new Label();
        label.Text = text;
        TextContainer.AddChild(label);
        currentLabel = label;
        return label;
    }

    public void AddButton(DialogButton button)
    {
        var btn = new Button();
        btn.Text = button.Name;
        btn.OnPressed += _ => button.OnAction.Act();
        btn.Margin = new Thickness(12, 0, 12, 0);
        ButtonContainer.AddChild(btn);
    }
    
    public void ClearText()
    {
        Continue.Visible = false;
        TextContainer.DisposeAllChildren();
        ClearButtons();
        currentLabel = null;
    }

    public void ClearButtons()
    {
        ButtonContainer.DisposeAllChildren();
    }
}