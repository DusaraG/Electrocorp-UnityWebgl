import React,{useState} from 'react';
import './App.css';
import './BackgroundVideo.js';
import './Music.js';
// import {Login} from "./Login.jsx";
import {Register} from "./Register.jsx";
import BackgroundVideo from './BackgroundVideo.js';
import Music from './Music.js';

function App() {
  const [currentForm,setCurrentForm] = useState("login");
  // const toggleForm = (formName) => {
  //   setCurrentForm(formName);
  // }
  return (
    <div className="App">
      <BackgroundVideo />
      <Music/>
      <Register/>

      {/* {
        currentForm === "login" ? <Login onFormSwitch={toggleForm}/> : <Register onFormSwitch={toggleForm}  />
      } */}
    </div>
  );
}

export default App;
