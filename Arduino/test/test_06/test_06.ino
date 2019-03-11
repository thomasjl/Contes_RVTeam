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

int pinBouton;

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

}

void loop()

{
  if(digitalRead(BUTTON_2)==HIGH || digitalRead(BUTTON_6)==HIGH || digitalRead(BUTTON_10)==HIGH)
  {
    Serial.println("2");
    digitalWrite(LED_3,HIGH);
    digitalWrite(LED_5,HIGH);

    digitalWrite(LED_7,HIGH);

    digitalWrite(LED_9,HIGH);

    digitalWrite(LED_11,HIGH);

    digitalWrite(LED_13,HIGH);


  }

  if(digitalRead(BUTTON_4)==HIGH)
  {
    Serial.println("4");
        digitalWrite(LED_5,HIGH);


  }

  if(digitalRead(BUTTON_6)==HIGH)
  {
    Serial.println("6");
        digitalWrite(LED_7,HIGH);


  }

  if(digitalRead(BUTTON_8)==HIGH)
  {
    Serial.println("8");
        digitalWrite(LED_9,HIGH);


  }

  if(digitalRead(BUTTON_10)==HIGH)
  {
    Serial.println("10");
        digitalWrite(LED_11,HIGH);

  }

  if(digitalRead(BUTTON_12)==HIGH)
  {
    Serial.println("12");
        digitalWrite(LED_13,HIGH);


  }

 
  
    
}
