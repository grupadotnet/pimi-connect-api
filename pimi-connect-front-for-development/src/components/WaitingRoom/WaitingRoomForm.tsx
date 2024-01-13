import React from "react";
import {Form} from "react-bootstrap";

interface WaitingRoomFormProps {
    setUsername: (v: any) => void,
    setChatId: (v: any) => void
}
export function WaitingRoomForm({setUsername, setChatId}: WaitingRoomFormProps) {
    return (
        <Form.Group>
            <Form.Control
                placeholder="Username"
                onChange={(e: any) => setUsername(e.target.value)}
            />
            <Form.Control
                placeholder="ChatId"
                onChange={(e: any) => setChatId(e.target.value)}
            />
        </Form.Group>
    );
}