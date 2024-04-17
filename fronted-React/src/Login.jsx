import React,{useState} from 'react';

export const Login = (props) => {
    const {email,setEmail} = useState("");
    const {pass,setPass} = useState("");

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log(email);
    }
    return (
        <div className="auth-form-container">
        <h2 className="logReg-title">Login</h2>
        <form className="login-form" onSubmit={handleSubmit}>
            <lable htmlFor ="email">Email</lable>
            <input value={email} type="email" placeholder="xxxx.gmail.com" id="email" name="email"/>
            <lable htmlFor ="password">Password</lable>
            <input value={pass} type="password" placeholder="******" id="password" name="password"/>
            <button className="submit-btn" type="submit">Log In</button>
        </form>
        <button className="log-to-reg-btn" onClick={()=> props.onFormSwitch('register')} >Don't have an account? Register here.</button>
        </div>
        
    )
}