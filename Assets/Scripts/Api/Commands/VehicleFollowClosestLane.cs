/**
 * Copyright (c) 2019 LG Electronics, Inc.
 *
 * This software contains code licensed as described in LICENSE.
 *
 */

using SimpleJSON;
using UnityEngine;

namespace Simulator.Api.Commands
{
    class VehicleFollowClosestLane : ICommand
    {
        public string Name => "vehicle/follow_closest_lane";

        public void Execute(JSONNode args)
        {
            Debug.Log("VehicleFollowClosestLane");
            var uid = args["uid"].Value;
            var follow = args["follow"].AsBool;
            var initialSpeed = args["initial_speed"].AsFloat;
            var maxSpeed = args["max_speed"].AsFloat;
            var isLaneChange = args["isLaneChange"].AsBool;
            var api = ApiManager.Instance;

            if (api.Agents.TryGetValue(uid, out GameObject obj))
            {
                var npc = obj.GetComponent<NPCController>();
                if (npc == null)
                {
                    api.SendError(this, $"Agent '{uid}' is not a NPC agent");
                    Debug.Log($"Agent '{uid}' is not a NPC agent");
                    return;
                }

                if (follow)
                {
                    var laneFollow = npc.SetBehaviour<NPCLaneFollowBehaviour>();
                    laneFollow.SetFollowClosestLane(initialSpeed, maxSpeed, isLaneChange);
                }
                else
                {
                    npc.SetBehaviour<NPCManualBehaviour>();
                }

                api.SendResult(this);
            }
            else
            {
                api.SendError(this, $"Agent '{uid}' not found");
                Debug.Log($"Agent '{uid}' not found");
            }
        }
    }
}
