package com.gcu.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

@Controller
@RequestMapping("/hello")
public class HelloWorldController 
{
	/**
	 * Simple Hello World Controller that returns a String in the response body
	 * Invoke using /test1 URI. Test Comment
	 * 
	 * @ return Hello World test string
	 */
	@GetMapping("/test1") // localhost:8080/hello/test1 - HTTP GET
	@ResponseBody         // Return Raw Content
	public String printHello()
	{
		// Simply return a String in the response body (must use @ResponseBody annotation)
		return "Hello World!";
	}

}
