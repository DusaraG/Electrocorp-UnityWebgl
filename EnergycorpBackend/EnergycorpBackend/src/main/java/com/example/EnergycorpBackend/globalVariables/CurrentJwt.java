package com.example.EnergycorpBackend.globalVariables;
import lombok.Getter;
import lombok.Setter;
import org.springframework.stereotype.Component;

@Component
@Getter
@Setter
public class CurrentJwt {
    public static String currentJwt = null;
}
