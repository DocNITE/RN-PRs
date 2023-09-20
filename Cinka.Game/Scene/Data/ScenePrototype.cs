using System.Collections.Generic;
using Cinka.Game.Location.Data;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.Manager.Attributes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;

namespace Cinka.Game.Scene.Data;

[Prototype("scene")]
public sealed class ScenePrototype : IPrototype
{
    [IdDataField] public string ID { get; private set; } = default!;
    
    [DataField] public HashSet<EntProtoId> Characters = new();

    [DataField] public List<Dialog.Data.Dialog> Dialogs = new();

    [DataField] public ProtoId<LocationPrototype> Location = "default";
    
}