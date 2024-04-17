package com.example.EnergycorpBackend.service;
import com.example.EnergycorpBackend.dto.Feedback;
import com.example.EnergycorpBackend.globalVariables.QuestionnaireFeedbacks;
import org.json.JSONObject;

import java.util.List;

public class Json {
    public static JSONObject getResultJson(List<Feedback> y, int score) {
        JSONObject jsonObject = new JSONObject();
        //jsonObject.put("score", score);
        Integer[] correctAns = {3,2,3,2,3,3,2,3,3,3};
        for (int i = 0; i < y.size(); i++) {
            JSONObject feedbackJson = new JSONObject();
            feedbackJson.put("givenAnswer", y.get(i).getGivenAnswer());
            feedbackJson.put("correctAnswer",correctAns[i]);
            feedbackJson.put("correct", y.get(i).isCorrect());
            feedbackJson.put("generalFeedback", y.get(i).getGeneralFeedback());
            feedbackJson.put("specificFeedback",y.get(i).getSpecificFeedback());
            jsonObject.put(y.get(i).getQuestionNo().toString(), feedbackJson);
        }
        System.out.println(jsonObject);
        return jsonObject;
    }
}
