/**
 * Copyright (c) 2019 LG Electronics, Inc.
 *
 * This software contains code licensed as described in LICENSE.
 *
 */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SimpleJSON;
using Simulator.Map;

namespace Simulator.Api.Commands
{
    class ConvertApolloPositionToWorldPosition : ICommand
    {
        public string Name => "simulator/convert_apollo_point";

        public void Execute(JSONNode args)
        {
            var position = args["position"].ReadVector2();

            MapOrigin mapOrigin = MapOrigin.Find();
            Debug.Assert(SimulatorManager.Instance.MapManager != null);
            // Get any point on any lane to get the y value
            Vector3 anyPosOnUnity = SimulatorManager.Instance.MapManager.GetLane(0).mapWorldPositions[0];

            Vector3 unityPos = new Vector3(
                    (double)-position.y + mapOrigin.OriginNorthing,
                    anyPosOnUnity.y,
                    (double)position.x - mapOrigin.OriginEasting
            );

            var result = new JSONObject();
            result.Add("position", unityPos);

            var api = ApiManager.Instance;
            api.SendResult(this, result);
        }
    }
}
