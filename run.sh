#Author: Gordon Dan
#Email:  gdan189@csu.fullerton.edu
#Course: CPSC 223N
#Semester: Fall 2019
#Assignment #: 5
#Program Name: Falling Apples


rm *.dll
rm *.exe

mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll -out:fallingUI.dll fallingUI.cs
mcs -r:System -r:System.Windows.Forms -r:fallingUI.dll -out:FallingApple.exe fallingMain.cs
./FallingApple.exe