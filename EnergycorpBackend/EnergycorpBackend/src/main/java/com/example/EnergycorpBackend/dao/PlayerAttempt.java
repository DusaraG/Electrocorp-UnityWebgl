package com.example.EnergycorpBackend.dao;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;
@Entity
@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
public class PlayerAttempt {
    @Id
    protected Integer playerNo = 1;
    protected boolean attempted;
}
