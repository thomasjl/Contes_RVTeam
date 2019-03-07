// PINS ////////////////
int BUTTON_2=2;
int LED_3=3;

int BUTTON_4=4;
int LED_5=5;

int BUTTON_6=6;
int LED_7=7;

int BUTTON_8=8;
int LED_9=9;

int BUTTON_10=10;
int LED_11=11;

int BUTTON_12=12;
int LED_13=13;

// CONSTANTS ///////////////
const int bufferSize = 20;

// VARIABLES /////////////
int tmp=0;
char bufferArray[bufferSize];
int canMakeChoices=1;

void setup()
{
  Serial.begin(9600);
  pinMode(LED_3,OUTPUT);
  pinMode(LED_5,OUTPUT);
  pinMode(LED_7,OUTPUT);
  pinMode(LED_9,OUTPUT);
  pinMode(LED_11,OUTPUT);
  pinMode(LED_13,OUTPUT);


  pinMode(BUTTON_2,INPUT);
  pinMode(BUTTON_4,INPUT);
  pinMode(BUTTON_6,INPUT);
  pinMode(BUTTON_8,INPUT);
  pinMode(BUTTON_10,INPUT);
  pinMode(BUTTON_12,INPUT);

  
  //digitalWrite(LED,HIGH);
}

void loop()
{
  
  int lf = 10;
  
  //Serial.readBytesUntil('\r',bufferArray,sizeof(bufferArray)-1);
  Serial.print(bufferArray);
  Serial.readBytesUntil(lf,bufferArray,1);
  //Serial.readBytesUntil('\n',bufferArray,bufferSize);
  
  if(strcmp(bufferArray,"R")==0)
  {
    Serial.print("erase");
    digitalWrite(LED_3,LOW);
    digitalWrite(LED_5,LOW);
    digitalWrite(LED_7,LOW);
    digitalWrite(LED_9,LOW);
    digitalWrite(LED_11,LOW);
    digitalWrite(LED_13,LOW);

    memset(bufferArray,0, sizeof(bufferArray));
    Serial.flush();


  }
  else if(strcmp(bufferArray,"E")==0)
  {
    canMakeChoices=1;
  }
  else if(strcmp(bufferArray,"D")==0)
  {
    canMakeChoices=0;
  }
  else if(strcmp(bufferArray,"3")==0)
  {
     digitalWrite(LED_3,HIGH);
  }
  else if(strcmp(bufferArray,"7")==0)
  {
     digitalWrite(LED_7,HIGH);
  }
  else if(strcmp(bufferArray,"9")==0)
  {
     digitalWrite(LED_11,HIGH);
  }
  
  if(canMakeChoices==1)
  {
    if(digitalRead(BUTTON_2)==HIGH)
    {
      digitalWrite(LED_3,HIGH);
      digitalWrite(LED_5,LOW);
      
      Serial.write(2);
      Serial.flush();
      delay(20);
    }
  
    if(digitalRead(BUTTON_4)==HIGH)
    {
      digitalWrite(LED_5,HIGH);
      digitalWrite(LED_3,LOW);
      
      Serial.write(4);
      Serial.flush();
      delay(20);
    }
  
  
    if(digitalRead(BUTTON_6)==HIGH)
    {
      digitalWrite(LED_7,HIGH);
      digitalWrite(LED_9,LOW);
      
      Serial.write(6);
      Serial.flush();
      delay(20);
    }
  
    if(digitalRead(BUTTON_8)==HIGH)
    {
      digitalWrite(LED_9,HIGH);
      digitalWrite(LED_7,LOW);
      
      Serial.write(8);
      Serial.flush();
      delay(20);
    }
    
   if(digitalRead(BUTTON_10)==HIGH)
    {
      digitalWrite(LED_11,HIGH);
      digitalWrite(LED_13,LOW);
      
      Serial.write(10);
      Serial.flush();
      delay(20);
    }
  
    
    if(digitalRead(BUTTON_12)==HIGH)
    {
      digitalWrite(LED_13,HIGH);
      digitalWrite(LED_11,LOW);
      
      Serial.write(12);
      Serial.flush();
      delay(20);
    }
  } 
   
}
