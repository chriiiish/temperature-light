#include <Adafruit_NeoPixel.h>
#include <Adafruit_Sensor.h>
#include <DHT.h>

#define LED_PIN D1
#define NUM_LEDS 10

#define DHT_PIN 4
#define DHT_TYPE DHT22

Adafruit_NeoPixel pixels = Adafruit_NeoPixel(NUM_LEDS, LED_PIN, NEO_GRB + NEO_KHZ800);
DHT dht(DHT_PIN, DHT_TYPE);

void setup() {
  Serial.begin(115200);

  pixels.begin();
  log("Pixels.begin();");
  
  dht.begin();
  log("dht.begin();");

  Serial.print("");
  log("Setup Complete.");
}

void loop() 
{
    delay(4000);
    Serial.println();
    Serial.println("Temperature(*C) Humidity(%)");
    while(true){
      // Do LED Things
      int led;
      for(led=0; led <= NUM_LEDS; led++)
      {
        setColor(led,255,0,0,100); //red
      }
    
      readTemp();
      
      for(led=0; led <= NUM_LEDS; led++)
      {
        setColor(led,0,255,255,100); //red
      }
    
      readTemp();
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

void log(String message){
  //Serial.println(message)
}

void readTemp(){
  log("Reading Temperature");
  float temperature = dht.readTemperature();
  float humidity = dht.readHumidity();
  if(isnan(temperature)){
    log("DHT Temperature Read Failed");
  }else{
    log(temperature);
    log(humidity);
    Serial.print(temperature);
    Serial.print(" ");
    Serial.print(humidity);
    Serial.println();
  }
}
