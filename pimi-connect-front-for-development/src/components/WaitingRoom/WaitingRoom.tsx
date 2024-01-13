import React, {useState} from "react";
import {Button, Col, Container, Form, Row} from "react-bootstrap";
import {Header} from "../Header/Header";
import {WaitingRoomForm} from "./WaitingRoomForm";

export function WaitingRoom() {
    const [username, setUsername] = useState<string>();
    const [chatId, setChatId] = useState<string>();

    const onSubmit = (e: any) => {
        e.preventDefault();
        //joinChat(username, chatId);
    };

    return (
        <Form onSubmit={onSubmit}>
            <Row className="px-5 py-5">
                <Col sm={12}>
                    <WaitingRoomForm
                        setUsername={setUsername}
                        setChatId={setChatId}
                    />
                </Col>
                <Col sm={12}>
                    <hr/>
                    <Button variant="success" type="submit">Join</Button>
                </Col>
            </Row>
        </Form>
    );
}