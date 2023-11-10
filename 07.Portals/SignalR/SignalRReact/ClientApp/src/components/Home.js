import React, { useState } from "react";
import { setupSignalRConnection } from "../signalR/signalR";
import { HubConnectionState } from "@microsoft/signalr";

export const Home = () => {
    const [message, setMessage] = useState("");
    const signalRConnection = setupSignalRConnection();
    const standardBid = {
        userID: 1,
        lotID: 47,
        maxBid: 100.0,
        bidIncrement: 5
    };

    signalRConnection.on("ReceiveMaxAutoBid", (currentBid, errorMessage) => {
        console.log(errorMessage);
        setMessage(currentBid);
    });

    const onClickSendMaxAutoBid = () => {
        if (signalRConnection.state === HubConnectionState.Connected) {
            signalRConnection.invoke("SendMaxAutoBidAsync", standardBid);
        }
    };

    signalRConnection.on("ReceiveQuickBid", (currentBid, errorMessage) => {
        console.log(errorMessage);
        setMessage(currentBid);
    });

    const onClickSendQuickBid = () => {
        if (signalRConnection.state === HubConnectionState.Connected) {
            signalRConnection.invoke("SendQuickBidAsync", standardBid);
        }
    };

  return (
    <div>
      <div className="row">
        <div className="col-md-4">
          <button
            className="btn btn-primary"
            onClick={(e) => onClickSendQuickBid()}>
            Quick Bid
          </button>
        </div>
      </div>
      <div className="row">
        <div className="col-md-4">
          <button
            className="btn btn-primary"
            onClick={(e) => onClickSendMaxAutoBid()}>
            Max Bid
          </button>
        </div>
        <div className="col-md-4">
          <span>Current Bid: ${message}</span>
        </div>
      </div>
    </div>
  );
};
