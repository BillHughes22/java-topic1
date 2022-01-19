package com.gcu;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.ComponentScan;

@ComponentScan({ "com.gcu" })
@SpringBootApplication
public class Topic21Application {

	public static void main(String[] args) {
		SpringApplication.run(Topic21Application.class, args);
	}

}
