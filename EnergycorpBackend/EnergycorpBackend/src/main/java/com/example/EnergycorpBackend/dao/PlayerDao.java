package com.example.EnergycorpBackend.dao;

import jakarta.persistence.Entity;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Entity
@Getter
@Setter
@NoArgsConstructor
public class PlayerDao extends PlayerAttempt{

    private Integer Q01;
    private boolean A1;
    private Integer Q02;
    private boolean A2;
    private Integer Q03;
    private boolean A3;
    private Integer Q04;
    private boolean A4;
    private Integer Q05;
    private boolean A5;
    private Integer Q06;
    private boolean A6;
    private Integer Q07;
    private boolean A7;
    private Integer Q08;
    private boolean A8;
    private Integer Q09;
    private boolean A9;
    private Integer Q10;
    private boolean A10;
    private Integer total;
    private Integer time;
    public PlayerDao(Integer playerNo, boolean attempted) {
        super(playerNo,attempted);
    }
}
