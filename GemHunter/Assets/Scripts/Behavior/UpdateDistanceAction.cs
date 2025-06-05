using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UpdateDistance", story: "Update [Self] and [Target] [CurrentDistance]", category: "Action", id: "f713ae3dd0efeb46a4360f01e06f3e70")]
public partial class UpdateDistanceAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> CurrentDistance;

    private EntityBase  self;
    private EntityBase  target;

    protected override Status OnStart()
    {
        self    = Self.Value.GetComponent<EntityBase>();
        target  = Target.Value.GetComponent<EntityBase>();

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        CurrentDistance.Value = Vector2.Distance(self.MiddlePoint, target.MiddlePoint);

        return Status.Success;
    }
}

