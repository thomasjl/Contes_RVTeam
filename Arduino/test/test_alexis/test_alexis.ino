// PIN PORT DEFINITION //

const int myButton = 8;

const int myLED    = 9;

int          myPWM = 0;
int    myDirection = 0;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(myButton,  INPUT);
  pinMode(myLED,    OUTPUT);
  Serial.print("Hello world\n");
}

void loop() {
  // put your main code here, to run repeatedly:
//  if (digitalRead(myButton)==HIGH)
//  {
//    Serial.print("myButton is active\n");
//    digitalWrite(myLED, HIGH);
//  }
//  else
//  {
//    Serial.print("myButton is inactive\n");
//    digitalWrite(myLED, LOW);
//  }
  
  if (myDirection == 0) 
  {
    // upward
    analogWrite(myLED, 5+myPWM);
  }
  else {
    // downward
    analogWrite(myLED, 250-myPWM);
  }

  if (myPWM == 250) {
    myDirection = !myDirection;
    myPWM = 0;
  }
  else {
    myPWM = myPWM + 10; ;
  }
  Serial.print("PWM value : ");
  Serial.println(myPWM, HEX);
  Serial.print("Direction : ");
  Serial.println(myDirection);
}
