# Team Dyson - Unity Game
Welcome to our Unity game called EnergyCorp! This project is an engaging simulation where players can delve into the world of energy conservation within an office environment. Embark on an exciting journey where you'll explore ways to save energy, tackle challenges, and unravel the mysteries of sustainable office practices. Below, you'll discover details about the game's scenes, frontend, backend, and how to navigate through them.

## Instructions to play
Use arrow keys to move the player.To interact with doors, move through dialogues etc use the 'Z' key. To change clothing press 'Y'

## Leaderboad overview
Leaderboard can be accessed by clicking and icon at the bottom right on the game 

Players are ranked according to the stock value of the company.
To increase the stock value,
  Buy laptops and decorations
To avoid decreasing the stock value,
  Avoid energy overconsumption
The list of players are retrieved from the provided rest API. Then they are provided wiht a random stock value.

## Phase 04 Evaluation Material

1. Assets folder link
- https://drive.google.com/file/d/1oDlOQXxz5GSyHU6yXyKG9k3cCVPfQT2V/view?usp=sharing
2. Build zip file can be found in repository with file name Build_Phase_04.zip
3. Full Presentation can be viewed in https://dms.uom.lk/apps/files/?dir=/&openfile=45314243
  
## Phase 03 Evaluation Material

1. Assets folder link
- https://drive.google.com/file/d/1kZWN4htVZ5zeb0w9ygu-xxhJOcrAn4Ke/view?usp=sharing
2. Build zip file can be found in repository with file name Build_Phase_03.zip
3. Full Presentation can be viewed in https://drive.google.com/file/d/1y0F-taOfC-aEAsny0S8G6YIukofWAugQ/view?usp=sharing


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
   - Description: The game gives the player an option of 3 floors, the first floor (which is the first scene that is loaded), the server room and the meeting floor. Each floor has been implemented as a different scene which the player can switch between by using the map function at the bottom right of the screen. Note that in the case of overconsumption, the player will not be permitted to switch floors.

     ![image](https://github.com/DusaraG/Electrocorp-UnityWebgl/assets/66544479/9fc37b35-c054-4ec4-82f8-26a56b902499)

     ![image](https://github.com/DusaraG/Electrocorp-UnityWebgl/assets/66544479/7a027f12-8473-41f6-b7b5-59da1d742277)

   - Each floor has similar game functionalities with the difference being in the floor design. The functionalities are discussed below.

        a. Player Status Bars

     These bars denote the players their budget, energy usage, company performance(and current level), and stock value.
     
        - The energy usage is taken by the total of the energy used by the equipment of the floor and subtracting the energy saved through manager tasks (more details shared below).
        - The budget will increase by 1 point every 5 seconds while the energy is in a normal state and decrease by 1 point every 5 seconds if in the over consuming state. The budget and the maximum budget attainable will also increase during level ups.
        - The current level will increase through experience points gained by tasks such as completing manager tasks. As the player increases his level, more experience points would be needed to proceed to the next level.
        - The stock value of the company which will be used for ranking in the leaderboard will increase when buying laptops and decorations from the mart (representing how improving company resources lead to better reputation). However laptops and decorations increase energy consumption, increasing the chance for overconsumption. When in the overconsuming state, the stock price would reduce by 1 point every 5 seconds.
     
     ![image](https://github.com/DusaraG/Electrocorp-UnityWebgl/assets/66544479/adfd4165-3b12-4902-9ba9-bd0c78324bcc)
     
        b. Mart

     The mart can be opened by clicking on the shop icon at the bottom right corner of the screen. Once opened, the player can use their collected budget so far to purchase items from the mart. The items can be found under 4 sections, laptops, decorations, power systems, and clothes. On buying power systems, the maximum value of the energy bar will increase by the price of the item purchased times the quantity.
  
     ![image](https://github.com/DusaraG/Electrocorp-UnityWebgl/assets/66544479/84890807-fa45-4d16-b918-47799d784586)
  
     ![image](https://github.com/DusaraG/Electrocorp-UnityWebgl/assets/66544479/ff7edbd3-e930-4943-9111-502bf706e26a)
     
        c. Equipment usage
     
     In each floor, there are mainly 3 appliances, the doors, air conditioners and light bulbs that can be controlled using the Z key. The doors will open and close when the Z key is pressed and the ACs and bulbs can be switched on or turned off by the Z key. The AC and bulbs are the main source of energy consumption in the game. If too many of these are turned on, the cummulative energy consumption might result in an overconsumption, affecting the players budget and the company stock value.
  
     ![image](https://github.com/DusaraG/Electrocorp-UnityWebgl/assets/66544479/e798347e-9926-446a-9fe0-c14f0b34e63d)

     
        d. Non-player Characters
  
     Each floor has a number of Non-Player characters (NPC) that will become active as per the rate of increase in the consumption value obtained from the module API. i.e, if there is an increase in the rate of increase in consumption in the API, the number of employees in the room will increase (exact number shown in the top left hand corner of the game) and along with the activation of an NPC, the bulbs and ACs around the NPC will also activate, pushing the energy state towards an overconsumption.
  
     ![image](https://github.com/DusaraG/Electrocorp-UnityWebgl/assets/66544479/7ce52a17-12c6-4953-97b8-dfc9f118aa73)
     
        e. Managers
  
     Each floor has managers in them that will ask the player questions based on energy saving tasks or company information. If the player chooses the correct answer, there will be a decrease in energy consumption and an increase in the experience gained. These manager tasks are activated and deactivated at random.
  
     ![image](https://github.com/DusaraG/Electrocorp-UnityWebgl/assets/66544479/1707ee68-7436-4299-9399-7a237a004793)     
     
        f. Player clothing
  
     3 clothing options have been given for the player and the character will change his clothing on the press of the "y" key.
  
     ![image](https://github.com/DusaraG/Electrocorp-UnityWebgl/assets/66544479/dbcd4fda-d6a3-475a-a930-4644d532e14d) ![image](https://github.com/DusaraG/Electrocorp-UnityWebgl/assets/66544479/60160676-1087-49f6-851f-776ad7a7c11f) ![image](https://github.com/DusaraG/Electrocorp-UnityWebgl/assets/66544479/6282c766-cde2-4622-a892-a1f21fd7d405)
     
        g. Player Profile

     The player can view their profile by clicking on the VIEW PROFILE option on the top right corner of the screen. Using this option, the player can view and edit their saved profile details and any achievements they have from the game.

     ![image](https://github.com/DusaraG/Electrocorp-UnityWebgl/assets/66544479/27a09c31-9e97-4416-9a10-7947f139b754)

     ![image](https://github.com/DusaraG/Electrocorp-UnityWebgl/assets/66544479/b9b980b2-0977-4ea4-a77f-b3eef3fb57ec)

     ![image](https://github.com/DusaraG/Electrocorp-UnityWebgl/assets/66544479/aaa85ed6-8795-4109-bb21-f2c7778dea66)

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
Database and Springboot application, both are deployed in amazon web services RDS and EC2 respectively(at least until the evaluation)

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
  
## Data Management

Within the game, to facilitate data storage between scenes and player sessions, PlayerPrefs, Unity's built-in key-value storage, has been used. The data stored are,

   - Total Experience
   - Next Level Experience
   - Current Level
   - Number of Money
   - Maximum Money
   - Maximum Energy
   - Stock Value

The initialisation of these values can be found in the Quiz.cs script.

## Credits

- Game developed by 

    - Dusara
    - Nipun
    - Oshan
    - Selani
