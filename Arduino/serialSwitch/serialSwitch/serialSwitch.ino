/*** serial switch ***/
// 1. connect device to computer
// 2. look for COM port (windows)
// 3. point Serial prefab to point to that port, at 9600 baud
// 4. send "1" to turn ON, send anything else for OFF

int led = 13;
int powerSwitch = 6;
int powerSwitchN = 5;
int sineNoise = 0;
int adder = 1;

String inputString = "";
boolean stringComplete = false;

void setup() {
  pinMode(led, OUTPUT);
  pinMode(powerSwitch, OUTPUT);
  pinMode(powerSwitchN, OUTPUT);
  digitalWrite(powerSwitchN, LOW);
  Serial.begin(9600);
  inputString.reserve(200);
  flash(led, 5);
}

void loop() {
  serialEvent();
  if (stringComplete) {
    if (inputString == "1") {
      digitalWrite(led, HIGH);
      digitalWrite(powerSwitch, HIGH);
    } else {
      digitalWrite(led, LOW);
      digitalWrite(powerSwitch, LOW);
    }
    //Serial.println(inputString); // echo back what we got
    inputString = "";
    stringComplete = false;
  }
  delay(25);
  sineNoise += adder;
  if(sineNoise > 99)
    adder = -1;
  if(sineNoise < 1)
    adder = 1;
  Serial.println(sineNoise);
}


void serialEvent() {
  while (Serial.available()) {
    char inChar = (char)Serial.read();
    if (inChar == '\n' || inChar == '\r')
      stringComplete = true;
    else
      inputString += inChar;
  }
}

void flash(int pin, int t){
  for(int i=0; i<t; i++){
    digitalWrite(pin, HIGH);
    delay(100);
    digitalWrite(pin, LOW);
    delay(100);
  }
}

