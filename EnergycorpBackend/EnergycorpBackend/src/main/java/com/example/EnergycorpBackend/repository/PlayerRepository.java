package com.example.EnergycorpBackend.repository;

import com.example.EnergycorpBackend.dao.PlayerDao;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;


@Repository
public interface PlayerRepository extends CrudRepository<PlayerDao,Integer> {
//the database has one table with the player id as the primary key
    //other fields included are answers to the questions, total score,
    // time taken, and whether the player has attempted the quiz

}
