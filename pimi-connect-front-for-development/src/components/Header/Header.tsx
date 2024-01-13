import React from "react";
import {Col, Row} from "react-bootstrap";

export function Header() {
    return (
        <>
            <Row class="px-5 my-5">
                <Col sm="12">
                    <h1 className="font-weight-light">Test frontend for SignalR</h1>
                </Col>
            </Row>
        </>
    );
}