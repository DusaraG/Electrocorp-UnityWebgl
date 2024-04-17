package com.example.EnergycorpBackend.dto;

import lombok.Getter;
import lombok.Setter;

@Setter
@Getter
public class Feedback {
    Integer questionNo;
    Integer givenAnswer;
    Integer correctAnswer;
    boolean correct;
    String generalFeedback;
    String specificFeedback;
}
