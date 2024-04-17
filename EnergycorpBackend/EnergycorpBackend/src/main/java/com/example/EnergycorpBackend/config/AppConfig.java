package com.example.EnergycorpBackend.config;

import org.modelmapper.ModelMapper;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.reactive.function.client.WebClient;


@Configuration
public class AppConfig {
    @Bean
    ModelMapper getMapper(){
        return new ModelMapper();
    }

    @Bean
    public WebClient webClient() {
        return WebClient.builder().baseUrl("http://20.15.114.131:8080").build();
    }

}
