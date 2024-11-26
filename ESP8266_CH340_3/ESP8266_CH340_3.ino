#include <ArduinoJson.h>

void setup() {
  Serial.begin(115200); // Ініціалізація серійного порту
  while (!Serial) {
    ; // Очікування підключення серійного порту
  }
  //Serial.println("ESP32 готовий до роботи через COM-порт.");
}

String getRandomChoice() {
  int randValue = random(0, 3);
  switch (randValue) {
    case 0: return "Rock";
    case 1: return "Paper";
    case 2: return "Scissors";
  }
  return "Rock"; // За замовчуванням
}

String determineWinner(String playerChoice, String aiChoice) {
  if (playerChoice == aiChoice) {
    return "Draw";
  }
  if ((playerChoice == "Rock" && aiChoice == "Scissors") ||
      (playerChoice == "Paper" && aiChoice == "Rock") ||
      (playerChoice == "Scissors" && aiChoice == "Paper")) {
    return "Player wins";
  }
  return "AI wins";
}

String determineWinner_AI_vs_AI(String aiChoice1, String aiChoice2) {
  if (aiChoice1 == aiChoice2) {
    return "Draw";
  }
  if ((aiChoice1 == "Rock" && aiChoice2 == "Scissors") ||
      (aiChoice1 == "Paper" && aiChoice2 == "Rock") ||
      (aiChoice1 == "Scissors" && aiChoice2 == "Paper")) {
    return "AI 1 wins";
  }
  return "AI 2 wins";
}

void loop() {
  // Перевірка наявності даних у серійному порті
  if (Serial.available() > 0) {
    String input = Serial.readStringUntil('\n'); // Зчитування JSON-запиту
    StaticJsonDocument<200> requestDoc;
    DeserializationError error = deserializeJson(requestDoc, input);
    //Serial.println("Received input: " + input);

    if (error) {
      //Serial.println("{\"error\":\"Invalid JSON format\"}");
      return;
    }


    // Зчитування параметрів із запиту
    String gameMode = requestDoc["GameMode"];
    String playerChoice = requestDoc["PlayerChoice"];

    if (gameMode == "Player vs AI") // Player vs AI
    {
      // Serial.println("{\"error\":\"Unsupported game mode\"}");
      // return;

      // Логіка гри
      String aiChoice = getRandomChoice();
      String result = determineWinner(playerChoice, aiChoice);

      // Формування JSON-відповіді
      StaticJsonDocument<200> responseDoc;
      responseDoc["PlayerChoice"] = playerChoice;
      responseDoc["AIChoice"] = aiChoice;
      responseDoc["Result"] = result;

      String response;
      serializeJson(responseDoc, response);
      Serial.println(response); // Надсилання відповіді через COM-порт


    }

    if (gameMode == "AI vs AI")
    {
      String aiChoice_1 = getRandomChoice(); // AI 1
      String aiChoice_2 = getRandomChoice(); // AI 2
      String result = determineWinner_AI_vs_AI(aiChoice_1, aiChoice_2);

       // Формування JSON-відповіді
      StaticJsonDocument<200> responseDoc;
      responseDoc["AI1Choice"] = aiChoice_1;
      responseDoc["AI2Choice"] = aiChoice_2;
      responseDoc["Result"] = result;

      String response;
      serializeJson(responseDoc, response);
      Serial.println(response); // Надсилання відповіді через COM-порт


      delay(5000);

    }

    
  }
}
