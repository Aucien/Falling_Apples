/*
Author: Gordon Dan
Email: gdan189@csu.fullerton.edu
Course: CPSC 223N
Semester: Fall 2019
Assignment #:5
Program Name: Falling Apples
 */

 using System;
 using System.Windows.Forms;

 public class TravelMain
 {
     static void Main(){
         System.Console.WriteLine("Hiya, this is the main method of Falling Apples");
         fallingUI app = new fallingUI();
         Application.Run(app);
         System.Console.WriteLine("Main Method of Falling Apples will shutdown");
     }
 }