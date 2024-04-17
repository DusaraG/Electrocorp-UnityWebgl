import React, { useState,useEffect } from 'react';

export const Register = (props) => {

  const [formData, setFormData] = useState({
   
    password: '',
    confirmpassword: '',
    firstname: '',
    lastname: '',
    username: '',
    nic: '',
    phoneNumber: '',
    email: '',
    profilePicUrl: '',
    gender: ''
  });


  const [isEmailValid, setIsEmailValid] = useState(false);
  const [isfNameValid, setIsfNameValid] = useState(false);
  const [islNameValid, setIslNameValid] = useState(false);
  const [isuserNameValid, setIsuserNameValid] = useState(false);
  const [isNicValid, setIsNicValid] = useState(false);
  const [isPhoneNumberValid, setIsPhoneNumberValid] = useState(false);
  const [Gender,setGender] = useState("")
  const [isFormValid, setIsFormValid] = useState(false);
  
  


 

  const validatePassword = (password) => {
    // Add your password validation logic here
    const passwordRegex =/^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,24}$/;
    return passwordRegex.test(password); // Minimum length of 8 characters
  };

  const validatefName = (firstname) => {
    // Add your name validation logic here
    const nameRegex = /^[A-z][A-z0-9-_]{3,23}$/;
    return nameRegex.test(firstname); // Minimum length of 3 characters
  };

  const validatelName = (lastname) => {
    // Add your name validation logic here
    const nameRegex = /^[A-z][A-z0-9-_]{3,23}$/;
    return nameRegex.test(lastname); // Minimum length of 3 characters
  };

  const validateNic = (nic) => {
    // Add your name validation logic here
    const nameRegex =/^([0-9]{9}[x|X|v|V]|[0-9]{12})$/;
    return nameRegex.test(nic); // Minimum length of 3 characters
  };

  const validateuserName = (username) => {
    // Add your name validation logic here
    const nameRegex = /^[A-z][A-z0-9-_]{3,23}$/;
    return nameRegex.test(username); // Minimum length of 3 characters
  };

  const validatePhoneNumber = (phoneNumber) => {
    // Add your name validation logic here
    const numberRegex = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/;
    return numberRegex.test(phoneNumber); // Minimum length of 3 characters
  };

  const validateEmail = (email) => {
    const emailRegex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    return emailRegex.test(email);
  };




  const handleInputfNameChange = (e) => {
    const { firstname, value } = e.target;
    if (validatefName(value)) {
      setIsfNameValid(true);
      console.log('First Name is valid');
    } else {
      console.log('First Name is invalid');

    }

    setFormData(() => ({
      [firstname]: value,
    }));
  };

  const handleInputlNameChange = (e) => {
    const { lastname, value } = e.target;
    if (validatelName(value)) {
      setIslNameValid(true);
      console.log('Last Name is valid');
    } else {
      console.log('Last Name is invalid');

    }

    setFormData(() => ({
      [lastname]: value,
    }));
  };

  const handleInputuserNameChange = (e) => {
    const { username, value } = e.target;
    if (validateuserName(value)) {
      setIsuserNameValid(true);
      console.log('User Name is valid');
    } else {
      console.log('User Name is invalid');

    }

    setFormData(() => ({
      [username]: value,
    }));
  };

  const handleInputNicChange = (e) => {
    const { nic, value } = e.target;
    if (validateNic(value)) {
      setIsNicValid(true);
      console.log('NIC is valid');
    } else {
      console.log('NIC is invalid');

    }

    setFormData(() => ({
      [nic]: value,
    }));
  }

  const handleInputphoneNumberChange = (e) => {
    const { phoneNumber, value } = e.target;
    if (validatePhoneNumber(value)) {
      setIsPhoneNumberValid(true);
      console.log('Phone Number is valid');
    } else {
      console.log('Phone Number is invalid');
    }

    setFormData(() => ({
      [phoneNumber]: value,
    }));
  }

  const handleInputEmailChange = (e) => {
    const { email, value } = e.target;
    if (validateEmail(value)) {
      setIsEmailValid(true);
      console.log('Email is valid');
    } else {
      console.log('Email is invalid');

    }
    setFormData(() => ({
      [email]: value,
    }));

    
  };



  const handleGenderSelection = (e) => {
    const {gender,value} = e.target;
    setFormData(() => ({ 
      [gender]: value,
    }));
    setGender(value);
    console.log(value);

  }

  useEffect(() => {
    console.log(".........................................")
    console.log('isFname: ',isfNameValid);
    console.log('islasr name: ',islNameValid);
    console.log('isusername ',isuserNameValid);
    console.log('isnic ',isNicValid);
    console.log('is4n ',isPhoneNumberValid);
    console.log("emailvalid: ",isEmailValid);
    console.log(".........................................")
    if ( isEmailValid && isfNameValid  && isNicValid && isPhoneNumberValid && isuserNameValid && islNameValid) {
      console.log('Form is valid...,You can submit the form now...');
      setIsFormValid(true);
    }

  },[isEmailValid,isfNameValid,isNicValid,isPhoneNumberValid,isuserNameValid,islNameValid] );

  const initialFormData = Object.freeze({
    password: '',
    confirmpassword: '',
    firstname: '',
    lastname: '',
    username: '',
    nic: '',
    phoneNumber: '',
    email: '',
    profilePicUrl: '',
  });

  const handleSubmit = (e) => {
    console.log('Pressed submit button...');
    e.preventDefault();
    if (isFormValid) {
      console.log(formData);
      alert('Form submitted successfully');
    }
    else{
      console.log('Sorry cannot submit the form...Re check the data you have given is accurate');
    }

  };

  return (
    <div className="auth-form-container">
      <h2 className="Reg-title">Register</h2>
      <form className="register-form" onSubmit={handleSubmit}>
        <label htmlFor="fname">First Name</label>
        <input
          value={formData.firstname}
          type="text"
          placeholder="Ex: John"
          id="name"
          name="name"
          onChange={handleInputfNameChange}
        />
        <label htmlFor="lname">Last Name</label>
        <input
          value={formData.lastname}
          type="text"
          placeholder="Ex: Wick"
          id="name"
          name="name"
          onChange={handleInputlNameChange}
        />

        <label htmlFor="username">User Name</label>
        <input
          value={formData.username}
          type="text"
          placeholder="Ex: John"
          id="name"
          name="name"
          onChange={handleInputuserNameChange}
        />

        <label htmlFor="nic">NIC</label>
        <input
          value={formData.nic}
          type="text"
          placeholder="Ex: XXXXXXXXXXV"
          id="nic"
          name="nic"
          onChange={handleInputNicChange}
        />
        <label htmlFor="phone-number">Phone Number</label>
        <input
          value={formData.nic}
          type="text"
          placeholder="Ex: 07X-XXXXXXX"
          id="number"
          name="number"
          onChange={handleInputphoneNumberChange}
        />




        <label htmlFor="email">Email</label>
        <input
          value={formData.email}
          type="email"
          placeholder="Ex: johnwick@gmail.com"
          id="email"
          name="email"
          onChange={handleInputEmailChange}
        />

        <div claaName='gender-dropdown'> Gender (Optional)
          <label htmlFor="gender" >
            <select className='gender-selection' onChange={handleGenderSelection}>
              <option className='drop' value=''>Select</option>
              <option className='drop' value='Male'>Male </option>
              <option className='drop' value='Female'>Female</option>
              <option className='drop' value='Other'>Other</option>
            </select>
          </label>
        </div>

        <button className="submit-btn" type="submit" disabled={!isFormValid} >Register</button>
      </form>
    </div>
  );
};