// PINS
int LED=12;
int BUTTON=4;

// CONSTANTS
const int bufferSize = 20;

// VARIABLES
int tmp=0;
char Buffer[bufferSize];

void setup()
{
  Serial.begin(9600);
  pinMode(LED,OUTPUT);
  pinMode(BUTTON,INPUT);
 // digitalWrite(LED,HIGH);
}

void loop()
{

  int lf = 10;

  Serial.readBytesUntil(lf,Buffer,1);
  if(strcmp(Buffer,"0")==0)
  {
    digitalWrite(LED,HIGH);
  }
  else if(strcmp(Buffer,"1")==0)
  {
    digitalWrite(LED,LOW);
  } 
   
}
