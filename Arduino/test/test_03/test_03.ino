int LED=12;
int BUTTON=4;

int tmp=0;

void setup()
{
  Serial.begin(9600);
  pinMode(LED,OUTPUT);
  pinMode(BUTTON,INPUT);
  digitalWrite(LED,HIGH);
}

void loop()
{
  
  if(digitalRead(BUTTON)==HIGH)
  {
    
    if(tmp==1)
    {
      tmp=0;
    }
    else
    {
      tmp=1;
    }
  }
  
  if(tmp==1)
  {
   digitalWrite(LED,HIGH);
  }
  else if(tmp==0)
  {
    digitalWrite(LED,LOW);
  }
  
   
}
