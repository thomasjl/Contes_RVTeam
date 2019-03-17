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
  //read data from unity

  // read data from unity
  int lf = 10;    
  Serial.readBytesUntil(lf,bufferArray,1);    
  
  if(strcmp(bufferArray,"E")==0)
  {
    Serial.println("E");
    canMakeChoices=1;
  }
  else if(strcmp(bufferArray,"D")==0)
  {
    Serial.println("D");
    canMakeChoices=0;
  }
  else if(strcmp(bufferArray,"R")==0)
  {
    Serial.println("R");
    DEFAULT_STATE_2=0;
    DEFAULT_STATE_4=0;
    DEFAULT_STATE_6=0;
    DEFAULT_STATE_8=0;
    DEFAULT_STATE_10=0;
    DEFAULT_STATE_12=0;

    digitalWrite(BUTTON_2,DEFAULT_STATE_2);
    digitalWrite(BUTTON_4,DEFAULT_STATE_4);
    digitalWrite(BUTTON_6,DEFAULT_STATE_6);
    digitalWrite(BUTTON_8,DEFAULT_STATE_8);
    digitalWrite(BUTTON_10,DEFAULT_STATE_10);
    digitalWrite(BUTTON_12,DEFAULT_STATE_12); 
  }
  else if(strcmp(bufferArray,"2")==0)
  {
    DEFAULT_STATE_2=1;
    DEFAULT_STATE_4=0;
    digitalWrite(BUTTON_2,DEFAULT_STATE_2);
    digitalWrite(BUTTON_4,DEFAULT_STATE_4);
  }
  else if(strcmp(bufferArray,"6")==0)
  {
    DEFAULT_STATE_6=1;
    DEFAULT_STATE_8=0;
    digitalWrite(BUTTON_6,DEFAULT_STATE_6);
    digitalWrite(BUTTON_8,DEFAULT_STATE_8);
  }
  else if(strcmp(bufferArray,"9")==0)
  {
    DEFAULT_STATE_10=1;
    DEFAULT_STATE_12=0;
    digitalWrite(BUTTON_10,DEFAULT_STATE_10);
    digitalWrite(BUTTON_12,DEFAULT_STATE_12);
  }
  
  int change_2=0;
  int change_6=0;
  int change_10=0;
  
  if(canMakeChoices==1)
  {
    if(DEFAULT_STATE_2==0 && digitalRead(BUTTON_2)==HIGH)
    {      
      //Serial.println("2");
      change_2=1;
      DEFAULT_STATE_2=1;
      DEFAULT_STATE_4=0;
      
      Serial.println(2);
      Serial.flush();
      delay(20);
      
    }

    if(change_2==0 && DEFAULT_STATE_4==0 && digitalRead(BUTTON_4)==HIGH)
    {
      DEFAULT_STATE_4=1;
      DEFAULT_STATE_2=0;
      Serial.println(4);
      Serial.flush();
      delay(20);
    }

    if(DEFAULT_STATE_6==0 && digitalRead(BUTTON_6)==HIGH)
    {
      change_6=1;
      DEFAULT_STATE_6=1;
      DEFAULT_STATE_8=0;
      Serial.println(6);
      Serial.flush();
      delay(20);
    }
  
    if(change_6==0 && DEFAULT_STATE_8==0 && digitalRead(BUTTON_8)==HIGH)
    {
      DEFAULT_STATE_8=1;
      DEFAULT_STATE_6=0;
      
      Serial.println(8);
      Serial.flush();
      delay(20);
    }
  
    if(DEFAULT_STATE_10==0 && digitalRead(BUTTON_10)==HIGH)
    {
      change_10=1;
      DEFAULT_STATE_10=1;
      DEFAULT_STATE_12=0;
      Serial.println(3);
      Serial.flush();
      delay(20);
    }
  
    if(change_10==0 && DEFAULT_STATE_12==0 && digitalRead(BUTTON_12)==HIGH)
    {
      DEFAULT_STATE_12=1;
      DEFAULT_STATE_10=0;
      Serial.println(5);
      Serial.flush();
      delay(20);
    }
  }
  else
  {
     
  }

  
    digitalWrite(BUTTON_2,DEFAULT_STATE_2);
    digitalWrite(BUTTON_4,DEFAULT_STATE_4);
    digitalWrite(BUTTON_6,DEFAULT_STATE_6);
    digitalWrite(BUTTON_8,DEFAULT_STATE_8);
    digitalWrite(BUTTON_10,DEFAULT_STATE_10);
    digitalWrite(BUTTON_12,DEFAULT_STATE_12); 

       
   
}
