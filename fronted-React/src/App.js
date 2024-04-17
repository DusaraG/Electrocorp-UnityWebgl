import React, { useState, useEffect, useRef } from "react";
import axios from "axios";

//importing all css files
import "./App.css";
import "./final-results.css";
import "./question-card.css";
import "./components/progressbar.css";
import "./components/timer.css";


//importing all components and decorations
import ProgressBar from "./components/progressbar";
import Timer from "./components/timer";
import buttonClickSound from "./decorations/music_effects/button-124476.mp3"; // Import  button click sound
import submitButtonClickSound from "./decorations/music_effects/cute-level-up-3-189853.mp3"; // Import  submit button sound
import answerclick from "./decorations/music_effects/tap-notification-180637.mp3"; //Import answer click sound



//class for sound handling
class SoundManager {
  constructor() {
    this.sounds = {}; // Store loaded sounds
  }

  loadSound(name, source) {  //load the sounds
    const audio = new Audio(source);
    this.sounds[name] = audio;
  }

  playSound(name) {     //playing the sounds
    const sound = this.sounds[name];
    if (sound) {
      sound.currentTime = 0;
      sound.play();
    }
  }
}

function App() {
  //initiate the states 
  const [isQuizzStarted, setIsQuizzStarted] = useState(false);
  const [showFinalResults, setShowFinalResults] = useState(false);
  const [score, setScore] = useState(0);
  const [currentQuestion, setCurrentQuestion] = useState(0);
  const [Previous, setPrevious] = useState(true);
  const [Next, setNext] = useState(true);
  const [timer, setTimer] = useState(0);
  const [canSubmit, setcanSubmit] = useState(false);
  const [answerTracker, setAnswerTracker] = useState([]);
  const [Feedback, setFeedback] = useState(false);
  const [AllAnswered, setAllAnswered] = useState(
    "You haven't answered all questions yet"
  );
  const [feedbackStore, setfeedbackStore] = useState([]);
  const feedbackContainerRef = useRef(null);
  const soundManager = useRef(new SoundManager()); // Create a reference for the SoundManager

  const questions = [
    {
      question:
        "What is the primary source of energy for most power grids around the world?",
      answers: [
        { id: 0, text: "Solar power" },
        { id: 1, text: "Wind power" },
        { id: 2, text: "Fossil fuels" },
        { id: 3, text: "Hydropower" },
      ],
    },
    {
      question:
        "How does electricity typically travel from power plants to consumers?",
      answers: [
        { id: 0, text: "Through water pipes" },
        { id: 1, text: "Via transmission and distribution networks" },
        { id: 2, text: "Directly from generators to homes" },
        { id: 3, text: "Through the internet" },
      ],
    },
    {
      question: "Why is energy efficiency important in homes and businesses?",
      answers: [
        { id: 0, text: "It increases energy consumption" },
        { id: 1, text: "It leads to higher energy costs" },
        { id: 2, text: "It reduces energy bills and environmental impact" },
        { id: 3, text: "It has no impact on the environment" },
      ],
    },
    {
      question:
        "What is the primary goal of demand management in energy usage?",
      answers: [
        { id: 0, text: "To increase the overall energy consumption" },
        { id: 1, text: "To balance energy supply and demand" },
        { id: 2, text: "To eliminate the use of renewable energy sources" },
        { id: 3, text: "To double the energy costs for consumers" },
      ],
    },
    {
      question:
        "Which of the following is a common method used in demand management to encourage lower energy use during peak hours?",
      answers: [
        { id: 0, text: "Increasing energy prices during off-peak hours" },
        { id: 1, text: "Providing incentives for high energy consumption" },
        {
          id: 2,
          text: "Offering lower rates or incentives for using less energy during peak times",
        },
        { id: 3, text: "Discouraging the use of energy-efficient appliances " },
      ],
    },
    {
      question: "Benefits to the consumer from demand management include:",
      answers: [
        { id: 0, text: "Higher energy bills" },
        { id: 1, text: "Less control over their energy use" },
        { id: 2, text: "Savings on their electricity bill" },
        { id: 3, text: "Reduced internet connectivity" },
      ],
    },
    {
      question:
        "How does implementing demand management strategies benefit the environment?",
      answers: [
        { id: 0, text: "By significantly increasing carbon emissions" },
        {
          id: 1,
          text: "By reducing reliance on fossil fuels and lowering carbon emissions",
        },
        { id: 2, text: "By eliminating the need for public transportation" },
        { id: 3, text: "By discouraging the use of renewable energy" },
      ],
    },
    {
      question:
        "What can be a direct benefit of participating in a demand management program for consumers?",
      answers: [
        { id: 0, text: "Higher energy bills" },
        { id: 1, text: "Less control over their energy use" },
        { id: 2, text: "Savings on their electricity bill" },
        { id: 3, text: "Reduced internet connectivity" },
      ],
    },
    {
      question: "Why is load shifting important in demand management?",
      answers: [
        { id: 0, text: "It increases the energy load during peak times" },
        {
          id: 1,
          text: "It shifts energy usage to times when demand is higher",
        },
        {
          id: 2,
          text: "It helps balance the power grid by using energy during lower-demand periods",
        },
        { id: 3, text: "It makes energy more expensive during off-peak hours" },
      ],
    },
    {
      question:
        "Which of the following electric loads can be effectively managed as part of a demand management program?",
      answers: [
        { id: 0, text: "Fixed lighting systems in public areas" },
        { id: 1, text: "Emergency medical equipment" },
        { id: 2, text: "Residential air conditioning units" },
        { id: 3, text: "Data centers that require constant cooling" },
      ],
    },
  ];


  useEffect(() => {
    soundManager.current.loadSound("buttonClick", buttonClickSound);
    soundManager.current.loadSound("answerclick", answerclick);
    soundManager.current.loadSound("submitButtonClick", submitButtonClickSound);
  }, []);

  //if answer tracker length is satisfied submit-button get enabled
  useEffect(() => {
    if (answerTracker.length === questions.length) {
      setcanSubmit(true);
      setAllAnswered("Congratulations !!! You have answered all questions.");
    }
  }, [answerTracker, questions.length]);

  //time is running  until quizz is finished
  useEffect(() => {
    let interval;
    if (!showFinalResults && isQuizzStarted) {
      interval = setInterval(() => {
        setTimer((prevTimer) => prevTimer + 1);
      }, 1000);
    }
    return () => clearInterval(interval);
  }, [showFinalResults, isQuizzStarted]);

  //privious-btn enable-disable handle
  useEffect(() => {
    if (currentQuestion !== 0) {
      setPrevious(false);
    } else {
      setPrevious(true);
    }
  }, [currentQuestion]);


  //next-btn enable-disable handle
  useEffect(() => {
    if (currentQuestion === questions.length - 1) {
      setNext(false);
    } else {
      setNext(true);
    }
  }, [currentQuestion, questions.length]);


  //quiz start when clicked
  const startQuiz = async () => {
    soundManager.current.playSound("submitButtonClick");
    setIsQuizzStarted(true);
  };


  //extract the token from the url
  const extractTokenFromCurrentURL = () => {
    const urlSearchParams = new URLSearchParams(window.location.search);
    const token = urlSearchParams.get("token");
    return token ? decodeURIComponent(token) : null;
  };
  const jwtToken = extractTokenFromCurrentURL();
  console.log("jwtToken :", jwtToken);

  //when feedback button is pressed, auto scrolled to the feedback section
  useEffect(() => {
    if (Feedback && feedbackContainerRef.current) {
      feedbackContainerRef.current.scrollIntoView({ behavior: "smooth" });
    }
  }, [Feedback]);

  //if answer is clicked ,AnswerTracker updates.sound is handled
  const answerClicked = (questionNumber, answerNumber) => {
    soundManager.current.playSound("answerclick");
    const newAnswer = { questionNumber, answerNumber };
    // Efficiently check for existing answer using a Set
    const existingAnswerIndex = answerTracker.findIndex(
      (answer) => answer.questionNumber === newAnswer.questionNumber
    );

    if (existingAnswerIndex !== -1) {
      // Update the existing answer in answerTracker
      answerTracker[existingAnswerIndex] = newAnswer;
    } else {
      // Add the new answer to answerTracker
      answerTracker.push(newAnswer);
    }
    setAnswerTracker([...answerTracker]); // Update answerTracker state
  };

  //previous-btn functionality is applied
  const previousClicked = () => {
    soundManager.current.playSound("buttonClick");
    if (currentQuestion !== 0) {
      setCurrentQuestion(currentQuestion - 1);
    }
  };

  //next-btn functionality is applied
  const nextClicked = () => {
    soundManager.current.playSound("buttonClick");
    if (currentQuestion < questions.length - 1) {
      setCurrentQuestion(currentQuestion + 1);
    }
  };

  //submmit-btn functionality is applied (send selected answers and get the no. of answeres correct)
  const submitClicked = async () => {
    // Count the number of correct answers
    soundManager.current.playSound("submitButtonClick");
    // Prepare JSON data
    const data = {
      playerNo: 1,
      q01: answerTracker[0].answerNumber + 1,
      q02: answerTracker[1].answerNumber + 1,
      q03: answerTracker[2].answerNumber + 1,
      q04: answerTracker[3].answerNumber + 1,
      q05: answerTracker[4].answerNumber + 1,
      q06: answerTracker[5].answerNumber + 1,
      q07: answerTracker[6].answerNumber + 1,
      q08: answerTracker[7].answerNumber + 1,
      q09: answerTracker[8].answerNumber + 1,
      q10: answerTracker[9].answerNumber + 1,
      time: timer,
    };

    // Send data to backend using Fetch API
    try {
      const response = await axios.post(
        "https://firefly-fluent-personally.ngrok-free.app/insert_results",
        data,
        {
          headers: {
            "Content-Type": "application/json", // Specify JSON content
            Authorization: `Bearer ${jwtToken}`, // Include JWT token in the Authorization header
          },
          // body: JSON.stringify(data), // Convert data to JSON string
        }
      );

      if (response.status >= 200 && response.status < 300) {
        console.log("Answers and time sent successfully!");
        // Get the calculated score from the backend
        const parsedData = response.data;
        setScore(parsedData);
        setShowFinalResults(true);
      } else {
        throw new Error(`Failed to submit answers: ${response.statusText}`);
      }
    } catch (error) {
      console.error("Error sending score:", error);
      const shouldRetry = window.confirm(
        "Failed to submit answers. Do you want to retry?"
      );
      if (shouldRetry) {
        submitClicked(jwtToken);
      } else {
        console.log("User chose not to resubmit.");
        restartGame();
      }
    }
  };



  // feedback-btn functionalit is applied here.(fetching the feedback from backend and save)
  const feedbackClicked = async () => {
    // Count the number of correct answers
    soundManager.current.playSound("buttonClick");

    // Prepare JSON data
    const data = 1;

    // Send data to backend using Fetch API
    try {
      const response = await axios.post(
        "https://firefly-fluent-personally.ngrok-free.app/get_results",
        { data },
        {
          headers: {
            "Content-Type": "application/json", // Specify JSON content
            Authorization: `Bearer ${jwtToken}`, // Include JWT token in the Authorization header
          },
        }
      );

      if (response.status >= 200 && response.status < 300) {
        const responseData = response.data;
        setfeedbackStore(responseData);
        setFeedback(true);

        if (feedbackContainerRef.current) {
          feedbackContainerRef.current.scrollIntoView({ behavior: "smooth" });
        }
      } else {
        throw new Error(`Failed to fetch feedbacks: ${response.statusText}`);
      }
    } catch (error) {
      console.error("Error fetching feedbacks:", error.message);
      const shouldRetryfeedbacks = window.confirm(
        "Failed to fetch feedbacks. Do you want to retry??"
      );
      if (shouldRetryfeedbacks) {
        feedbackClicked(jwtToken);
      }
    }
  };

  //restart game function
  const restartGame = () => {
    setIsQuizzStarted(false);
    setShowFinalResults(false);
    setScore(0);
    setCurrentQuestion(0);
    setPrevious(true);
    setNext(true);
    setTimer(0);
    setcanSubmit(false);
    setAnswerTracker([]);
    setFeedback(false);
    setAllAnswered("You haven't answered all questions yet");
    setfeedbackStore([]);
  };


  //selection of correct answer 
  const answer_num = (q_num, ans_num) => {
    if (
      answerTracker.some(
        (answerItem) =>
          answerItem.questionNumber === q_num &&
          answerItem.answerNumber === ans_num
      )
    ) {
      return true;
    }
    return false;
  };



  //TODO : has to implement this fucntion to direct back to the game
  const BackToGame = () => {
    soundManager.current.playSound("buttonClick");
    window.location.href = "https://google.com";
  };

  return (
    <div className="App">
      {isQuizzStarted ? (
        showFinalResults ? (
          <div>
            <div className="final-results">
              <h1>Final Results</h1>
              <h4>Time you have spent on the quiz: {timer} s</h4>
              <h2>
                Score: {score} out of {questions.length}
              </h2>
              <button className="feedback-btn" onClick={feedbackClicked}>
                Show Feedbacks
              </button>
              <p className="congrats">Congratulations!!!</p>
              <p className="msg">
                You Can now Play{" "}
                <span style={{ fontWeight: "bold" }}>Energy Corp</span>
              </p>

              <button className="quiz-end-btn" onClick={BackToGame}>
                Start Envergy Saving...
              </button>
            </div>
            <div className="progress-bar">
              <ProgressBar
                percentage={(score / questions.length) * 100}
                circleWidth="200"
              />
            </div>
            {Feedback && (
              <div className="feedback-container" ref={feedbackContainerRef}>
                <h1>Feedback</h1>
                {Object.keys(feedbackStore).map((index) => (
                  <div
                    className={`feedbacks ${feedbackStore[index].correct ? "correct" : "incorrect"
                      }`}
                    key={index}
                  >
                    <p className="q">Question {index} </p>
                    <p className="question">{questions[index - 1].question}</p>
                    {questions[index - 1].answers.map((answer) => (
                      <div className="feedbackAnswers" key={answer.id}>
                        <p
                          className={
                            answer.id + 1 === feedbackStore[index].correctAnswer
                              ? "special-answer"
                              : ""
                          }
                        >
                          {answer.id + 1}.{answer.text}
                          {answer_num(index - 1, answer.id) && (
                            <span className="selected-answer">
                              (Your choise)
                            </span>
                          )}
                        </p>
                      </div>
                    ))}
                    <p className="specific-fbk">
                      <span className="general-specific">
                        Specific Feedback:
                      </span>{" "}
                      {feedbackStore[index]?.specificFeedback}
                    </p>
                    <p className="general-fbk">
                      <span className="general-specific">
                        General Feedback:
                      </span>{" "}
                      {feedbackStore[index]?.generalFeedback}
                    </p>
                  </div>
                ))}
              </div>
            )}
          </div>
        ) : (
          <div>
            <div className="header">
              <h1>Quiz</h1>
              <h1>
                Question <span className="current-Ques">{currentQuestion + 1}</span > out of <span className="No_all_Ques">{questions.length}</span>
              </h1>
              <h3 className="answered-state">{AllAnswered}</h3>
            </div>
            <div className="question-card">
              <h3 className="question-test">
                {questions[currentQuestion].question}
              </h3>
              <ul>
                {questions[currentQuestion].answers.map((answer) => {
                  const isAnswered = answerTracker.some(
                    (answerItem) =>
                      answerItem.questionNumber === currentQuestion &&
                      answerItem.answerNumber === answer.id
                  );
                  return (
                    <li
                      className={isAnswered ? "answered" : ""}
                      onClick={() => answerClicked(currentQuestion, answer.id)}
                      key={answer.id}
                    >
                      {answer.text}
                      {isAnswered && (
                        <span className="selected-answer">(Selected)</span>
                      )}
                    </li>
                  );
                })}
              </ul>
              <button
                className="previous-btn"
                onClick={previousClicked}
                disabled={Previous}
              >
                {"<<<"}Previous
              </button>

              <button
                className="next-btn"
                onClick={nextClicked}
                disabled={!Next}
              >
                Next {">>>"}
              </button>

              <button
                className="submit-btn"
                onClick={submitClicked}
                disabled={!canSubmit}
              >
                SUBMIT
              </button>
              <Timer />
            </div>
          </div>
        )
      ) : (
        <div>
          <div className="first-card">
            <h1>Welcome to the Quiz</h1>
            <h2>Are you ready to test your knowledge on Energy saving?</h2>

            {/*}  <input type="text" placeholder="Username" className="User" onChange={(event)=>handleUserChange(event)} ></input>
            <input type="text" placeholder="Token" className="Token" onChange={(event)=>handleInputChange(event)} ></input> */}
            <button className="start-quiz-btn" onClick={startQuiz}>
              Start Quiz
            </button>
          </div>
        </div>
      )}
    </div>
  );
}
export default App;
