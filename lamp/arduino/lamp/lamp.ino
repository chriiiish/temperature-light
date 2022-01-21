#include <Adafruit_NeoPixel.h>

#define PIN D1
#define NUM_LEDS 10

Adafruit_NeoPixel pixels = Adafruit_NeoPixel(NUM_LEDS, PIN, NEO_GRB + NEO_KHZ800);

void setup() {
  pixels.begin();
  Serial.begin(115200);
  Serial.println("Setup Complete");
}

void loop() 
{
  int led;
  Serial.println();
  Serial.print("RED: "); 
  for(led=0; led <= NUM_LEDS; led++)
  {
    setColor(led,255,0,0,100); //red
    Serial.print(led); Serial.print(", ");
  }
  Serial.println();
  Serial.print("CYAN: "); 
  for(led=0; led <= NUM_LEDS; led++)
  {
    setColor(led,0,255,255,100); //CYAN
    Serial.print(led); Serial.print(", ");
  }
  Serial.println();
  Serial.println("PURPLE: ");
  for(led=0; led <= NUM_LEDS; led++)
  {
    setColor(led,255,0,255,100); //PURPLE
    Serial.print(led); Serial.print(", ");
  }
}
 
//simple function which takes values for the red, green and blue led and also
//a delay
void setColor(int led, int redValue, int greenValue, int blueValue, int delayValue)
{
  pixels.setPixelColor(led, pixels.Color(redValue/5, greenValue/5, blueValue/5)); 
  pixels.show();
  delay(delayValue);
}
