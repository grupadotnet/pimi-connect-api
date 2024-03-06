import React from "react";
import {Col, Row} from "react-bootstrap";

export function Header() {
    return (
        <>
            <Row className="px-5 pt-5">
                <Col sm="12">
                    <h1 className="font-weight-light">Test frontend for SignalR</h1>
                </Col>
            </Row>
        </>
    );
}