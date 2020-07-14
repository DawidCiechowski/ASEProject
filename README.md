# Advanced Software Engineering Software

## Table of Contents

- [ Introduction ](#intro)
- [ Technologies ](#tech)
- [ Usage ](#usage)

<a name="intro"></a>
## Introduction 

Univeristy project, revolving around the idea of developing simple programming language, in C#. The language would containt graphical user interface, where by
invoking the commands, with their particular parameters, an object would be drawn to the screen, during the compilation. User is also able to create their own functions, for loops 
and if statements, to make the program behave the way they'd like to.

<a name="technology"></a>
## Technologies

- C#
- Visual Studio Forms

<a name="usage"></a>
## Usage

The language works in very similiar way to Python programming language. There is no need for classes, and program can run without explicitly defined functions.

### Commands

Software has a number of different commands, which user can invoke when programing in this particular language. 

#### User defined variables

User can create their own variables, however only of integer type. The assignment works by putting "var" keyword, after which a name of variable and instantiation should follow.

```
var a = 10
circle(a)
```

#### If statements

If statements  can be invoked in similiar way to pythonic if statements. Since the language does not support boolean or string values, the comparison
is made between two integer values at all times.

```
if i == 5:
  {Do something}
```

#### For loops

For loops can be invoked in three different ways.

1. One parameter if statement. For loop will execute N number of times.

```
for var i in range = 10
  {Do Something}
 endfor
 ```
 
 2. Two parameter for loop. Loop with starting value and end value.
 
 ```
 for var i in range = 2, 5
  {Do something}
 endfor
 ```
 
 The loop will be executed 3 times, where the value of i will start at 2.
 
 3. Three parameter for loop. Loop with starting, end and step values.
 
 ```
 for var i in range = 2, 10, 2
  {Do something}
 endfor
 ```
 
 The loop will be executed 5 times, where the starting value of i will be 2 and end value 10, where with each iteration step value will be added to i. 
 
 ### User Defined Functions
 
 User can create their own function, which can hold its own command and act upon them, any number of times the function is invoked. The function can be empty or contain 
 parameters, which will be used within it.
 
 1. Function No parameters.
 
 ```
 func FunctionNoParams()
  circle(10)
  moveTo(150, 150)
 endfunc
 
 FunctionNoParams()
 ```
 
 2. Function with parameters
 
 ```
 func FunctionWithParameters(a, b, c)
  for i in range = a, b
    circle(c)
    c = c + 1
   endfor
 endfunc
 
 FunctionWithParameters(2, 4, 10)
 ```

#### moveTo Command

Originally, when invoking the geomatrical commands to draw on the screen, they will be drawn at X: 0 and Y: 0. By invoking "moveTo()" command, we can change the destination
of next drawn objects. It can take both user defined variables or simple integer values, as its parameters.

1. Without user defined variables

```
moveTo(100, 100)
```

2. With user defined variables

```
var a = 100
var b = 100
moveTo(a,b)
```

### drawTo Command

To move coordinates X and Y, while drawing out the path, drawTo command can be invoked, to draw the line from point A to point B. drawTo takes two integer parameters: X and Y. 
It will draw the line from its' original position, all the way to new destination, while setting new X and Y coordinates. It can take both user defined variables or simple 
integer values, as its parameters.

1. Without user defined variables

```
drawTo(100, 100)
```
2. With user defined variables

```
var a = 100
var b = 100
drawTo(a,b)
```

#### circle Command

"circle()" command can be used to draw eliptical shape to the screen. It takes one integer parameter, which is its radius. It can take both user defined variables or simple 
integer values, as its parameters.

1. Without user defined variables

```
circle(100)
```
2. With user defined variables

```
var a = 100
circle(a)
```

#### rectangle Command

"rectangle()" command can be used to draw rectangular shape to the screen. It takes two integer parameters, which are: width and height. It can take both user defined variables
or simple integer values, as its parameters.

1. Without user defined variables

```
rectangle(100)
```
2. With user defined variables

```
var a = 100
var b = 100
rectangle(a,b)
```

#### triangle Command

"triangle()" command can be used to draw a triangle to the screen. It takes three integer parameters, which are lengths of its sides. Triangle must be valid, otherwise it won't
be drawn to the screen and user will see an error. It can take both user defined variables or simple integer values, as its parameters.

1. Without user defined variables

```
triangle(2,3,5)
```
2. With user defined variables

```
var a = 2
var b = 3
var c = 5
rectangle(a,b,c)
```

#### clear Command

"clear()" command can be usd to clear all currently drawn objects from the screen. It takes no parameters.

```
clear()
```

#### Save Option

Software contains an option to Save the current file and load new ones. It accepts .txt files format. 
