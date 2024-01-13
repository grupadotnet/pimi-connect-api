import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css'
import './App.css';
import {WaitingRoom} from "./components/WaitingRoom/WaitingRoom";
import {Col, Container, Row} from "react-bootstrap";
import {Header} from "./components/Header/Header";

function App() {
  return (
      <div>
        <main>
            <Container>
                <Header/>
                <WaitingRoom/>
            </Container>
        </main>
      </div>
  );
}

export default App;
