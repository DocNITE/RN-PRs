﻿using System;
using System.Numerics;
using Cinka.Game.Gameplay;
using Robust.Client;
using Robust.Client.Animations;
using Robust.Client.AutoGenerated;
using Robust.Client.ResourceManagement;
using Robust.Client.State;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;
using Robust.Shared.Animations;
using Robust.Shared.Configuration;
using Robust.Shared.IoC;
using Robust.Shared.Localization;
using Robust.Shared.Utility;

namespace Cinka.Game.Menu.UI;

[GenerateTypedNameReferences]
public sealed partial class MenuScreen : UIScreen
{
    [Dependency] private readonly IStateManager _stateManager = default!;
    [Dependency] private readonly IGameController _gameController = default!;
    
    public AnimationExtend<Vector2> MenuLabelAnim;
    public AnimationExtend<Vector2> ButtonAnim;
    
    private bool _resizeFirstTime = true;
    
    public MenuScreen()
    {
        RobustXamlLoader.Load(this);
        IoCManager.InjectDependencies(this);
        
        SetAnchorPreset(MainContainer, LayoutPreset.Wide);
        SetAnchorPreset(Background, LayoutPreset.Wide);

        MenuLabelAnim = new AnimationExtend<Vector2>(o =>
        {
            SetPosition(ButtonContainer, o);
        }, this);
        MenuLabelAnim.Track = new AnimationTrackControlProperty()
        {
            InterpolationMode = AnimationInterpolationMode.Cubic,
            KeyFrames =
            {
                new AnimationTrackProperty.KeyFrame(new Vector2(-1500, 70),0.1f),
                new AnimationTrackProperty.KeyFrame(new Vector2(55, 70),0.3f),
                new AnimationTrackProperty.KeyFrame(new Vector2(0, 70),0.4f),
            }
        };

        ButtonAnim = new AnimationExtend<Vector2>(o =>
        {
            SetPosition(MenuLabel, o);
        }, this);
        ButtonAnim.Track = new AnimationTrackControlProperty()
        {
            InterpolationMode = AnimationInterpolationMode.Cubic,
            KeyFrames =
            {
                new AnimationTrackProperty.KeyFrame(new Vector2(-1500, 0), 0.3f),
                new AnimationTrackProperty.KeyFrame(new Vector2(55, 0), 0.3f),
                new AnimationTrackProperty.KeyFrame(new Vector2(0, 00), 0.4f),
            }
        };
        
        IoCManager.Resolve<IConfigurationManager>().OnValueChanged(CCVars.CCVars.BackroundMenu,OnBackroundChanged,true);
        
        PlayButton.OnPressed += PlayButtonOnOnPressed;
        Exit.OnPressed += OnExitPressed;
        
        Config.OnPressed += _ => PlayAnimations();;
        MenuLabel.SetMessage(FormattedMessage.FromMarkup(Loc.GetString("menu-label")));
    }
    protected override void Resized()
    {
        base.Resized();
        if (!_resizeFirstTime) return;
        
        PlayAnimations();
        _resizeFirstTime = false;
    }

    private void PlayAnimations()
    {
        if(!MenuLabelAnim.HasRunningAnimation())
            MenuLabelAnim.PlayAnimation();
        if(!ButtonAnim.HasRunningAnimation())
            ButtonAnim.PlayAnimation();
    }

    private void OnExitPressed(BaseButton.ButtonEventArgs obj)
    {
        _gameController.Shutdown("user exit");
    }

    private void PlayButtonOnOnPressed(BaseButton.ButtonEventArgs obj)
    {
        _stateManager.RequestStateChange<GameplayStateBase>();
    }
    
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        PlayButton.OnPressed -= PlayButtonOnOnPressed;
        Exit.OnPressed -= OnExitPressed;
        IoCManager.Resolve<IConfigurationManager>().UnsubValueChanged(CCVars.CCVars.BackroundMenu,OnBackroundChanged);
    }

    private void OnBackroundChanged(string path)
    {
        Background.Texture = IoCManager.Resolve<IResourceCache>().GetResource<TextureResource>(path).Texture;
    }
}