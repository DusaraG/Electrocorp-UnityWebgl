package com.example.EnergycorpBackend.dto;

import com.example.EnergycorpBackend.globalVariables.CurrentJwt;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@AllArgsConstructor
public class JwtToken {
    private String token;

    public JwtToken() {
        this.token = CurrentJwt.currentJwt;
    }
}
