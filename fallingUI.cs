/*
Author: Gordon Dan
Email: gdan189@csu.fullerton.edu
Course: CPSC 223N
Semester: Fall 2019
Assignment #:5
Program Name: Falling Apples
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class fallingUI : Form{
    //Window
    private Label title = new Label();
    private const int formwidth = 1280;
    private const int formheight = 1000;
    private Button exitButton = new Button();
    private Button startButton = new Button();

    private Font style = new System.Drawing.Font("Arial", 12, FontStyle.Regular);
    private Font winStyle = new System.Drawing.Font("Arial", 30, FontStyle.Regular);

    //Ball
    private bool visible = false;
    private const int diameter = 30;
    private float AppleXcord = 0;
    private float AppleYCord = 0;
    private float gravity = 1;
    private int ApplesClicked = 0;
    private bool win = false;
    //Motion Clock
    private static System.Timers.Timer motionClock = new System.Timers.Timer();
    private const double motionClockRate = 60;

    //Refresh Clock;
    private static System.Timers.Timer graphicsClock = new System.Timers.Timer();
    private const double graphicRefreshRate = 30;

    public fallingUI(){
        Size = new Size (formwidth, formheight);
        Text = "Falling Ball";
        BackColor = Color.LightSkyBlue;

        //Clocks
        motionClock.Enabled = false;
        motionClock.Elapsed += new ElapsedEventHandler(UpdatePosition);
        graphicsClock.Enabled = false;
        graphicsClock.Elapsed += new ElapsedEventHandler(UpdateDisplay);

        //Title
        title.Text = "Falling Apples by Gordon Dan";
        title.BackColor = Color.LightGreen;
        title.Size = new Size(200, 25);
        title.Location = new Point(510, 0);
        Controls.Add(title);

        //Exit Button
        exitButton.Text = "Exit";
        exitButton.Size = new Size(150, 50);
        exitButton.Location = new Point(600, 800);
        exitButton.Click += new EventHandler(stop);
        Controls.Add(exitButton);

        //Start Button
        startButton.Text = "start";
        startButton.Size = new Size(150, 50);
        startButton.Location = new Point(400, 800);
        startButton.Click += new EventHandler(Begin);
        Controls.Add(startButton);
    }

    protected override void OnPaint(PaintEventArgs ee){
        Graphics graph = ee.Graphics;
        string grav = "Gravity: " + gravity;
        string caught = "Apples Caught: " + ApplesClicked;
        string Winner = "Congratulations You've Won";
        graph.FillRectangle(Brushes.Tan, 0, 500, formwidth, 250);
        graph.FillRectangle(Brushes.Yellow, 0, 750, formwidth, 250);
        if(visible)graph.FillEllipse(Brushes.Red, AppleXcord, AppleYCord, diameter, diameter);
        graph.FillRectangle(Brushes.LightGreen, 0, 0, formwidth, 60);
        graph.DrawString(grav, style, Brushes.Black, new Point(150, 825));
        graph.DrawString(caught, style, Brushes.Black, new Point(150, 800));
        if(win)graph.DrawString(Winner, winStyle, Brushes.Green, new Point(365, 300));
        base.OnPaint(ee);
    }
    
    protected void StartGraphicClock(double refreshRate){
        double acutalRefreshRate = 1.0;
        double elapsedTics;
        if(refreshRate > acutalRefreshRate){
            acutalRefreshRate = refreshRate;
        }
        elapsedTics = 1000.0/acutalRefreshRate;
        graphicsClock.Interval = (int)System.Math.Round(elapsedTics);
        graphicsClock.Enabled = true;
    }

    protected void StartAppleClock(double UpdateRate){
        double elapsedMoves;
        if(UpdateRate< 1.0) UpdateRate = 1.0;
        elapsedMoves = 1000.0/UpdateRate;
        motionClock.Interval = (int)System.Math.Round(elapsedMoves);
        motionClock.Enabled = true;
    }

    protected void Begin(Object sender, EventArgs evt){
        gravity = 1;
        ApplesClicked = 0;
        visible = true;
        win = false;
        StartGraphicClock(graphicRefreshRate);
        generateApple();
        StartAppleClock(motionClockRate);
    }

    protected void UpdateDisplay(System.Object sender, ElapsedEventArgs evt){
        Invalidate();
        if(ApplesClicked >= 10){
            visible = false;
            win = true;
            motionClock.Enabled = false;
        }
        if(!motionClock.Enabled){
            graphicsClock.Enabled = false;
        }
    }

    protected void UpdatePosition(System.Object sender, ElapsedEventArgs evt){
        AppleYCord = AppleYCord + gravity;
        if(AppleYCord >= 750){
            generateApple();
        }
    }

    protected void generateApple(){
        Random r = new Random();
        int rInt = r.Next(8, 1250);
        AppleXcord = rInt;
        AppleYCord = 0;
        Invalidate();
    }

    protected override void OnMouseDown(MouseEventArgs me){
        if(me.X >= (AppleXcord - 5) && me.X <= (AppleXcord + diameter + 5) && me.Y >= (AppleYCord - 5) && me.Y <= (AppleYCord + diameter + 5)){
            generateApple();
            gravity += 0.5f;
            ApplesClicked++;
        }
        Invalidate();
    }

    protected void stop(Object sender, EventArgs events){
        graphicsClock.Enabled = false;
        motionClock.Enabled = false;
        Close();
    }
}