import {useState} from "react";
import {HubConnection, HubConnectionBuilder, LogLevel} from "@microsoft/signalr";


export function useJoinChatRoom() {
    const [conn, setConnection] = useState<HubConnection>();

    const joinChatroom = async (username: any, chatroom: any) => {
        try {
            const conn = new HubConnectionBuilder()
                .withUrl("https://localhost:7241/chat")
                .configureLogging(LogLevel.Information)
                .build();

            conn.on("ReceiveMessage", (username: any, msg: any) => {
                console.log("msg: " + msg);
            });

            await conn.start();
            await conn.invoke("JoinSpecificChatroom", {username, chatroom});

            setConnection(conn)
        }
        catch (e) {
            console.log(e)
        }
    };

    return {joinChatroom, conn};
}

