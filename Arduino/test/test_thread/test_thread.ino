// PINS ////////////////
int BUTTON_2=2;
int BUTTON_4=4;
int BUTTON_6=6;
int BUTTON_8=8;
int BUTTON_10=10;
int BUTTON_12=12;


// CONSTANTS ///////////////
const int bufferSize = 20;

// VARIABLES /////////////
int tmp=0;
char bufferArray[bufferSize];
int canMakeChoices=1;

int DEFAULT_STATE_2=0;
int DEFAULT_STATE_4=0;
int DEFAULT_STATE_6=0;
int DEFAULT_STATE_8=0;
int DEFAULT_STATE_10=0;
int DEFAULT_STATE_12=0;



void setup()
{  
  Serial.begin(9600);
  
  Serial.setTimeout(100);
  /*
  pinMode(BUTTON_2,INPUT);  
  pinMode(BUTTON_4,INPUT);
  pinMode(BUTTON_6,INPUT);  
  pinMode(BUTTON_8,INPUT);  
  pinMode(BUTTON_10,INPUT);  
  pinMode(BUTTON_12,INPUT);
  */

  pinMode(BUTTON_2,OUTPUT);  
  pinMode(BUTTON_4,OUTPUT);
  pinMode(BUTTON_6,OUTPUT);  
  pinMode(BUTTON_8,OUTPUT);  
  pinMode(BUTTON_10,OUTPUT);  
  pinMode(BUTTON_12,OUTPUT);
  
  digitalWrite(BUTTON_2,DEFAULT_STATE_2);
  digitalWrite(BUTTON_4,DEFAULT_STATE_4);
  digitalWrite(BUTTON_6,DEFAULT_STATE_6);
  digitalWrite(BUTTON_8,DEFAULT_STATE_8);
  digitalWrite(BUTTON_10,DEFAULT_STATE_10);
  digitalWrite(BUTTON_12,DEFAULT_STATE_12);
  
}

void loop()
{
       if(digitalRead(BUTTON_2)==HIGH || digitalRead(BUTTON_6)==HIGH || digitalRead(BUTTON_10)==HIGH)
       {
        Serial.println(2);
        Serial.flush();
        delay(20);
       }
   
}
