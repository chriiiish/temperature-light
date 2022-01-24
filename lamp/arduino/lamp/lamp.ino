#include <Adafruit_NeoPixel.h>
#include <Adafruit_Sensor.h>
#include <DHT.h>

#define LED_PIN D1
#define NUM_LEDS 10

#define DHT_PIN 4 // GPIO - D2
#define DHT_TYPE DHT22

#define TEMP_DIFF_MINOR 2   // Difference of up to this much is considered a "minor" deviation from the ideal
#define TEMP_DIFF_MAJOR 6  // Difference of more than this is considered a "major" deviation from the ideal

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
  float difference = getTempDifference(temperature);
  
  setColourTemperature(difference);
  
  // Wait for next check
  delay(2 * 1000);
}


/*
 * Set the colour temperature of the led strip based on the difference in temperature
 */
void setColourTemperature(float temperature_difference)
{
  log("Setting colour temperature for difference of " + String(temperature_difference));
  
  // Default to white
  int colourRGB[] = {255, 255, 255};


  // Set Colours for COLD
  if(temperature_difference < 0){
    log("Cold Minor");
    memcpy(colourRGB, (const int[]){50, 100, 255}, sizeof colourRGB);
  }

  if(temperature_difference < -1 * TEMP_DIFF_MINOR){
    log("Cold Minor-Major");
    memcpy(colourRGB, (const int[]){25, 50, 255}, sizeof colourRGB);
  }

  if(temperature_difference < -1 * TEMP_DIFF_MAJOR){
    log("Cold Major");
    memcpy(colourRGB, (const int[]){0, 0, 255}, sizeof colourRGB);  
  }


  // Set Colours for HOT
  if(temperature_difference > 0){
    log("Hot Minor");
    memcpy(colourRGB, (const int[]){255, 100, 50}, sizeof colourRGB);
  }

  if(temperature_difference > TEMP_DIFF_MINOR){
    log("Hot Minor-Major");
    memcpy(colourRGB, (const int[]){255, 50, 25}, sizeof colourRGB);
  }

  if(temperature_difference > TEMP_DIFF_MAJOR){
    log("Hot Major");
    memcpy(colourRGB, (const int[]){255, 0, 0}, sizeof colourRGB);
  }


  log("Setting Led colours...");
  setLeds(colourRGB[0], colourRGB[1], colourRGB[2]);    
}

 
/*
 * Sets the colour of the leds in the strip
 */
void setLeds(int redValue, int greenValue, int blueValue)
{
  float brightness = 0.25;

  log("Setting Leds to R=" + String(redValue) + " G=" + String(greenValue) + " B=" + String(blueValue) + " with brightness " + String(brightness));  
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
  //Serial.println(message);
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

/*
 * Gets the difference in temperature from the ideal
 */
float getTempDifference(float temperature)
{
  log("temperature is " + String(temperature));
  
  // Room too cold
  if (temperature < 20) {
    log("room too cold");
    return temperature - 20;
  }
  
  // Room too hot
  else if (temperature > 22) {
    log("room too hot");
    return temperature - 22;
  }

  log("room just right");
  return 0;
}
