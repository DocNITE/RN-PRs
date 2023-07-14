using Cinka.Game.Background.Data;
using JetBrains.Annotations;
using Robust.Client.Graphics;
using Robust.Client.ResourceManagement;
using Robust.Client.Utility;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Log;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations;
using Robust.Shared.Utility;

namespace Cinka.Game.Background.Manager;

public sealed class BackgroundManager : IBackgroundManager
{
    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;

    private Texture[] _currentBackground;
    private BackgoundPrototype _currentBackgroundPrototype;

    public BackgroundManager()
    {
        
    }
    
    public Texture[] GetCurrentBackground()
    {
        return _currentBackground;
    }

    public void LoadBackground(string name)
    {
        _currentBackgroundPrototype = _prototypeManager.Index<BackgoundPrototype>(name);
        _currentBackground = new Texture[_currentBackgroundPrototype.Layers.Count + 1];

        var backState = _currentBackgroundPrototype.State ?? "default";
        if (!TryGetRSI(null, out var brsi) || !brsi.TryGetState(backState, out var bstate))
            return;
        _currentBackground[0] = bstate.Frame0;

        for (var i = 0; i < _currentBackgroundPrototype.Layers.Count; i++)
        {
            var layer = _currentBackgroundPrototype.Layers[i];
            var layerState = layer.State ?? "default";
            if (!TryGetRSI(layer, out var rsi) || !rsi.TryGetState(layerState, out var state))
                state = bstate;
            
            _currentBackground[i+1] = state.Frame0;
        }
    }
    

    private bool TryGetRSI([CanBeNull] PrototypeLayerData data, out RSI rsi)
    {
        rsi = null;

        if (data?.RsiPath != null)
            rsi = StaticIoC.ResC.GetResource<RSIResource>(SpriteSpecifierSerializer.TextureRoot / data.RsiPath).RSI;
        else if (_currentBackgroundPrototype.RsiPath != null)
            rsi = StaticIoC.ResC
                .GetResource<RSIResource>(SpriteSpecifierSerializer.TextureRoot / _currentBackgroundPrototype.RsiPath)
                .RSI;
        
        return rsi != null;
    }

    public void UnloadBackground()
    {
        _currentBackground = null;
    }
}