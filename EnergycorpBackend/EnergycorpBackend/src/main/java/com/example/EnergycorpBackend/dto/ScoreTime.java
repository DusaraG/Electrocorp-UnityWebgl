package com.example.EnergycorpBackend.dto;

public class ScoreTime {
    private final int score;
    private final int time;

    public ScoreTime(int score, int time) {
        this.score = score;
        this.time = time;
    }

    public int getScore() {
        return score;
    }

    public int getTime() {
        return time;
    }
}
