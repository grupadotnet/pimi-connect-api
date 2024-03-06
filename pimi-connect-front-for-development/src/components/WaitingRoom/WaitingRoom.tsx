import React, {useState} from "react";
import {Button, Col, Form, Row} from "react-bootstrap";
import {WaitingRoomForm} from "./WaitingRoomForm";
import {useJoinChatRoom} from "../../hooks/useJoinChatRoom";


export function WaitingRoom() {
    const [username, setUsername] = useState<string>();
    const [chatroom, setChatroom] = useState<string>();
    const {joinChatroom} = useJoinChatRoom();

    const onSubmit = (e: any) => {
        e.preventDefault();
        joinChatroom(username, chatroom);
    };

    return (
        <Form onSubmit={onSubmit}>
            <Row className="px-5 py-5">
                <Col sm={12}>
                    <WaitingRoomForm
                        setUsername={setUsername}
                        setChatroom={setChatroom}
                    />
                </Col>
                <Col sm={12}>
                    <Button variant="success" type="submit">Join</Button>
                </Col>
            </Row>
        </Form>
    );
}