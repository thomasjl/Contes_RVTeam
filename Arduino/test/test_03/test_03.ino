int LED_3=3;
int BUTTON_2=2;

int LED_5=5;
int BUTTON_4=4;

int tmp=0;

void setup()
{
  //Serial.begin(9600);
  pinMode(LED_3,OUTPUT);
  pinMode(BUTTON_2,INPUT);
  
  pinMode(LED_5,OUTPUT);
  pinMode(BUTTON_4,INPUT);
  
}

void loop()
{
  
  if(digitalRead(BUTTON_2)==HIGH)
  {
    Serial.println("2");
        digitalWrite(LED_3,HIGH);
  }

  if(digitalRead(BUTTON_4)==HIGH)
  {
    Serial.println("4");
        digitalWrite(LED_5,HIGH);
  }
   
}
