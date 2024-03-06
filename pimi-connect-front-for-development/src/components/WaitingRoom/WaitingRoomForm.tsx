import React from "react";
import {Form} from "react-bootstrap";

interface WaitingRoomFormProps {
    setUsername: (v: any) => void,
    setChatroom: (v: any) => void
}
export function WaitingRoomForm({setUsername, setChatroom}: WaitingRoomFormProps) {
    return (
        <Form.Group>
            <Form.Control
                className={"mb-3"}
                placeholder="Username"
                onChange={(e: any) => setUsername(e.target.value)}
            />
            <Form.Control
                className={"mb-3"}
                placeholder="Chatroom"
                onChange={(e: any) => setChatroom(e.target.value)}
            />
        </Form.Group>
    );
}