﻿using ProjectTrinity.Networking;
using ProjectTrinity.Networking.Messages;
using ProjectTrinity.Root;
using UnityEngine;

namespace ProjectTrinity.MatchStateMachine
{
    public class TimeSyncMatchState : IMatchState
    {
        public void Initialize(MatchStateMachine matchStateMachine)
        {
            DIContainer.NetworkTimeService.Synch(DIContainer.UDPClient, () =>
            {
                DIContainer.AckedMessageHelper.SendAckedMessage(new TimeSyncDoneMessage(), MessageId.TIME_SYNC_DONE_ACK, ackMessage =>
                {
                    Debug.Log("Time synch done, switching to WaitForStartMatchState");
                    TimeSyncDoneAckMessage receivedMessage = new TimeSyncDoneAckMessage(ackMessage);
                    matchStateMachine.LocalPlayerId = receivedMessage.PlayerId;
                    matchStateMachine.ChangeMatchState(new WaitForStartMatchState());
                });
            });
        }

        public void OnFixedUpdateTick()
        {

        }
    }
}