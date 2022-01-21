#include <Adafruit_NeoPixel.h>

#define PIN D1
#define NUM_LEDS 10

Adafruit_NeoPixel pixels = Adafruit_NeoPixel(NUM_LEDS, PIN, NEO_GRB + NEO_KHZ800);

void setup() {
  // put your setup code here, to run once:
  pixels.begin();
}

void loop() 
{
  int led;
  for(led=0; led <= NUM_LEDS; led++)
  {
    setColor(led,255,0,0,100); //red
  }
  for(led=0; led <= NUM_LEDS; led++)
  {
    setColor(led,0,255,255,100); //
  }
    for(led=0; led <= NUM_LEDS; led++)
  {
    setColor(led,255,0,255,100); //
  }
}
 
//simple function which takes values for the red, green and blue led and also
//a delay
void setColor(int led, int redValue, int greenValue, int blueValue, int delayValue)
{
  pixels.setPixelColor(led, pixels.Color(redValue, greenValue, blueValue)); 
  pixels.show();
  delay(delayValue);
}
