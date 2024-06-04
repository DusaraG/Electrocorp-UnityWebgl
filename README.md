# Team Dyson - Unity Game
Note: If the endpoints are not working it may be due to the backend server not being run at the moment (servers are hosted locally and ngrok is used give access from the cloud). In that case please inform us, so that we can turn on the servers.  

Welcome to our Unity game called EnergyCorp! This project is an engaging simulation where players can delve into the world of energy conservation within an office environment. Embark on an exciting journey where you'll explore ways to save energy, tackle challenges, and unravel the mysteries of sustainable office practices. Below, you'll discover details about the game's scenes, frontend, backend, and how to navigate through them.

## Scene and Script Overview

The relevant scenes can be found under GameEnvironment/Scenes and the scripts can be found under GameEnvironment/Scripts.

1. **Main Menu V2**
   - Description: This scene serves as the entry point of the game. Players can press play to start the game. The script used in this scene is the MainMenu.cs. On pressing play, players are redirected to the next scene which is the Game Entry Scene. 

2. **Game Entry**
   - Description: In this scene, players can choose to either continue a previously started game or start a new game. The script for this scene is GameEntry.cs. If the player chooses to continue the game they will be redirected to the Ground Floor Scene. If they choose to start a new game, they will be redirected to the Quiz Scene.

3. **Quiz**
   - Description: In this scene the players will be prompted to play a quiz to boost their score. On clicking the button they will be redirected to the quiz website. After the quiz is completed, the continue option will become interactable and the player can move into the next scene which is the Results scene. The script for the Quiz scene is Quiz.cs.

4. **Results**
   - Description: The results scene will display the players quiz score, time taken to complete the quiz and the boost to the players energy meter based on their score and time taken. The script for this is the GetScoreTime.cs. The player can then continue to the player profile scene.

5. **Player Profile**
   - Description: In this scene, the player will input their details. Error handling has been implemented to check that the fields are all filled with the correct formats. The script for this is InputHandler.cs. After filling, the player can press submit and move into the ground floor.

6. **Game Floors**
   - Description: The game gives the player an option of 3 floors, the first floor (which is the first scene that is loaded), the server room and the meeting floor. Each floor has been implemented as a different scene which the player can switch between by using the map function at the bottom right of the screen. Note that in the case of overconsumption, the player will not be permitted to switch scenes.

     ![image](https://github.com/DusaraG/Electrocorp-UnityWebgl/assets/66544479/9fc37b35-c054-4ec4-82f8-26a56b902499)

     ![image](https://github.com/DusaraG/Electrocorp-UnityWebgl/assets/66544479/7a027f12-8473-41f6-b7b5-59da1d742277)

   - Each floor has similar game functionalities with the difference being in the floor design. The functionalities are discussed below.

        a. Player Status Bar
              This bar denotes the players current level, their budget, energy usage and company performance.
     
                 - The energy usage is the total of the energy used by the equipment of the floor and the energy saved through manager tasks (more details shared below).
     
                 - The budget will increase by 1 point every 5 seconds while the energy is in an underconsuming state and decrease by 1 point every 5 seconds if in the over consuming state. The budget and the maximum budget attainable will also increase during level ups.
     
                 - The current level will increase through experience points gained by tasks such as completing manager tasks. As the player increases his level, more experience points would be needed to proceed to the next level.
     
                 - The company performance is the stock price of the company which would increase by buying equipment from the mart. When in the overconsuming state, the stock price would reduce by 1 point every 30 seconds.
     
     ![image](https://github.com/DusaraG/Electrocorp-UnityWebgl/assets/66544479/adfd4165-3b12-4902-9ba9-bd0c78324bcc)
     
        b. Mart
              The mart can be opened by clicking on the shop icon at the bottom right corner of the screen. Once opened, the player can use their collected budget so far to purchase items from the 
     
        c. Equipment usage
        d. Non-player Characters
        e. Managers
        f. Player clothing
        g. Player Profile

## Frontend Overview

The Quiz has been deployed on Netlify and developed using Javascript, CSS, and HTML. Here's a detailed overview of the features and functionalities:

1. Sound Effects

    - Immersive sound effects are integrated throughout the game to captivate users and elevate their gaming experience.

2. Responsive Buttons

    - Buttons within the interface dynamically adapt to the user's state, enhancing usability and intuitiveness. For example:
    - The previous button is disabled when the user is on the first question.
    - The next button is disabled when the user is on the 10th question.
    - The submit button is enabled only when the user has responded to all the answers, with a visual indicator to instantly show if all responses are completed.

3. Interactive Questionnaire

    - The questionnaire interface allows users to:
    - Navigate forward and backward through questions.
    - Change their answers at any time during the quiz.
    - Receive real-time feedback on their progress.

4. Dynamic State Management

    - Our frontend implements a dynamically changing state-responsive questionnaire, ensuring a smooth and intuitive user experience as they progress through the game.

5. Results Display

    - Upon submission of the questionnaire, users are presented with their results in a clear and concise manner.

6. Feedback System

    - Users have the option to view detailed feedback by clicking the "Show Feedback" button. This feedback is seamlessly fetched from the backend, providing valuable insights and enhancing the learning experience.

7. Error Handling

    - Comprehensive error handling is implemented to gracefully manage failed API requests during submission and feedback retrieval. Exception handling mechanisms ensure uninterrupted gameplay even in the face of unexpected errors.

8. JWT Authorization

    - We prioritize security by utilizing JSON Web Tokens (JWT) for authorization, ensuring secure access to game features and resources.

## Backend Overview

The backend of our game is powered by Java Spring Boot. Here's an overview of key components:

1. Database Management: MySQL DBMS is utilized to store player information, including:

    - Player ID
    - Questionnaire attempt status
    - Answers provided by the player
    - Time taken to answer the questionnaire
    - Total score
    - JWT Authentication: A method is implemented to retrieve the JSON Web Token (JWT) from a mock API and store it for authentication purposes. When the web application requests the JWT, it is checked for validity and sent. If invalid, a new JWT is obtained from the mock API and sent.

2. Questionnaire Submission: A POST HTTP endpoint is established for players to submit their answers to the questionnaire. The total score is returned in the response upon submission.

3. Data Retrieval: Endpoints are provided to fetch player results and information at any time. This includes:

    - Player's answers
    - Correctness of answers
    - Correct answers
    - General and specific feedback

4. Additional Endpoints:

    - Endpoint to check if the player has attempted the questionnaire
    - Endpoint to return the score and time taken when the player ID is provided
    - Endpoint to reset player information

## Credits

- Game developed by 

    - Dusara
    - Nipun
    - Oshan
    - Selani
