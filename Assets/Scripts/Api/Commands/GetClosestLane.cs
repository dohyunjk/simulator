/**
 * Copyright (c) 2023 Sanggu Han
 *
 * This software contains code licensed as described in LICENSE.
 *
 */

using SimpleJSON;
using Simulator.Map;

namespace Simulator.Api.Commands
{
    class GetClosestLane : ICommand
    {
        public string Name => "simulator/get_closest_lane";

        public void Execute(JSONNode args)
        {
            var api = ApiManager.Instance;

            var manager = SimulatorManager.Instance.MapManager;

            var position = args["position"].ReadVector3();
            MapTrafficLane closestLane = manager.GetClosestLaneOptimized(position);

            var result = new JSONString(closestLane.id);
            api.SendResult(this, result);
        }
    }
}
