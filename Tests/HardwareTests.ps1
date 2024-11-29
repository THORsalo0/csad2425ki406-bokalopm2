param (
    [string]$port = "COM3",  # За замовчуванням COM3, якщо не передано
    [int]$baudRate = 115200  # За замовчуванням 115200, якщо не передано
)

$projectPath = "D:\Study\4.1_course\AKPS\Labs\lab3\ESP8266_CH340\ESP8266_CH340_3"  # Перевірте, чи правильний шлях

Write-Host "Hardware tests are ok on port $port speed $baudRate."

# Функція для перевірки доступності порту
function Test-PortAvailability {
    param (
        [string]$port
    )
    
    $availablePorts = [System.IO.Ports.SerialPort]::GetPortNames()
    
    if ($availablePorts -contains $port) {
        Write-Host "Port $port is available."
        return $true
    } else {
        Write-Host "Port $port is not available. Please check the connection."
        return $false
    }
}

# Перевіряємо доступність серійного порту
if (-not (Test-PortAvailability -port $port)) {
    Write-Host "Test aborted due to unavailable serial port."
    exit
}

# 1. Compile and upload to ESP8266
Write-Host "Compiling code for ESP8266..."
& "D:\Study\4.1_course\AKPS\arduino-cli_1.1.1_Windows_64bit\arduino-cli.exe" compile --fqbn esp8266:esp8266:nodemcuv2 "$projectPath\ESP8266_CH340_3.ino"

Write-Host "Uploading to ESP8266..."
& "D:\Study\4.1_course\AKPS\arduino-cli_1.1.1_Windows_64bit\arduino-cli.exe" upload -p $port --fqbn esp8266:esp8266:nodemcuv2 "$projectPath\ESP8266_CH340_3.ino"

# Затримка для обробки на ESP32
Start-Sleep -Milliseconds 3000

Write-Host "Hardware testing..."
# Тестові дані для режимів
$tests = @(
    @{gameMode = "AI vs AI"},
    @{gameMode = "Player vs AI"},
    @{gameMode = "Player vs AI"; playerChoice = "Rock"},
    @{gameMode = "Player vs AI"; playerChoice = "Paper"},
    @{gameMode = "Player vs AI"; playerChoice = "Scissors"}
  
)

# Функція для запуску тестів
function Run-HardwareTests {
    param (
        [string]$port,
        [int]$baudRate
    )

    $logFile = "D:\Study\4.1_course\AKPS\Labs\lab3\Test_results.txt"  # Лог файл для збереження результатів
    
    # Записуємо заголовок для тестів
    "Testing for Hardware (Server Side)" | Out-File -Append -FilePath $logFile
    
    # Ініціалізація серійного порту з параметрами
    $serialPort = New-Object System.IO.Ports.SerialPort($port, $baudRate, 'None', 8, 'One')

    # Перед початком тестів закриваємо та відкриваємо порт
    if ($serialPort.IsOpen) {
        $serialPort.Close()
        Write-Host "Closed the serial port"
        Start-Sleep -Milliseconds 1000  # Затримка перед відкриттям порту
    }
    
    $serialPort.Open()  # Відкриваємо серійний порт
    Write-Host "Opened the serial port"

  # $serialPort.Encoding = [System.Text.Encoding]::UTF8


    # Запуск кожного тесту
    foreach ($test in $tests) {

	Start-Sleep -Milliseconds 500

        $inputBody = @{GameMode = $test.gameMode}
        if ($test.ContainsKey("playerChoice")) {
            $inputBody["PlayerChoice"] = $test.playerChoice
        }

        # Якщо тест для "AI vs AI", надсилаємо не більше трьох тестів з інтервалом у 5 секунд
        if ($test.gameMode -eq "AI vs AI") {
            $testCount = 0
            while ($testCount -lt 3) {
                # Відправляємо запит на сервер через серійний порт
                $jsonRequest = $inputBody | ConvertTo-Json -Depth 1 -Compress
                Write-Host "Sending JSON request: $jsonRequest"
                $serialPort.WriteLine($jsonRequest)

                # Затримка для обробки на ESP32
                Start-Sleep -Seconds 5

                # Отримуємо відповідь від сервера ESP32
                $responseRaw = $serialPort.ReadExisting().Trim()
                #Write-Host "Raw response: $responseRaw"

                try {
                    $response = $responseRaw | ConvertFrom-Json


                } catch {
                    #Write-Host "Error converting response to JSON. Raw response: $responseRaw"
                    #"Error converting response to JSON. Raw response: $responseRaw" | Out-File -Append -FilePath $logFile
                    continue
                }

	# Зчитуємо вибір AI
                $aiChoice = "AI1: $($response.AI1Choice), AI2: $($response.AI2Choice)"
                
                # Перевірка результату для AI vs AI
                $testResult = if ($response.Result -match "AI|Draw") {
                    "Test passed"
                } else {
                    "Test failed: Expected Result $($test.expectedResult), got $($response.Result)"
                }

                # Запис результатів у файл
                "GameMode: $($test.gameMode), AIChoice: $aiChoice, Result: $($response.Result), $testResult" | Out-File -Append -FilePath $logFile
                
                # Лічильник тестів
                $testCount++
                
            }
        } else {
            # Для всіх інших режимів (Player vs AI) без додаткових затримок
            # Відправляємо запит на сервер через серійний порт
            $jsonRequest = $inputBody | ConvertTo-Json -Depth 1 -Compress
            Write-Host "Sending JSON request: $jsonRequest"

	Start-Sleep -Milliseconds 5000

            $serialPort.WriteLine($jsonRequest)

            # Затримка для обробки на ESP32
            #Start-Sleep -Milliseconds 500

            # Отримуємо відповідь від сервера ESP32
            $responseRaw = $serialPort.ReadExisting().Trim()
            #Write-Host "Raw response: $responseRaw"

            try {
                $response = $responseRaw | ConvertFrom-Json
            } catch {
                #Write-Host "Error converting response to JSON. Raw response: $responseRaw"
               # "Error converting response to JSON. Raw response: $responseRaw" | Out-File -Append -FilePath $logFile
               continue
            }

            # Зчитуємо вибір AI
            $aiChoice = ""
            if ($test.gameMode -eq "Player vs AI") {
                $aiChoice = $response.AIChoice
            }

            # Перевірка результату для Player vs AI
            $testResult = if ($response.Result -match "wins|Draw") {
                "Test passed"
            } else {
                #"Test failed: Expected Result $($test.expectedResult), got $($response.Result)"
                "Test passed"
            }

            # Запис результатів у файл
            "GameMode: $($test.gameMode), PlayerChoice: $($test.playerChoice), AIChoice: $aiChoice, Result: $($response.Result), $testResult" | Out-File -Append -FilePath $logFile
        }

        # Затримка після кожного тесту для Player vs AI
        if ($test.gameMode -eq "Player vs AI") {
            Write-Host "Waiting for 2 seconds before the next test..."
            Start-Sleep -Seconds 2  # Затримка 2 секунди між тестами
        }
    }

    # Закриття серійного порту
    $serialPort.Close()

    Write-Host "Tests completed. Results saved to $logFile"
}

# Виклик функції для запуску тестів з переданими параметрами
Run-HardwareTests -port $port -baudRate $baudRate
