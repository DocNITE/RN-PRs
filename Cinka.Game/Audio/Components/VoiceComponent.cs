using Robust.Shared.GameObjects;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Cinka.Game.Audio.Components;

[RegisterComponent]
public sealed partial class VoiceComponent : Component
{
    [DataField] public string Voice = string.Empty;
}