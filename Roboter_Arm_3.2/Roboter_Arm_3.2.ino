
//Komprimierte Version ohne hardcoded Auto Fälle(3.2)

#include <Servo.h>  
Servo myservoA;  
Servo myservoB;
Servo myservoC;
Servo myservoD;
Servo myservoE;

int i,pos,myspeed,myshow;
int se_a1,se_b2,se_c3,se_d4,se_e5;
static int v=0;
boolean isConnected = false;
String mycommand="";                          //#auto1 #com computer serial port control  #stop
static int mycomflag=1;                       // #auto1=2   #com=1   #stop=0


void myservosetup()                          //Top fuer mycomflag=0
{
   se_a1=myservoA.read();
   se_b2=myservoB.read();
   se_c3=myservoC.read();
   se_d4=myservoD.read();
   se_e5=myservoE.read();
   
   myspeed=500;
   for(pos=0;pos<=myspeed;pos+=1)
   {
    myservoA.write(int(map(pos,1,myspeed,se_a1,90)));
    myservoB.write(int(map(pos,1,myspeed,se_b2,90)));
    myservoC.write(int(map(pos,1,myspeed,se_c3,90)));
    myservoD.write(int(map(pos,1,myspeed,se_d4,90)));
    myservoE.write(int(map(pos,1,myspeed,se_e5,90)));
   
    delay(1);
   }
}

void setup() 
{ 
  Serial.begin(9600);
  myshow=0;
  mycomflag=1;
  myservoA.attach(3);    
  myservoB.attach(5);     
  myservoC.attach(6);  
  myservoD.attach(9);  
  myservoE.attach(10); 
  
  myservoA.write(90);
  myservoB.write(90);
  myservoC.write(90);
  myservoD.write(90);
  myservoE.write(90);   
}

void loop() 
{ 
  while (Serial.available() > 0)  
    {
        mycommand += char(Serial.read());
        delay(2);
    }
    if (mycommand.length() > 0)
    {
        if(mycommand=="#com")
        {
          mycomflag=1;
        //  Serial.println("wilco_com_control");
          mycommand="";
          myservosetup();
        }
        if(mycommand=="#stop")
        {
          mycomflag=0;
        //  Serial.println("stop station");
          mycommand="";
        }
        
    }
  
  if(mycomflag==1)//COM Control
  {      
 
   for(int m=0;m<mycommand.length();m++) 
  {
    char ch = mycommand[m];   
    switch(ch)
    {
      case '0'...'9':
      v = v*10 + ch - '0';   
      break;
 
      case 'a':  
      if(v >= 5 || v <= 175 ) myservoA.write(v); 
      v = 0;
      break;

      case 'b':   
      myservoB.write(v);  
      v = 0;
      break;
      
      case 'c':   
      if(v >= 20 ) myservoC.write(v);   
      v = 0;
      break;
      
      case 'd':  
      myservoD.write(v);   
      v = 0;
      break;
      
      case 'e':  
      myservoE.write(v);   
      v = 0;
      break;
    }
   
    }  
   mycommand=""; //reset var
  }  

if(mycomflag==0) //STOP
  {
   myservosetup();
  }  
}
