package com.example.EnergycorpBackend.controller;
import com.example.EnergycorpBackend.dto.JwtToken;
import com.example.EnergycorpBackend.dto.Results;
import com.example.EnergycorpBackend.dto.ScoreTime;
import com.example.EnergycorpBackend.service.*;
import jakarta.servlet.http.HttpServletRequest;
import lombok.extern.log4j.Log4j2;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@Log4j2
@CrossOrigin(origins = "*")
public class QuestionnaireController {
    @Autowired
    PlayerService playerService;

    @GetMapping("get_jwt")
    JwtToken newJwt(){
        return playerService.getJwt();
    }

    @PostMapping("questionnaire_attempted")
    boolean hasAttempted(@RequestBody Integer playerNo){
        //playerService.testConnection();
        return playerService.hasAttempted(playerNo);
    }

    @PostMapping("reset_player")
    void reset(@RequestBody Integer id){
        playerService.resetPlayer(id);
    }


    @PostMapping("insert_results")
    ResponseEntity<Integer> results(@RequestBody Results results,HttpServletRequest request){
        String authHeader = request.getHeader("Authorization");
        //the request authenticated by checking the Authorization header for the most recent JWT token
        if (authHeader.equals("Bearer "+playerService.getJwt().getToken())) {
            return new ResponseEntity<>(playerService.insert_results(results), HttpStatus.OK);
        }else
            return new ResponseEntity<>(401, HttpStatus.UNAUTHORIZED);
    }


    @PostMapping("get_results")
     ResponseEntity<String> getResults(@RequestBody String playerID, HttpServletRequest request){
        String authHeader = request.getHeader("Authorization");

        if (authHeader.equals("Bearer "+playerService.getJwt().getToken())) {
            int player = Integer.parseInt( playerID.replaceAll("[^0-9]", ""));
            return new ResponseEntity<>(playerService.getResults(player).toString(), HttpStatus.OK);
        }else
            return new ResponseEntity<>("Authorization header is missing", HttpStatus.UNAUTHORIZED);

    }

    @PostMapping("unity/get_score")
        ScoreTime getScore(@RequestBody Integer playerID){
        return playerService.getScoreTime(playerID);
    }
}
