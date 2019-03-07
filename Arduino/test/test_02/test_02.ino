int LED=12;
int BUTTON=4;
void setup()
{
  Serial.begin(9600);
  pinMode(LED,OUTPUT);
  pinMode(BUTTON,INPUT);
}

void loop()
{
  
  if(digitalRead(BUTTON)==HIGH)
  {
    digitalWrite(LED,HIGH);
    Serial.write(1);
    Serial.flush();
    delay(20);
  }
  else
  {
      digitalWrite(LED,LOW);
      Serial.write(0);
      Serial.flush();
      delay(20);
  }
}
