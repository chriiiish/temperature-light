#include <Adafruit_NeoPixel.h>
#include <Adafruit_Sensor.h>
#include <DHT.h>

#define LED_PIN D1
#define NUM_LEDS 10

#define DHT_PIN 4 // GPIO - D2
#define DHT_TYPE DHT22

Adafruit_NeoPixel pixels = Adafruit_NeoPixel(NUM_LEDS, LED_PIN, NEO_GRB + NEO_KHZ800);
DHT dht(DHT_PIN, DHT_TYPE);


void setup() {
  Serial.begin(115200);

  pixels.begin();
  log("Pixels.begin();");
  
  dht.begin();
  log("dht.begin();");

  log("Setup Complete.");
}


void loop() 
{
  float temperature = readTemp();

  // Room too cold
  if (temperature < 20) {
    setColour(0, 255, 255);  // Cyan
  }
  
  // Room too hot
  else if (temperature > 22) {
    setColour(255, 0, 0);  // Red
  }
  
  // Room just right
  else{
    setColour(255, 255, 255);  // White
  }


  // Wait for next check
  delay(1 * 1000);
}

 
/*
 * Sets the colour of the leds in the strip
 */
void setColour(int redValue, int greenValue, int blueValue)
{
  float brightness = 0.25;
  
  for(int led = 0; led < NUM_LEDS; led++){
    pixels.setPixelColor(led, pixels.Color(redValue * brightness, greenValue * brightness, blueValue * brightness)); 
  }
  
  pixels.show();
}


/*
 * Logs messages
 */
void log(String message)
{
  //Serial.println(message)
}


/* 
 *  Read the temperature from the probe, log for plotting
 */
float readTemp()
{
  log("Reading Temperature");
  float temperature = dht.readTemperature();

  if(isnan(temperature)){
    log("DHT Temperature Read Failed");
    return 0.0;
  }
  else{
    log(temperature);
    Serial.println(temperature);
    return temperature;
  }
}
